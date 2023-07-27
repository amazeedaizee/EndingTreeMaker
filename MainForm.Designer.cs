using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;

namespace NSOSimulationTreeMaker
{
    partial class MainForm
    {
        private Button button;
        private Label label;

        private void InitializeComponent()
        {
            Text = "My Application";

            label = Controls.Add(new Label {
                Left = 10,
                Top = 80,
                Width = 250
            });

            button = Controls.Add(new Button {
                Left = 10,
                Top = 40,
                Text = "Click Me!"
            });

            button.Click += ButtonClicked;
        }
    }
}
