using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;

namespace folder_Protector
{
    public partial class AdminSettings : Form
    {

        private const string RegistryPath = @"Software\Folder_Protector\Keys\LoginProcess";
        public AdminSettings()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    string encryptedUsername = key.GetValue("Username_admin") as string;
                    if (encryptedUsername != null)
                    {
                        txtNewUserName.Text = EncryptionHelper.Decrypt(encryptedUsername);
                    }
                }
            }
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUserName.Text;
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Username or password fields cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SaveNewCredentials(newUsername, newPassword);
                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }

        }

        private void SaveNewCredentials(string username, string password)
        {
            string encryptedUsername = EncryptionHelper.Encrypt(username);
            string encryptedPassword = EncryptionHelper.Encrypt(password);

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue("Username_admin", encryptedUsername);
                key.SetValue("Password_admin", encryptedPassword);
            }


        }
    }
}
