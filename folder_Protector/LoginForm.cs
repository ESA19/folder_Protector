using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace folder_Protector
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill all empty fileds!");
                return;
            }

            if (AuthenticateUser(username, password, role))
            {
                if (role == "Admin")
                {
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                }
                else if(role=="User")
                {
                    UserForm userForm = new UserForm();
                    userForm.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid password or username. Please try again!");
            }
        }

        private bool AuthenticateUser(string username, string password, string role)

        {
            if(GetCredentials!=null )
            {
                var results=GetCredentials(role);

                string userName = results.Item1;
                string Password = results.Item2;


                return (username == userName && password == Password && role =="Admin" ) || (username == userName && password == Password && role == "User");
            }
            return false;
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
        private (string username, string password) GetCredentials(string role)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Folder_Protector\Keys\LoginProcess"))
            {
                if (key != null)
                {
                    if (role == "Admin")
                    {
                        string encryptedUsername = key.GetValue("Username_admin") as string;
                        string encryptedPassword = key.GetValue("Password_admin") as string;

                        if (encryptedUsername != null && encryptedPassword != null)
                        {
                            return (EncryptionHelper.Decrypt(encryptedUsername), EncryptionHelper.Decrypt(encryptedPassword));
                        }
                    }
                    else if(role =="User")
                    {
                        string encryptedUsername = key.GetValue("Username_user") as string;
                        string encryptedPassword = key.GetValue("Password_user") as string;

                        if (encryptedUsername != null && encryptedPassword != null)
                        {
                            return (EncryptionHelper.Decrypt(encryptedUsername), EncryptionHelper.Decrypt(encryptedPassword));
                        }
                    }
                }
            }
            return (null, null);
        }
    }
}
