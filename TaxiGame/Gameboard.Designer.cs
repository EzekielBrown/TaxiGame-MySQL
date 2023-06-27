namespace TaxiGame
{
    partial class Gameboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gameboard));
            buttonHome = new Button();
            panelGame = new Panel();
            score = new Label();
            buttonUp = new Button();
            buttonLeft = new Button();
            buttonRight = new Button();
            buttonDown = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // buttonHome
            // 
            buttonHome.BackColor = Color.Transparent;
            buttonHome.FlatAppearance.BorderSize = 0;
            buttonHome.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);
            buttonHome.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonHome.FlatStyle = FlatStyle.Flat;
            buttonHome.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHome.ForeColor = Color.FromArgb(245, 215, 166);
            buttonHome.Location = new Point(578, 71);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new Size(171, 46);
            buttonHome.TabIndex = 13;
            buttonHome.Text = "HOME";
            buttonHome.UseVisualStyleBackColor = false;
            buttonHome.Click += buttonHome_Click;
            // 
            // panelGame
            // 
            panelGame.Location = new Point(158, 144);
            panelGame.Name = "panelGame";
            panelGame.Size = new Size(700, 500);
            panelGame.TabIndex = 1;
            panelGame.Click += TilePanel_Click;
            // 
            // score
            // 
            score.AutoSize = true;
            score.BackColor = Color.Transparent;
            score.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            score.ForeColor = Color.FromArgb(245, 215, 166);
            score.Location = new Point(264, 84);
            score.Name = "score";
            score.Size = new Size(18, 20);
            score.TabIndex = 15;
            score.Text = "0";
            // 
            // buttonUp
            // 
            buttonUp.BackColor = Color.FromArgb(35, 31, 36);
            buttonUp.FlatAppearance.BorderSize = 0;
            buttonUp.FlatStyle = FlatStyle.Flat;
            buttonUp.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonUp.ForeColor = Color.FromArgb(245, 215, 166);
            buttonUp.Location = new Point(54, 428);
            buttonUp.Name = "buttonUp";
            buttonUp.Size = new Size(50, 46);
            buttonUp.TabIndex = 16;
            buttonUp.Text = "W";
            buttonUp.UseVisualStyleBackColor = false;
            buttonUp.Click += buttonUp_Click;
            // 
            // buttonLeft
            // 
            buttonLeft.BackColor = Color.FromArgb(35, 31, 36);
            buttonLeft.FlatAppearance.BorderSize = 0;
            buttonLeft.FlatStyle = FlatStyle.Flat;
            buttonLeft.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLeft.ForeColor = Color.FromArgb(245, 215, 166);
            buttonLeft.Location = new Point(-2, 480);
            buttonLeft.Name = "buttonLeft";
            buttonLeft.Size = new Size(50, 46);
            buttonLeft.TabIndex = 17;
            buttonLeft.Text = "A";
            buttonLeft.UseVisualStyleBackColor = false;
            buttonLeft.Click += buttonLeft_Click;
            // 
            // buttonRight
            // 
            buttonRight.BackColor = Color.FromArgb(35, 31, 36);
            buttonRight.FlatAppearance.BorderSize = 0;
            buttonRight.FlatStyle = FlatStyle.Flat;
            buttonRight.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRight.ForeColor = Color.FromArgb(245, 215, 166);
            buttonRight.Location = new Point(109, 480);
            buttonRight.Name = "buttonRight";
            buttonRight.Size = new Size(50, 46);
            buttonRight.TabIndex = 18;
            buttonRight.Text = "D";
            buttonRight.UseVisualStyleBackColor = false;
            buttonRight.Click += buttonRight_Click;
            // 
            // buttonDown
            // 
            buttonDown.BackColor = Color.FromArgb(35, 31, 36);
            buttonDown.FlatAppearance.BorderSize = 0;
            buttonDown.FlatStyle = FlatStyle.Flat;
            buttonDown.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDown.ForeColor = Color.FromArgb(245, 215, 166);
            buttonDown.Location = new Point(54, 480);
            buttonDown.Name = "buttonDown";
            buttonDown.Size = new Size(50, 46);
            buttonDown.TabIndex = 19;
            buttonDown.Text = "S";
            buttonDown.UseVisualStyleBackColor = false;
            buttonDown.Click += buttonDown_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 144);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(129, 129);
            richTextBox1.TabIndex = 21;
            richTextBox1.Text = "Click on the home tile to start.\n\nPick up Pink Passengers and drop them off at the Blue Drop off zone\n";
            // 
            // Gameboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(984, 647);
            Controls.Add(richTextBox1);
            Controls.Add(buttonDown);
            Controls.Add(buttonRight);
            Controls.Add(buttonLeft);
            Controls.Add(buttonUp);
            Controls.Add(score);
            Controls.Add(panelGame);
            Controls.Add(buttonHome);
            KeyPreview = true;
            Name = "Gameboard";
            Text = "Gameboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonHome;
        private Panel panelGame;
        private Label score;
        private Button buttonUp;
        private Button buttonLeft;
        private Button buttonRight;
        private Button buttonDown;
        private RichTextBox richTextBox1;
    }
}