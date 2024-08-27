using Microsoft.Win32;

namespace folder_Protector
{

    public partial class UserSettings : Form
    {

        private const string RegistryPath = @"Software\Folder_Protector\Keys\LoginProcess";
        public UserSettings()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettings));
            label1 = new Label();
            label2 = new Label();
            txtNewUserName = new TextBox();
            txtNewPassword = new TextBox();
            btn_SaveChanges = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 52);
            label1.Name = "label1";
            label1.Size = new Size(112, 20);
            label1.TabIndex = 0;
            label1.Text = "New Username:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 92);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 1;
            label2.Text = "New Password:";
            // 
            // txtNewUserName
            // 
            txtNewUserName.Location = new Point(135, 52);
            txtNewUserName.Name = "txtNewUserName";
            txtNewUserName.Size = new Size(125, 27);
            txtNewUserName.TabIndex = 2;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(135, 92);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(125, 27);
            txtNewPassword.TabIndex = 3;
            // 
            // btn_SaveChanges
            // 
            btn_SaveChanges.Location = new Point(135, 137);
            btn_SaveChanges.Name = "btn_SaveChanges";
            btn_SaveChanges.Size = new Size(111, 31);
            btn_SaveChanges.TabIndex = 4;
            btn_SaveChanges.Text = "Change";
            btn_SaveChanges.UseVisualStyleBackColor = true;
            btn_SaveChanges.Click += btn_SaveChanges_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label3.Location = new Point(0, 9);
            label3.Name = "label3";
            label3.Size = new Size(143, 28);
            label3.TabIndex = 5;
            label3.Text = "User Processes:";
            // 
            // UserSettings
            // 
            ClientSize = new Size(272, 184);
            Controls.Add(label3);
            Controls.Add(btn_SaveChanges);
            Controls.Add(txtNewPassword);
            Controls.Add(txtNewUserName);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UserSettings";
            Text = "User Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadCurrentSettings()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    string encryptedUsername = key.GetValue("Username_user") as string;
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
                key.SetValue("Username_user", encryptedUsername);
                key.SetValue("Password_user", encryptedPassword);
            }


        }




    }
}
