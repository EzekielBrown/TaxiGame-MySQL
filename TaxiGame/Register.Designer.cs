namespace TaxiGame
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            pictureBox1 = new PictureBox();
            buttonCreate = new Button();
            textUsername = new TextBox();
            textPassword = new TextBox();
            buttonBack = new Button();
            textEmail = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 500);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // buttonCreate
            // 
            buttonCreate.BackColor = Color.FromArgb(35, 31, 36);
            buttonCreate.FlatAppearance.BorderSize = 0;
            buttonCreate.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonCreate.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreate.ForeColor = Color.FromArgb(245, 215, 166);
            buttonCreate.Location = new Point(465, 383);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(171, 46);
            buttonCreate.TabIndex = 4;
            buttonCreate.Text = "CREATE";
            buttonCreate.UseVisualStyleBackColor = false;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // textUsername
            // 
            textUsername.Location = new Point(465, 210);
            textUsername.MaxLength = 30;
            textUsername.Name = "textUsername";
            textUsername.PlaceholderText = "USERNAME";
            textUsername.Size = new Size(171, 23);
            textUsername.TabIndex = 1;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(465, 268);
            textPassword.MaxLength = 40;
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.PlaceholderText = "PASSWORD";
            textPassword.Size = new Size(171, 23);
            textPassword.TabIndex = 3;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.FromArgb(35, 31, 36);
            buttonBack.FlatAppearance.BorderSize = 0;
            buttonBack.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonBack.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBack.ForeColor = Color.FromArgb(245, 215, 166);
            buttonBack.Location = new Point(465, 435);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(171, 46);
            buttonBack.TabIndex = 5;
            buttonBack.Text = "BACK";
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += buttonBack_Click;
            // 
            // textEmail
            // 
            textEmail.Location = new Point(465, 239);
            textEmail.MaxLength = 40;
            textEmail.Name = "textEmail";
            textEmail.PlaceholderText = "EMAIL";
            textEmail.Size = new Size(171, 23);
            textEmail.TabIndex = 2;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 495);
            Controls.Add(textEmail);
            Controls.Add(buttonBack);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(buttonCreate);
            Controls.Add(pictureBox1);
            Name = "Register";
            Text = "Taxi - The Game";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button buttonCreate;
        private TextBox textUsername;
        private TextBox textPassword;
        private Button buttonBack;
        private TextBox textEmail;
    }
}