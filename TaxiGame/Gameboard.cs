using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxiGame
{
    public partial class Gameboard : Form
    {
        private Home _home;
        public Gameboard()
        {
            InitializeComponent();
            _home = new Home();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            _home.Show();
            
        }
    }
}
