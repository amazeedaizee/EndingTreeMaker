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
            this.Experiment_Check = new System.Windows.Forms.CheckBox();
            this.Notes_Title = new System.Windows.Forms.Label();
            this.Notes = new System.Windows.Forms.TextBox();
            this.MoveBranchDown_Button = new System.Windows.Forms.Button();
            this.MoveBranchUp_Button = new System.Windows.Forms.Button();
            this.EditEndingBranch = new System.Windows.Forms.Button();
            this.DeleteEndingBranch = new System.Windows.Forms.Button();
            this.EndingListView = new System.Windows.Forms.ListView();
            this.EndingNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartingDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LatestDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsStressBreakdownColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndingTree_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewEndingTree_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportLog = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_SepOne = new System.Windows.Forms.ToolStripSeparator();
            this.OpenEndingTree_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRecent_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_SepTwo = new System.Windows.Forms.ToolStripSeparator();
            this.SaveEndingTree_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveEndingTreeAs_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_SepThree = new System.Windows.Forms.ToolStripSeparator();
            this.ExportAsCSV_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_SepFour = new System.Windows.Forms.ToolStripSeparator();
            this.ResetEndingTree_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Favorites_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotOne_Base = new System.Windows.Forms.ToolStripMenuItem();
            this.Slot1_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotOneSep = new System.Windows.Forms.ToolStripSeparator();
            this.saveTreeToSlot1_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot1_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotTwo_Base = new System.Windows.Forms.ToolStripMenuItem();
            this.Slot2_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotTwoSep = new System.Windows.Forms.ToolStripSeparator();
            this.saveTreeToSlot2_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot2_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotThree_Base = new System.Windows.Forms.ToolStripMenuItem();
            this.Slot3_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotThreeSep = new System.Windows.Forms.ToolStripSeparator();
            this.saveTreeToSlot3_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot3_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotFour_Base = new System.Windows.Forms.ToolStripMenuItem();
            this.Slot4_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotFourSep = new System.Windows.Forms.ToolStripSeparator();
            this.saveTreeToSlot5_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot4_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotFive_Base = new System.Windows.Forms.ToolStripMenuItem();
            this.Slot5_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.SlotFiveSep = new System.Windows.Forms.ToolStripSeparator();
            this.saveTreeToSlot5_MenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot5_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndingSim_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetCurrent_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Simu_SepOne = new System.Windows.Forms.ToolStripSeparator();
            this.SetSlotOne_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetSlotTwo_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetSlotThree_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetSlotFour_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetSlotFive_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Simu_SepTwo = new System.Windows.Forms.ToolStripSeparator();
            this.OpenLogs_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenNSOwSteam_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayGame_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.UnvalidBranches_Label = new System.Windows.Forms.Label();
            this.CurrentEndingTree_Group.SuspendLayout();
            this.EndingTree_MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddEndingButton
            // 
            this.AddEndingButton.Location = new System.Drawing.Point(15, 19);
            this.AddEndingButton.Name = "AddEndingButton";
            this.AddEndingButton.Size = new System.Drawing.Size(218, 35);
            this.AddEndingButton.TabIndex = 4;
            this.AddEndingButton.Text = "Add Ending Branch";
            this.AddEndingButton.UseVisualStyleBackColor = true;
            this.AddEndingButton.Click += new System.EventHandler(this.AddEndingButton_Click);
            // 
            // CurrentEndingTree_Group
            // 
            this.CurrentEndingTree_Group.BackColor = System.Drawing.Color.Transparent;
            this.CurrentEndingTree_Group.Controls.Add(this.Experiment_Check);
            this.CurrentEndingTree_Group.Controls.Add(this.Notes_Title);
            this.CurrentEndingTree_Group.Controls.Add(this.Notes);
            this.CurrentEndingTree_Group.Controls.Add(this.MoveBranchDown_Button);
            this.CurrentEndingTree_Group.Controls.Add(this.MoveBranchUp_Button);
            this.CurrentEndingTree_Group.Controls.Add(this.EditEndingBranch);
            this.CurrentEndingTree_Group.Controls.Add(this.DeleteEndingBranch);
            this.CurrentEndingTree_Group.Controls.Add(this.AddEndingButton);
            this.CurrentEndingTree_Group.Location = new System.Drawing.Point(14, 27);
            this.CurrentEndingTree_Group.Name = "CurrentEndingTree_Group";
            this.CurrentEndingTree_Group.Size = new System.Drawing.Size(248, 445);
            this.CurrentEndingTree_Group.TabIndex = 9;
            this.CurrentEndingTree_Group.TabStop = false;
            // 
            // Experiment_Check
            // 
            this.Experiment_Check.AutoSize = true;
            this.Experiment_Check.Location = new System.Drawing.Point(15, 417);
            this.Experiment_Check.Name = "Experiment_Check";
            this.Experiment_Check.Size = new System.Drawing.Size(116, 17);
            this.Experiment_Check.TabIndex = 12;
            this.Experiment_Check.Text = "Experimental Mode";
            this.Experiment_Check.UseVisualStyleBackColor = true;
            this.Experiment_Check.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Day2Exp_Check_MouseClick);
            // 
            // Notes_Title
            // 
            this.Notes_Title.AutoSize = true;
            this.Notes_Title.Location = new System.Drawing.Point(12, 256);
            this.Notes_Title.Name = "Notes_Title";
            this.Notes_Title.Size = new System.Drawing.Size(35, 13);
            this.Notes_Title.TabIndex = 7;
            this.Notes_Title.Text = "Notes";
            // 
            // Notes
            // 
            this.Notes.Location = new System.Drawing.Point(15, 277);
            this.Notes.Multiline = true;
            this.Notes.Name = "Notes";
            this.Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Notes.Size = new System.Drawing.Size(218, 132);
            this.Notes.TabIndex = 7;
            this.Notes.Click += new System.EventHandler(this.Notes_Click);
            this.Notes.TextChanged += new System.EventHandler(this.Notes_TextChanged);
            // 
            // MoveBranchDown_Button
            // 
            this.MoveBranchDown_Button.Location = new System.Drawing.Point(15, 199);
            this.MoveBranchDown_Button.Name = "MoveBranchDown_Button";
            this.MoveBranchDown_Button.Size = new System.Drawing.Size(218, 35);
            this.MoveBranchDown_Button.TabIndex = 6;
            this.MoveBranchDown_Button.Text = "Move Branch Down";
            this.MoveBranchDown_Button.UseVisualStyleBackColor = true;
            this.MoveBranchDown_Button.Click += new System.EventHandler(this.MoveBranchDown_Button_Click);
            // 
            // MoveBranchUp_Button
            // 
            this.MoveBranchUp_Button.Location = new System.Drawing.Point(15, 158);
            this.MoveBranchUp_Button.Name = "MoveBranchUp_Button";
            this.MoveBranchUp_Button.Size = new System.Drawing.Size(218, 35);
            this.MoveBranchUp_Button.TabIndex = 6;
            this.MoveBranchUp_Button.Text = "Move Branch Up";
            this.MoveBranchUp_Button.UseVisualStyleBackColor = true;
            this.MoveBranchUp_Button.Click += new System.EventHandler(this.MoveBranchUp_Button_Click);
            // 
            // EditEndingBranch
            // 
            this.EditEndingBranch.Location = new System.Drawing.Point(15, 97);
            this.EditEndingBranch.Name = "EditEndingBranch";
            this.EditEndingBranch.Size = new System.Drawing.Size(218, 35);
            this.EditEndingBranch.TabIndex = 6;
            this.EditEndingBranch.Text = "Edit Ending Branch";
            this.EditEndingBranch.UseVisualStyleBackColor = true;
            this.EditEndingBranch.Click += new System.EventHandler(this.EditEndingBranch_Click);
            // 
            // DeleteEndingBranch
            // 
            this.DeleteEndingBranch.Location = new System.Drawing.Point(15, 58);
            this.DeleteEndingBranch.Name = "DeleteEndingBranch";
            this.DeleteEndingBranch.Size = new System.Drawing.Size(218, 35);
            this.DeleteEndingBranch.TabIndex = 5;
            this.DeleteEndingBranch.Text = "Delete Ending Branch";
            this.DeleteEndingBranch.UseVisualStyleBackColor = true;
            this.DeleteEndingBranch.Click += new System.EventHandler(this.DeleteEndingBranch_Click);
            // 
            // EndingListView
            // 
            this.EndingListView.AllowDrop = true;
            this.EndingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EndingNameColumn,
            this.StartingDayColumn,
            this.LatestDayColumn,
            this.IsStressBreakdownColumn});
            this.EndingListView.FullRowSelect = true;
            this.EndingListView.GridLines = true;
            this.EndingListView.HideSelection = false;
            this.EndingListView.Location = new System.Drawing.Point(280, 35);
            this.EndingListView.MultiSelect = false;
            this.EndingListView.Name = "EndingListView";
            this.EndingListView.Size = new System.Drawing.Size(473, 437);
            this.EndingListView.TabIndex = 8;
            this.EndingListView.UseCompatibleStateImageBehavior = false;
            this.EndingListView.View = System.Windows.Forms.View.Details;
            this.EndingListView.SelectedIndexChanged += new System.EventHandler(this.EndingListView_SelectedIndexChanged);
            this.EndingListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.EndingListView_DragDrop);
            this.EndingListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.EndingListView_DragEnter);
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
            // EndingTree_MenuStrip
            // 
            this.EndingTree_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_MenuItem,
            this.Favorites_MenuItem,
            this.EndingSim_MenuItem});
            this.EndingTree_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.EndingTree_MenuStrip.Name = "EndingTree_MenuStrip";
            this.EndingTree_MenuStrip.Size = new System.Drawing.Size(766, 24);
            this.EndingTree_MenuStrip.TabIndex = 10;
            this.EndingTree_MenuStrip.Text = "Tree";
            // 
            // File_MenuItem
            // 
            this.File_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewEndingTree_MenuItem,
            this.toolStripSeparator1,
            this.ImportLog,
            this.Tree_SepOne,
            this.OpenEndingTree_MenuItem,
            this.OpenRecent_MenuItem,
            this.Tree_SepTwo,
            this.SaveEndingTree_MenuItem,
            this.SaveEndingTreeAs_MenuItem,
            this.Tree_SepThree,
            this.ExportAsCSV_MenuItem,
            this.Tree_SepFour,
            this.ResetEndingTree_MenuItem});
            this.File_MenuItem.Name = "File_MenuItem";
            this.File_MenuItem.Size = new System.Drawing.Size(37, 20);
            this.File_MenuItem.Text = "File";
            this.File_MenuItem.DropDownOpened += new System.EventHandler(this.File_MenuItem_DropDownOpened);
            // 
            // NewEndingTree_MenuItem
            // 
            this.NewEndingTree_MenuItem.Name = "NewEndingTree_MenuItem";
            this.NewEndingTree_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewEndingTree_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.NewEndingTree_MenuItem.Text = "New Ending Tree";
            this.NewEndingTree_MenuItem.Click += new System.EventHandler(this.NewTree_MenuItem);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(311, 6);
            // 
            // ImportLog
            // 
            this.ImportLog.Enabled = false;
            this.ImportLog.Name = "ImportLog";
            this.ImportLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ImportLog.Size = new System.Drawing.Size(314, 22);
            this.ImportLog.Text = "Import Playthrough Log";
            this.ImportLog.Visible = false;
            this.ImportLog.Click += new System.EventHandler(this.ImportLog_Click);
            // 
            // Tree_SepOne
            // 
            this.Tree_SepOne.Name = "Tree_SepOne";
            this.Tree_SepOne.Size = new System.Drawing.Size(311, 6);
            this.Tree_SepOne.Visible = false;
            // 
            // OpenEndingTree_MenuItem
            // 
            this.OpenEndingTree_MenuItem.Name = "OpenEndingTree_MenuItem";
            this.OpenEndingTree_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenEndingTree_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.OpenEndingTree_MenuItem.Text = "Open Ending Tree";
            this.OpenEndingTree_MenuItem.Click += new System.EventHandler(this.openEndingTreeToolStripMenuItem_Click);
            // 
            // OpenRecent_MenuItem
            // 
            this.OpenRecent_MenuItem.Name = "OpenRecent_MenuItem";
            this.OpenRecent_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.OpenRecent_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.OpenRecent_MenuItem.Text = "Open Recent Ending Tree:";
            this.OpenRecent_MenuItem.Click += new System.EventHandler(this.openRecentEndingTreeToolStripMenuItem_Click);
            // 
            // Tree_SepTwo
            // 
            this.Tree_SepTwo.Name = "Tree_SepTwo";
            this.Tree_SepTwo.Size = new System.Drawing.Size(311, 6);
            // 
            // SaveEndingTree_MenuItem
            // 
            this.SaveEndingTree_MenuItem.Name = "SaveEndingTree_MenuItem";
            this.SaveEndingTree_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveEndingTree_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.SaveEndingTree_MenuItem.Text = "Save Ending Tree";
            this.SaveEndingTree_MenuItem.Click += new System.EventHandler(this.saveEndingTreeToolStripMenuItem_Click);
            // 
            // SaveEndingTreeAs_MenuItem
            // 
            this.SaveEndingTreeAs_MenuItem.Name = "SaveEndingTreeAs_MenuItem";
            this.SaveEndingTreeAs_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveEndingTreeAs_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.SaveEndingTreeAs_MenuItem.Text = "Save Ending Tree as...";
            this.SaveEndingTreeAs_MenuItem.Click += new System.EventHandler(this.saveEndingTreeAsToolStripMenuItem_Click);
            // 
            // Tree_SepThree
            // 
            this.Tree_SepThree.Name = "Tree_SepThree";
            this.Tree_SepThree.Size = new System.Drawing.Size(311, 6);
            // 
            // ExportAsCSV_MenuItem
            // 
            this.ExportAsCSV_MenuItem.Name = "ExportAsCSV_MenuItem";
            this.ExportAsCSV_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ExportAsCSV_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.ExportAsCSV_MenuItem.Text = "Export Ending Tree as CSV";
            this.ExportAsCSV_MenuItem.Click += new System.EventHandler(this.exportTreeAsCSVToolStripMenuItem_Click);
            // 
            // Tree_SepFour
            // 
            this.Tree_SepFour.Name = "Tree_SepFour";
            this.Tree_SepFour.Size = new System.Drawing.Size(311, 6);
            // 
            // ResetEndingTree_MenuItem
            // 
            this.ResetEndingTree_MenuItem.Name = "ResetEndingTree_MenuItem";
            this.ResetEndingTree_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.ResetEndingTree_MenuItem.Size = new System.Drawing.Size(314, 22);
            this.ResetEndingTree_MenuItem.Text = "Reset Ending Tree To Last Saved";
            this.ResetEndingTree_MenuItem.Click += new System.EventHandler(this.ResetEndingTree_MenuCLick);
            // 
            // Favorites_MenuItem
            // 
            this.Favorites_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SlotOne_Base,
            this.SlotTwo_Base,
            this.SlotThree_Base,
            this.SlotFour_Base,
            this.SlotFive_Base});
            this.Favorites_MenuItem.Name = "Favorites_MenuItem";
            this.Favorites_MenuItem.Size = new System.Drawing.Size(66, 20);
            this.Favorites_MenuItem.Text = "Favorites";
            // 
            // SlotOne_Base
            // 
            this.SlotOne_Base.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot1_Name,
            this.SlotOneSep,
            this.saveTreeToSlot1_MenuItem,
            this.loadTreeFromSlot1_MenuItem});
            this.SlotOne_Base.Name = "SlotOne_Base";
            this.SlotOne_Base.Size = new System.Drawing.Size(103, 22);
            this.SlotOne_Base.Text = "Slot 1";
            // 
            // Slot1_Name
            // 
            this.Slot1_Name.Enabled = false;
            this.Slot1_Name.Name = "Slot1_Name";
            this.Slot1_Name.Size = new System.Drawing.Size(222, 22);
            this.Slot1_Name.Text = "(none)";
            // 
            // SlotOneSep
            // 
            this.SlotOneSep.Name = "SlotOneSep";
            this.SlotOneSep.Size = new System.Drawing.Size(219, 6);
            // 
            // saveTreeToSlot1_MenuItem
            // 
            this.saveTreeToSlot1_MenuItem.Name = "saveTreeToSlot1_MenuItem";
            this.saveTreeToSlot1_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
            this.saveTreeToSlot1_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveTreeToSlot1_MenuItem.Text = "Save Tree To Slot 1";
            this.saveTreeToSlot1_MenuItem.Click += new System.EventHandler(this.SaveToSLot1);
            // 
            // loadTreeFromSlot1_MenuItem
            // 
            this.loadTreeFromSlot1_MenuItem.Name = "loadTreeFromSlot1_MenuItem";
            this.loadTreeFromSlot1_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.loadTreeFromSlot1_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.loadTreeFromSlot1_MenuItem.Text = "Load Tree From Slot 1";
            this.loadTreeFromSlot1_MenuItem.Click += new System.EventHandler(this.LoadToSlot1);
            // 
            // SlotTwo_Base
            // 
            this.SlotTwo_Base.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot2_Name,
            this.SlotTwoSep,
            this.saveTreeToSlot2_MenuItem,
            this.loadTreeFromSlot2_MenuItem});
            this.SlotTwo_Base.Name = "SlotTwo_Base";
            this.SlotTwo_Base.Size = new System.Drawing.Size(103, 22);
            this.SlotTwo_Base.Text = "Slot 2";
            // 
            // Slot2_Name
            // 
            this.Slot2_Name.Enabled = false;
            this.Slot2_Name.Name = "Slot2_Name";
            this.Slot2_Name.Size = new System.Drawing.Size(222, 22);
            this.Slot2_Name.Text = "(none)";
            // 
            // SlotTwoSep
            // 
            this.SlotTwoSep.Name = "SlotTwoSep";
            this.SlotTwoSep.Size = new System.Drawing.Size(219, 6);
            // 
            // saveTreeToSlot2_MenuItem
            // 
            this.saveTreeToSlot2_MenuItem.Name = "saveTreeToSlot2_MenuItem";
            this.saveTreeToSlot2_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
            this.saveTreeToSlot2_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveTreeToSlot2_MenuItem.Text = "Save Tree To Slot 2";
            this.saveTreeToSlot2_MenuItem.Click += new System.EventHandler(this.SaveToSlot2);
            // 
            // loadTreeFromSlot2_MenuItem
            // 
            this.loadTreeFromSlot2_MenuItem.Name = "loadTreeFromSlot2_MenuItem";
            this.loadTreeFromSlot2_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.loadTreeFromSlot2_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.loadTreeFromSlot2_MenuItem.Text = "Load Tree From Slot 2";
            this.loadTreeFromSlot2_MenuItem.Click += new System.EventHandler(this.LoadToSlot2);
            // 
            // SlotThree_Base
            // 
            this.SlotThree_Base.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot3_Name,
            this.SlotThreeSep,
            this.saveTreeToSlot3_MenuItem,
            this.loadTreeFromSlot3_MenuItem});
            this.SlotThree_Base.Name = "SlotThree_Base";
            this.SlotThree_Base.Size = new System.Drawing.Size(103, 22);
            this.SlotThree_Base.Text = "Slot 3";
            // 
            // Slot3_Name
            // 
            this.Slot3_Name.Enabled = false;
            this.Slot3_Name.Name = "Slot3_Name";
            this.Slot3_Name.Size = new System.Drawing.Size(222, 22);
            this.Slot3_Name.Text = "(none)";
            // 
            // SlotThreeSep
            // 
            this.SlotThreeSep.Name = "SlotThreeSep";
            this.SlotThreeSep.Size = new System.Drawing.Size(219, 6);
            // 
            // saveTreeToSlot3_MenuItem
            // 
            this.saveTreeToSlot3_MenuItem.Name = "saveTreeToSlot3_MenuItem";
            this.saveTreeToSlot3_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.saveTreeToSlot3_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveTreeToSlot3_MenuItem.Text = "Save Tree To Slot 3";
            this.saveTreeToSlot3_MenuItem.Click += new System.EventHandler(this.SaveToSlot3);
            // 
            // loadTreeFromSlot3_MenuItem
            // 
            this.loadTreeFromSlot3_MenuItem.Name = "loadTreeFromSlot3_MenuItem";
            this.loadTreeFromSlot3_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.loadTreeFromSlot3_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.loadTreeFromSlot3_MenuItem.Text = "Load Tree From Slot 3";
            this.loadTreeFromSlot3_MenuItem.Click += new System.EventHandler(this.LoadToSlot3);
            // 
            // SlotFour_Base
            // 
            this.SlotFour_Base.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot4_Name,
            this.SlotFourSep,
            this.saveTreeToSlot5_MenuItem,
            this.loadTreeFromSlot4_MenuItem});
            this.SlotFour_Base.Name = "SlotFour_Base";
            this.SlotFour_Base.Size = new System.Drawing.Size(103, 22);
            this.SlotFour_Base.Text = "Slot 4";
            // 
            // Slot4_Name
            // 
            this.Slot4_Name.Enabled = false;
            this.Slot4_Name.Name = "Slot4_Name";
            this.Slot4_Name.Size = new System.Drawing.Size(222, 22);
            this.Slot4_Name.Text = "(none)";
            // 
            // SlotFourSep
            // 
            this.SlotFourSep.Name = "SlotFourSep";
            this.SlotFourSep.Size = new System.Drawing.Size(219, 6);
            // 
            // saveTreeToSlot5_MenuItem
            // 
            this.saveTreeToSlot5_MenuItem.Name = "saveTreeToSlot5_MenuItem";
            this.saveTreeToSlot5_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.saveTreeToSlot5_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveTreeToSlot5_MenuItem.Text = "Save Tree To Slot 4";
            this.saveTreeToSlot5_MenuItem.Click += new System.EventHandler(this.SaveToSlot4);
            // 
            // loadTreeFromSlot4_MenuItem
            // 
            this.loadTreeFromSlot4_MenuItem.Name = "loadTreeFromSlot4_MenuItem";
            this.loadTreeFromSlot4_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.loadTreeFromSlot4_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.loadTreeFromSlot4_MenuItem.Text = "Load Tree From Slot 4";
            this.loadTreeFromSlot4_MenuItem.Click += new System.EventHandler(this.LoadToSLot4);
            // 
            // SlotFive_Base
            // 
            this.SlotFive_Base.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot5_Name,
            this.SlotFiveSep,
            this.saveTreeToSlot5_MenuItem1,
            this.loadTreeFromSlot5_MenuItem});
            this.SlotFive_Base.Name = "SlotFive_Base";
            this.SlotFive_Base.Size = new System.Drawing.Size(103, 22);
            this.SlotFive_Base.Text = "Slot 5";
            // 
            // Slot5_Name
            // 
            this.Slot5_Name.Enabled = false;
            this.Slot5_Name.Name = "Slot5_Name";
            this.Slot5_Name.Size = new System.Drawing.Size(222, 22);
            this.Slot5_Name.Text = "(none)";
            // 
            // SlotFiveSep
            // 
            this.SlotFiveSep.Name = "SlotFiveSep";
            this.SlotFiveSep.Size = new System.Drawing.Size(219, 6);
            // 
            // saveTreeToSlot5_MenuItem1
            // 
            this.saveTreeToSlot5_MenuItem1.Name = "saveTreeToSlot5_MenuItem1";
            this.saveTreeToSlot5_MenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.saveTreeToSlot5_MenuItem1.Size = new System.Drawing.Size(222, 22);
            this.saveTreeToSlot5_MenuItem1.Text = "Save Tree To Slot 5";
            this.saveTreeToSlot5_MenuItem1.Click += new System.EventHandler(this.SaveToSlot5);
            // 
            // loadTreeFromSlot5_MenuItem
            // 
            this.loadTreeFromSlot5_MenuItem.Name = "loadTreeFromSlot5_MenuItem";
            this.loadTreeFromSlot5_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.loadTreeFromSlot5_MenuItem.Size = new System.Drawing.Size(222, 22);
            this.loadTreeFromSlot5_MenuItem.Text = "Load Tree From Slot 5";
            this.loadTreeFromSlot5_MenuItem.Click += new System.EventHandler(this.LoadToSlot5);
            // 
            // EndingSim_MenuItem
            // 
            this.EndingSim_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetCurrent_MenuItem,
            this.Simu_SepOne,
            this.SetSlotOne_MenuItem,
            this.SetSlotTwo_MenuItem,
            this.SetSlotThree_MenuItem,
            this.SetSlotFour_MenuItem,
            this.SetSlotFive_MenuItem,
            this.Simu_SepTwo,
            this.OpenLogs_MenuItem,
            this.OpenNSOwSteam_MenuItem,
            this.PlayGame_Button});
            this.EndingSim_MenuItem.Name = "EndingSim_MenuItem";
            this.EndingSim_MenuItem.Size = new System.Drawing.Size(135, 20);
            this.EndingSim_MenuItem.Text = "Ending Tree Simulator";
            // 
            // SetCurrent_MenuItem
            // 
            this.SetCurrent_MenuItem.Name = "SetCurrent_MenuItem";
            this.SetCurrent_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetCurrent_MenuItem.Text = "Set Current Tree as Main Simulation";
            this.SetCurrent_MenuItem.Click += new System.EventHandler(this.SetCurrent_MenuItem_Click);
            // 
            // Simu_SepOne
            // 
            this.Simu_SepOne.Name = "Simu_SepOne";
            this.Simu_SepOne.Size = new System.Drawing.Size(439, 6);
            // 
            // SetSlotOne_MenuItem
            // 
            this.SetSlotOne_MenuItem.Name = "SetSlotOne_MenuItem";
            this.SetSlotOne_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetSlotOne_MenuItem.Text = "Set Slot 1 Tree as Main Simulation";
            this.SetSlotOne_MenuItem.Click += new System.EventHandler(this.SetSlot1_MenuItem_Click);
            // 
            // SetSlotTwo_MenuItem
            // 
            this.SetSlotTwo_MenuItem.Name = "SetSlotTwo_MenuItem";
            this.SetSlotTwo_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetSlotTwo_MenuItem.Text = "Set Slot 2 Tree as Main Simulation";
            this.SetSlotTwo_MenuItem.Click += new System.EventHandler(this.SetSlot2_MenuItem_Click);
            // 
            // SetSlotThree_MenuItem
            // 
            this.SetSlotThree_MenuItem.Name = "SetSlotThree_MenuItem";
            this.SetSlotThree_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetSlotThree_MenuItem.Text = "Set Slot 3 Tree as Main Simulation";
            this.SetSlotThree_MenuItem.Click += new System.EventHandler(this.SetSlot3_MenuItem_Click);
            // 
            // SetSlotFour_MenuItem
            // 
            this.SetSlotFour_MenuItem.Name = "SetSlotFour_MenuItem";
            this.SetSlotFour_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetSlotFour_MenuItem.Text = "Set Slot 4 Tree as Main Simulation";
            this.SetSlotFour_MenuItem.Click += new System.EventHandler(this.SetSlot4_MenuItem_Click);
            // 
            // SetSlotFive_MenuItem
            // 
            this.SetSlotFive_MenuItem.Name = "SetSlotFive_MenuItem";
            this.SetSlotFive_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.SetSlotFive_MenuItem.Text = "Set Slot 5 Tree as Main Simulation";
            this.SetSlotFive_MenuItem.Click += new System.EventHandler(this.SetSlot5_MenuItem_Click);
            // 
            // Simu_SepTwo
            // 
            this.Simu_SepTwo.Name = "Simu_SepTwo";
            this.Simu_SepTwo.Size = new System.Drawing.Size(439, 6);
            // 
            // OpenLogs_MenuItem
            // 
            this.OpenLogs_MenuItem.Name = "OpenLogs_MenuItem";
            this.OpenLogs_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.OpenLogs_MenuItem.Text = "Open Simulation Logs";
            this.OpenLogs_MenuItem.Click += new System.EventHandler(this.openSimulationLogsToolStripMenuItem_Click);
            // 
            // OpenNSOwSteam_MenuItem
            // 
            this.OpenNSOwSteam_MenuItem.Name = "OpenNSOwSteam_MenuItem";
            this.OpenNSOwSteam_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.OpenNSOwSteam_MenuItem.Size = new System.Drawing.Size(442, 22);
            this.OpenNSOwSteam_MenuItem.Text = "Open NEEDY STREAMER OVERLOAD with Steam";
            this.OpenNSOwSteam_MenuItem.Click += new System.EventHandler(this.OpenSteamNSO);
            // 
            // PlayGame_Button
            // 
            this.PlayGame_Button.Name = "PlayGame_Button";
            this.PlayGame_Button.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.PlayGame_Button.Size = new System.Drawing.Size(442, 22);
            this.PlayGame_Button.Text = "Open NEEDY STREAMER OVERLOAD without Steam";
            this.PlayGame_Button.Click += new System.EventHandler(this.PlayGame_Button_Click);
            // 
            // UnvalidBranches_Label
            // 
            this.UnvalidBranches_Label.AutoSize = true;
            this.UnvalidBranches_Label.Location = new System.Drawing.Point(14, 479);
            this.UnvalidBranches_Label.Name = "UnvalidBranches_Label";
            this.UnvalidBranches_Label.Size = new System.Drawing.Size(195, 13);
            this.UnvalidBranches_Label.TabIndex = 11;
            this.UnvalidBranches_Label.Text = "Unvalidated Ending Branches Detected";
            this.UnvalidBranches_Label.VisibleChanged += new System.EventHandler(this.UnvalidBranch_LabelToggle);
            // 
            // EndingTreeForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 500);
            this.Controls.Add(this.UnvalidBranches_Label);
            this.Controls.Add(this.EndingListView);
            this.Controls.Add(this.CurrentEndingTree_Group);
            this.Controls.Add(this.EndingTree_MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.EndingTree_MenuStrip;
            this.MaximizeBox = false;
            this.Name = "EndingTreeForm";
            this.Text = "Main Ending Tree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndingTreeForm_FormClosing);
            this.Load += new System.EventHandler(this.EndingTreeForm_Load);
            this.Click += new System.EventHandler(this.EndingTreeForm_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.EndingTreeForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.EndingTreeForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndingTreeForm_KeyDown);
            this.Leave += new System.EventHandler(this.EndingTreeForm_Leave);
            this.CurrentEndingTree_Group.ResumeLayout(false);
            this.CurrentEndingTree_Group.PerformLayout();
            this.EndingTree_MenuStrip.ResumeLayout(false);
            this.EndingTree_MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddEndingButton;
        private System.Windows.Forms.GroupBox CurrentEndingTree_Group;
        private System.Windows.Forms.Button DeleteEndingBranch;
        private System.Windows.Forms.Button EditEndingBranch;
        private System.Windows.Forms.Label Notes_Title;
        private System.Windows.Forms.TextBox Notes;
        private System.Windows.Forms.ListView EndingListView;
        private System.Windows.Forms.ColumnHeader EndingNameColumn;
        private System.Windows.Forms.ColumnHeader StartingDayColumn;
        private System.Windows.Forms.ColumnHeader IsStressBreakdownColumn;
        private System.Windows.Forms.ColumnHeader LatestDayColumn;
        private System.Windows.Forms.MenuStrip EndingTree_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenEndingTree_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenRecent_MenuItem;
        private System.Windows.Forms.ToolStripSeparator Tree_SepTwo;
        private System.Windows.Forms.ToolStripMenuItem SaveEndingTree_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveEndingTreeAs_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportAsCSV_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Favorites_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SlotOne_Base;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot1_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot1_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SlotTwo_Base;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot2_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot2_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SlotThree_Base;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot3_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot3_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SlotFour_Base;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot5_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot4_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SlotFive_Base;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot5_MenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot5_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndingSim_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetCurrent_MenuItem;
        private System.Windows.Forms.ToolStripSeparator Simu_SepOne;
        private System.Windows.Forms.ToolStripMenuItem SetSlotOne_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetSlotTwo_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetSlotThree_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetSlotFour_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetSlotFive_MenuItem;
        private System.Windows.Forms.ToolStripSeparator Simu_SepTwo;
        private System.Windows.Forms.ToolStripMenuItem OpenLogs_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetEndingTree_MenuItem;
        private System.Windows.Forms.ToolStripSeparator Tree_SepOne;
        private System.Windows.Forms.ToolStripMenuItem Slot1_Name;
        private System.Windows.Forms.ToolStripSeparator SlotOneSep;
        private System.Windows.Forms.ToolStripMenuItem Slot2_Name;
        private System.Windows.Forms.ToolStripSeparator SlotTwoSep;
        private System.Windows.Forms.ToolStripMenuItem Slot3_Name;
        private System.Windows.Forms.ToolStripSeparator SlotThreeSep;
        private System.Windows.Forms.ToolStripMenuItem Slot4_Name;
        private System.Windows.Forms.ToolStripSeparator SlotFourSep;
        private System.Windows.Forms.ToolStripMenuItem Slot5_Name;
        private System.Windows.Forms.ToolStripSeparator SlotFiveSep;
        private System.Windows.Forms.ToolStripMenuItem File_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayGame_Button;
        private System.Windows.Forms.ToolStripSeparator Tree_SepThree;
        private System.Windows.Forms.ToolStripMenuItem NewEndingTree_MenuItem;
        private System.Windows.Forms.ToolStripSeparator Tree_SepFour;
        private System.Windows.Forms.Label UnvalidBranches_Label;
        private System.Windows.Forms.CheckBox Experiment_Check;
        private System.Windows.Forms.ToolStripMenuItem OpenNSOwSteam_MenuItem;
        private System.Windows.Forms.Button MoveBranchDown_Button;
        private System.Windows.Forms.Button MoveBranchUp_Button;
        private System.Windows.Forms.ToolStripMenuItem ImportLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}