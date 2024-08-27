namespace folder_Protector
{
    partial class AdminSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminSettings));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtNewUserName = new TextBox();
            txtNewPassword = new TextBox();
            btn_SaveChanges = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(3, 13);
            label1.Name = "label1";
            label1.Size = new Size(176, 28);
            label1.TabIndex = 0;
            label1.Text = "Account Processes:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(112, 20);
            label2.TabIndex = 1;
            label2.Text = "New Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 100);
            label3.Name = "label3";
            label3.Size = new Size(107, 20);
            label3.TabIndex = 2;
            label3.Text = "New Password:";
            // 
            // txtNewUserName
            // 
            txtNewUserName.Location = new Point(154, 54);
            txtNewUserName.Name = "txtNewUserName";
            txtNewUserName.Size = new Size(125, 27);
            txtNewUserName.TabIndex = 3;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(154, 95);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(125, 27);
            txtNewPassword.TabIndex = 4;
            // 
            // btn_SaveChanges
            // 
            btn_SaveChanges.BackColor = SystemColors.ButtonHighlight;
            btn_SaveChanges.ForeColor = SystemColors.ActiveCaptionText;
            btn_SaveChanges.Location = new Point(154, 143);
            btn_SaveChanges.Name = "btn_SaveChanges";
            btn_SaveChanges.Size = new Size(94, 29);
            btn_SaveChanges.TabIndex = 5;
            btn_SaveChanges.Text = "Change";
            btn_SaveChanges.UseVisualStyleBackColor = false;
            btn_SaveChanges.Click += btn_SaveChanges_Click;
            // 
            // AdminSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 188);
            Controls.Add(btn_SaveChanges);
            Controls.Add(txtNewPassword);
            Controls.Add(txtNewUserName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AdminSettings";
            Text = "Admin Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtNewUserName;
        private TextBox txtNewPassword;
        private Button btn_SaveChanges;
    }
}