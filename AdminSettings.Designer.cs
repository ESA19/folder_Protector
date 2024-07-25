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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtNewUserName = new TextBox();
            txtNewPassword = new TextBox();
            btn_SaveChanges = new Button();
            label4 = new Label();
            label7 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
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
            btn_SaveChanges.ForeColor = SystemColors.ActiveCaptionText;
            btn_SaveChanges.Location = new Point(154, 143);
            btn_SaveChanges.Name = "btn_SaveChanges";
            btn_SaveChanges.Size = new Size(94, 29);
            btn_SaveChanges.TabIndex = 5;
            btn_SaveChanges.Text = "Change";
            btn_SaveChanges.UseVisualStyleBackColor = false;
            btn_SaveChanges.Click += btn_SaveChanges_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label4.Location = new Point(3, 189);
            label4.Name = "label4";
            label4.Size = new Size(197, 28);
            label4.TabIndex = 6;
            label4.Text = "Permission Processes:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(37, 302);
            label7.Name = "label7";
            label7.Size = new Size(0, 20);
            label7.TabIndex = 9;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 236);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(65, 24);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "Read";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(12, 275);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(67, 24);
            checkBox2.TabIndex = 11;
            checkBox2.Text = "Write";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(12, 315);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(107, 24);
            checkBox3.TabIndex = 12;
            checkBox3.Text = "Full Control";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // AdminSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 359);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(btn_SaveChanges);
            Controls.Add(txtNewPassword);
            Controls.Add(txtNewUserName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AdminSettings";
            Text = "AdminSettings";
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
        private Label label4;
        private Label label7;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}