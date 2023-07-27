using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class AddEndingBranch : Form
    {
        public EndingBranchData NewEnding;

        private EndingTreeForm mainForm;
        public AddEndingBranch(EndingTreeForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AddEndingBranch_Load(object sender, EventArgs e)
        {
            NewEnding = new(2, NGO.EndingType.Ending_None, new List<TargetActionData>());
            StartingDayNumeric.Value = NewEnding.EndingBranch.StartingDay;
            AddEnding_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(NewEnding.EndingBranch.EndingToGet);
            IsStressfulBreakdown_CheckAdd.Checked = NewEnding.EndingBranch.IsStressfulBressdown;
        }

        private void AddEndingButton_Click(object sender, EventArgs e)
        {
            if (NewEnding.EndingBranch.StartingDay == 1)
            {
                NewEnding.EndingBranch.AllActions.Add(new(1, 2, ngov3.CmdType.None));
            }
            if (mainForm != null)
            {
                if (NewEnding.EndingBranch.StartingDay > 1)
                {
                    var newAction = mainForm.SetStartingAction(NewEnding);
                    if (newAction == null)
                    {
                        MessageBox.Show("Could not create the branch using the current starting day. \nEither the day does not exist in the branch list, or is currently inaccessible based on the previous branches.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    NewEnding.EndingBranch.AllActions.Add(newAction);
                    mainForm.SetStartingDayData(NewEnding);
                }
                mainForm.CurrentEndingTree.EndingsList.Add(NewEnding);
                mainForm.AddEndingToListView(NewEnding);
            }
            this.Close();

        }

        private void AddEnding_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewEnding.EndingBranch.EndingToGet = NSODataManager.EndingsList[AddEnding_Dropdown.SelectedIndex];
        }

        private void StartingDayNumeric_ValueChanged(object sender, EventArgs e)
        {
            NewEnding.EndingBranch.StartingDay = (int)StartingDayNumeric.Value;
        }

        private void IsStressfulBreakdown_CheckAdd_CheckedChanged(object sender, EventArgs e)
        {
            NewEnding.EndingBranch.IsStressfulBressdown = IsStressfulBreakdown_CheckAdd.Checked;
        }
    }
}
