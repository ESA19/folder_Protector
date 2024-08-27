using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Win32;

namespace folder_Protector
{
    public partial class AdminForm : Form
    {
        public static Dictionary<string, (byte[] Key, byte[] IV)> folderKeys = new Dictionary<string, (byte[] Key, byte[] IV)>();
        public AdminForm()
        {
            InitializeComponent();
            LoadFolderKeysFromRegistry();
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

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItem != null)
            {
                string selectedFolder = lstFolders.SelectedItem.ToString();

                if (EncryptFolder(selectedFolder))
                {
                    MessageBox.Show($"Encrypting folder: {selectedFolder}");
                }

            }
            else
            {
                MessageBox.Show("Please select a folder to encrypt.");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItem != null)
            {
                string selectedFolder = lstFolders.SelectedItem.ToString();
                if (DecryptFolder(selectedFolder))
                {
                    MessageBox.Show($"Folder decrypted: {selectedFolder}");
                }

            }
            else
            {
                MessageBox.Show("Please select a folder to decrypt");
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            UserSettings userSettings = new UserSettings();
            userSettings.ShowDialog();
        }

        public static bool EncryptFolder(string folderPath)
        {
            if (folderKeys.ContainsKey(folderPath))
            {
                MessageBox.Show($"Folder '{folderPath}' is already encrypted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                folderKeys[folderPath] = (aes.Key, aes.IV);
                SaveFolderKeysToRegistry(folderPath, aes.Key, aes.IV);

                try
                {
                    foreach (string filePath in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
                    {
                        if (filePath.EndsWith(".encrypted")) continue;

                        string originalExtension = Path.GetExtension(filePath);
                        string encryptedFilePath = filePath + ".enc";

                        try
                        {
                            using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(encryptedFilePath, FileMode.Create))
                            using (var encryptor = aes.CreateEncryptor())
                            using (var cryptoStream = new CryptoStream(destinationStream, encryptor, CryptoStreamMode.Write))
                            {
                                byte[] extensionBytes = System.Text.Encoding.UTF8.GetBytes(originalExtension);
                                destinationStream.WriteByte((byte)extensionBytes.Length);
                                destinationStream.Write(extensionBytes, 0, extensionBytes.Length);
                                sourceStream.CopyTo(cryptoStream);
                            }

                            File.Delete(filePath);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show($"Error encrypting file '{filePath}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error encrypting folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }
        public static bool DecryptFolder(string folderPath)
        {
            if (!folderKeys.ContainsKey(folderPath))
            {
                MessageBox.Show("Encryption keys not found for this folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                (byte[] key, byte[] iv) = folderKeys[folderPath];

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    foreach (string filePath in Directory.GetFiles(folderPath, "*.encrypted", SearchOption.AllDirectories))
                    {
                        string decryptedFilePath = filePath.Substring(0, filePath.Length - ".encrypted".Length);

                        try
                        {
                            using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(decryptedFilePath, FileMode.Create))
                            using (var decryptor = aes.CreateDecryptor())
                            using (var cryptoStream = new CryptoStream(sourceStream, decryptor, CryptoStreamMode.Read))
                            {
                                int extensionLength = sourceStream.ReadByte();
                                byte[] extensionBytes = new byte[extensionLength];
                                sourceStream.Read(extensionBytes, 0, extensionLength);
                                string originalExtension = System.Text.Encoding.UTF8.GetString(extensionBytes);
                                decryptedFilePath += originalExtension;

                                byte[] buffer = new byte[1048576];
                                int read;
                                while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    destinationStream.Write(buffer, 0, read);
                                }
                            }

                            File.Delete(filePath);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Error decrypting file '{filePath}': {ex.Message}");
                        }
                    }
                }

                folderKeys.Remove(folderPath); // Remove keys after decryption
                RemoveFolderKeysFromRegistry(folderPath);
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error decrypting folder: {ex.Message}", "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private static void RemoveFolderKeysFromRegistry(string folderPath)
        {
            using (RegistryKey keyRegistry = Registry.CurrentUser.OpenSubKey(@"Software\Folder_Protector\Keys\FolderProcess", true))
            {
                if (keyRegistry != null)
                {
                    keyRegistry.DeleteValue(folderPath + "_Key", false);
                    keyRegistry.DeleteValue(folderPath + "_IV", false);
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {

                Application.Exit();
            }
        }
        private static void SaveFolderKeysToRegistry(string folderPath, byte[] key, byte[] iv)
        {
            using (RegistryKey keyRegistry = Registry.CurrentUser.CreateSubKey(@"Software\Folder_Protector\Keys\FolderProcess"))
            {
                string base64Key = Convert.ToBase64String(key);
                string base64IV = Convert.ToBase64String(iv);
                keyRegistry.SetValue(folderPath + "_Key", base64Key);
                keyRegistry.SetValue(folderPath + "_IV", base64IV);
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

        private void btnAdminSettings_Click(object sender, EventArgs e)
        {
            AdminSettings adminSettings = new AdminSettings();
            adminSettings.ShowDialog();

        }

        private void btnPermissions_Click(object sender, EventArgs e)
        {
            PermissionSettings permissions = new PermissionSettings();
            permissions.ShowDialog();   
        }

    }
}