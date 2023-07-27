using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class BranchErrorDetails : Form
    {
        private List<(string TargetAction, string ErrorMsg)> Errors;
        bool ShowingTargetActions;
        public BranchErrorDetails(List<(string TargetAction, string ErrorMsg)> actionList, bool showTargetActions)
        {
            InitializeComponent();
            ErrorIcon.Image = SystemIcons.Error.ToBitmap();
            Errors = actionList;
            ShowingTargetActions = showTargetActions;
        }

        public void OrganizeErrors()
        {
            for (int i = 0; i < Errors.Count; i++)
            {
                ErrorList.Rows.Add();
                ErrorList.Rows[i].Cells[0].Value = ShowingTargetActions ? "Daily Action" : "Ending Branch";
                ErrorList.Rows[i].Cells[1].Value = Errors[i].TargetAction;
                ErrorList.Rows[i].Cells[2].Value = Errors[i].ErrorMsg;
            }
        }

        private void ErrorConfirm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BranchErrorDetails_Load(object sender, EventArgs e)
        {
            OrganizeErrors();
            ErrorList.ClearSelection();
        }

        private void ErrorList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ErrorList_Leave(object sender, EventArgs e)
        {
            ErrorList.ClearSelection();
        }

        private void BranchErrorDetails_Click(object sender, EventArgs e)
        {
            ErrorList.ClearSelection();
        }
    }
}
