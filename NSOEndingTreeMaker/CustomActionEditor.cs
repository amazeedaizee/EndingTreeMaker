using ngov3;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class CustomActionEditor : Form
    {
        private EndingBranchEditor branchWindow;
        private int _dayIndex;
        private int _dayPart;
        public TargetActionData action = null;

        public CustomActionEditor(EndingBranchEditor window, int dayIndex = 2, int dayPart = 0, TargetActionData act = null)
        {
            branchWindow = window;
            _dayIndex = dayIndex;
            _dayPart = dayPart;
            if (act != null)
            {
                action = act;
            }

            InitializeComponent();
        }

        public void SaveAction()
        {

            var name = ActionNameTB.Text;
            var command = new CommandAction(
                "Custom",
                ActionNameTB.Text.Trim(),
                (int)FollowersNumUD.Value,
                (int)StressNumUD.Value,
                (int)AffectionNumUD.Value,
                (int)DarknessNumUD.Value,
                (int)ImpactNumUD.Value,
                (int)ExperienceNumUD.Value,
                (int)StreakNumUD.Value,
                AlertBonusCheck.Checked ? 1 : 0,
                (int)GameNumUD.Value,
                (int)MovieNumUD.Value,
                (int)CommNumUD.Value,
                (int)RabbitHoleNumUD.Value,
                (int)DayPassNumUD.Value
                );
            action = new(new(_dayIndex, _dayPart, ActionType.None, branchWindow.IgnoreDMCheck.Checked), name, command);
            action.Command = (CmdType)1000;
            action.TargetAction.Action = FollowsMultiCheck.Checked ? ActionType.Haishin : ActionType.None;
            action.TargetAction.IgnoreDM = branchWindow.IgnoreDMCheck.Checked;


        }

        private void CustomActionOK_Click(object sender, System.EventArgs e)
        {
            if (!Properties.Settings.Default.ExperimentMode)
            {
                MessageBox.Show("To do this action, you must be in Experiment Mode.", "No Experiments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ActionNameTB.Text))
            {
                MessageBox.Show("Action must have a name!", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            SaveAction();
            branchWindow.TargetActionButtonOnClick(sender, e);
        }

        private void CustomActionCancel_Click(object sender, System.EventArgs e)
        {
            branchWindow.customActWindow = null;
            this.Close();
        }

        private void CustomActionEditor_Load(object sender, System.EventArgs e)
        {
            if (action != null)
            {
                ActionNameTB.Text = action.ActionName;
                FollowersNumUD.Value = action.CommandResult.followers;
                StressNumUD.Value = action.CommandResult.stress;
                AffectionNumUD.Value = action.CommandResult.affection;
                DarknessNumUD.Value = action.CommandResult.darkness;
                ImpactNumUD.Value = action.CommandResult.impact;
                ExperienceNumUD.Value = action.CommandResult.experience;
                StreakNumUD.Value = action.CommandResult.streamstreak;
                AlertBonusCheck.Checked = action.CommandResult.prebonus > 0;
                GameNumUD.Value = action.CommandResult.gamer;
                MovieNumUD.Value = action.CommandResult.movie;
                CommNumUD.Value = action.CommandResult.communication;
                RabbitHoleNumUD.Value = action.CommandResult.tinfoil;
                DayPassNumUD.Value = action.CommandResult.daypart;
                FollowsMultiCheck.Checked = action.TargetAction.Action == ActionType.Haishin;
            }
        }
    }
}
