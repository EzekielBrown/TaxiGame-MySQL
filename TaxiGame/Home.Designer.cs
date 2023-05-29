﻿namespace TaxiGame
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            pictureBox1 = new PictureBox();
            buttonNewGame = new Button();
            buttonLogOut = new Button();
            buttonAdmin = new Button();
            listBoxPlayers = new ListBox();
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
            // buttonNewGame
            // 
            buttonNewGame.BackColor = Color.FromArgb(35, 31, 36);
            buttonNewGame.FlatAppearance.BorderSize = 0;
            buttonNewGame.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonNewGame.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonNewGame.FlatStyle = FlatStyle.Flat;
            buttonNewGame.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonNewGame.ForeColor = Color.FromArgb(245, 215, 166);
            buttonNewGame.Location = new Point(465, 200);
            buttonNewGame.Name = "buttonNewGame";
            buttonNewGame.Size = new Size(171, 46);
            buttonNewGame.TabIndex = 9;
            buttonNewGame.Text = "NEW GAME";
            buttonNewGame.UseVisualStyleBackColor = false;
            buttonNewGame.Click += buttonNewGame_Click;
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
            buttonLogOut.Location = new Point(465, 437);
            buttonLogOut.Name = "buttonLogOut";
            buttonLogOut.Size = new Size(171, 46);
            buttonLogOut.TabIndex = 10;
            buttonLogOut.Text = "LOG OUT";
            buttonLogOut.UseVisualStyleBackColor = false;
            buttonLogOut.Click += buttonLogOut_Click;
            // 
            // buttonAdmin
            // 
            buttonAdmin.BackColor = Color.FromArgb(35, 31, 36);
            buttonAdmin.FlatAppearance.BorderSize = 0;
            buttonAdmin.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonAdmin.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAdmin.FlatStyle = FlatStyle.Flat;
            buttonAdmin.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAdmin.ForeColor = Color.FromArgb(245, 215, 166);
            buttonAdmin.Location = new Point(465, 252);
            buttonAdmin.Name = "buttonAdmin";
            buttonAdmin.Size = new Size(171, 46);
            buttonAdmin.TabIndex = 11;
            buttonAdmin.Text = "ADMIN";
            buttonAdmin.UseVisualStyleBackColor = false;
            buttonAdmin.Click += buttonAdmin_Click;
            // 
            // listBoxPlayers
            // 
            listBoxPlayers.FormattingEnabled = true;
            listBoxPlayers.ItemHeight = 15;
            listBoxPlayers.Location = new Point(27, 43);
            listBoxPlayers.Name = "listBoxPlayers";
            listBoxPlayers.Size = new Size(120, 94);
            listBoxPlayers.TabIndex = 12;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 495);
            Controls.Add(listBoxPlayers);
            Controls.Add(buttonAdmin);
            Controls.Add(buttonLogOut);
            Controls.Add(buttonNewGame);
            Controls.Add(pictureBox1);
            Name = "Home";
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button buttonNewGame;
        private Button buttonLogOut;
        private Button buttonAdmin;
        private ListBox listBoxPlayers;
    }
}