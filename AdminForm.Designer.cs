namespace folder_Protector
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            btnSelectFolder = new Button();
            lstFolders = new ListBox();
            label1 = new Label();
            btnEncrypt = new Button();
            btnManageUsers = new Button();
            btnDecrypt = new Button();
            btnAdminSettings = new Button();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.AutoSize = true;
            btnSelectFolder.Image = (Image)resources.GetObject("btnSelectFolder.Image");
            btnSelectFolder.ImageAlign = ContentAlignment.MiddleLeft;
            btnSelectFolder.Location = new Point(300, 65);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(144, 38);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lstFolders
            // 
            lstFolders.FormattingEnabled = true;
            lstFolders.Location = new Point(12, 65);
            lstFolders.Name = "lstFolders";
            lstFolders.Size = new Size(282, 284);
            lstFolders.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(137, 23);
            label1.TabIndex = 2;
            label1.Text = "Selected Folders:";
            // 
            // btnEncrypt
            // 
            btnEncrypt.AutoSize = true;
            btnEncrypt.Image = (Image)resources.GetObject("btnEncrypt.Image");
            btnEncrypt.ImageAlign = ContentAlignment.MiddleLeft;
            btnEncrypt.Location = new Point(300, 109);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(94, 38);
            btnEncrypt.TabIndex = 3;
            btnEncrypt.Text = "Encrypt";
            btnEncrypt.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // btnManageUsers
            // 
            btnManageUsers.AutoSize = true;
            btnManageUsers.Image = (Image)resources.GetObject("btnManageUsers.Image");
            btnManageUsers.Location = new Point(299, 270);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(145, 38);
            btnManageUsers.TabIndex = 4;
            btnManageUsers.Text = "User Settings";
            btnManageUsers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnManageUsers.UseVisualStyleBackColor = true;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.AutoSize = true;
            btnDecrypt.Image = (Image)resources.GetObject("btnDecrypt.Image");
            btnDecrypt.ImageAlign = ContentAlignment.MiddleLeft;
            btnDecrypt.Location = new Point(299, 153);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(95, 38);
            btnDecrypt.TabIndex = 5;
            btnDecrypt.Text = "Decrypt";
            btnDecrypt.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // btnAdminSettings
            // 
            btnAdminSettings.AutoSize = true;
            btnAdminSettings.Image = (Image)resources.GetObject("btnAdminSettings.Image");
            btnAdminSettings.ImageAlign = ContentAlignment.MiddleRight;
            btnAdminSettings.Location = new Point(300, 313);
            btnAdminSettings.Name = "btnAdminSettings";
            btnAdminSettings.Size = new Size(144, 38);
            btnAdminSettings.TabIndex = 6;
            btnAdminSettings.Text = "Admin Settings";
            btnAdminSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdminSettings.UseVisualStyleBackColor = true;
            btnAdminSettings.Click += btnAdminSettings_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(450, 378);
            Controls.Add(btnAdminSettings);
            Controls.Add(btnDecrypt);
            Controls.Add(btnManageUsers);
            Controls.Add(btnEncrypt);
            Controls.Add(label1);
            Controls.Add(lstFolders);
            Controls.Add(btnSelectFolder);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AdminForm";
            Text = "Admin Panel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFolder;
        private ListBox lstFolders;
        private Label label1;
        private Button btnEncrypt;
        private Button btnManageUsers;
        private Button btnDecrypt;
        private Button btnAdminSettings;
    }
}