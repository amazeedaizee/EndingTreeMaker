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
            this.EndingListView = new System.Windows.Forms.ListView();
            this.EndingNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartingDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LatestDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsStressBreakdownColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openEndingTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentEndingTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveEndingTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEndingTreeAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTreeAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Favorites_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slot1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTreeToSlot1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slot2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTreeToSlot2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slot3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTreeToSlot3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slot4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTreeToSlot5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slot5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTreeToSlot5ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeFromSlot5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndingSim_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCurrentTreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSlot1TreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSlot2TreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSlot3TreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setSlot4TreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSlot5TreeAsMainSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openSimulationLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetEndingTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Slot1_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Slot2_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.Slot3_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.Slot4_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.Slot5_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.PlayGame_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.newEndingTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentEndingTree_Group.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.CurrentEndingTree_Group.Location = new System.Drawing.Point(14, 29);
            this.CurrentEndingTree_Group.Name = "CurrentEndingTree_Group";
            this.CurrentEndingTree_Group.Size = new System.Drawing.Size(260, 443);
            this.CurrentEndingTree_Group.TabIndex = 9;
            this.CurrentEndingTree_Group.TabStop = false;
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
            this.Notes.Size = new System.Drawing.Size(218, 225);
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
            this.EndingListView.Location = new System.Drawing.Point(280, 35);
            this.EndingListView.MultiSelect = false;
            this.EndingListView.Name = "EndingListView";
            this.EndingListView.Size = new System.Drawing.Size(473, 437);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_MenuItem,
            this.Favorites_MenuItem,
            this.EndingSim_MenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_MenuItem
            // 
            this.File_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEndingTreeToolStripMenuItem,
            this.toolStripSeparator4,
            this.openEndingTreeToolStripMenuItem,
            this.openRecentEndingTreeToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveEndingTreeToolStripMenuItem,
            this.saveEndingTreeAsToolStripMenuItem,
            this.toolStripSeparator10,
            this.exportTreeAsCSVToolStripMenuItem,
            this.toolStripSeparator11,
            this.resetEndingTreeToolStripMenuItem});
            this.File_MenuItem.Name = "File_MenuItem";
            this.File_MenuItem.Size = new System.Drawing.Size(37, 20);
            this.File_MenuItem.Text = "File";
            // 
            // openEndingTreeToolStripMenuItem
            // 
            this.openEndingTreeToolStripMenuItem.Name = "openEndingTreeToolStripMenuItem";
            this.openEndingTreeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openEndingTreeToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.openEndingTreeToolStripMenuItem.Text = "Open Ending Tree";
            this.openEndingTreeToolStripMenuItem.Click += new System.EventHandler(this.openEndingTreeToolStripMenuItem_Click);
            // 
            // openRecentEndingTreeToolStripMenuItem
            // 
            this.openRecentEndingTreeToolStripMenuItem.Name = "openRecentEndingTreeToolStripMenuItem";
            this.openRecentEndingTreeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openRecentEndingTreeToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.openRecentEndingTreeToolStripMenuItem.Text = "Open Recent Ending Tree:";
            this.openRecentEndingTreeToolStripMenuItem.Click += new System.EventHandler(this.openRecentEndingTreeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(309, 6);
            // 
            // saveEndingTreeToolStripMenuItem
            // 
            this.saveEndingTreeToolStripMenuItem.Name = "saveEndingTreeToolStripMenuItem";
            this.saveEndingTreeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveEndingTreeToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.saveEndingTreeToolStripMenuItem.Text = "Save Ending Tree";
            this.saveEndingTreeToolStripMenuItem.Click += new System.EventHandler(this.saveEndingTreeToolStripMenuItem_Click);
            // 
            // saveEndingTreeAsToolStripMenuItem
            // 
            this.saveEndingTreeAsToolStripMenuItem.Name = "saveEndingTreeAsToolStripMenuItem";
            this.saveEndingTreeAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveEndingTreeAsToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.saveEndingTreeAsToolStripMenuItem.Text = "Save Ending Tree as...";
            this.saveEndingTreeAsToolStripMenuItem.Click += new System.EventHandler(this.saveEndingTreeAsToolStripMenuItem_Click);
            // 
            // exportTreeAsCSVToolStripMenuItem
            // 
            this.exportTreeAsCSVToolStripMenuItem.Name = "exportTreeAsCSVToolStripMenuItem";
            this.exportTreeAsCSVToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportTreeAsCSVToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.exportTreeAsCSVToolStripMenuItem.Text = "Export Ending Tree as CSV";
            this.exportTreeAsCSVToolStripMenuItem.Click += new System.EventHandler(this.exportTreeAsCSVToolStripMenuItem_Click);
            // 
            // Favorites_MenuItem
            // 
            this.Favorites_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slot1ToolStripMenuItem,
            this.slot2ToolStripMenuItem,
            this.slot3ToolStripMenuItem,
            this.slot4ToolStripMenuItem,
            this.slot5ToolStripMenuItem});
            this.Favorites_MenuItem.Name = "Favorites_MenuItem";
            this.Favorites_MenuItem.Size = new System.Drawing.Size(66, 20);
            this.Favorites_MenuItem.Text = "Favorites";
            // 
            // slot1ToolStripMenuItem
            // 
            this.slot1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot1_Name,
            this.toolStripSeparator5,
            this.saveTreeToSlot1ToolStripMenuItem,
            this.loadTreeFromSlot1ToolStripMenuItem});
            this.slot1ToolStripMenuItem.Name = "slot1ToolStripMenuItem";
            this.slot1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slot1ToolStripMenuItem.Text = "Slot 1";
            // 
            // saveTreeToSlot1ToolStripMenuItem
            // 
            this.saveTreeToSlot1ToolStripMenuItem.Name = "saveTreeToSlot1ToolStripMenuItem";
            this.saveTreeToSlot1ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
            this.saveTreeToSlot1ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveTreeToSlot1ToolStripMenuItem.Text = "Save Tree To Slot 1";
            this.saveTreeToSlot1ToolStripMenuItem.Click += new System.EventHandler(this.saveTreeToSlot1ToolStripMenuItem_Click);
            // 
            // loadTreeFromSlot1ToolStripMenuItem
            // 
            this.loadTreeFromSlot1ToolStripMenuItem.Name = "loadTreeFromSlot1ToolStripMenuItem";
            this.loadTreeFromSlot1ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.loadTreeFromSlot1ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTreeFromSlot1ToolStripMenuItem.Text = "Load Tree From Slot 1";
            this.loadTreeFromSlot1ToolStripMenuItem.Click += new System.EventHandler(this.loadTreeFromSlot1ToolStripMenuItem_Click);
            // 
            // slot2ToolStripMenuItem
            // 
            this.slot2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot2_Name,
            this.toolStripSeparator6,
            this.saveTreeToSlot2ToolStripMenuItem,
            this.loadTreeFromSlot2ToolStripMenuItem});
            this.slot2ToolStripMenuItem.Name = "slot2ToolStripMenuItem";
            this.slot2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slot2ToolStripMenuItem.Text = "Slot 2";
            // 
            // saveTreeToSlot2ToolStripMenuItem
            // 
            this.saveTreeToSlot2ToolStripMenuItem.Name = "saveTreeToSlot2ToolStripMenuItem";
            this.saveTreeToSlot2ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
            this.saveTreeToSlot2ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveTreeToSlot2ToolStripMenuItem.Text = "Save Tree To Slot 2";
            this.saveTreeToSlot2ToolStripMenuItem.Click += new System.EventHandler(this.saveTreeToSlot2ToolStripMenuItem_Click);
            // 
            // loadTreeFromSlot2ToolStripMenuItem
            // 
            this.loadTreeFromSlot2ToolStripMenuItem.Name = "loadTreeFromSlot2ToolStripMenuItem";
            this.loadTreeFromSlot2ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.loadTreeFromSlot2ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTreeFromSlot2ToolStripMenuItem.Text = "Load Tree From Slot 2";
            this.loadTreeFromSlot2ToolStripMenuItem.Click += new System.EventHandler(this.loadTreeFromSlot2ToolStripMenuItem_Click);
            // 
            // slot3ToolStripMenuItem
            // 
            this.slot3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot3_Name,
            this.toolStripSeparator7,
            this.saveTreeToSlot3ToolStripMenuItem,
            this.loadTreeFromSlot3ToolStripMenuItem});
            this.slot3ToolStripMenuItem.Name = "slot3ToolStripMenuItem";
            this.slot3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slot3ToolStripMenuItem.Text = "Slot 3";
            // 
            // saveTreeToSlot3ToolStripMenuItem
            // 
            this.saveTreeToSlot3ToolStripMenuItem.Name = "saveTreeToSlot3ToolStripMenuItem";
            this.saveTreeToSlot3ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.saveTreeToSlot3ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveTreeToSlot3ToolStripMenuItem.Text = "Save Tree To Slot 3";
            this.saveTreeToSlot3ToolStripMenuItem.Click += new System.EventHandler(this.saveTreeToSlot3ToolStripMenuItem_Click);
            // 
            // loadTreeFromSlot3ToolStripMenuItem
            // 
            this.loadTreeFromSlot3ToolStripMenuItem.Name = "loadTreeFromSlot3ToolStripMenuItem";
            this.loadTreeFromSlot3ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.loadTreeFromSlot3ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTreeFromSlot3ToolStripMenuItem.Text = "Load Tree From Slot 3";
            this.loadTreeFromSlot3ToolStripMenuItem.Click += new System.EventHandler(this.loadTreeToSlot3ToolStripMenuItem_Click);
            // 
            // slot4ToolStripMenuItem
            // 
            this.slot4ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot4_Name,
            this.toolStripSeparator8,
            this.saveTreeToSlot5ToolStripMenuItem,
            this.loadTreeFromSlot4ToolStripMenuItem});
            this.slot4ToolStripMenuItem.Name = "slot4ToolStripMenuItem";
            this.slot4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slot4ToolStripMenuItem.Text = "Slot 4";
            // 
            // saveTreeToSlot5ToolStripMenuItem
            // 
            this.saveTreeToSlot5ToolStripMenuItem.Name = "saveTreeToSlot5ToolStripMenuItem";
            this.saveTreeToSlot5ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.saveTreeToSlot5ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveTreeToSlot5ToolStripMenuItem.Text = "Save Tree To Slot 4";
            this.saveTreeToSlot5ToolStripMenuItem.Click += new System.EventHandler(this.saveTreeToSlot5ToolStripMenuItem_Click);
            // 
            // loadTreeFromSlot4ToolStripMenuItem
            // 
            this.loadTreeFromSlot4ToolStripMenuItem.Name = "loadTreeFromSlot4ToolStripMenuItem";
            this.loadTreeFromSlot4ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.loadTreeFromSlot4ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTreeFromSlot4ToolStripMenuItem.Text = "Load Tree From Slot 4";
            this.loadTreeFromSlot4ToolStripMenuItem.Click += new System.EventHandler(this.loadTreeFromSlot5ToolStripMenuItem_Click);
            // 
            // slot5ToolStripMenuItem
            // 
            this.slot5ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slot5_Name,
            this.toolStripSeparator9,
            this.saveTreeToSlot5ToolStripMenuItem1,
            this.loadTreeFromSlot5ToolStripMenuItem});
            this.slot5ToolStripMenuItem.Name = "slot5ToolStripMenuItem";
            this.slot5ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slot5ToolStripMenuItem.Text = "Slot 5";
            // 
            // saveTreeToSlot5ToolStripMenuItem1
            // 
            this.saveTreeToSlot5ToolStripMenuItem1.Name = "saveTreeToSlot5ToolStripMenuItem1";
            this.saveTreeToSlot5ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.saveTreeToSlot5ToolStripMenuItem1.Size = new System.Drawing.Size(220, 22);
            this.saveTreeToSlot5ToolStripMenuItem1.Text = "Save Tree To Slot 5";
            this.saveTreeToSlot5ToolStripMenuItem1.Click += new System.EventHandler(this.saveTreeToSlot5ToolStripMenuItem1_Click);
            // 
            // loadTreeFromSlot5ToolStripMenuItem
            // 
            this.loadTreeFromSlot5ToolStripMenuItem.Name = "loadTreeFromSlot5ToolStripMenuItem";
            this.loadTreeFromSlot5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.loadTreeFromSlot5ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTreeFromSlot5ToolStripMenuItem.Text = "Load Tree From Slot 5";
            this.loadTreeFromSlot5ToolStripMenuItem.Click += new System.EventHandler(this.loadTreeFromSlot5ToolStripMenuItem1_Click);
            // 
            // EndingSim_MenuItem
            // 
            this.EndingSim_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCurrentTreeAsMainSimulationToolStripMenuItem,
            this.toolStripSeparator2,
            this.setSlot1TreeAsMainSimulationToolStripMenuItem,
            this.setSlot2TreeAsMainSimulationToolStripMenuItem,
            this.setSlot3TreeAsMainSimulationToolStripMenuItem,
            this.setSlot4TreeAsMainSimulationToolStripMenuItem,
            this.setSlot5TreeAsMainSimulationToolStripMenuItem,
            this.toolStripSeparator3,
            this.openSimulationLogsToolStripMenuItem,
            this.PlayGame_Button});
            this.EndingSim_MenuItem.Name = "EndingSim_MenuItem";
            this.EndingSim_MenuItem.Size = new System.Drawing.Size(134, 20);
            this.EndingSim_MenuItem.Text = "Ending Tree Simulator";
            // 
            // setCurrentTreeAsMainSimulationToolStripMenuItem
            // 
            this.setCurrentTreeAsMainSimulationToolStripMenuItem.Name = "setCurrentTreeAsMainSimulationToolStripMenuItem";
            this.setCurrentTreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setCurrentTreeAsMainSimulationToolStripMenuItem.Text = "Set Current Tree as Main Simulation";
            this.setCurrentTreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setCurrentTreeAsMainSimulationToolStripMenuItem_Click);
            // 
            // setSlot1TreeAsMainSimulationToolStripMenuItem
            // 
            this.setSlot1TreeAsMainSimulationToolStripMenuItem.Name = "setSlot1TreeAsMainSimulationToolStripMenuItem";
            this.setSlot1TreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setSlot1TreeAsMainSimulationToolStripMenuItem.Text = "Set Slot 1 Tree as Main Simulation";
            this.setSlot1TreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setSlot1TreeAsMainSimulationToolStripMenuItem_Click);
            // 
            // setSlot2TreeAsMainSimulationToolStripMenuItem
            // 
            this.setSlot2TreeAsMainSimulationToolStripMenuItem.Name = "setSlot2TreeAsMainSimulationToolStripMenuItem";
            this.setSlot2TreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setSlot2TreeAsMainSimulationToolStripMenuItem.Text = "Set Slot 2 Tree as Main Simulation";
            this.setSlot2TreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setSlot2TreeAsMainSimulationToolStripMenuItem_Click);
            // 
            // setSlot3TreeAsMainSimulationToolStripMenuItem
            // 
            this.setSlot3TreeAsMainSimulationToolStripMenuItem.Name = "setSlot3TreeAsMainSimulationToolStripMenuItem";
            this.setSlot3TreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setSlot3TreeAsMainSimulationToolStripMenuItem.Text = "Set Slot 3 Tree as Main Simulation";
            this.setSlot3TreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setSlot3TreeAsMainSimulationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(358, 6);
            // 
            // setSlot4TreeAsMainSimulationToolStripMenuItem
            // 
            this.setSlot4TreeAsMainSimulationToolStripMenuItem.Name = "setSlot4TreeAsMainSimulationToolStripMenuItem";
            this.setSlot4TreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setSlot4TreeAsMainSimulationToolStripMenuItem.Text = "Set Slot 4 Tree as Main Simulation";
            this.setSlot4TreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setSlot4TreeAsMToolStripMenuItem_Click);
            // 
            // setSlot5TreeAsMainSimulationToolStripMenuItem
            // 
            this.setSlot5TreeAsMainSimulationToolStripMenuItem.Name = "setSlot5TreeAsMainSimulationToolStripMenuItem";
            this.setSlot5TreeAsMainSimulationToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setSlot5TreeAsMainSimulationToolStripMenuItem.Text = "Set Slot 5 Tree as Main Simulation";
            this.setSlot5TreeAsMainSimulationToolStripMenuItem.Click += new System.EventHandler(this.setSlot5TreeAsMainSimulationToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(358, 6);
            // 
            // openSimulationLogsToolStripMenuItem
            // 
            this.openSimulationLogsToolStripMenuItem.Name = "openSimulationLogsToolStripMenuItem";
            this.openSimulationLogsToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.openSimulationLogsToolStripMenuItem.Text = "Open Simulation Logs";
            this.openSimulationLogsToolStripMenuItem.Click += new System.EventHandler(this.openSimulationLogsToolStripMenuItem_Click);
            // 
            // resetEndingTreeToolStripMenuItem
            // 
            this.resetEndingTreeToolStripMenuItem.Name = "resetEndingTreeToolStripMenuItem";
            this.resetEndingTreeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.resetEndingTreeToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.resetEndingTreeToolStripMenuItem.Text = "Reset Ending Tree To Last Saved";
            this.resetEndingTreeToolStripMenuItem.Click += new System.EventHandler(this.resetEndingTreeToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(309, 6);
            // 
            // Slot1_Name
            // 
            this.Slot1_Name.Enabled = false;
            this.Slot1_Name.Name = "Slot1_Name";
            this.Slot1_Name.Size = new System.Drawing.Size(220, 22);
            this.Slot1_Name.Text = "(none)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(217, 6);
            // 
            // Slot2_Name
            // 
            this.Slot2_Name.Enabled = false;
            this.Slot2_Name.Name = "Slot2_Name";
            this.Slot2_Name.Size = new System.Drawing.Size(220, 22);
            this.Slot2_Name.Text = "(none)";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(217, 6);
            // 
            // Slot3_Name
            // 
            this.Slot3_Name.Enabled = false;
            this.Slot3_Name.Name = "Slot3_Name";
            this.Slot3_Name.Size = new System.Drawing.Size(220, 22);
            this.Slot3_Name.Text = "(none)";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(217, 6);
            // 
            // Slot4_Name
            // 
            this.Slot4_Name.Enabled = false;
            this.Slot4_Name.Name = "Slot4_Name";
            this.Slot4_Name.Size = new System.Drawing.Size(220, 22);
            this.Slot4_Name.Text = "(none)";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(217, 6);
            // 
            // Slot5_Name
            // 
            this.Slot5_Name.Enabled = false;
            this.Slot5_Name.Name = "Slot5_Name";
            this.Slot5_Name.Size = new System.Drawing.Size(220, 22);
            this.Slot5_Name.Text = "(none)";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(217, 6);
            // 
            // PlayGame_Button
            // 
            this.PlayGame_Button.Name = "PlayGame_Button";
            this.PlayGame_Button.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.PlayGame_Button.Size = new System.Drawing.Size(361, 22);
            this.PlayGame_Button.Text = "Open NEEDY STREAMER OVERLOAD";
            this.PlayGame_Button.Click += new System.EventHandler(this.PlayGame_Button_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(309, 6);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(309, 6);
            // 
            // newEndingTreeToolStripMenuItem
            // 
            this.newEndingTreeToolStripMenuItem.Name = "newEndingTreeToolStripMenuItem";
            this.newEndingTreeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newEndingTreeToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.newEndingTreeToolStripMenuItem.Text = "New Ending Tree";
            this.newEndingTreeToolStripMenuItem.Click += new System.EventHandler(this.newEndingTreeToolStripMenuItem_Click);
            // 
            // EndingTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 500);
            this.Controls.Add(this.EndingListView);
            this.Controls.Add(this.CurrentEndingTree_Group);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "EndingTreeForm";
            this.Text = "Main Ending Tree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndingTreeForm_FormClosing);
            this.Load += new System.EventHandler(this.EndingTreeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndingTreeForm_KeyDown);
            this.Leave += new System.EventHandler(this.EndingTreeForm_Leave);
            this.CurrentEndingTree_Group.ResumeLayout(false);
            this.CurrentEndingTree_Group.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openEndingTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecentEndingTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveEndingTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEndingTreeAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTreeAsCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Favorites_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem slot1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slot2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slot3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slot4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slot5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTreeToSlot5ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadTreeFromSlot5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndingSim_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCurrentTreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setSlot1TreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSlot2TreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSlot3TreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSlot4TreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSlot5TreeAsMainSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openSimulationLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetEndingTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem Slot1_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem Slot2_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem Slot3_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem Slot4_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem Slot5_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem File_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayGame_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem newEndingTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    }
}

