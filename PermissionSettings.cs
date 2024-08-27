using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace folder_Protector
{
    public partial class PermissionSettings : Form
    {
        public PermissionSettings()
        {
            InitializeComponent();
            LoadWindowsUsers();
        }
        private void LoadWindowsUsers()
        {
            using (var context = new PrincipalContext(ContextType.Machine))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                        if (de.Name != null)
                        {
                            cmbUsers.Items.Add(de.Name);
                        }

                    }
                }
            }
        }

        private void btnSetPermision_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolderPath.Text) || cmbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user and specify a folder path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderPath = txtFolderPath.Text;
            string user = cmbUsers.SelectedItem.ToString();

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("The specified folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(folderPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                FileSystemRights allowRights = 0;

                if (chkRead.Checked) allowRights |= FileSystemRights.Read;
                if (chkWrite.Checked) allowRights |= FileSystemRights.Write;
                if (chkFullControl.Checked) allowRights |= FileSystemRights.FullControl;
                if (chkDelete.Checked) allowRights |= FileSystemRights.Delete;
                if (chkDelete.Checked) allowRights |= FileSystemRights.DeleteSubdirectoriesAndFiles;



                FileSystemRights denyRights = FileSystemRights.Delete | FileSystemRights.DeleteSubdirectoriesAndFiles;



                NTAccount nTAccount = new NTAccount(user);
                SecurityIdentifier sid = (SecurityIdentifier)nTAccount.Translate(typeof(SecurityIdentifier));


                FileSystemAccessRule allowRule = new FileSystemAccessRule(sid, allowRights, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                FileSystemAccessRule denyRule = new FileSystemAccessRule(sid, denyRights, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Deny);
                if (!chkDelete.Checked && !chkFullControl.Checked)
                {
                    dSecurity.AddAccessRule(denyRule);
                }

                dSecurity.PurgeAccessRules(sid);

                dSecurity.AddAccessRule(allowRule);

                string permissionsSet = GetPermissionsString(allowRights);

                dInfo.SetAccessControl(dSecurity);
                MessageBox.Show($"Permissions successfully set for {folderPath}. \nSet Permissions: {permissionsSet}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DisplayUserPermissions(folderPath, user);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting permissions for {folderPath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetPermissionsString(FileSystemRights rights)
        {
            List<string> permissions = new List<string>();

            if ((rights & FileSystemRights.Read) == FileSystemRights.Read) permissions.Add("Read");
            if ((rights & FileSystemRights.Write) == FileSystemRights.Write) permissions.Add("Write");
            if ((rights & FileSystemRights.Delete) == FileSystemRights.Delete) permissions.Add("Delete");
            if ((rights & FileSystemRights.FullControl) == FileSystemRights.FullControl) permissions.Add("Full Control");
            return string.Join(", ", permissions);

        }

        private void DisplayUserPermissions(string folderPath, string user)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(folderPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                NTAccount nTAccount = new NTAccount(user);
                SecurityIdentifier sid = (SecurityIdentifier)nTAccount.Translate(typeof(SecurityIdentifier));
                AuthorizationRuleCollection rules = dSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

                FileSystemRights userRights = 0;

                foreach (FileSystemAccessRule rule in rules)
                {
                    if (rule.IdentityReference.Value == sid.Value)
                    {
                        if (rule.AccessControlType == AccessControlType.Allow)
                        {
                            userRights |= rule.FileSystemRights;
                        }
                    }
                }
                chkRead.Checked = (userRights & FileSystemRights.Read) == FileSystemRights.Read;
                chkWrite.Checked = (userRights & FileSystemRights.Write) == FileSystemRights.Write;
                chkFullControl.Checked = (userRights & FileSystemRights.FullControl) == FileSystemRights.FullControl;
                chkDelete.Checked = (userRights & FileSystemRights.Delete) == FileSystemRights.Delete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching permissions for {folderPath}: {ex.Message}");
            }
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFolderPath.Text) && cmbUsers.SelectedItem != null)
            {
                string folderPath = txtFolderPath.Text;
                string user = cmbUsers.SelectedItem.ToString();

                if (Directory.Exists(folderPath))
                {
                    DisplayUserPermissions(folderPath, user);
                }
                else
                {
                    MessageBox.Show("The specified folder doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderBrowser.SelectedPath;
                }
            }
        }


        private void chkFullControl_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFullControl.Checked)
            {
                chkRead.Checked = true;
                chkWrite.Checked = true;
                chkDelete.Checked = true;
            }
        }

        
    }
}
