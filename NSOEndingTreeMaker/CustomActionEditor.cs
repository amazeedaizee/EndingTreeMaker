using ngov3;
using System;
using System.IO;
using System.Threading.Tasks;
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
            action.TargetAction.Stream = AlphaType.none;
            action.TargetAction.IgnoreDM = IgnoreDMCheck.Checked;
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case 0:
                    action.TargetAction.Action = ActionType.Haishin;
                    action.TargetAction.Stream = NSODataManager.StreamTopicList[Action_Dropdown.SelectedIndex];
                    break;
                case 1:
                    action.TargetAction.Action = NSODataManager.HangOutActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 2:
                    action.TargetAction.Action = NSODataManager.SleepActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 3:
                    action.TargetAction.Action = NSODataManager.DrugActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 4:
                    action.TargetAction.Action = NSODataManager.InternetActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 5:
                    ActionType outType = (ActionType)Enum.Parse(typeof(ActionType), NSODataManager.OutsideList[Action_Dropdown.SelectedIndex].ToString());
                    action.TargetAction.Action = outType;
                    break;
                case 6:
                    action.TargetAction.Action = ActionType.Darkness;
                    break;
                default:
                    action.TargetAction.Action = ActionType.None;
                    break;

            }


        }


        private void ChangeActionOptionsByParent()
        {
            var actionOptions = Action_Dropdown.Items;
            actionOptions.Clear();
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    actionOptions.AddRange([

                        "Chat & Chill",
                        "Letsplay",
                        "Nerd Talk",
                        "Conspiracy Theories",
                        "Netlore",
                        "ASMR",
                        "Sexy Stream",
                        "Angel Explains",
                        "KAngel Tries Stuff",
                        "Breakdown Stream",
                        "Sponsorship",
                        "Internet Angel",
                        "Darkness"

                        ]);
                    break;
                case 1:
                    actionOptions.Add("Play Game");
                    actionOptions.Add("Spend Time Together");
                    actionOptions.Add("***");
                    break;
                case 2:
                    actionOptions.Add("Sleep To Dusk");
                    actionOptions.Add("Sleep To Night");
                    actionOptions.Add("Sleep To Tomorrow");
                    break;
                case 3:
                    actionOptions.Add("Prescription (normal dose)");
                    actionOptions.Add("Prescription GO!");
                    actionOptions.Add("OTC (normal dose)");
                    actionOptions.Add("OTC GO!");
                    actionOptions.Add("Sleeping Pills GO!");
                    actionOptions.Add("Magic Grass GO!");
                    actionOptions.Add("Magic Paper GO!");
                    break;
                case 4:
                    actionOptions.Add("Social Media");
                    actionOptions.Add("Vanity Search");
                    actionOptions.Add("Video Streaming");
                    actionOptions.Add("/st/");
                    actionOptions.Add("Dinder");
                    break;
                case 5:
                    actionOptions.Add("Nakano");
                    actionOptions.Add("Harajuku");
                    actionOptions.Add("Akihabara");
                    actionOptions.Add("Shibuya");
                    actionOptions.Add("Ikebukuro");
                    actionOptions.Add("Ueno");
                    actionOptions.Add("Jinbocho");
                    actionOptions.Add("Shimokitazawa");
                    actionOptions.Add("Kichijoji");
                    actionOptions.Add("Gisneyland");
                    actionOptions.Add("Hikarigaoka Park");
                    actionOptions.Add("Asakusa");
                    actionOptions.Add("Shinjuku");
                    actionOptions.Add("Toyosu");
                    actionOptions.Add("Ichigaya");
                    actionOptions.Add("Hospital");
                    break;
                case 6:
                    Action_Dropdown.Enabled = false;
                    Action_Dropdown.SelectedText = "";
                    ImportCmdButton.Enabled = true;
                    break;
                default:
                    Action_Dropdown.Enabled = false;
                    Action_Dropdown.SelectedText = "";
                    ImportCmdButton.Enabled = false;
                    break;

            }
            if (ParentAction_Dropdown.SelectedIndex >= 0 && ParentAction_Dropdown.SelectedIndex < 6)
            {
                Action_Dropdown.Enabled = true;
                Action_Dropdown.SelectedIndex = 0;
                ImportCmdButton.Enabled = true;
            }

        }

        private void CustomActionOK_Click(object sender, System.EventArgs e)
        {
            if (!Properties.Settings.Default.ExperimentMode)
            {
                MessageBox.Show("To do custom actions, you must be in Experiment Mode.", "No Experiments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ActionNameTB.Text))
            {
                MessageBox.Show("Custom action must have a name!", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            SaveAction();
            branchWindow.TargetActionButtonOnClick(sender, e);
        }

        public void SetCustomStatChanges(CommandAction cmd)
        {
            if (cmd == null) return;
            SetCustomStatChanges(cmd.followers, cmd.stress, cmd.affection, cmd.darkness, cmd.impact, cmd.experience, cmd.streamstreak, cmd.prebonus, cmd.gamer, cmd.movie, cmd.communication, cmd.tinfoil, cmd.daypart);
        }
        public void SetCustomStatChanges(int followers, int stress, int affection, int darkness, int impact, int experience, int streamstreak, int prebonus, int gamer, int movie, int communication, int tinfoil, int daypart)
        {
            FollowersNumUD.Value =followers;
            StressNumUD.Value = stress;
            AffectionNumUD.Value = affection;
            DarknessNumUD.Value =darkness;
            ImpactNumUD.Value = impact;
            ExperienceNumUD.Value = experience;
            StreakNumUD.Value = streamstreak;
            AlertBonusCheck.Checked = prebonus > 0;
            GameNumUD.Value = gamer;
            MovieNumUD.Value = movie;
            CommNumUD.Value = communication;
            RabbitHoleNumUD.Value = tinfoil;
            DayPassNumUD.Value = daypart;
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
                IgnoreDMCheck.Checked = action.TargetAction.IgnoreDM;
                Action_Dropdown.Enabled = action.TargetAction.Action != ActionType.Haishin;
                ParentAction_Dropdown.SelectedIndex = NSODataManager.ParentActionIndex(action.TargetAction.Action);
               
                ChangeActionOptionsByParent();
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case 0:
                        Action_Dropdown.SelectedIndex = NSODataManager.StreamTopicList.IndexOf(action.TargetAction.Stream);
                        FollowerLabel.Text = "Followers Multiplier";
                        break;
                    case 1:
                        Action_Dropdown.SelectedIndex = NSODataManager.HangOutActionList.IndexOf(action.TargetAction.Action);
                        FollowerLabel.Text = "Followers Increase";
                        break;
                    case 2:
                        Action_Dropdown.SelectedIndex = NSODataManager.SleepActionList.IndexOf(action.TargetAction.Action);
                        FollowerLabel.Text = "Followers Increase";
                        break;
                    case 3:
                        Action_Dropdown.SelectedIndex = NSODataManager.DrugActionList.IndexOf(action.TargetAction.Action);
                        FollowerLabel.Text = "Followers Increase";
                        break;
                    case 4:
                        Action_Dropdown.SelectedIndex = NSODataManager.InternetActionList.IndexOf(action.TargetAction.Action);
                        if (action.TargetAction.Action == ActionType.InternetPoketter)
                            FollowerLabel.Text = "Followers Multiplier";

                        break;
                    case 5:
                        CmdType outType = (CmdType)Enum.Parse(typeof(CmdType), action.TargetAction.Action.ToString());
                        Action_Dropdown.SelectedIndex = NSODataManager.OutsideList.IndexOf(outType);
                        FollowerLabel.Text = "Followers Increase";
                        break;
                    default:
                        Action_Dropdown.Enabled = false;
                        Action_Dropdown.SelectedText = "";
                        break;

                }
            }
            else
            {
                ParentAction_Dropdown.SelectedIndex = 7;
                Action_Dropdown.Enabled = false;
                Action_Dropdown.SelectedText = "";
                ImportCmdButton.Enabled = false;
            }
        }

        private void label9_Click(object sender, System.EventArgs e)
        {

        }

        private void label15_Click(object sender, System.EventArgs e)
        {

        }

        private void ParentAction_Dropdown_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ChangeActionOptionsByParent();
            if (ParentAction_Dropdown.SelectedIndex == 0 || ParentAction_Dropdown.SelectedIndex == 4)
            {
                FollowerLabel.Text = "Followers Multiplier";
            }
            else
            {
                FollowerLabel.Text = "Followers Increase";
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {
            var act = ActionType.None;
            var topic = AlphaType.none;
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case 0:
                    act = ActionType.Haishin;
                    topic = NSODataManager.StreamTopicList[Action_Dropdown.SelectedIndex];
                    break;
                case 1:
                    act = NSODataManager.HangOutActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 2:
                    act = NSODataManager.SleepActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 3:
                    act = NSODataManager.DrugActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 4:
                    act = NSODataManager.InternetActionList[Action_Dropdown.SelectedIndex];
                    break;
                case 5:
                    ActionType outType = (ActionType)Enum.Parse(typeof(ActionType), NSODataManager.OutsideList[Action_Dropdown.SelectedIndex].ToString());
                    act = outType;
                    break;
                case 6:
                    act = ActionType.Darkness;
                    break;
                default:
                    act = ActionType.None;
                    break;

            }
            ActionImporter actWin = new ActionImporter(this, act, topic);
            actWin.ShowDialog();
            ChangeActionOptionsByParent();
        }

        private void IgnoreDMCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Action_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParentAction_Dropdown.SelectedIndex == 4 && Action_Dropdown.SelectedIndex == 0)
            {
                FollowerLabel.Text = "Followers Multiplier";
            }
            else
            {
                FollowerLabel.Text = "Followers Increase";
            }
        }
    }
}
