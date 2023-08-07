using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class AddEndingBranch : Form
    {
        public EndingBranchData NewEnding;
        private bool isAdvanced;
        internal int endingIndex;

        public delegate void NewBranchCreated();
        public event NewBranchCreated OnNewBranchCreated;

        private EndingTreeForm mainForm;
        internal EndingBranchEditor branchEditor;
        public AddEndingBranch(EndingTreeForm mainForm, bool isAdvanced = true, int endingIndex = 1)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.isAdvanced = isAdvanced;
            this.endingIndex = endingIndex;
            InsertAtEndingIndex_Numeric.Maximum = mainForm.CurrentEndingTree.EndingsList.Count + 1;
            SetAdvanced();
        }

        private void AddEndingBranch_Load(object sender, EventArgs e)
        {
            NewEnding = new(2, NGO.EndingType.Ending_None, new List<TargetActionData>());
            if (branchEditor == null)
                StartingDayNumeric.Value = NewEnding.EndingBranch.StartingDay;
            AddEnding_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(NewEnding.EndingBranch.EndingToGet);
            IsStressfulBreakdown_CheckAdd.Checked = NewEnding.EndingBranch.IsStressfulBressdown;
        }

        public void CreateNewEndingBranch()
        {
            if (NewEnding.EndingBranch.StartingDay == 1)
            {
                NewEnding.EndingBranch.AllActions.Add(new(1, 2, ngov3.CmdType.None));
            }
            if (mainForm != null)
            {
                NewEnding.EndingBranch.EndingToGet = NSODataManager.EndingsList[AddEnding_Dropdown.SelectedIndex];
                NewEnding.EndingBranch.StartingDay = (int)StartingDayNumeric.Value;
                NewEnding.EndingBranch.IsStressfulBressdown = IsStressfulBreakdown_CheckAdd.Checked;
                if (NewEnding.EndingBranch.StartingDay > 1)
                {
                    var newAction = isAdvanced ? this.mainForm.SetStartingAction(NewEnding, true, endingIndex - 1) : this.mainForm.SetStartingAction(NewEnding);
                    if (newAction.Followers == 0)
                    {
                        var confirm = MessageBox.Show("Chosen starting day does not exist in the branch list, or is currently inaccessible based on the previous branches. (ending is queued to be on a day, etc)\n\nAre you sure you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (confirm == DialogResult.No)
                            return;
                    }
                    if (mainForm.CurrentEndingTree.isDay2Exp && NewEnding.EndingBranch.StartingDay == 2)
                    {
                        newAction.TargetAction.IgnoreDM = true;
                    }
                    NewEnding.EndingBranch.AllActions.Add(newAction);
                    switch (isAdvanced)
                    {
                        case true:
                            mainForm.ResetStartingDayData(NewEnding, endingIndex-1);
                            mainForm.CurrentEndingTree.EndingsList.Insert(endingIndex, NewEnding);
                            mainForm.InsertEndingToListView(NewEnding, endingIndex);
                            break;
                        default:
                            mainForm.SetStartingDayData(NewEnding);
                            mainForm.CurrentEndingTree.EndingsList.Add(NewEnding);
                            mainForm.AddEndingToListView(NewEnding);
                            break;
                    }
                }
                mainForm.SetEndingListViewData();
                mainForm.isBranchEdited = true;
            }
        }
        private void AddEndingButton_Click(object sender, EventArgs e)
        {
            CreateNewEndingBranch();
            OnNewBranchCreated?.Invoke();
            this.Close();

        }

        private void AddEnding_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewEnding != null)
                NewEnding.EndingBranch.EndingToGet = NSODataManager.EndingsList[AddEnding_Dropdown.SelectedIndex];
        }

        private void StartingDayNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (NewEnding != null)
                NewEnding.EndingBranch.StartingDay = (int)StartingDayNumeric.Value;
        }

        private void IsStressfulBreakdown_CheckAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (NewEnding != null)
                NewEnding.EndingBranch.IsStressfulBressdown = IsStressfulBreakdown_CheckAdd.Checked;
        }

        private void AddEndingBranch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (branchEditor == null)
                Dispose();
        }

        private void SetAdvanced()
        {
            if (!isAdvanced)
            {
                isAdvanced = true;
                Size = new System.Drawing.Size(450,230);
            }
            else
            {
                isAdvanced = false;
                Size = new System.Drawing.Size(450, 180);
            }
        }
        private void Advanced_Button_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetAdvanced();
        }

        private void InsertAtEndingIndex_Numeric_ValueChanged(object sender, EventArgs e)
        {
            endingIndex = ((int)InsertAtEndingIndex_Numeric.Value-1);
        }
    }
}
