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
            button1 = new Button();
            textBox1 = new TextBox();
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
            buttonCreate.TabIndex = 8;
            buttonCreate.Text = "CREATE";
            buttonCreate.UseVisualStyleBackColor = false;
            // 
            // textUsername
            // 
            textUsername.Location = new Point(465, 210);
            textUsername.Name = "textUsername";
            textUsername.PlaceholderText = "USERNAME";
            textUsername.Size = new Size(171, 23);
            textUsername.TabIndex = 9;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(465, 268);
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.PlaceholderText = "PASSWORD";
            textPassword.Size = new Size(171, 23);
            textPassword.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(35, 31, 36);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            button1.FlatAppearance.MouseOverBackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(245, 215, 166);
            button1.Location = new Point(465, 435);
            button1.Name = "button1";
            button1.Size = new Size(171, 46);
            button1.TabIndex = 11;
            button1.Text = "BACK";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(465, 239);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "EMAIL";
            textBox1.Size = new Size(171, 23);
            textBox1.TabIndex = 12;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 495);
            Controls.Add(textBox1);
            Controls.Add(button1);
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
        private Button button1;
        private TextBox textBox1;
    }
}