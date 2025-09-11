namespace Tetris.Miscellanea
{
    partial class frmLogincs
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
            lblUsername = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            lblWrongUsernameOrPassword = new System.Windows.Forms.Label();
            btnLogin = new System.Windows.Forms.Button();
            btnCreateAccount = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new System.Drawing.Point(22, 26);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new System.Drawing.Size(78, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(27, 69);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(73, 20);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password:";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(132, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(125, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(132, 69);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(125, 27);
            textBox2.TabIndex = 3;
            // 
            // lblWrongUsernameOrPassword
            // 
            lblWrongUsernameOrPassword.AutoSize = true;
            lblWrongUsernameOrPassword.ForeColor = System.Drawing.Color.Crimson;
            lblWrongUsernameOrPassword.Location = new System.Drawing.Point(44, 159);
            lblWrongUsernameOrPassword.Name = "lblWrongUsernameOrPassword";
            lblWrongUsernameOrPassword.Size = new System.Drawing.Size(213, 20);
            lblWrongUsernameOrPassword.TabIndex = 4;
            lblWrongUsernameOrPassword.Text = "*Wrong username or password";
            lblWrongUsernameOrPassword.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.Location = new System.Drawing.Point(166, 116);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(91, 29);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.Location = new System.Drawing.Point(22, 116);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new System.Drawing.Size(129, 29);
            btnCreateAccount.TabIndex = 6;
            btnCreateAccount.Text = "Create Account";
            btnCreateAccount.UseVisualStyleBackColor = true;
            btnCreateAccount.Click += btnCreateAccount_Click;
            // 
            // frmLogincs
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(324, 188);
            Controls.Add(btnCreateAccount);
            Controls.Add(btnLogin);
            Controls.Add(lblWrongUsernameOrPassword);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Name = "frmLogincs";
            Text = "frmLogincs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblWrongUsernameOrPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCreateAccount;
    }
}