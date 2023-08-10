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
            this.Advanced_Button = new System.Windows.Forms.LinkLabel();
            this.InsertAtEndingIndex_Numeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StartingDayNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsertAtEndingIndex_Numeric)).BeginInit();
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
            // Advanced_Button
            // 
            this.Advanced_Button.AutoSize = true;
            this.Advanced_Button.Location = new System.Drawing.Point(10, 109);
            this.Advanced_Button.Name = "Advanced_Button";
            this.Advanced_Button.Size = new System.Drawing.Size(56, 13);
            this.Advanced_Button.TabIndex = 5;
            this.Advanced_Button.TabStop = true;
            this.Advanced_Button.Text = "Advanced";
            this.Advanced_Button.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Advanced_Button_LinkClicked);
            // 
            // InsertAtEndingIndex_Numeric
            // 
            this.InsertAtEndingIndex_Numeric.Location = new System.Drawing.Point(132, 140);
            this.InsertAtEndingIndex_Numeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.InsertAtEndingIndex_Numeric.Name = "InsertAtEndingIndex_Numeric";
            this.InsertAtEndingIndex_Numeric.Size = new System.Drawing.Size(54, 20);
            this.InsertAtEndingIndex_Numeric.TabIndex = 1;
            this.InsertAtEndingIndex_Numeric.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.InsertAtEndingIndex_Numeric.ValueChanged += new System.EventHandler(this.InsertAtEndingIndex_Numeric_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Insert As Ending Num:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(397, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Note: Using this feature might break any ending branches that go past this number" +
    ".";
            // 
            // AddEndingBranch
            // 
            this.AcceptButton = this.AddEndingButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 191);
            this.Controls.Add(this.Advanced_Button);
            this.Controls.Add(this.IsStressfulBreakdown_CheckAdd);
            this.Controls.Add(this.AddEnding_Label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartingDay_Label);
            this.Controls.Add(this.InsertAtEndingIndex_Numeric);
            this.Controls.Add(this.StartingDayNumeric);
            this.Controls.Add(this.AddEnding_Dropdown);
            this.Controls.Add(this.AddEndingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 180);
            this.Name = "AddEndingBranch";
            this.Text = "Add Ending Branch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddEndingBranch_FormClosing);
            this.Load += new System.EventHandler(this.AddEndingBranch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StartingDayNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsertAtEndingIndex_Numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddEndingButton;
        private System.Windows.Forms.ComboBox AddEnding_Dropdown;
        private System.Windows.Forms.Label StartingDay_Label;
        private System.Windows.Forms.Label AddEnding_Label;
        private System.Windows.Forms.CheckBox IsStressfulBreakdown_CheckAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown InsertAtEndingIndex_Numeric;
        internal System.Windows.Forms.NumericUpDown StartingDayNumeric;
        internal System.Windows.Forms.LinkLabel Advanced_Button;
    }
}