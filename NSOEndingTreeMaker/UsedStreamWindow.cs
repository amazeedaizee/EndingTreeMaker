using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static NSOEndingTreeMaker.EndingBranchSubData;

namespace NSOEndingTreeMaker
{
    public partial class UsedStreamWindow : Form
    {
        private EndingBranchEditor branchWindow;
        public List<StreamUsedObj> UsedList;
        public UsedStreamWindow(EndingBranchEditor window)
        {
            branchWindow = window;
            branchWindow.OnBranchChanged += UsedStreamWindow_Load;
            InitializeComponent();
        }

        public void UpdateUsed()
        {
            UsedStream_ListView.Items.Clear();
            for (int i = 0; i < UsedList.Count; i++)
            {
                ListViewItem item = new ListViewItem(NSODataManager.CmdName(UsedList[i].UsedStream));
                item.SubItems.Add($"Day {UsedList[i].DayIndex}");
                UsedStream_ListView.Items.Add(item);
            }
            UsedStream_ListView.EndUpdate();
        }
        private void UsedStreamWindow_Load(object sender, EventArgs e)
        {
            UsedList = branchWindow.UnsavedEndingBranchData.StreamUsedList;
            UsedStream_ListView.BeginUpdate();
            UpdateUsed();
        }

        private void UsedStreamWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            branchWindow.OnBranchChanged -= UsedStreamWindow_Load;
            if (branchWindow.usedWindow != null) { branchWindow.usedWindow = null; }
        }

        private void UsedStream_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
