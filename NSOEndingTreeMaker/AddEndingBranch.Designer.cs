namespace NSOEndingTreeMaker
{
    partial class AddEndingBranch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEndingBranch));
            this.AddEndingButton = new System.Windows.Forms.Button();
            this.AddEnding_Dropdown = new System.Windows.Forms.ComboBox();
            this.StartingDayNumeric = new System.Windows.Forms.NumericUpDown();
            this.StartingDay_Label = new System.Windows.Forms.Label();
            this.AddEnding_Label = new System.Windows.Forms.Label();
            this.IsStressfulBreakdown_CheckAdd = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.StartingDayNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // AddEndingButton
            // 
            this.AddEndingButton.Location = new System.Drawing.Point(328, 90);
            this.AddEndingButton.Name = "AddEndingButton";
            this.AddEndingButton.Size = new System.Drawing.Size(90, 34);
            this.AddEndingButton.TabIndex = 4;
            this.AddEndingButton.Text = "Add Ending";
            this.AddEndingButton.UseVisualStyleBackColor = true;
            this.AddEndingButton.Click += new System.EventHandler(this.AddEndingButton_Click);
            // 
            // AddEnding_Dropdown
            // 
            this.AddEnding_Dropdown.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AddEnding_Dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddEnding_Dropdown.FormattingEnabled = true;
            this.AddEnding_Dropdown.Items.AddRange(new object[] {
            "None",
            "Do You Love Me?",
            "Unhappy End World",
            "Rainbow Girl",
            "Utopian Parody",
            "Catastrophe",
            "Labor Is Evil",
            "Needy Girl Overdose",
            "Painful Future",
            "Fallen Angel",
            "Normie Life",
            "Nymphomania",
            "Cucked",
            "Ground Control To Psychoelectric Angel",
            "Bomber Girl",
            "There Are No Angels",
            "Flatline",
            "Internet Overdose",
            "Nerdy Girl Overload",
            "Welcome To My Religion",
            "So Close Yet So Far",
            "Galactic Express",
            "Dark Angel",
            "Internet Runaway Angel: Be Invoked"});
            this.AddEnding_Dropdown.Location = new System.Drawing.Point(148, 34);
            this.AddEnding_Dropdown.Name = "AddEnding_Dropdown";
            this.AddEnding_Dropdown.Size = new System.Drawing.Size(270, 21);
            this.AddEnding_Dropdown.TabIndex = 2;
            this.AddEnding_Dropdown.SelectedIndexChanged += new System.EventHandler(this.AddEnding_Dropdown_SelectedIndexChanged);
            // 
            // StartingDayNumeric
            // 
            this.StartingDayNumeric.Location = new System.Drawing.Point(13, 34);
            this.StartingDayNumeric.Maximum = new decimal(new int[] {
            29,
            0,
            0,
            0});
            this.StartingDayNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.StartingDayNumeric.Name = "StartingDayNumeric";
            this.StartingDayNumeric.Size = new System.Drawing.Size(120, 20);
            this.StartingDayNumeric.TabIndex = 1;
            this.StartingDayNumeric.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.StartingDayNumeric.ValueChanged += new System.EventHandler(this.StartingDayNumeric_ValueChanged);
            // 
            // StartingDay_Label
            // 
            this.StartingDay_Label.AutoSize = true;
            this.StartingDay_Label.Location = new System.Drawing.Point(9, 14);
            this.StartingDay_Label.Name = "StartingDay_Label";
            this.StartingDay_Label.Size = new System.Drawing.Size(65, 13);
            this.StartingDay_Label.TabIndex = 3;
            this.StartingDay_Label.Text = "Starting Day";
            // 
            // AddEnding_Label
            // 
            this.AddEnding_Label.AutoSize = true;
            this.AddEnding_Label.Location = new System.Drawing.Point(145, 14);
            this.AddEnding_Label.Name = "AddEnding_Label";
            this.AddEnding_Label.Size = new System.Drawing.Size(76, 13);
            this.AddEnding_Label.TabIndex = 4;
            this.AddEnding_Label.Text = "Ending To Get";
            // 
            // IsStressfulBreakdown_CheckAdd
            // 
            this.IsStressfulBreakdown_CheckAdd.AutoSize = true;
            this.IsStressfulBreakdown_CheckAdd.Location = new System.Drawing.Point(12, 71);
            this.IsStressfulBreakdown_CheckAdd.Name = "IsStressfulBreakdown_CheckAdd";
            this.IsStressfulBreakdown_CheckAdd.Size = new System.Drawing.Size(134, 17);
            this.IsStressfulBreakdown_CheckAdd.TabIndex = 3;
            this.IsStressfulBreakdown_CheckAdd.Text = "Is Stressful Breakdown";
            this.IsStressfulBreakdown_CheckAdd.UseVisualStyleBackColor = true;
            this.IsStressfulBreakdown_CheckAdd.CheckedChanged += new System.EventHandler(this.IsStressfulBreakdown_CheckAdd_CheckedChanged);
            // 
            // AddEndingBranch
            // 
            this.AcceptButton = this.AddEndingButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 142);
            this.Controls.Add(this.IsStressfulBreakdown_CheckAdd);
            this.Controls.Add(this.AddEnding_Label);
            this.Controls.Add(this.StartingDay_Label);
            this.Controls.Add(this.StartingDayNumeric);
            this.Controls.Add(this.AddEnding_Dropdown);
            this.Controls.Add(this.AddEndingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEndingBranch";
            this.Text = "Add Ending Branch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddEndingBranch_FormClosing);
            this.Load += new System.EventHandler(this.AddEndingBranch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StartingDayNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddEndingButton;
        private System.Windows.Forms.ComboBox AddEnding_Dropdown;
        private System.Windows.Forms.NumericUpDown StartingDayNumeric;
        private System.Windows.Forms.Label StartingDay_Label;
        private System.Windows.Forms.Label AddEnding_Label;
        private System.Windows.Forms.CheckBox IsStressfulBreakdown_CheckAdd;
    }
}