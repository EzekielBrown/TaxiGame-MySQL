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
            // Gameboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(984, 647);
            Controls.Add(panelGame);
            Controls.Add(buttonHome);
            Name = "Gameboard";
            Text = "Gameboard";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonHome;
        private Panel panelGame;
    }
}