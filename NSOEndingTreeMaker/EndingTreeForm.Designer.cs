namespace NSOEndingTreeMaker
{
    partial class EndingTreeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndingTreeForm));
            this.AddEndingButton = new System.Windows.Forms.Button();
            this.CurrentEndingTree_Group = new System.Windows.Forms.GroupBox();
            this.Notes_Title = new System.Windows.Forms.Label();
            this.Notes = new System.Windows.Forms.TextBox();
            this.EditEndingBranch = new System.Windows.Forms.Button();
            this.DeleteEndingBranch = new System.Windows.Forms.Button();
            this.LoadEndingTreeButton = new System.Windows.Forms.Button();
            this.SaveEndingTreeButton = new System.Windows.Forms.Button();
            this.ExportEndingTreeButton = new System.Windows.Forms.Button();
            this.EndingListView = new System.Windows.Forms.ListView();
            this.EndingNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartingDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LatestDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsStressBreakdownColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentEndingTree_Group.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddEndingButton
            // 
            this.AddEndingButton.Location = new System.Drawing.Point(19, 28);
            this.AddEndingButton.Name = "AddEndingButton";
            this.AddEndingButton.Size = new System.Drawing.Size(218, 36);
            this.AddEndingButton.TabIndex = 4;
            this.AddEndingButton.Text = "Add Ending Branch";
            this.AddEndingButton.UseVisualStyleBackColor = true;
            this.AddEndingButton.Click += new System.EventHandler(this.AddEndingButton_Click);
            // 
            // CurrentEndingTree_Group
            // 
            this.CurrentEndingTree_Group.Controls.Add(this.Notes_Title);
            this.CurrentEndingTree_Group.Controls.Add(this.Notes);
            this.CurrentEndingTree_Group.Controls.Add(this.EditEndingBranch);
            this.CurrentEndingTree_Group.Controls.Add(this.DeleteEndingBranch);
            this.CurrentEndingTree_Group.Controls.Add(this.AddEndingButton);
            this.CurrentEndingTree_Group.Location = new System.Drawing.Point(13, 121);
            this.CurrentEndingTree_Group.Name = "CurrentEndingTree_Group";
            this.CurrentEndingTree_Group.Size = new System.Drawing.Size(260, 351);
            this.CurrentEndingTree_Group.TabIndex = 9;
            this.CurrentEndingTree_Group.TabStop = false;
            this.CurrentEndingTree_Group.Text = "Current Ending Tree";
            // 
            // Notes_Title
            // 
            this.Notes_Title.AutoSize = true;
            this.Notes_Title.Location = new System.Drawing.Point(15, 176);
            this.Notes_Title.Name = "Notes_Title";
            this.Notes_Title.Size = new System.Drawing.Size(35, 13);
            this.Notes_Title.TabIndex = 7;
            this.Notes_Title.Text = "Notes";
            // 
            // Notes
            // 
            this.Notes.Location = new System.Drawing.Point(19, 195);
            this.Notes.Multiline = true;
            this.Notes.Name = "Notes";
            this.Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Notes.Size = new System.Drawing.Size(218, 136);
            this.Notes.TabIndex = 7;
            this.Notes.TextChanged += new System.EventHandler(this.Notes_TextChanged);
            // 
            // EditEndingBranch
            // 
            this.EditEndingBranch.Location = new System.Drawing.Point(19, 120);
            this.EditEndingBranch.Name = "EditEndingBranch";
            this.EditEndingBranch.Size = new System.Drawing.Size(218, 36);
            this.EditEndingBranch.TabIndex = 6;
            this.EditEndingBranch.Text = "Edit Ending Branch";
            this.EditEndingBranch.UseVisualStyleBackColor = true;
            this.EditEndingBranch.Click += new System.EventHandler(this.EditEndingBranch_Click);
            // 
            // DeleteEndingBranch
            // 
            this.DeleteEndingBranch.Location = new System.Drawing.Point(19, 74);
            this.DeleteEndingBranch.Name = "DeleteEndingBranch";
            this.DeleteEndingBranch.Size = new System.Drawing.Size(218, 36);
            this.DeleteEndingBranch.TabIndex = 5;
            this.DeleteEndingBranch.Text = "Delete Ending Branch";
            this.DeleteEndingBranch.UseVisualStyleBackColor = true;
            this.DeleteEndingBranch.Click += new System.EventHandler(this.DeleteEndingBranch_Click);
            // 
            // LoadEndingTreeButton
            // 
            this.LoadEndingTreeButton.Location = new System.Drawing.Point(13, 20);
            this.LoadEndingTreeButton.Name = "LoadEndingTreeButton";
            this.LoadEndingTreeButton.Size = new System.Drawing.Size(128, 39);
            this.LoadEndingTreeButton.TabIndex = 1;
            this.LoadEndingTreeButton.Text = "Load Ending Tree";
            this.LoadEndingTreeButton.UseVisualStyleBackColor = true;
            this.LoadEndingTreeButton.Click += new System.EventHandler(this.LoadEndingTree);
            // 
            // SaveEndingTreeButton
            // 
            this.SaveEndingTreeButton.Location = new System.Drawing.Point(147, 20);
            this.SaveEndingTreeButton.Name = "SaveEndingTreeButton";
            this.SaveEndingTreeButton.Size = new System.Drawing.Size(126, 39);
            this.SaveEndingTreeButton.TabIndex = 2;
            this.SaveEndingTreeButton.Text = "Save Ending Tree";
            this.SaveEndingTreeButton.UseVisualStyleBackColor = true;
            this.SaveEndingTreeButton.Click += new System.EventHandler(this.SaveEndingTree);
            // 
            // ExportEndingTreeButton
            // 
            this.ExportEndingTreeButton.Location = new System.Drawing.Point(13, 65);
            this.ExportEndingTreeButton.Name = "ExportEndingTreeButton";
            this.ExportEndingTreeButton.Size = new System.Drawing.Size(260, 39);
            this.ExportEndingTreeButton.TabIndex = 3;
            this.ExportEndingTreeButton.Text = "Export Ending Tree As CSV";
            this.ExportEndingTreeButton.UseVisualStyleBackColor = true;
            this.ExportEndingTreeButton.Click += new System.EventHandler(this.ExportEndingTreeToCSV);
            // 
            // EndingListView
            // 
            this.EndingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EndingNameColumn,
            this.StartingDayColumn,
            this.LatestDayColumn,
            this.IsStressBreakdownColumn});
            this.EndingListView.FullRowSelect = true;
            this.EndingListView.GridLines = true;
            this.EndingListView.HideSelection = false;
            this.EndingListView.Location = new System.Drawing.Point(280, 20);
            this.EndingListView.MultiSelect = false;
            this.EndingListView.Name = "EndingListView";
            this.EndingListView.Size = new System.Drawing.Size(473, 452);
            this.EndingListView.TabIndex = 8;
            this.EndingListView.UseCompatibleStateImageBehavior = false;
            this.EndingListView.View = System.Windows.Forms.View.Details;
            this.EndingListView.SelectedIndexChanged += new System.EventHandler(this.EndingListView_SelectedIndexChanged);
            this.EndingListView.DoubleClick += new System.EventHandler(this.EndingListView_DoubleClick);
            this.EndingListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndingListView_KeyDown);
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
            // IsStressBreakdownColumn
            // 
            this.IsStressBreakdownColumn.Text = "Is Stressful Breakdown";
            this.IsStressBreakdownColumn.Width = 120;
            // 
            // EndingTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 500);
            this.Controls.Add(this.EndingListView);
            this.Controls.Add(this.SaveEndingTreeButton);
            this.Controls.Add(this.ExportEndingTreeButton);
            this.Controls.Add(this.CurrentEndingTree_Group);
            this.Controls.Add(this.LoadEndingTreeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EndingTreeForm";
            this.Text = "Main Ending Tree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndingTreeForm_FormClosing);
            this.Load += new System.EventHandler(this.EndingTreeForm_Load);
            this.Leave += new System.EventHandler(this.EndingTreeForm_Leave);
            this.CurrentEndingTree_Group.ResumeLayout(false);
            this.CurrentEndingTree_Group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AddEndingButton;
        private System.Windows.Forms.GroupBox CurrentEndingTree_Group;
        private System.Windows.Forms.Button LoadEndingTreeButton;
        private System.Windows.Forms.Button SaveEndingTreeButton;
        private System.Windows.Forms.Button ExportEndingTreeButton;
        private System.Windows.Forms.Button DeleteEndingBranch;
        private System.Windows.Forms.Button EditEndingBranch;
        private System.Windows.Forms.Label Notes_Title;
        private System.Windows.Forms.TextBox Notes;
        private System.Windows.Forms.ListView EndingListView;
        private System.Windows.Forms.ColumnHeader EndingNameColumn;
        private System.Windows.Forms.ColumnHeader StartingDayColumn;
        private System.Windows.Forms.ColumnHeader IsStressBreakdownColumn;
        private System.Windows.Forms.ColumnHeader LatestDayColumn;
    }
}

