namespace TaxiGame
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            buttonLogin = new Button();
            textUsername = new TextBox();
            textPassword = new TextBox();
            buttonNewUser = new Button();
            pictureBox1 = new PictureBox();
            buttonLogOut = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.FromArgb(35, 31, 36);
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonLogin.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogin.ForeColor = Color.FromArgb(245, 215, 166);
            buttonLogin.Location = new Point(521, 274);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(115, 46);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "LOGIN";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // textUsername
            // 
            textUsername.Location = new Point(465, 202);
            textUsername.Name = "textUsername";
            textUsername.PlaceholderText = "USERNAME";
            textUsername.Size = new Size(171, 23);
            textUsername.TabIndex = 2;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(465, 231);
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.PlaceholderText = "PASSWORD";
            textPassword.Size = new Size(171, 23);
            textPassword.TabIndex = 3;
            // 
            // buttonNewUser
            // 
            buttonNewUser.BackColor = Color.FromArgb(35, 31, 36);
            buttonNewUser.FlatAppearance.BorderSize = 0;
            buttonNewUser.FlatStyle = FlatStyle.Flat;
            buttonNewUser.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonNewUser.ForeColor = Color.FromArgb(245, 215, 166);
            buttonNewUser.Location = new Point(465, 274);
            buttonNewUser.Name = "buttonNewUser";
            buttonNewUser.Size = new Size(50, 46);
            buttonNewUser.TabIndex = 4;
            buttonNewUser.Text = "+";
            buttonNewUser.UseVisualStyleBackColor = false;
            buttonNewUser.Click += buttonNewUser_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 500);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // buttonLogOut
            // 
            buttonLogOut.BackColor = Color.FromArgb(35, 31, 36);
            buttonLogOut.FlatAppearance.BorderSize = 0;
            buttonLogOut.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonLogOut.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonLogOut.FlatStyle = FlatStyle.Flat;
            buttonLogOut.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogOut.ForeColor = Color.FromArgb(245, 215, 166);
            buttonLogOut.Location = new Point(465, 326);
            buttonLogOut.Name = "buttonLogOut";
            buttonLogOut.Size = new Size(171, 46);
            buttonLogOut.TabIndex = 11;
            buttonLogOut.Text = "EXIT";
            buttonLogOut.UseVisualStyleBackColor = false;
            buttonLogOut.Click += buttonLogOut_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(694, 495);
            Controls.Add(buttonLogOut);
            Controls.Add(buttonNewUser);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(buttonLogin);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "Login";
            Text = "Taxi - The Game";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonLogin;
        private TextBox textUsername;
        private TextBox textPassword;
        private Button buttonNewUser;
        private PictureBox pictureBox1;
        private Button buttonLogOut;
    }
}