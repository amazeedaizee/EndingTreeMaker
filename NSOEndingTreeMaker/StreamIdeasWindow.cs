using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static NSOEndingTreeMaker.EndingBranchSubData;

namespace NSOEndingTreeMaker
{
    public partial class StreamIdeasWindow : Form
    {
        private EndingBranchEditor branchWindow;
        public List<StreamIdeaObj> IdeaList;
        public StreamIdeasWindow(EndingBranchEditor window)
        {
            branchWindow = window;
            branchWindow.OnBranchChanged += StreamIdeasWindow_Load;
            InitializeComponent();
        }

        public void SetListData()
        {
            for (int i = 0; i < StreamIdeas_ListView.Items.Count; i++)
            {
                if (StreamIdeas_ListView.Items[i].SubItems.Count == 1)
                {
                    StreamIdeas_ListView.Items[i].SubItems.Add("");
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 1"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 10000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("SPonsorships 2"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 30000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 2"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 100000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 3"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 250000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 4"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 500000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 5"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 1000000 Followers or more.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("Internet Angel 6"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 1000000 Followers or more, while Really Stressed. Replaces Internet Angel 5, if that idea has already been found.";
                    continue;
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("3"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 10000 Followers or more.";
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("4"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 100000 Followers or more.";
                }
                if (StreamIdeas_ListView.Items[i].SubItems[0].Text.Contains("5"))
                {
                    StreamIdeas_ListView.Items[i].ToolTipText = "Only unlockable at 250000 Followers or more.";
                }
            }
            for (int i = 0; i < StreamTopic_ListView.Items.Count; i++)
            {
                if (StreamTopic_ListView.Items[i].SubItems.Count == 2)
                {
                    StreamTopic_ListView.Items[i].SubItems.Add("");
                }
                if (StreamTopic_ListView.Items[i].SubItems[0].Text.Contains("6") && StreamTopic_ListView.Items[i].Group.Name == "Internet Angel")
                {
                    StreamTopic_ListView.Items[i].ToolTipText = "Replaces Internet Angel 5, if that idea has already been found.";
                    continue;
                }
                if (StreamTopic_ListView.Items[i].SubItems[0].Text.Contains("3") && StreamTopic_ListView.Items[i].Group.Name != "Internet Angel")
                {
                    StreamTopic_ListView.Items[i].ToolTipText = "Only unlockable at 10000 Followers or more.";
                }
                if (StreamTopic_ListView.Items[i].SubItems[0].Text.Contains("4") && StreamTopic_ListView.Items[i].Group.Name != "Internet Angel")
                {
                    StreamTopic_ListView.Items[i].ToolTipText = "Only unlockable at 100000 Followers or more.";
                }
                if (StreamTopic_ListView.Items[i].SubItems[0].Text.Contains("5") && StreamTopic_ListView.Items[i].Group.Name != "Internet Angel")
                {
                    StreamTopic_ListView.Items[i].ToolTipText = "Only unlockable at 250000 Followers or more.";
                }
            }
            UpdateFoundIdeas();
        }

        public void UpdateFoundIdeas()
        {
            for (int i = 0; i < NSODataManager.StreamListSortedByIdea.Count; i++)
            {
                if (IdeaList.Exists(s => s.Idea == NSODataManager.StreamListSortedByIdea[i]))
                {
                    StreamIdeas_ListView.Items[i].SubItems[1].Text = $"Found on Day {IdeaList.Find(s => s.Idea == NSODataManager.StreamListSortedByIdea[i]).DayIndex}, {NSODataManager.DayPartNames[IdeaList.Find(s => s.Idea == NSODataManager.StreamListSortedByIdea[i]).DayPart]}";
                }
                else { StreamIdeas_ListView.Items[i].SubItems[1].Text = ""; }
            }

            for (int i = 0; i < NSODataManager.StreamListSortedByTopic.Count; i++)
            {
                if (IdeaList.Exists(s => s.Idea == NSODataManager.StreamListSortedByTopic[i]))
                {
                    StreamTopic_ListView.Items[i].SubItems[2].Text = $"Found on Day {IdeaList.Find(s => s.Idea == NSODataManager.StreamListSortedByTopic[i]).DayIndex}, {NSODataManager.DayPartNames[IdeaList.Find(s => s.Idea == NSODataManager.StreamListSortedByTopic[i]).DayPart]}";
                }
                else { StreamTopic_ListView.Items[i].SubItems[2].Text = ""; }
            }

            StreamIdeas_ListView.EndUpdate();
            StreamTopic_ListView.EndUpdate();
        }

        private void StreamIdeasWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            branchWindow.OnBranchChanged -= StreamIdeasWindow_Load;
            if (branchWindow.ideasWindow != null) { branchWindow.ideasWindow = null; }
        }

        private void StreamIdeasWindow_Load(object sender, EventArgs e)
        {
            IdeaList = branchWindow.UnsavedEndingBranchData.StreamIdeaList;
            StreamIdeas_ListView.BeginUpdate();
            StreamTopic_ListView.BeginUpdate();
            SetListData();
        }

        private void Topic_RadioButt_CheckedChanged(object sender, EventArgs e)
        {
            StreamTopic_ListView.Visible = Topic_RadioButt.Checked;
            StreamIdeas_ListView.Visible = !Topic_RadioButt.Checked;
        }
    }
}
