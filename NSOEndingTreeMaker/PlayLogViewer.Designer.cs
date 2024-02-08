namespace NSOEndingTreeMaker
{
    partial class PlayLogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayLogViewer));
            this.PlaythroughLog = new System.Windows.Forms.TabControl();
            this.DataOne = new System.Windows.Forms.TabPage();
            this.DataOneEndings = new System.Windows.Forms.ListView();
            this.EndingNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartingDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LatestDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataTwo = new System.Windows.Forms.TabPage();
            this.DataTwoEndings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataThree = new System.Windows.Forms.TabPage();
            this.DataThreeEndings = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogDayListView = new System.Windows.Forms.ListView();
            this.DayIndexColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DayPartColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ActionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IgnoreDMColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ImportThree = new System.Windows.Forms.Button();
            this.ImportTwo = new System.Windows.Forms.Button();
            this.ImportOne = new System.Windows.Forms.Button();
            this.AppendToStart = new System.Windows.Forms.Button();
            this.AppendToEnd = new System.Windows.Forms.Button();
            this.ResetLogView = new System.Windows.Forms.Button();
            this.PlaythroughLog.SuspendLayout();
            this.DataOne.SuspendLayout();
            this.DataTwo.SuspendLayout();
            this.DataThree.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlaythroughLog
            // 
            this.PlaythroughLog.Controls.Add(this.DataOne);
            this.PlaythroughLog.Controls.Add(this.DataTwo);
            this.PlaythroughLog.Controls.Add(this.DataThree);
            this.PlaythroughLog.Location = new System.Drawing.Point(13, 13);
            this.PlaythroughLog.Name = "PlaythroughLog";
            this.PlaythroughLog.SelectedIndex = 0;
            this.PlaythroughLog.Size = new System.Drawing.Size(375, 509);
            this.PlaythroughLog.TabIndex = 0;
            // 
            // DataOne
            // 
            this.DataOne.Controls.Add(this.DataOneEndings);
            this.DataOne.Location = new System.Drawing.Point(4, 22);
            this.DataOne.Name = "DataOne";
            this.DataOne.Padding = new System.Windows.Forms.Padding(3);
            this.DataOne.Size = new System.Drawing.Size(367, 483);
            this.DataOne.TabIndex = 0;
            this.DataOne.Text = "Data1";
            this.DataOne.UseVisualStyleBackColor = true;
            // 
            // DataOneEndings
            // 
            this.DataOneEndings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EndingNameColumn,
            this.StartingDayColumn,
            this.LatestDayColumn});
            this.DataOneEndings.FullRowSelect = true;
            this.DataOneEndings.GridLines = true;
            this.DataOneEndings.HideSelection = false;
            this.DataOneEndings.Location = new System.Drawing.Point(0, 0);
            this.DataOneEndings.MultiSelect = false;
            this.DataOneEndings.Name = "DataOneEndings";
            this.DataOneEndings.Size = new System.Drawing.Size(367, 483);
            this.DataOneEndings.TabIndex = 9;
            this.DataOneEndings.UseCompatibleStateImageBehavior = false;
            this.DataOneEndings.View = System.Windows.Forms.View.Details;
            this.DataOneEndings.SelectedIndexChanged += new System.EventHandler(this.DataOneEndings_SelectedIndexChanged);
            // 
            // EndingNameColumn
            // 
            this.EndingNameColumn.Text = "Ending";
            this.EndingNameColumn.Width = 211;
            // 
            // StartingDayColumn
            // 
            this.StartingDayColumn.Text = "Starting Day";
            this.StartingDayColumn.Width = 71;
            // 
            // LatestDayColumn
            // 
            this.LatestDayColumn.Text = "Latest Day";
            this.LatestDayColumn.Width = 65;
            // 
            // DataTwo
            // 
            this.DataTwo.Controls.Add(this.DataTwoEndings);
            this.DataTwo.Location = new System.Drawing.Point(4, 22);
            this.DataTwo.Name = "DataTwo";
            this.DataTwo.Padding = new System.Windows.Forms.Padding(3);
            this.DataTwo.Size = new System.Drawing.Size(367, 483);
            this.DataTwo.TabIndex = 1;
            this.DataTwo.Text = "Data2";
            this.DataTwo.UseVisualStyleBackColor = true;
            // 
            // DataTwoEndings
            // 
            this.DataTwoEndings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.DataTwoEndings.FullRowSelect = true;
            this.DataTwoEndings.GridLines = true;
            this.DataTwoEndings.HideSelection = false;
            this.DataTwoEndings.Location = new System.Drawing.Point(0, 0);
            this.DataTwoEndings.MultiSelect = false;
            this.DataTwoEndings.Name = "DataTwoEndings";
            this.DataTwoEndings.Size = new System.Drawing.Size(367, 483);
            this.DataTwoEndings.TabIndex = 10;
            this.DataTwoEndings.UseCompatibleStateImageBehavior = false;
            this.DataTwoEndings.View = System.Windows.Forms.View.Details;
            this.DataTwoEndings.SelectedIndexChanged += new System.EventHandler(this.DataTwoEndings_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ending";
            this.columnHeader1.Width = 211;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Starting Day";
            this.columnHeader2.Width = 71;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Latest Day";
            this.columnHeader3.Width = 65;
            // 
            // DataThree
            // 
            this.DataThree.Controls.Add(this.DataThreeEndings);
            this.DataThree.Location = new System.Drawing.Point(4, 22);
            this.DataThree.Name = "DataThree";
            this.DataThree.Padding = new System.Windows.Forms.Padding(3);
            this.DataThree.Size = new System.Drawing.Size(367, 483);
            this.DataThree.TabIndex = 2;
            this.DataThree.Text = "Data3";
            this.DataThree.UseVisualStyleBackColor = true;
            // 
            // DataThreeEndings
            // 
            this.DataThreeEndings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.DataThreeEndings.FullRowSelect = true;
            this.DataThreeEndings.GridLines = true;
            this.DataThreeEndings.HideSelection = false;
            this.DataThreeEndings.Location = new System.Drawing.Point(0, 0);
            this.DataThreeEndings.MultiSelect = false;
            this.DataThreeEndings.Name = "DataThreeEndings";
            this.DataThreeEndings.Size = new System.Drawing.Size(367, 483);
            this.DataThreeEndings.TabIndex = 10;
            this.DataThreeEndings.UseCompatibleStateImageBehavior = false;
            this.DataThreeEndings.View = System.Windows.Forms.View.Details;
            this.DataThreeEndings.SelectedIndexChanged += new System.EventHandler(this.DataThreeEndings_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ending";
            this.columnHeader4.Width = 211;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Starting Day";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Latest Day";
            this.columnHeader6.Width = 65;
            // 
            // LogDayListView
            // 
            this.LogDayListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogDayListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DayIndexColumn,
            this.DayPartColumn,
            this.ActionColumn,
            this.IgnoreDMColumn});
            this.LogDayListView.FullRowSelect = true;
            this.LogDayListView.GridLines = true;
            this.LogDayListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LogDayListView.HideSelection = false;
            this.LogDayListView.Location = new System.Drawing.Point(395, 35);
            this.LogDayListView.Name = "LogDayListView";
            this.LogDayListView.Size = new System.Drawing.Size(321, 332);
            this.LogDayListView.TabIndex = 1;
            this.LogDayListView.UseCompatibleStateImageBehavior = false;
            this.LogDayListView.View = System.Windows.Forms.View.Details;
            // 
            // DayIndexColumn
            // 
            this.DayIndexColumn.Text = "Day";
            this.DayIndexColumn.Width = 31;
            // 
            // DayPartColumn
            // 
            this.DayPartColumn.Text = "Time Of Day";
            this.DayPartColumn.Width = 71;
            // 
            // ActionColumn
            // 
            this.ActionColumn.Text = "Action";
            this.ActionColumn.Width = 146;
            // 
            // IgnoreDMColumn
            // 
            this.IgnoreDMColumn.Text = "Ignore DM";
            this.IgnoreDMColumn.Width = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ImportThree);
            this.groupBox1.Controls.Add(this.ImportTwo);
            this.groupBox1.Controls.Add(this.ImportOne);
            this.groupBox1.Location = new System.Drawing.Point(551, 373);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 149);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import:";
            // 
            // ImportThree
            // 
            this.ImportThree.Location = new System.Drawing.Point(9, 102);
            this.ImportThree.Name = "ImportThree";
            this.ImportThree.Size = new System.Drawing.Size(147, 34);
            this.ImportThree.TabIndex = 0;
            this.ImportThree.Text = "Data3";
            this.ImportThree.UseVisualStyleBackColor = true;
            this.ImportThree.Click += new System.EventHandler(this.ImportThree_Click);
            // 
            // ImportTwo
            // 
            this.ImportTwo.Location = new System.Drawing.Point(9, 62);
            this.ImportTwo.Name = "ImportTwo";
            this.ImportTwo.Size = new System.Drawing.Size(147, 34);
            this.ImportTwo.TabIndex = 0;
            this.ImportTwo.Text = "Data2";
            this.ImportTwo.UseVisualStyleBackColor = true;
            this.ImportTwo.Click += new System.EventHandler(this.ImportTwo_Click);
            // 
            // ImportOne
            // 
            this.ImportOne.Location = new System.Drawing.Point(9, 22);
            this.ImportOne.Name = "ImportOne";
            this.ImportOne.Size = new System.Drawing.Size(147, 34);
            this.ImportOne.TabIndex = 0;
            this.ImportOne.Text = "Data1";
            this.ImportOne.UseVisualStyleBackColor = true;
            this.ImportOne.Click += new System.EventHandler(this.ImportOne_Click);
            // 
            // AppendToStart
            // 
            this.AppendToStart.Location = new System.Drawing.Point(394, 395);
            this.AppendToStart.Name = "AppendToStart";
            this.AppendToStart.Size = new System.Drawing.Size(147, 34);
            this.AppendToStart.TabIndex = 0;
            this.AppendToStart.Text = "Add .ame Log To Start";
            this.AppendToStart.UseVisualStyleBackColor = true;
            this.AppendToStart.Click += new System.EventHandler(this.AppendToStart_Click);
            // 
            // AppendToEnd
            // 
            this.AppendToEnd.Location = new System.Drawing.Point(394, 435);
            this.AppendToEnd.Name = "AppendToEnd";
            this.AppendToEnd.Size = new System.Drawing.Size(147, 34);
            this.AppendToEnd.TabIndex = 0;
            this.AppendToEnd.Text = "Add .ame Log To End";
            this.AppendToEnd.UseVisualStyleBackColor = true;
            this.AppendToEnd.Click += new System.EventHandler(this.AppendToEnd_Click);
            // 
            // ResetLogView
            // 
            this.ResetLogView.Location = new System.Drawing.Point(394, 475);
            this.ResetLogView.Name = "ResetLogView";
            this.ResetLogView.Size = new System.Drawing.Size(147, 34);
            this.ResetLogView.TabIndex = 0;
            this.ResetLogView.Text = "Reset Log View";
            this.ResetLogView.UseVisualStyleBackColor = true;
            this.ResetLogView.Click += new System.EventHandler(this.ResetLogView_Click);
            // 
            // PlayLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 534);
            this.Controls.Add(this.ResetLogView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AppendToEnd);
            this.Controls.Add(this.AppendToStart);
            this.Controls.Add(this.LogDayListView);
            this.Controls.Add(this.PlaythroughLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayLogViewer";
            this.Text = "Playthrough Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayLogViewer_FormClosing);
            this.Click += new System.EventHandler(this.PlayLogViewer_Click);
            this.PlaythroughLog.ResumeLayout(false);
            this.DataOne.ResumeLayout(false);
            this.DataTwo.ResumeLayout(false);
            this.DataThree.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl PlaythroughLog;
        private System.Windows.Forms.TabPage DataOne;
        private System.Windows.Forms.TabPage DataTwo;
        private System.Windows.Forms.TabPage DataThree;
        private System.Windows.Forms.ListView DataOneEndings;
        private System.Windows.Forms.ColumnHeader EndingNameColumn;
        private System.Windows.Forms.ColumnHeader StartingDayColumn;
        private System.Windows.Forms.ColumnHeader LatestDayColumn;
        private System.Windows.Forms.ListView DataTwoEndings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView DataThreeEndings;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView LogDayListView;
        private System.Windows.Forms.ColumnHeader DayIndexColumn;
        private System.Windows.Forms.ColumnHeader DayPartColumn;
        private System.Windows.Forms.ColumnHeader ActionColumn;
        private System.Windows.Forms.ColumnHeader IgnoreDMColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ImportOne;
        private System.Windows.Forms.Button ImportThree;
        private System.Windows.Forms.Button ImportTwo;
        private System.Windows.Forms.Button AppendToStart;
        private System.Windows.Forms.Button AppendToEnd;
        private System.Windows.Forms.Button ResetLogView;
    }
}