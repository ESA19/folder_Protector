namespace folder_Protector
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            lstFolders = new ListBox();
            btnSelectFolder = new Button();
            label1 = new Label();
            trvFolderContents = new TreeView();
            SuspendLayout();
            // 
            // lstFolders
            // 
            lstFolders.FormattingEnabled = true;
            lstFolders.Location = new Point(287, 64);
            lstFolders.Name = "lstFolders";
            lstFolders.Size = new Size(255, 344);
            lstFolders.TabIndex = 0;
            lstFolders.DoubleClick += lstFolders_DoubleClick;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.AutoSize = true;
            btnSelectFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSelectFolder.Image = (Image)resources.GetObject("btnSelectFolder.Image");
            btnSelectFolder.Location = new Point(548, 62);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(125, 30);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(121, 23);
            label1.TabIndex = 2;
            label1.Text = "Accesible Files:";
            // 
            // trvFolderContents
            // 
            trvFolderContents.Location = new Point(12, 62);
            trvFolderContents.Name = "trvFolderContents";
            trvFolderContents.Size = new Size(269, 346);
            trvFolderContents.TabIndex = 3;
            trvFolderContents.NodeMouseDoubleClick += trvFolderContents_NodeMouseDoubleClick;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(678, 427);
            Controls.Add(trvFolderContents);
            Controls.Add(label1);
            Controls.Add(btnSelectFolder);
            Controls.Add(lstFolders);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UserForm";
            Text = "User Panel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstFolders;
        private Button btnSelectFolder;
        private Label label1;
        private TreeView trvFolderContents;
    }
}