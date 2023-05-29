namespace TaxiGame
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            buttonDeleteUser = new Button();
            buttonReturn = new Button();
            buttonGetUser = new Button();
            pictureBox1 = new PictureBox();
            textBoxID = new TextBox();
            textBoxUsername = new TextBox();
            textBoxEmail = new TextBox();
            textBoxPassword = new TextBox();
            buttonUpdate = new Button();
            checkBoxIsLocked = new CheckBox();
            checkBoxIsAdmin = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonDeleteUser
            // 
            buttonDeleteUser.BackColor = Color.FromArgb(35, 31, 36);
            buttonDeleteUser.FlatAppearance.BorderSize = 0;
            buttonDeleteUser.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonDeleteUser.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonDeleteUser.FlatStyle = FlatStyle.Flat;
            buttonDeleteUser.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDeleteUser.ForeColor = Color.FromArgb(245, 215, 166);
            buttonDeleteUser.Location = new Point(465, 304);
            buttonDeleteUser.Name = "buttonDeleteUser";
            buttonDeleteUser.Size = new Size(171, 46);
            buttonDeleteUser.TabIndex = 11;
            buttonDeleteUser.Text = "DELETE USER";
            buttonDeleteUser.UseVisualStyleBackColor = false;
            buttonDeleteUser.Click += buttonDeleteUser_Click;
            // 
            // buttonReturn
            // 
            buttonReturn.BackColor = Color.FromArgb(35, 31, 36);
            buttonReturn.FlatAppearance.BorderSize = 0;
            buttonReturn.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonReturn.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonReturn.FlatStyle = FlatStyle.Flat;
            buttonReturn.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonReturn.ForeColor = Color.FromArgb(245, 215, 166);
            buttonReturn.Location = new Point(464, 436);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Size = new Size(171, 46);
            buttonReturn.TabIndex = 12;
            buttonReturn.Text = "RETURN";
            buttonReturn.UseVisualStyleBackColor = false;
            buttonReturn.Click += buttonReturn_Click;
            // 
            // buttonGetUser
            // 
            buttonGetUser.BackColor = Color.FromArgb(35, 31, 36);
            buttonGetUser.FlatAppearance.BorderSize = 0;
            buttonGetUser.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonGetUser.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonGetUser.FlatStyle = FlatStyle.Flat;
            buttonGetUser.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonGetUser.ForeColor = Color.FromArgb(245, 215, 166);
            buttonGetUser.Location = new Point(465, 200);
            buttonGetUser.Name = "buttonGetUser";
            buttonGetUser.Size = new Size(171, 46);
            buttonGetUser.TabIndex = 13;
            buttonGetUser.Text = "GET USER";
            buttonGetUser.UseVisualStyleBackColor = false;
            buttonGetUser.Click += buttonGetUser_Click;
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
            // textBoxID
            // 
            textBoxID.Location = new Point(12, 36);
            textBoxID.Name = "textBoxID";
            textBoxID.PlaceholderText = "ID";
            textBoxID.Size = new Size(100, 23);
            textBoxID.TabIndex = 14;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(12, 65);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.PlaceholderText = "USERNAME";
            textBoxUsername.Size = new Size(100, 23);
            textBoxUsername.TabIndex = 15;
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(12, 94);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.PlaceholderText = "EMAIL";
            textBoxEmail.Size = new Size(100, 23);
            textBoxEmail.TabIndex = 16;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(12, 123);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PlaceholderText = "PASSWORD";
            textBoxPassword.Size = new Size(100, 23);
            textBoxPassword.TabIndex = 17;
            // 
            // buttonUpdate
            // 
            buttonUpdate.BackColor = Color.FromArgb(35, 31, 36);
            buttonUpdate.FlatAppearance.BorderSize = 0;
            buttonUpdate.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonUpdate.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonUpdate.FlatStyle = FlatStyle.Flat;
            buttonUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonUpdate.ForeColor = Color.FromArgb(245, 215, 166);
            buttonUpdate.Location = new Point(465, 252);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(171, 46);
            buttonUpdate.TabIndex = 18;
            buttonUpdate.Text = "UPDATE USER";
            buttonUpdate.UseVisualStyleBackColor = false;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // checkBoxIsLocked
            // 
            checkBoxIsLocked.AutoSize = true;
            checkBoxIsLocked.Location = new Point(12, 152);
            checkBoxIsLocked.Name = "checkBoxIsLocked";
            checkBoxIsLocked.Size = new Size(117, 19);
            checkBoxIsLocked.TabIndex = 19;
            checkBoxIsLocked.Text = "Account Locked?";
            checkBoxIsLocked.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsAdmin
            // 
            checkBoxIsAdmin.AutoSize = true;
            checkBoxIsAdmin.Location = new Point(12, 177);
            checkBoxIsAdmin.Name = "checkBoxIsAdmin";
            checkBoxIsAdmin.Size = new Size(67, 19);
            checkBoxIsAdmin.TabIndex = 20;
            checkBoxIsAdmin.Text = "Admin?";
            checkBoxIsAdmin.UseVisualStyleBackColor = true;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 495);
            Controls.Add(checkBoxIsAdmin);
            Controls.Add(checkBoxIsLocked);
            Controls.Add(buttonUpdate);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxEmail);
            Controls.Add(textBoxUsername);
            Controls.Add(textBoxID);
            Controls.Add(buttonGetUser);
            Controls.Add(buttonReturn);
            Controls.Add(buttonDeleteUser);
            Controls.Add(pictureBox1);
            Name = "Admin";
            Text = "Admin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonDeleteUser;
        private Button buttonReturn;
        private Button buttonGetUser;
        private PictureBox pictureBox1;
        private TextBox textBoxID;
        private TextBox textBoxUsername;
        private TextBox textBoxEmail;
        private TextBox textBoxPassword;
        private Button buttonUpdate;
        private CheckBox checkBoxIsLocked;
        private CheckBox checkBoxIsAdmin;
    }
}