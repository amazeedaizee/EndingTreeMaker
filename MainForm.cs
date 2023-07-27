using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;

namespace NSOSimulationTreeMaker
{
    public partial class MainForm : Form
    {
        private int counter = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            label.Text = $"Clicked {++counter} times!";
        }
    }
}
