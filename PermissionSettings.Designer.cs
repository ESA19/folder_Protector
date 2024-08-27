namespace folder_Protector
{
    partial class PermissionSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionSettings));
            chkDelete = new CheckBox();
            btnSetPermision = new Button();
            btnSelectFolder = new Button();
            txtFolderPath = new TextBox();
            cmbUsers = new ComboBox();
            chkFullControl = new CheckBox();
            chkWrite = new CheckBox();
            chkRead = new CheckBox();
            label7 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // chkDelete
            // 
            chkDelete.AutoSize = true;
            chkDelete.Location = new Point(23, 208);
            chkDelete.Name = "chkDelete";
            chkDelete.Size = new Size(75, 24);
            chkDelete.TabIndex = 27;
            chkDelete.Text = "Delete";
            chkDelete.UseVisualStyleBackColor = true;
            // 
            // btnSetPermision
            // 
            btnSetPermision.AutoSize = true;
            btnSetPermision.Image = (Image)resources.GetObject("btnSetPermision.Image");
            btnSetPermision.Location = new Point(156, 250);
            btnSetPermision.Name = "btnSetPermision";
            btnSetPermision.Size = new Size(138, 30);
            btnSetPermision.TabIndex = 26;
            btnSetPermision.Text = "Set Permission";
            btnSetPermision.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSetPermision.UseVisualStyleBackColor = true;
            btnSetPermision.Click += btnSetPermision_Click;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.AutoSize = true;
            btnSelectFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSelectFolder.Image = (Image)resources.GetObject("btnSelectFolder.Image");
            btnSelectFolder.ImageAlign = ContentAlignment.MiddleLeft;
            btnSelectFolder.Location = new Point(174, 71);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(125, 30);
            btnSelectFolder.TabIndex = 25;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(12, 71);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(151, 27);
            txtFolderPath.TabIndex = 24;
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(12, 113);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(151, 28);
            cmbUsers.TabIndex = 23;
            // 
            // chkFullControl
            // 
            chkFullControl.AutoSize = true;
            chkFullControl.Location = new Point(165, 208);
            chkFullControl.Name = "chkFullControl";
            chkFullControl.Size = new Size(107, 24);
            chkFullControl.TabIndex = 22;
            chkFullControl.Text = "Full Control";
            chkFullControl.UseVisualStyleBackColor = true;
            chkFullControl.CheckStateChanged += chkFullControl_CheckStateChanged;
            // 
            // chkWrite
            // 
            chkWrite.AutoSize = true;
            chkWrite.Location = new Point(165, 169);
            chkWrite.Name = "chkWrite";
            chkWrite.Size = new Size(67, 24);
            chkWrite.TabIndex = 21;
            chkWrite.Text = "Write";
            chkWrite.UseVisualStyleBackColor = true;
            // 
            // chkRead
            // 
            chkRead.AutoSize = true;
            chkRead.Location = new Point(23, 169);
            chkRead.Name = "chkRead";
            chkRead.Size = new Size(65, 24);
            chkRead.TabIndex = 20;
            chkRead.Text = "Read";
            chkRead.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(48, 235);
            label7.Name = "label7";
            label7.Size = new Size(0, 20);
            label7.TabIndex = 19;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label4.Location = new Point(12, 19);
            label4.Name = "label4";
            label4.Size = new Size(197, 28);
            label4.TabIndex = 18;
            label4.Text = "Permission Processes:";
            // 
            // PermissionSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(306, 301);
            Controls.Add(chkDelete);
            Controls.Add(btnSetPermision);
            Controls.Add(btnSelectFolder);
            Controls.Add(txtFolderPath);
            Controls.Add(cmbUsers);
            Controls.Add(chkFullControl);
            Controls.Add(chkWrite);
            Controls.Add(chkRead);
            Controls.Add(label7);
            Controls.Add(label4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PermissionSettings";
            Text = "PermissionSettings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkDelete;
        private Button btnSetPermision;
        private Button btnSelectFolder;
        private TextBox txtFolderPath;
        private ComboBox cmbUsers;
        private CheckBox chkFullControl;
        private CheckBox chkWrite;
        private CheckBox chkRead;
        private Label label7;
        private Label label4;
    }
}