using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Cryptography;

namespace folder_Protector
{
    public partial class UserForm : Form
    {
        private List<string> decryptedFolders = new List<string>();
        private List<string> tempFiles = new List<string>();
        private List<string> openedFiles = new List<string>();
        private GlobalKeyboardMouseHook _globalHook;
        public static Dictionary<string, (byte[] Key, byte[] IV)> folderKeys = new Dictionary<string, (byte[] Key, byte[] IV)>();
        private List<Process> openedProcesses = new List<Process>();
        
        public UserForm()
        {
            InitializeComponent();
            LoadFolderKeysFromRegistry();

            trvFolderContents.NodeMouseDoubleClick += trvFolderContents_NodeMouseDoubleClick;
            _globalHook = new GlobalKeyboardMouseHook();
        }

        private void lstFolders_DoubleClick(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItem != null)
            {
                string selectedFolder = lstFolders.SelectedItem.ToString();
                DisplayFolderContents(selectedFolder);
            }
        }

        private bool IsEncrypted(string folderPath)
        {
            return folderKeys.ContainsKey(folderPath);
        }

        private void DisplayFolderContents(string folderPath)
        {
            trvFolderContents.Nodes.Clear();

            try
            {
                TreeNode rootNode = new TreeNode(Path.GetFileName(folderPath));
                trvFolderContents.Nodes.Add(rootNode);
                PopulateTreeNode(rootNode, folderPath);
                trvFolderContents.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void PopulateTreeNode(TreeNode node, string path)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file));
                    fileNode.Tag = file;
                    node.Nodes.Add(fileNode);
                }

                foreach (string directory in Directory.GetDirectories(path))
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(directory));
                    node.Nodes.Add(subNode);
                    PopulateTreeNode(subNode, directory);
                }
            }
            catch (Exception ex)
            {
                node.Nodes.Add(new TreeNode($"Error: {ex.Message}"));
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    if (!lstFolders.Items.Contains(folderPath))
                    {
                        lstFolders.Items.Add(folderPath);
                    }
                    else
                    {
                        MessageBox.Show("This folder is already added!");
                    }

                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!CanCloseProgram())
            {
                e.Cancel = true; // Cancel closing the form
                MessageBox.Show("Please close all opened files before exiting the program.", "Files Still Open", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {

                CleanupTempFiles();
                _globalHook.Dispose();
                Application.Exit();
            }
        }
       
        private void CleanupTempFiles()
        {
            foreach (string tempFile in tempFiles)
            {
                try
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting temp file {tempFile}: {ex.Message}");
                }
            }

            string tempDir = Path.Combine(Path.GetTempPath(), "FolderProtector");
            if (Directory.Exists(tempDir))
            {
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting temp directory {tempDir}: {ex.Message}");
                }
            }

            tempFiles.Clear();
        }

        private bool CanCloseProgram()
        {
            foreach (var filePath in openedFiles)
            {
                if (IsFileOpen(filePath))
                {
                    return false; // Return false if any file is still open
                }

            }
            foreach (var process in openedProcesses.ToList())
            {
                if (!process.HasExited)
                {
                    try
                    {
                        if (!process.WaitForExit(100))  // Wait for 100ms
                        {
                            return false;  // Process is still running
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // Process has already exited
                        openedProcesses.Remove(process);
                    }
                }
                else
                {
                    openedProcesses.Remove(process);
                }
            }
            return true;

        }

        private void OpenFile(string filePath, string originalPath = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"The file '{filePath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var processInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true,
                };

                Process process = Process.Start(processInfo); // Open file with default program
                if (process != null)
                {
                    openedProcesses.Add(process);
                }

                openedFiles.Add(filePath); // Track opened file

                // If it's a decrypted file, track the original path for re-encryption
                if (originalPath != null)
                {
                    process.Exited += (sender, e) => EncryptAndReplaceFile(filePath, originalPath);
                    CloseFile(filePath);
                    process.EnableRaisingEvents = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EncryptAndReplaceFile(string modifiedFilePath, string originalFilePath)
        {
            try
            {
                string parentFolder = Path.GetDirectoryName(originalFilePath);

                if (!folderKeys.ContainsKey(parentFolder))
                {
                    MessageBox.Show($"No encryption key found for the folder: {parentFolder}", "Encryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                (byte[] key, byte[] iv) = folderKeys[parentFolder];

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    string encryptedFilePath = originalFilePath;

                    using (var sourceStream = new FileStream(modifiedFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var destinationStream = new FileStream(encryptedFilePath, FileMode.Create))
                    using (var encryptor = aes.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(destinationStream, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] extensionBytes = System.Text.Encoding.UTF8.GetBytes(Path.GetExtension(modifiedFilePath));
                        destinationStream.WriteByte((byte)extensionBytes.Length);
                        destinationStream.Write(extensionBytes, 0, extensionBytes.Length);
                        sourceStream.CopyTo(cryptoStream);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during file encryption: {ex.Message}", "Encryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CloseFile(string filePath)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(filePath));
                foreach (Process process in processes)
                {
                    process.Kill(); // Close process associated with the file
                }
                openedFiles.Remove(filePath); // Remove from opened files list
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsFileOpen(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false; // File is not locked
                }
            }
            catch (IOException)
            {
                return true; // File is locked by another process
            }
        }

        private void trvFolderContents_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is string filePath)
            {
                try
                {
                    if (IsFileEncrypted(filePath))
                    {
                        string decryptedFilePath = DecryptFile(filePath);
                        if (decryptedFilePath != null)
                        {
                            OpenFile(decryptedFilePath, filePath); // Pass original path
                        }
                    }
                    else
                    {
                        OpenFile(filePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while opening the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsFileEncrypted(string filePath)
        {
            return filePath.EndsWith(".encrypted");
        }

        private string DecryptFile(string sourcePath)
        {
            try
            {
                string parentFolder = Path.GetDirectoryName(sourcePath);

                if (!folderKeys.ContainsKey(parentFolder))
                {
                    MessageBox.Show($"No encryption key found for the folder: {parentFolder}", "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                (byte[] key, byte[] iv) = folderKeys[parentFolder];

                string tempDir = Path.Combine(Path.GetTempPath(), "FolderProtector");
                Directory.CreateDirectory(tempDir);
                string tempFile = Path.Combine(tempDir, Path.GetFileNameWithoutExtension(sourcePath));

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var sourceStream = new FileStream(sourcePath, FileMode.Open))
                    using (var destStream = new FileStream(tempFile, FileMode.Create))
                    using (var decryptor = aes.CreateDecryptor())
                    using (var cryptoStream = new CryptoStream(sourceStream, decryptor, CryptoStreamMode.Read))
                    {
                        int extensionLength = sourceStream.ReadByte();
                        byte[] extensionBytes = new byte[extensionLength];
                        sourceStream.Read(extensionBytes, 0, extensionLength);
                        string originalExtension = System.Text.Encoding.UTF8.GetString(extensionBytes);

                        byte[] buffer = new byte[1048576];
                        int bytesRead;
                        while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            destStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                // Return the tempFile and store the original path for re-encryption
                tempFiles.Add(tempFile);
                return tempFile;
            }
            catch (CryptographicException cryptoEx)
            {
                MessageBox.Show($"Cryptographic error during decryption: {cryptoEx.Message}", "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during file decryption: {ex.Message}", "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void LoadFolderKeysFromRegistry()
        {
            string registryKeyPath = @"Software\Folder_Protector\Keys\FolderProcess";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath))
            {
                if (key == null) return;

                foreach (var valueName in key.GetValueNames())
                {
                    if (valueName.EndsWith("_Key"))
                    {
                        string folderPath = valueName.Substring(0, valueName.Length - "_Key".Length);
                        byte[] folderKey = Convert.FromBase64String((string)key.GetValue(valueName));
                        byte[] folderIv = Convert.FromBase64String((string)key.GetValue($"{folderPath}_IV"));

                        folderKeys[folderPath] = (folderKey, folderIv);
                    }
                }
            }
        }
    }
}
