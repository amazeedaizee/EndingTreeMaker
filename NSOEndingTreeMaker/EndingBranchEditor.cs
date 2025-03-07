using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static NSOEndingTreeMaker.EndingBranchSubData;

namespace NSOEndingTreeMaker
{
    public partial class EndingBranchEditor : Form
    {
        public EndingTreeForm MainForm;
        public StreamIdeasWindow ideasWindow;
        public UsedStreamWindow usedWindow;
        public EndingBranchData SelectedEndingBranch;
        public int SelectedEndingIndex;
        public EndingBranchData UnsavedEndingBranchData;
        public List<TargetActionData> ActionList = new();

        public delegate void BranchChangedEvent(object sender, EventArgs e);

        public event BranchChangedEvent OnBranchChanged;


        public (int DayIndex, int DayPart, EndingType ending) ExpectedEnding;

        public TargetActionData SelectedAction;
        public TargetActionData NewAction;
        public int NewActionParentIndex = -1;

        public EditHistory EditHistory = new();

        private bool isDeleting;
        private bool isReseting;
        private bool isSaving;
        private bool isChanged;

        private string MilestoneTooltipText;
        private int tipCoordX;
        private int tipCoordY;

        public EndingBranchEditor(EndingBranchData data, EndingTreeForm mainForm)
        {
            InitializeComponent();
            SelectedEndingBranch = data;
            UnsavedEndingBranchData = new EndingBranchData(data);
            ActionList = UnsavedEndingBranchData.EndingBranch.AllActions;
            MainForm = mainForm;
            int dayPartStart = -1;
            if (ActionList.Count == 1)
            {
                dayPartStart = 0;
            }
            NewAction = new TargetActionData(ActionList[ActionList.Count - 1].TargetAction.DayIndex, dayPartStart, CmdType.None);
            SelectedEndingIndex = MainForm.CurrentEndingTree.EndingsList.IndexOf(SelectedEndingBranch);
        }

        internal void AddActionVisualData(TargetActionData action, bool addAlsoToActionList)
        {
            ListViewItem item = new ListViewItem(action.TargetAction.DayIndex.ToString());
            item.SubItems.Add(NSODataManager.DayPartNames[action.TargetAction.DayPart]);
            item.SubItems.Add(action.ActionName);
            item.SubItems.Add(action.TargetAction.IgnoreDM ? "Yes" : "");
            item.SubItems.Add(action.Followers.ToString());
            item.SubItems.Add(action.Stress.ToString());
            item.SubItems.Add(action.Affection.ToString());
            item.SubItems.Add(action.Darkness.ToString());
            item.SubItems.Add(action.StreamStreak.ToString());
            item.SubItems.Add(action.PreAlertBonus ? "Active" : "");
            item.SubItems.Add(action.GamerGirl.ToString());
            item.SubItems.Add(action.Cinephile.ToString());
            item.SubItems.Add(action.Impact.ToString());
            item.SubItems.Add(action.Experience.ToString());
            item.SubItems.Add(action.Communication.ToString());
            item.SubItems.Add(action.RabbitHole.ToString());
            if (!addAlsoToActionList)
            {
                ActionListView.Items.Add(item);
                return;
            }
            InitializeListView(item, action);
        }

        internal void EditActionVisualData(TargetActionData action)
        {
            ListViewItem item = ActionListView.Items[ActionList.IndexOf(action)];
            item.Text = action.TargetAction.DayIndex.ToString();
            item.SubItems[0].Text = action.TargetAction.DayIndex.ToString();
            item.SubItems[1].Text = NSODataManager.DayPartNames[action.TargetAction.DayPart];
            item.SubItems[2].Text = action.ActionName;
            if (action.MilestoneIdea != CmdType.None)
            {
                item.SubItems[1].Text += "*";
                item.ToolTipText = $"Achieved {NSODataManager.CmdName(action.MilestoneIdea)} at the start of this day.";
            }
            else item.ToolTipText = "";
            item.SubItems[3].Text = action.TargetAction.IgnoreDM ? "Yes" : "";
            item.SubItems[4].Text = action.Followers.ToString();
            item.SubItems[5].Text = action.Stress.ToString();
            item.SubItems[6].Text = action.Affection.ToString();
            item.SubItems[7].Text = action.Darkness.ToString();
            item.SubItems[8].Text = action.StreamStreak.ToString();
            item.SubItems[9].Text = action.PreAlertBonus ? "Active" : "";
            item.SubItems[10].Text = action.GamerGirl.ToString();
            item.SubItems[11].Text = action.Cinephile.ToString();
            item.SubItems[12].Text = action.Impact.ToString();
            item.SubItems[13].Text = action.Experience.ToString();
            item.SubItems[14].Text = action.Communication.ToString();
            item.SubItems[15].Text = action.RabbitHole.ToString();
        }

        private void InitializeListView(ListViewItem item, TargetActionData actionData)
        {
            if (actionData.TargetAction.DayIndex == 2 && actionData.TargetAction.DayPart == -1 && ActionList.Exists(a => a.TargetAction.DayIndex == 1) && MainForm.CurrentEndingTree.isDay2Exp)
            {
                ActionList.Insert(1, actionData);
                ActionListView.Items.Insert(1, item);
                return;
            }
            if (actionData.TargetAction.DayIndex == 2 && actionData.TargetAction.DayPart == -1 && MainForm.CurrentEndingTree.isDay2Exp)
            {
                ActionList.Insert(0, actionData);
                ActionListView.Items.Insert(0, item);
                return;
            }
            if (actionData.TargetAction.DayPart == -1 && !ActionList.Exists(a => a.TargetAction.DayIndex == 1))
            {
                InitializeStartingDay();
                return;
            }
            for (int i = 0; i < ActionList.Count; i++)
            {
                if (ActionList[i].TargetAction.DayIndex == 1 || ActionList[i].TargetAction.DayPart == -1) continue;
                if (ActionList.Count == 1)
                {
                    ActionList.Add(actionData);
                    ActionListView.Items.Add(item);
                    return;
                }
                if (actionData.TargetAction.DayIndex == 15 && actionData.TargetAction.DayPart == 3)
                {
                    if (ActionList[i].TargetAction.DayIndex == 15 && ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart >= 3)
                    {
                        ActionList.Insert(i + 1, actionData);
                        ActionListView.Items.Insert(i + 1, item);
                        return;
                    }
                    else continue;
                }
                if (ActionList[i].TargetAction.DayIndex == actionData.TargetAction.DayIndex)
                {
                    if (ActionList[i].TargetAction.DayPart == 0 && actionData.TargetAction.DayPart == 2 && ActionList.Exists(a => a.TargetAction.DayIndex == actionData.TargetAction.DayIndex && a.TargetAction.DayPart == 1))
                    {
                        ActionList.Insert(i + 2, actionData);
                        ActionListView.Items.Insert(i + 2, item);
                        return;
                    }
                    if (ActionList[i].TargetAction.DayPart > actionData.TargetAction.DayPart)
                    {
                        ActionList.Insert(i, actionData);
                        ActionListView.Items.Insert(i, item);
                        return;
                    }
                    if (ActionList[i].TargetAction.DayPart < actionData.TargetAction.DayPart)
                    {
                        ActionList.Insert(i + 1, actionData);
                        ActionListView.Items.Insert(i + 1, item);
                        return;
                    }
                }
                if (ActionList[i].TargetAction.DayIndex > actionData.TargetAction.DayIndex)
                {
                    ActionList.Insert(i, actionData);
                    ActionListView.Items.Insert(i, item);
                    return;
                }
            }
            ActionList.Add(actionData);
            ActionListView.Items.Add(item);

            void InitializeStartingDay()
            {
                ActionList.Insert(0, actionData);
                ActionListView.Items.Insert(0, item);
                UnsavedEndingBranchData.EndingBranch.StartingDay = actionData.TargetAction.DayIndex;
                int index = SelectedEndingIndex;
                var newFirstDay = MainForm.SetStartingAction(UnsavedEndingBranchData, false, index - 1);
                MainForm.ResetStartingDayData(UnsavedEndingBranchData, index - 1);
                ActionList[0] = newFirstDay;
                ActionList[0].TargetAction.DayPart = -1;
                EditActionVisualData(ActionList[0]);
                InitializeBreakdown();
            }
        }

        internal void InitializeBreakdown()
        {
            TargetActionData breakdown = ActionList.FirstOrDefault(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart == 3);
            TargetActionData dayBeforeBreak = InitializeDayBeforeBreakdown();
            if (breakdown == null)
            {
                if (dayBeforeBreak != null)
                {
                    TargetActionData newBreakdown = new TargetActionData(15, 3, "Breakdown", new CommandAction());
                    newBreakdown.CommandResult = new CommandAction();
                    if (dayBeforeBreak.Affection >= 60 && dayBeforeBreak.Darkness >= 60)
                    {
                        newBreakdown.ActionName = "Trauma Dump";
                    }
                    else if (UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown)
                    {
                        newBreakdown.ActionName = "Stressful Breakdown";
                        newBreakdown.CommandResult.stress = 7;
                    }
                    else { newBreakdown.CommandResult.stress = -8; }
                    AddActionVisualData(newBreakdown, true);
                }
                UnsavedEndingBranchData.InitializeActionStats(this);
                ToggleBranchLabelIfUnsaved();
                return;
            }
            if (breakdown != null)
            {
                breakdown.CommandResult = new CommandAction();
                if (dayBeforeBreak == null)
                {
                    int index = ActionList.IndexOf(breakdown);
                    ActionList.RemoveAt(index);
                    ActionListView.Items.RemoveAt(index);
                    UnsavedEndingBranchData.InitializeActionStats(this);
                    ToggleBranchLabelIfUnsaved();
                    return;
                }
                if (dayBeforeBreak.Affection >= 60 && dayBeforeBreak.Darkness >= 60)
                {
                    breakdown.ActionName = "Trauma Dump";
                    breakdown.CommandResult.stress = 0;
                }
                else if (UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown)
                {
                    breakdown.ActionName = "Stressful Breakdown";
                    breakdown.CommandResult.stress = 7;
                }
                else
                {
                    breakdown.ActionName = "Breakdown";
                    breakdown.CommandResult.stress = -8;
                }
                EditActionVisualData(breakdown);
            }
            UnsavedEndingBranchData.InitializeActionStats(this);
            ToggleBranchLabelIfUnsaved();
        }

        private TargetActionData InitializeDayBeforeBreakdown()
        {
            if (!ActionList.Exists(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart != 3 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3 && a.TargetAction.DayPart != 3))
                return null;
            var dayBeforeBreak = ActionList.Find((a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart != 3 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3 && a.TargetAction.DayPart != 3));
            var dayBeforeDayBeforeBreak = ActionList.FindLast(a => (a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart != 3 && a.TargetAction.DayPart < dayBeforeBreak.TargetAction.DayPart) || a.TargetAction.DayIndex < dayBeforeBreak.TargetAction.DayIndex);
            NSOCommandManager.CalculateStats(UnsavedEndingBranchData, dayBeforeDayBeforeBreak, dayBeforeBreak);
            if (dayBeforeBreak.TargetAction.IgnoreDM)
            {
                var ignoredAction = new TargetActionData(dayBeforeBreak);
                ignoredAction.Affection = dayBeforeBreak.Affection;
                ignoredAction.Darkness = dayBeforeBreak.Darkness;
                ignoredAction.Affection += -5;
                dayBeforeBreak = ignoredAction;
            }
            return dayBeforeBreak;
        }
        private void ToggleBranchLabelIfUnsaved()
        {
            bool changed = isChanged || UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown != SelectedEndingBranch.EndingBranch.IsStressfulBressdown || UnsavedEndingBranchData.IgnoreNightEndings != SelectedEndingBranch.IgnoreNightEndings;
            ToolStrip_Branch.Text = changed ? "Branch*" : "Branch";
        }
        internal void SetEventInfo(EndingBranchData branch)
        {

            GalacticRail_Bool.Checked = branch.hasGalacticRail.isEventing;
            switch (GalacticRail_Bool.Checked)
            {
                case true:
                    GalacticRail_Tooltip.SetToolTip(GalacticRail_Bool, $"Only true if Ame recently did Netlore 5, and gained this idea at Midnight after without a fixed Midnight event. (Day 22, etc)\nGalactic Rail is available after the day this flag is achieved. \n\nAchieved this event flag on: Day {branch.hasGalacticRail.DayIndex - 1}");
                    break;
                default:
                    GalacticRail_Tooltip.SetToolTip(GalacticRail_Bool, $"Only true if Ame recently did Netlore 5, and gained this idea at Midnight after without a fixed Midnight event. (Day 22, etc)\nGalactic Rail is available after the day this flag is achieved.");
                    break;
            }
            Stressed_Bool.Checked = branch.isStressed.isEventing;
            switch (Stressed_Bool.Checked)
            {
                case true:
                    StressedInfo_Tooltip.SetToolTip(Stressed_Bool, $"Only true if Ame self-harms on any day, after 2 days where she is at 80+ stress (save for fixed event days). \n\nAchieved this event flag on: Day {branch.isStressed.DayIndex}");
                    break;
                default:
                    StressedInfo_Tooltip.SetToolTip(Stressed_Bool, $"Only true if Ame self-harms on any day, after 2 days where she is at 80+ stress (save for fixed event days).");
                    break;
            }
            ReallyStressed_Bool.Checked = branch.isReallyStressed.isEventing;
            switch (ReallyStressed_Bool.Checked)
            {
                case true:
                    ReallyStressed_ToolTip.SetToolTip(ReallyStressed_Bool, $"Only true if Ame goes crazy on any day, after her stress reaches 100, and if Stressed is true. Stress max increases to 120. \n\nAchieved this event flag on: Day {branch.isReallyStressed.DayIndex}");
                    break;
                default:
                    ReallyStressed_ToolTip.SetToolTip(ReallyStressed_Bool, $"Only true if Ame goes crazy on any day, after her stress reaches 100, and if Stressed is true. Stress max increases to 120.");
                    break;
            }
            Vomited_Bool.Checked = branch.isHorror.isEventing;
            Vomited_ToolTip.SetToolTip(Vomited_Bool, "Only true if at the end on Day 24, Ame has 80+ stress, and her stress maximum (not current stress) is 120. ");
            Trauma_Bool.Checked = branch.isTrauma.isEventing;
            Trauma_Tooltip.SetToolTip(Trauma_Bool, "Only true if Ame trauma dumps on you on Day 15 instead of breaking down.");
            Parents_Bool.Checked = branch.isReallyLove.isEventing;
            Parents_Tooltip.SetToolTip(Parents_Bool, "Only true if Ame visits her parents on Day 24, if at the end of day 23 she has 80+ affection.");
            MV_bool.Checked = branch.isVideo.isEventing;
            MV_Tooltip.SetToolTip(MV_bool, "Only true if Ame goes out to record her MV on Day 27, if she has 500000 or more followers at the end of Day 26.");
            NoMeds_Bool.Checked = branch.NoMeds.isEventing;
            switch (ReallyStressed_Bool.Checked)
            {
                case true:
                    NoMeds_Tooltip.SetToolTip(NoMeds_Bool, $"Only true if Internet Angel 5 has been done previously. \n\nStreamed Internet Angel 5 on: Day {branch.NoMeds.DayIndex}");
                    break;
                default:
                    NoMeds_Tooltip.SetToolTip(NoMeds_Bool, "Only true if Internet Angel 5 has been done previously.");
                    break;
            }
        }

        internal void SetExtraMilestoneGraphic(EndingBranchData branch)
        {

            ExtraMilestones_Label.Text = "🤍 🤍 🤍";
            var ExtraMilestoneTooltipText = "";
            if (branch.is150M.isEventing)
            {
                ExtraMilestones_Label.Text = "🧡 🤍 🤍";
                ExtraMilestoneTooltipText += $"Achieved 150 Million Followers on Day {branch.is150M.DayIndex - 1}!\n";
            }
            if (branch.is300M.isEventing)
            {
                ExtraMilestones_Label.Text = "🧡 🧡 🤍";
                ExtraMilestoneTooltipText += $"Achieved 300 Million Followers on Day {branch.is300M.DayIndex - 1}!\n";
            }
            if (branch.is500M.isEventing)
            {
                ExtraMilestones_Label.Text = "🧡 🧡 🧡";
                ExtraMilestoneTooltipText += $"Achieved 500 Million Followers on Day {branch.is500M.DayIndex - 1}!\n";
            }
            ExtraMilestoneTooltip.SetToolTip(ExtraMilestones_Label, ExtraMilestoneTooltipText);

        }

        internal void SetGuessedEnding(EndingBranchData branch, (int, int, EndingType) expectedEnding)
        {

            ExpectedEnding = branch.ExpectedDayOfEnd;
            int endedDay = ExpectedEnding.DayIndex;
            int endedDayPart = ExpectedEnding.DayPart;
            if (ExpectedEnding.ending == EndingType.Ending_KowaiInternet)
            {
                endedDay = 25;
                endedDayPart = 0;
            }
            else if (ExpectedEnding.ending == EndingType.Ending_Grand ||
                ExpectedEnding.ending == EndingType.Ending_Happy ||
                ExpectedEnding.ending == EndingType.Ending_Normal ||
                ExpectedEnding.ending == EndingType.Ending_Yarisute ||
                ExpectedEnding.ending == EndingType.Ending_Needy ||
                ExpectedEnding.ending == EndingType.Ending_Sucide ||
                ExpectedEnding.ending == EndingType.Ending_Work ||
                ExpectedEnding.ending == EndingType.Ending_Bad)
            {
                endedDay = 30;
                endedDayPart = 0;
            }
            else if (ExpectedEnding.ending == EndingType.Ending_Stressful ||
               ExpectedEnding.ending == EndingType.Ending_Healthy ||
               ExpectedEnding.ending == EndingType.Ending_Sukisuki ||
               ExpectedEnding.ending == EndingType.Ending_Ntr ||
               ExpectedEnding.ending == EndingType.Ending_Meta)
            {

                endedDayPart = 2;
            }
            else if (ExpectedEnding.ending == EndingType.Ending_Jikka ||
              ExpectedEnding.ending == EndingType.Ending_Ideon)
            {
                endedDayPart = 3;
            }
            else if (ExpectedEnding.ending == EndingType.Ending_DarkAngel)
            {
                endedDayPart = 0;
            }
            else endedDayPart -= 1;
            EndingGuesser.Text = ExpectedEnding.ending == EndingType.Ending_None ? "Ending To Expect: None" : $"Ending To Expect: {NSODataManager.EndingNames[ExpectedEnding.ending]} on Day {endedDay}, {NSODataManager.DayPartNames[endedDayPart]}";
        }

        internal void SetActionCounterText(EndingBranchData branch)
        {
            LoveCount.Text = $"Love Counter: {branch.LoveCounter.Count}";
            PaperCount.Text = $"Paper Counter: {branch.PsycheCounter.Count}";
            IgnoredCount.Text = $"Ignored DM's: {branch.IgnoreCounter.Count}";
            LoveCounter_Tooltip.SetToolTip(LoveCount, $"{(branch.LoveCounter.Count > 0 ? "Times you ***:" : "")}" + SetActionCounterListTooltip(branch.LoveCounter));
            PsycheCounter_Tooltip.SetToolTip(PaperCount, $"{(branch.PsycheCounter.Count > 0 ? "Times you took Magic Paper:" : "")}" + SetActionCounterListTooltip(branch.PsycheCounter));
            IgnoreCounter_Tooltip.SetToolTip(IgnoredCount, $"{(branch.IgnoreCounter.Count > 0 ? "Times you ignored a DM:" : "")}" + SetActionCounterListTooltip(branch.IgnoreCounter));
        }

        internal string SetActionCounterListTooltip(List<ActionCounter> countList)
        {
            var countString = "";
            if (countList.Count == 0) return countString;
            for (int i = 0; i < countList.Count; i++)
            {
                countString += $"\nDay {countList[i].DayIndex}, {NSODataManager.DayPartNames[countList[i].DayPart]}";
            }
            return countString;
        }

        private void ChangeActionOptionsByParent()
        {
            var actionOptions = Action_Dropdown.Items;
            if (ParentAction_Dropdown.SelectedIndex != 0)
            {
                Action_Label.Visible = true;
                Action_Dropdown.Visible = true;
                StreamLevelNumeric.Visible = false;
                StreamLevel_Label.Visible = false;
                StreamTopic_Label.Visible = false;
                StreamTopic_Dropdown.Visible = false;
            }
            actionOptions.Clear();
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    Action_Label.Visible = false;
                    Action_Dropdown.Visible = false;
                    StreamLevelNumeric.Visible = true;
                    StreamLevel_Label.Visible = true;
                    StreamTopic_Label.Visible = true;
                    StreamTopic_Dropdown.Visible = true;
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
                    actionOptions.Add("Ame's Parents");
                    actionOptions.Add("Music Video");
                    actionOptions.Add("Galactic Rail");
                    break;
                case 6:
                    actionOptions.Add("Cut Wrists");
                    actionOptions.Add("Go Berserk");
                    break;

            }

        }

        private CmdType ConvertActionChoiceToCmd(TargetActionData pastAction)
        {
            if (ParentAction_Dropdown.SelectedIndex > 0)
                TargetActionButton.Enabled = true;
            if (pastAction == null)
                return CmdType.None;
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    if (StreamTopic_Dropdown.SelectedIndex == -1 || StreamLevelNumeric.Value == -1)
                        break;
                    if (ParentAction_Dropdown.SelectedIndex == 0 && !(StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && StreamLevelNumeric.Value == 6))
                        return (CmdType)Enum.Parse(typeof(CmdType), $"{NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex]}_{StreamLevelNumeric.Value}");
                    else return CmdType.Error;
                case 1:
                    switch (Action_Dropdown.SelectedIndex)
                    {
                        case -1:
                            break;
                        case 0:
                            return CmdType.EntameGame;
                        case 1:
                            if (pastAction.Affection < 40)
                                return CmdType.PlayIchatukuTalk;
                            if (pastAction.Affection >= 80)
                                return CmdType.PlayIchatukuKizu;
                            return CmdType.PlayIchatukuIchatuku;
                        case 2:
                            if (pastAction.TargetAction.DayPart == 0 && (pastAction.TargetAction.Action.ToString().Contains("Overdose") || pastAction.TargetAction.Action == ActionType.OkusuriHappa || pastAction.TargetAction.Action == ActionType.OkusuriPsyche))
                                return CmdType.PlayKimeLove;
                            return CmdType.PlayMakeLove;
                    }
                    break;
                case 2:
                    switch (Action_Dropdown.SelectedIndex)
                    {
                        case -1:
                            break;
                        case 0:
                            if (DayPart_Dropdown.SelectedIndex != 0)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            return CmdType.SleepToTwilight1;
                        case 1:
                            if (DayPart_Dropdown.SelectedIndex == 2)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            if (DayPart_Dropdown.SelectedIndex == 0)
                                return CmdType.SleepToNight2;
                            return CmdType.SleepToNight1;
                        case 2:
                            if (DayPart_Dropdown.SelectedIndex == 0)
                                return CmdType.SleepToTomorrow3;
                            if (DayPart_Dropdown.SelectedIndex == 1)
                                return CmdType.SleepToTomorrow2;
                            return CmdType.SleepToTomorrow1;
                    }
                    break;
                case 3:
                    switch (Action_Dropdown.SelectedIndex)
                    {
                        case -1:
                            break;
                        case 0:
                            return CmdType.OkusuriDaypassModerate;
                        case 1:
                            if (pastAction.Darkness < 20)
                                return CmdType.OkusuriDaypassOverdoseY1;
                            if (pastAction.Darkness >= 20 && pastAction.Darkness < 40)
                                return CmdType.OkusuriDaypassOverdoseY2;
                            if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                                return CmdType.OkusuriDaypassOverdoseY3;
                            return CmdType.OkusuriDaypassOverdoseY4;
                        case 2:
                            if (pastAction.Darkness < 20)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            return CmdType.OkusuriPuronModerate;
                        case 3:
                            if (pastAction.Darkness < 20)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            if (pastAction.Darkness < 40)
                                return CmdType.OkusuriPuronOverdoseY2;
                            if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                                return CmdType.OkusuriPuronOverdoseY3;
                            return CmdType.OkusuriPuronOverdoseY4;
                        case 4:
                            if (pastAction.Darkness < 40)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            if (pastAction.Darkness < 60)
                                return CmdType.OkusuriHiPuronOverdoseY3;
                            return CmdType.OkusuriHiPuronOverdoseY4;
                        case 5:
                            if (pastAction.Darkness < 60)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            if (pastAction.Darkness < 80)
                                return CmdType.OkusuriHappaY4;
                            return CmdType.OkusuriHappaY5;
                        case 6:
                            if (pastAction.Darkness < 80)
                            {
                                TargetActionButton.Enabled = false;
                                return CmdType.None;
                            }
                            return CmdType.OkusuriPsyche;
                    }
                    break;
                case 4:
                    switch (Action_Dropdown.SelectedIndex)
                    {
                        case -1:
                            break;
                        case 0:
                            if (pastAction.Followers < 10000 && pastAction.Darkness < 20)
                                return CmdType.InternetPoketterF0Y12;
                            if ((pastAction.Followers < 10000 && pastAction.Darkness >= 20) || (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness >= 40 && pastAction.Darkness < 60))
                                return CmdType.InternetPoketterF0Y45;
                            if (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness < 40)
                                return CmdType.InternetPoketterF1Y12;
                            if (pastAction.Followers >= 10000 && pastAction.Followers < 100000 && pastAction.Darkness >= 60)
                                return CmdType.InternetPoketterF1Y45;
                            return CmdType.InternetPoketterPoem;
                        case 1:
                            return CmdType.InternetPoketterF0Y3;
                        case 2:
                            return CmdType.InternetYoutube;
                        case 3:
                            if (pastAction.Darkness < 40)
                                return CmdType.Internet2chY12;
                            if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                                return CmdType.Internet2chY3;
                            return CmdType.Internet2chY45;
                        case 4:
                            return CmdType.InternetDeai2;
                    }
                    break;
                case 5:
                    if (Action_Dropdown.SelectedIndex == -1) break;
                    return NSODataManager.OutsideList[Action_Dropdown.SelectedIndex];

                case 6:
                    if (Action_Dropdown.SelectedIndex == -1) break;
                    return NSODataManager.DarknessList[Action_Dropdown.SelectedIndex];
            }
            return CmdType.None;
        }

        private void ConvertActionChoiceNames(TargetActionData pastAction = null)
        {
            if (pastAction == null)
            {
                try
                {
                    pastAction = ActionList[ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value)];
                }
                catch
                {
                    return;
                }
            }
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case -1:
                case 0:
                    return;
                case 1:
                    if (pastAction.Affection < 40)
                        Action_Dropdown.Items[1] = "Spend Time Together";
                    else if (pastAction.Affection >= 80)
                        Action_Dropdown.Items[1] = "Spend Time Together (Pity Party)";
                    else Action_Dropdown.Items[1] = "Spend Time Together (Cuddle)";

                    if (pastAction.TargetAction.DayPart == 0 && NSODataManager.IsOverdoseAction(pastAction))
                        Action_Dropdown.Items[2] = "*** (Have Chem***)";
                    else Action_Dropdown.Items[2] = "***";
                    break;
                case 2:
                    if (DayPart_Dropdown.SelectedIndex == 0)
                        Action_Dropdown.Items[1] = "Sleep To Night (Noon)";
                    else Action_Dropdown.Items[1] = "Sleep To Night (Dusk)";
                    if (DayPart_Dropdown.SelectedIndex == 0)
                        Action_Dropdown.Items[2] = "Sleep To Tomorrow (Noon)";
                    else if (DayPart_Dropdown.SelectedIndex == 1)
                        Action_Dropdown.Items[2] = "Sleep To Tomorrow (Dusk)";
                    else Action_Dropdown.Items[2] = "Sleep To Tomorrow (Night)";
                    break;
                case 3:

                    if (pastAction.Darkness < 20)
                        Action_Dropdown.Items[1] = "Prescription GO! (strongest)";
                    else if (pastAction.Darkness >= 20 && pastAction.Darkness < 40)
                        Action_Dropdown.Items[1] = "Prescription GO! (OTC)";
                    else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                        Action_Dropdown.Items[1] = "Prescription GO! (Embian)";
                    else Action_Dropdown.Items[1] = "Prescription GO! (weakest)";

                    if (pastAction.Darkness < 40)
                        Action_Dropdown.Items[3] = "OTC GO! (strongest)";
                    else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                        Action_Dropdown.Items[3] = "OTC GO! (Embian)";
                    else Action_Dropdown.Items[3] = "OTC GO! (weakest)";

                    if (pastAction.Darkness < 60)
                        Action_Dropdown.Items[4] = "Sleeping Pills GO! (strongest)";
                    else Action_Dropdown.Items[4] = "Sleeping Pills GO! (weakest)";

                    if (pastAction.Darkness < 80)
                        Action_Dropdown.Items[5] = "Magic Grass GO! (strongest)";
                    else Action_Dropdown.Items[5] = "Magic Grass GO! (weakest)";

                    break;
                case 4:

                    if (pastAction.Followers < 10000 && pastAction.Darkness < 20)
                        Action_Dropdown.Items[0] = "Social Media (Daily Tweet)";
                    else if ((pastAction.Followers < 10000 && pastAction.Darkness >= 20) || (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness >= 40 && pastAction.Darkness < 60))
                        Action_Dropdown.Items[0] = "Social Media (Vent On Main)";
                    else if (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness < 40)
                        Action_Dropdown.Items[0] = "Social Media (Make Business Tweet)";
                    else if (pastAction.Followers >= 10000 && pastAction.Followers < 100000 && pastAction.Darkness >= 60)
                        Action_Dropdown.Items[0] = "Social Media (Bash Others)";
                    else Action_Dropdown.Items[0] = "Social Media (Muse)";

                    if (pastAction.Darkness < 40)
                        Action_Dropdown.Items[3] = "/st/ (Search Opinions)";
                    else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                        Action_Dropdown.Items[3] = "/st/ (Go Undercover)";
                    else Action_Dropdown.Items[3] = "/st/ (Stir Shit)";

                    break;
            }
        }

        private void ChangeActionEnableBool(bool isEnabled)
        {
            DayPart_Label.Enabled = isEnabled;
            DayPart_Dropdown.Enabled = isEnabled;
            ParentAction_Label.Enabled = isEnabled;
            ParentAction_Dropdown.Enabled = isEnabled;
            Action_Label.Enabled = isEnabled;
            Action_Dropdown.Enabled = isEnabled;
            StreamLevelNumeric.Enabled = isEnabled;
            StreamLevel_Label.Enabled = isEnabled;
            StreamTopic_Label.Enabled = isEnabled;
            StreamTopic_Dropdown.Enabled = isEnabled;
            IgnoreDMCheck.Enabled = isEnabled;
        }


        private void SetDayMinimum()
        {
            if (UnsavedEndingBranchData.EndingBranch.StartingDay == 1)
            {
                DayIndexNumeric.Minimum = 2;
                return;
            }
            DayIndexNumeric.Minimum = UnsavedEndingBranchData.EndingBranch.StartingDay;
        }

        private void SetStatChangePreview()
        {
            if (ActionListView.SelectedIndices.Count > 1)
            {
                ClearStatPreview();
                PreventEditForCertainActionsWhenMultiSelect();
                return;
            }
            if (SelectedAction != null && (SelectedAction.TargetAction.DayPart == -1 || SelectedAction.TargetAction.DayIndex == 1) && !(MainForm.CurrentEndingTree.isDay2Exp && SelectedAction.TargetAction.DayIndex == 2 && SelectedAction.TargetAction.DayPart == -1 && (SelectedAction.Command != CmdType.None || ParentAction_Dropdown.SelectedIndex != -1)))
            {
                FollowersDiff.Text = SelectedAction.Followers.ToString();
                StressDiff.Text = SelectedAction.Stress.ToString();
                AffectionDiff.Text = SelectedAction.Affection.ToString();
                DarknessDiff.Text = SelectedAction.Darkness.ToString();
                SetHiatusText();
                BonusStatDelta.Items.Clear();
                return;
            }
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case 0:
                    if (StreamTopic_Dropdown.SelectedIndex == -1)
                    {
                        ClearStatPreview();
                        return;
                    }
                    SetStreamStatusPreview();
                    break;
                default:
                    if (Action_Dropdown.SelectedIndex == -1)
                    {
                        ClearStatPreview();
                        return;
                    }
                    break;
            }
            try
            {
                var branch = UnsavedEndingBranchData;
                TargetActionData pastAction = null;
                if (MainForm.CurrentEndingTree.isDay2Exp && SelectedAction.TargetAction.DayIndex == 2 && SelectedAction.TargetAction.DayPart == -1)
                {
                    pastAction = new TargetActionData(1, 2, CmdType.None);
                    pastAction.CommandResult = new();
                }
                else pastAction = ActionList[ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value)];
                CommandAction command = NSOCommandManager.CmdTypeToCommand(ConvertActionChoiceToCmd(pastAction));
                int addStress = command.stress;
                CmdType stream = CmdType.Error;
                if (ParentAction_Dropdown.SelectedIndex == 0 && !(StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && StreamLevelNumeric.Value == 6))
                    stream = (CmdType)Enum.Parse(typeof(CmdType), $"{NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex]}_{StreamLevelNumeric.Value}");
                TargetActionData followerAction = new TargetActionData((int)DayIndexNumeric.Value, DayPart_Dropdown.SelectedIndex, stream);
                if (command.id == "Internet: Social Media" && command.stress > 0)
                {
                    followerAction.TargetAction.Action = ActionType.InternetPoketter;
                    followerAction.CommandResult = command;
                }
                int followerCalc = ParentAction_Dropdown.SelectedIndex == 0 || followerAction.TargetAction.Action == ActionType.InternetPoketter ? NSOCommandManager.CalculateFollowers(pastAction, followerAction) : 0;
                int followerResult = pastAction.Followers + followerCalc;
                if (branch.NoMeds.isEventing && branch.NoMeds.DayIndex <= (int)DayIndexNumeric.Value && ParentAction_Dropdown.SelectedIndex == 0)
                    addStress = 0;
                int stressResult = pastAction.Stress + addStress;
                if (IgnoreDMCheck.Checked) stressResult += 4;
                int affectionResult = pastAction.Affection + command.affection;
                int darknessResult = pastAction.Darkness + command.darkness;
                if (followerResult > 9999999) followerResult = 9999999;
                if (stressResult < 0) stressResult = 0;
                if (affectionResult < 0) affectionResult = 0;
                if (darknessResult < 0) darknessResult = 0;
                if (stressResult > 100)
                {
                    if (stressResult >= 120 && UnsavedEndingBranchData.isReallyStressed.isEventing && DayIndexNumeric.Value >= UnsavedEndingBranchData.isReallyStressed.DayIndex) stressResult = 120;
                    else if (!UnsavedEndingBranchData.isReallyStressed.isEventing || UnsavedEndingBranchData.isReallyStressed.isEventing && DayIndexNumeric.Value < UnsavedEndingBranchData.isReallyStressed.DayIndex) stressResult = 100;
                }
                if (affectionResult > 100)
                {
                    if (affectionResult >= 120 && UnsavedEndingBranchData.isReallyLove.isEventing && DayIndexNumeric.Value >= UnsavedEndingBranchData.isReallyLove.DayIndex) affectionResult = 120;
                    else if (!UnsavedEndingBranchData.isReallyLove.isEventing || UnsavedEndingBranchData.isReallyLove.isEventing && DayIndexNumeric.Value < UnsavedEndingBranchData.isReallyLove.DayIndex) affectionResult = 100;
                }
                if (IgnoreDMCheck.Checked) affectionResult -= 5;
                if (darknessResult > 100) darknessResult = 100;
                SetFollowersTooltip(followerCalc, pastAction, followerAction.CommandResult);
                FollowersDiff.Text = $"{pastAction.Followers} => {followerResult}";
                StressDiff.Text = $"{pastAction.Stress} => {stressResult}";
                AffectionDiff.Text = $"{pastAction.Affection} => {affectionResult}";
                DarknessDiff.Text = $"{pastAction.Darkness} => {darknessResult}";
                SetStreamIdeaPreview(pastAction);
                SetHiatusText();
                SetBonuses(command, pastAction);

            }
            catch
            {
                ClearStatPreview();
            }

            void SetStreamStatusPreview()
            {
                int lastIndex = ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value);
                if (lastIndex < 0)
                {
                    ClearStatPreview();
                    return;
                }
                var pastAction = ActionList[lastIndex];
                if (ParentAction_Dropdown.SelectedIndex != 0) return;
                CmdType stream = CmdType.Error;
                if (ParentAction_Dropdown.SelectedIndex == 0 && !(StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && StreamLevelNumeric.Value == 6))
                    stream = (CmdType)Enum.Parse(typeof(CmdType), $"{NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex]}_{StreamLevelNumeric.Value}");
                var streamIdea = UnsavedEndingBranchData.StreamIdeaList.FirstOrDefault(i => i.Idea == stream);
                var streamUsed = UnsavedEndingBranchData.StreamUsedList.FirstOrDefault(i => i.UsedStream == stream);
                var breakdownThree = UnsavedEndingBranchData.StreamUsedList.FirstOrDefault(u => u.UsedStream == CmdType.Yamihaishin_3);
                if (StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Darkness))
                {
                    if (pastAction.Command != CmdType.DarknessS1 && stream == CmdType.Darkness_1)
                    {
                        StreamIdea_Label.Text = $"Can't stream this.";
                        TargetActionButton.Enabled = false;
                    }
                    else if (pastAction.Command == CmdType.DarknessS1 && stream == CmdType.Darkness_1)
                    {
                        StreamIdea_Label.Text = $"What are you waiting for?";
                        TargetActionButton.Enabled = true;
                    }
                    if (pastAction.Command != CmdType.DarknessS2 && stream == CmdType.Darkness_2)
                    {
                        StreamIdea_Label.Text = $"Can't stream this.";
                        TargetActionButton.Enabled = false;
                    }
                    else if (pastAction.Command == CmdType.DarknessS2 && stream == CmdType.Darkness_2)
                    {
                        StreamIdea_Label.Text = $"What are you waiting for?";
                        TargetActionButton.Enabled = true;
                    }
                    return;
                }
                if (breakdownThree != null && DayIndexNumeric.Value > breakdownThree.DayIndex && DayIndexNumeric.Value <= (breakdownThree.DayIndex + 2))
                {
                    StreamIdea_Label.Text = $"Can't stream now.";
                    TargetActionButton.Enabled = false;
                }
                else if (streamUsed != null && streamUsed.DayIndex < DayIndexNumeric.Value)
                {
                    StreamIdea_Label.Text = $"Note: You streamed this on Day {streamUsed.DayIndex}.";
                    TargetActionButton.Enabled = true;
                }
                else if (streamIdea != null && streamUsed == null)
                {
                    StreamIdea_Label.Text = $"You can stream this.";
                    TargetActionButton.Enabled = true;
                }
                else if (streamIdea == null)
                {
                    StreamIdea_Label.Text = $"Note: You haven't found this idea yet.";
                    TargetActionButton.Enabled = true;
                }
                else
                {
                    StreamIdea_Label.Text = $"";
                    TargetActionButton.Enabled = true;
                }
                if (DayPart_Dropdown.SelectedIndex != 2)
                {
                    StreamIdea_Label.Text = $"You can only stream at night.";
                    TargetActionButton.Enabled = false;
                }
            }
            void SetFollowersTooltip(int followersCalculation, TargetActionData pastAction, CommandAction streamCmd)
            {
                if (followersCalculation == 0)
                {
                    FollowerResults_Tooltip.SetToolTip(FollowersDiff, "");
                    return;
                }
                double followerBase = Math.Min((float)(Math.Log10(pastAction.Followers) * 25f), Math.Floor(pastAction.Followers / 100.0));
                string followerMultiplier = $"\nBase Follower Multiplier: {streamCmd.followers}";
                string streakMultiplier = streamCmd.id == "Stream" ? $"\nStream Streak Multiplier: {pastAction.StreamStreak + 1f}" : "";
                string preAlertMultiplier = pastAction.PreAlertBonus ? $"\nPre-Alert Multiplier: {(double)1.2f}" : "";
                string gameMultiplier = streamCmd.name.Contains("Letsplay") ? $"\nGamer Girl Multiplier: {(double)(pastAction.GamerGirl / 2f + 1f)}" : "";
                string movieMultiplier = streamCmd.name.Contains("Nerd Talk") ? $"\nCinephile Multiplier: {(double)(pastAction.Cinephile / 2f + 1f)}" : "";
                string impactMultiplier = streamCmd.name.Contains("Breakdown Stream") || streamCmd.name.Contains("Darkness ") ? $"\nImpact Multiplier: {(double)(pastAction.Impact / 2f + 1f)}" : "";
                string expMultiplier = streamCmd.name.Contains("ASMR") || streamCmd.name.Contains("Sexy Stream") ? $"\nExperience Multiplier: {(double)(pastAction.Experience / 2f + 1f)}" : "";
                string commMultiplier = streamCmd.id == "Stream" ? $"\nCommunication Multiplier: {(double)(pastAction.Communication / 10f + 1f)}" : "";
                string holeMultiplier = pastAction.RabbitHole > 0 && streamCmd.id == "Stream" ? $"\nRabbit Hole Multiplier: {(double)(1f - (pastAction.RabbitHole / 100f))}" : "";
                string followerDesc = $"Base Number: {followerBase}{followerMultiplier}{streakMultiplier}{preAlertMultiplier}{gameMultiplier}{movieMultiplier}{impactMultiplier}{expMultiplier}{commMultiplier}{holeMultiplier}\n\nNew Followers: {followersCalculation}";
                FollowerResults_Tooltip.SetToolTip(FollowersDiff, followerDesc);
            }

            void SetStreamIdeaPreview(TargetActionData pastAction)
            {
                if (ParentAction_Dropdown.SelectedIndex == 0) return;
                TargetActionButton.Enabled = true;
                var b = UnsavedEndingBranchData;
                var ideaAction = new TargetActionData((int)DayIndexNumeric.Value, DayPart_Dropdown.SelectedIndex, CmdType.None, IgnoreDMCheck.Checked);
                ideaAction.Command = ConvertActionChoiceToCmd(pastAction);
                ideaAction.TargetAction.Action = NSODataManager.CmdToActionConverter(ideaAction.Command);
                var (DayIndex, DayPart, Idea) = NSODataManager.ActionToStreamIdea(pastAction, ideaAction, UnsavedEndingBranchData);
                var mandatoryEvents = new List<CmdType>() { CmdType.DarknessS1, CmdType.DarknessS2, CmdType.OdekakeOdaiba, CmdType.OdekakeZikka };

                if (SelectedAction != null && (SelectedAction.Command != ideaAction.Command && SelectedAction.StreamIdea != Idea && SelectedAction.StreamIdea != CmdType.None && Idea != CmdType.None))
                {
                    StreamIdea_Label.Text = $"Idea: {NSODataManager.CmdName(Idea)}";
                }
                else if (SelectedAction != null && SelectedAction.StreamIdea != CmdType.None && SelectedAction.Command == ideaAction.Command)
                {
                    StreamIdea_Label.Text = $"Idea: {NSODataManager.CmdName(SelectedAction.StreamIdea)}";
                }
                else if (Idea != CmdType.None)
                {
                    StreamIdea_Label.Text = $"Idea: {NSODataManager.CmdName(Idea)}";

                }
                else { StreamIdea_Label.Text = ""; }

                if (ideaAction.Command == CmdType.DarknessS1)
                {
                    if (!b.isStressed.isEventing || !(ideaAction.TargetAction.DayIndex == b.isStressed.DayIndex && ideaAction.TargetAction.DayPart == 0))
                    {
                        StreamIdea_Label.Text = $"Cannot do this action yet.";
                        TargetActionButton.Enabled = false;
                    }
                    else StreamIdea_Label.Text = $"She's stressed.";
                }
                else if (ideaAction.Command == CmdType.DarknessS2)
                {
                    if (!b.isReallyStressed.isEventing || !(ideaAction.TargetAction.DayIndex == b.isReallyStressed.DayIndex && ideaAction.TargetAction.DayPart == 0))
                    {
                        StreamIdea_Label.Text = $"Cannot do this action yet.";
                        TargetActionButton.Enabled = false;
                    }
                    else StreamIdea_Label.Text = $"You've really done it now.";
                }
                else if (ideaAction.Command == CmdType.OdekakeZikka)
                {
                    if (!b.isReallyLove.isEventing || !(ideaAction.TargetAction.DayIndex == b.isReallyLove.DayIndex && ideaAction.TargetAction.DayPart == 0))
                    {
                        StreamIdea_Label.Text = $"Cannot do this action yet.";
                        TargetActionButton.Enabled = false;
                    }
                    else StreamIdea_Label.Text = $"Aww, she really loves you!";
                }
                else if (ideaAction.Command == CmdType.OdekakeOdaiba)
                {
                    if (!b.isVideo.isEventing || !(ideaAction.TargetAction.DayIndex == b.isVideo.DayIndex && ideaAction.TargetAction.DayPart == 0))
                    {
                        StreamIdea_Label.Text = $"Cannot do this action yet.";
                        TargetActionButton.Enabled = false;
                    }
                    else StreamIdea_Label.Text = $"Music video unlocked!";
                }
            }
            void SetBonuses(CommandAction command, TargetActionData pastAction)
            {
                BonusStatDelta.Items.Clear();
                if (command.streamstreak > 0) BonusStatDelta.Items.Add($"Stream Streak: {pastAction.StreamStreak} => {pastAction.StreamStreak + command.streamstreak}");
                if (command.prebonus > 0) BonusStatDelta.Items.Add($"Pre-Alert Bonus: Active");
                if (command.gamer > 0) BonusStatDelta.Items.Add($"Gamer Girl: {pastAction.GamerGirl} => {pastAction.GamerGirl + command.gamer}");
                if (command.movie > 0) BonusStatDelta.Items.Add($"Cinephile: {pastAction.Cinephile} => {pastAction.Cinephile + command.movie}");
                if (command.impact > 0) BonusStatDelta.Items.Add($"Impact: {pastAction.Impact} => {pastAction.Impact + command.impact}");
                if (command.experience > 0) BonusStatDelta.Items.Add($"Experience: {pastAction.Experience} => {pastAction.Experience + command.experience}");
                if (command.communication > 0) BonusStatDelta.Items.Add($"Communication: {pastAction.Communication} => {pastAction.Communication + command.communication}");
                if (command.tinfoil > 0) BonusStatDelta.Items.Add($"Communication: {pastAction.RabbitHole} => {pastAction.RabbitHole + command.tinfoil}");
            }

            void SetHiatusText()
            {
                var breakdownThree = UnsavedEndingBranchData.StreamUsedList.FirstOrDefault(u => u.UsedStream == CmdType.Yamihaishin_3);
                if (breakdownThree != null && DayIndexNumeric.Value > breakdownThree.DayIndex && DayIndexNumeric.Value <= (breakdownThree.DayIndex + 2))
                    Hiatus_Label.Visible = true;
                else Hiatus_Label.Visible = false;
            }

            void ClearStatPreview()
            {
                FollowersDiff.Text = "---";
                StressDiff.Text = "---";
                AffectionDiff.Text = "---";
                DarknessDiff.Text = "---";
                StreamIdea_Label.Text = "";
                Hiatus_Label.Visible = false;
                BonusStatDelta.Items.Clear();
                if (ActionListView.SelectedIndices.Count <= 1)
                    TargetActionButton.Enabled = false;
            }
        }

        private void EndingBranchEditorOnLoad(object sender, EventArgs e)
        {
            ToolStrip_Branch.DropDown = Branch_ContextMenuStrip;
            ToolStrip_Edit.DropDown = Edit_ContextMenuStrip;
            ActionListView.ContextMenuStrip = Edit_ContextMenuStrip;
            ActionListView.Capture = true;
            Action_Label.Visible = true;
            Action_Dropdown.Visible = true;
            StreamLevelNumeric.Visible = false;
            StreamLevel_Label.Visible = false;
            StreamTopic_Label.Visible = false;
            StreamTopic_Dropdown.Visible = false;
            TargetActionButton.Enabled = false;
            SetImportedBaseBranchData();
        }


        private void IgnoreDmCheck_CheckedChanged(object sender, EventArgs e)
        {
            SetStatChangePreview();
        }

        private void TargetActionButtonOnClick(object sender, EventArgs e)
        {
            CmdType command = CmdType.None;
            var selectedActions = ActionListView.SelectedIndices;
            TargetActionData pastAction = null;
            if (ActionListView.Items.Count > 0 && !ActionListView.SelectedIndices.Contains(0) && DayPart_Dropdown.SelectedIndex > -1)
                pastAction = ActionList[ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value)];
            if (DayPart_Dropdown.SelectedIndex == -1 && (selectedActions.Count == 0 || (selectedActions.Count == 1 && ActionListView.SelectedIndices[0] != 0 && !(MainForm.CurrentEndingTree.isDay2Exp && selectedActions.Count == 1 && ActionList[selectedActions[0]].TargetAction.DayIndex == 2 && ActionList[selectedActions[0]].TargetAction.DayPart == -1))))
            {
                MessageBox.Show("Time of Day cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ParentAction_Dropdown.SelectedIndex > 0 && Action_Dropdown.SelectedIndex == -1 && (selectedActions.Count == 0 || (selectedActions.Count > 0 && ActionListView.SelectedIndices[0] != 0)))
            {
                MessageBox.Show("Action cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            command = ConvertActionChoiceToCmd(pastAction);
            if (SelectedAction != null)
            {
                if (selectedActions.Count == 1 && selectedActions[0] == 0 && !(MainForm.CurrentEndingTree.isDay2Exp && SelectedAction.TargetAction.DayIndex == 2 && SelectedAction.TargetAction.DayPart == -1))
                {
                    InitializeSelectedStartDay();
                    return;
                }
                if (selectedActions.Count > 1)
                {
                    ChangeMultipleActions();
                    return;
                }
                if (ActionList.Exists(a => (a.TargetAction.DayIndex == (int)DayIndexNumeric.Value) && (a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex) && a != SelectedAction) && ActionListView.SelectedIndices[0] != 0)
                {
                    MessageBox.Show("This action sames the same day and time of day as another action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ChangeSelectedAction();
                return;
            }
            AddNewAction();

            void AddNewAction()
            {
                if (ActionList.Exists(a => a.TargetAction.DayIndex == (int)DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex))
                {
                    MessageBox.Show("This action sames the same day and time of day as another action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TargetActionData newAction = new TargetActionData((int)DayIndexNumeric.Value, DayPart_Dropdown.SelectedIndex, command, IgnoreDMCheck.Checked);
                EditHistory.undoActions.Add(new() { new ActionHistoryObj(EditType.Add, ActionList.Count, newAction) });
                EditHistory.redoActions.Clear();
                isChanged = true;
                AddActionVisualData(newAction, true);
                InitializeBreakdown();
                ChangeNumeralDropdowns(newAction);
            }

            void ChangeSelectedAction()
            {
                var actionToUndo = new TargetActionData(SelectedAction.TargetAction.DayIndex, SelectedAction.TargetAction.DayPart, SelectedAction.Command, SelectedAction.TargetAction.IgnoreDM);
                SelectedAction.Command = command;
                SelectedAction.TargetAction.DayIndex = (int)DayIndexNumeric.Value;
                SelectedAction.TargetAction.DayPart = DayPart_Dropdown.SelectedIndex;
                SelectedAction.TargetAction.Action = NSODataManager.CmdToActionConverter(command);
                SelectedAction.TargetAction.Stream = AlphaType.none;
                if (ParentAction_Dropdown.SelectedIndex == 0)
                {
                    SelectedAction.TargetAction.Stream = NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex];
                }
                SelectedAction.TargetAction.IgnoreDM = IgnoreDMCheck.Checked;
                SelectedAction.ActionName = NSODataManager.CmdName(command);
                TargetActionData moveSelected = new TargetActionData(SelectedAction.TargetAction.DayIndex, SelectedAction.TargetAction.DayPart, SelectedAction.Command, SelectedAction.TargetAction.IgnoreDM);
                int index = ActionList.IndexOf(SelectedAction);
                if (!(moveSelected.TargetAction.DayIndex == actionToUndo.TargetAction.DayIndex && moveSelected.TargetAction.DayPart == actionToUndo.TargetAction.DayPart && moveSelected.Command == actionToUndo.Command && moveSelected.TargetAction.IgnoreDM == actionToUndo.TargetAction.IgnoreDM))
                {
                    isChanged = true;
                    EditHistory.undoActions.Add(new() { new(EditType.Edit, index, actionToUndo, moveSelected) });
                    EditHistory.redoActions.Clear();
                }
                ActionListView.Items.RemoveAt(index);
                ActionList.RemoveAt(index);
                AddActionVisualData(moveSelected, true);
                InitializeBreakdown();
                ChangeNumeralDropdowns(moveSelected, true);
            }

            void ChangeMultipleActions()
            {
                List<ActionHistoryObj> listForUndoHistory = new();
                List<TargetActionData> selectedDatas = new();
                List<int> selectedIndexes = new List<int>();

                for (int i = 0; i < selectedActions.Count; i++)
                {
                    selectedDatas.Add(ActionList[selectedActions[i]]);
                    selectedIndexes.Add(selectedActions[i]);
                }
                for (var i = 0; i < selectedDatas.Count; i++)
                {
                    if ((selectedDatas[i].TargetAction.DayPart == -1 && !(MainForm.CurrentEndingTree.isDay2Exp && selectedDatas[i].TargetAction.DayIndex == 2 && selectedDatas[i].TargetAction.DayPart == -1)) || selectedDatas[i].TargetAction.DayIndex == 1)
                    {
                        continue;
                    }
                    var newAction = new TargetActionData(selectedDatas[i].TargetAction.DayIndex, selectedDatas[i].TargetAction.DayPart, command, IgnoreDMCheck.Checked);
                    var undoObj = new ActionHistoryObj(EditType.Edit, selectedIndexes[i], ActionList[selectedIndexes[i]], newAction);
                    if (!(newAction.TargetAction.DayIndex == selectedDatas[i].TargetAction.DayIndex && newAction.TargetAction.DayPart == selectedDatas[i].TargetAction.DayPart && newAction.Command == selectedDatas[i].Command && newAction.TargetAction.IgnoreDM == selectedDatas[i].TargetAction.IgnoreDM))
                    {

                        listForUndoHistory.Add(undoObj);
                    }
                    ActionList.RemoveAt(selectedIndexes[i]);
                    ActionListView.Items.RemoveAt(selectedIndexes[i]);
                    AddActionVisualData(newAction, true);
                }

                if (listForUndoHistory.Count > 0)
                {
                    isChanged = true;
                    EditHistory.undoActions.Add(listForUndoHistory);
                    EditHistory.redoActions.Clear();
                }
                InitializeBreakdown();
            }

            void ChangeNumeralDropdowns(TargetActionData action, bool applyDayToNewAction = false)
            {
                int nextDayPart = action.TargetAction.DayPart;
                int nextDay = action.TargetAction.DayIndex;
                nextDayPart += action.CommandResult.daypart > 0 ? action.CommandResult.daypart : 1;
                if (nextDayPart >= 3)
                {
                    if (nextDay >= DayIndexNumeric.Maximum) return;
                    nextDay++;
                    DayPart_Dropdown.SelectedIndex = 0;
                    DayIndexNumeric.Value = nextDay;
                    if (ParentAction_Dropdown.SelectedIndex == 0)
                    {
                        ParentAction_Dropdown.SelectedIndex = -1;
                    }
                    if (applyDayToNewAction)
                    {
                        NewAction.TargetAction.DayPart = 0;
                        NewAction.TargetAction.DayIndex = nextDay;
                    }
                    DisableSubmitIfActionNull();
                    ForceMandatoryEvents();
                    ConvertActionChoiceNames();
                    SelectNextDay(action);
                    return;
                }
                DayPart_Dropdown.SelectedIndex = nextDayPart;
                if (applyDayToNewAction)
                {
                    NewAction.TargetAction.DayPart = nextDayPart;
                    NewAction.TargetAction.DayIndex = nextDay;
                }
                DisableSubmitIfActionNull();
                ForceMandatoryEvents();
                ForceMandatoryNewAction(NewAction);
                ConvertActionChoiceNames();
                SelectNextDay(action);

            }

            void SelectNextDay(TargetActionData action)
            {
                int nextDayPart = action.TargetAction.DayPart;
                int nextDay = action.TargetAction.DayIndex;
                nextDayPart += action.CommandResult.daypart > 0 ? action.CommandResult.daypart : 1;
                if (nextDayPart >= 3)
                {
                    nextDay++;
                    nextDayPart = 0;
                }

                ActionListView.SelectedIndices.Clear();
                var nextAction = ActionList.FindIndex(a => a.TargetAction.DayIndex == nextDay && a.TargetAction.DayPart == nextDayPart);
                if (nextAction > -1)
                {
                    ActionListView.SelectedIndices.Add(nextAction);
                }
            }


            bool InitializeSelectedStartDay()
            {
                if (DayIndexNumeric.Value == 1)
                {
                    TargetActionData dayOneAction = new(1, 2, CmdType.None);
                    ActionList[0] = dayOneAction;
                    UnsavedEndingBranchData.InitializeActionStats(this);
                }
                else
                {
                    if (ActionList.Count > 1 && DayIndexNumeric.Value > ActionList[1].TargetAction.DayIndex)
                    {
                        MessageBox.Show("Can't set the starting day higher than other action days.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    int pastStartingDay = UnsavedEndingBranchData.EndingBranch.StartingDay;
                    int index = SelectedEndingIndex;
                    UnsavedEndingBranchData.EndingBranch.StartingDay = (int)DayIndexNumeric.Value;
                    var newAction = MainForm.SetStartingAction(UnsavedEndingBranchData, false, index - 1);
                    if (newAction.Followers == 0)
                    {
                        var caution = MessageBox.Show("This day either does not exist in the branch list, or is currently inaccessible based on the previous branches. Do you still want to set the starting day to this day?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (caution == DialogResult.No)
                        {
                            UnsavedEndingBranchData.EndingBranch.StartingDay = pastStartingDay;
                            return false;
                        }
                    }
                    MainForm.ResetStartingDayData(UnsavedEndingBranchData, index - 1);
                    if (ActionList[0].TargetAction.DayIndex != newAction.TargetAction.DayIndex)
                    {
                        isChanged = true;
                        EditHistory.undoActions.Add(new() { new ActionHistoryObj(EditType.Edit, 0, ActionList[0], newAction) });
                        EditHistory.redoActions.Clear();
                    }
                    ActionList[0] = newAction;
                    ActionList[selectedActions[0]].TargetAction.DayPart = -1;
                    EditActionVisualData(ActionList[0]);
                    UnsavedEndingBranchData.InitializeActionStats(this);
                    ChangeNumeralDropdowns(ActionList[0], true);
                }
                return true;
            }
        }

        private void ForceMandatoryEvents()
        {
            bool isStartingDay = SelectedAction != null && SelectedAction.TargetAction.DayPart == -1;
            var stress = UnsavedEndingBranchData.isStressed;
            var veryStress = UnsavedEndingBranchData.isReallyStressed;
            var veryLove = UnsavedEndingBranchData.isReallyLove;
            var musicVideo = UnsavedEndingBranchData.isVideo;
            if (ActionListView.SelectedIndices.Count > 1)
                return;
            if (isStartingDay)
                return;
            if (stress.isEventing && DayIndexNumeric.Value == stress.DayIndex)
            {
                var isDarkOneExists = ActionList.Exists(a => a.TargetAction.DayIndex == stress.DayIndex && a.TargetAction.DayPart == 0 && a.Command == CmdType.DarknessS1);
                StreamLevelNumeric.Value = 1;
                if (DayPart_Dropdown.SelectedIndex == 1 || !isDarkOneExists)
                    DayPart_Dropdown.SelectedIndex = 0;
                if (DayPart_Dropdown.SelectedIndex == 0)
                {
                    StreamIdea_Label.Text = $"She's stressed.";
                    MakeActionOrStreamDropdownVisible(false);
                    ParentAction_Dropdown.SelectedIndex = 6;
                    Action_Dropdown.SelectedIndex = 0;
                    IgnoreDMCheck.Enabled = true;
                }
                if (DayPart_Dropdown.SelectedIndex == 2)
                {
                    MakeActionOrStreamDropdownVisible(true);
                    ParentAction_Dropdown.SelectedIndex = 0;
                    StreamLevelNumeric.Value = 1;
                    StreamTopic_Dropdown.SelectedIndex = 12;
                    IgnoreDMCheck.Enabled = false;
                }
                EnableActions(false);
                return;
            }
            if (veryStress.isEventing && DayIndexNumeric.Value == veryStress.DayIndex)
            {
                var isDarkTwoExists = ActionList.Exists(a => a.TargetAction.DayIndex == veryStress.DayIndex && a.TargetAction.DayPart == 0 && a.Command == CmdType.DarknessS2);
                StreamLevelNumeric.Value = 1;
                if (DayPart_Dropdown.SelectedIndex == 1 || !isDarkTwoExists)
                    DayPart_Dropdown.SelectedIndex = 0;
                if (DayPart_Dropdown.SelectedIndex == 0)
                {
                    StreamIdea_Label.Text = $"You really did it now.";
                    MakeActionOrStreamDropdownVisible(false);
                    ParentAction_Dropdown.SelectedIndex = 6;
                    Action_Dropdown.SelectedIndex = 1;
                    IgnoreDMCheck.Enabled = false;
                }
                if (DayPart_Dropdown.SelectedIndex == 2)
                {
                    MakeActionOrStreamDropdownVisible(true);
                    ParentAction_Dropdown.SelectedIndex = 0;
                    StreamLevelNumeric.Value = 2;
                    StreamTopic_Dropdown.SelectedIndex = 12;
                    IgnoreDMCheck.Enabled = false;
                }
                EnableActions(false);
                return;
            }
            if (veryLove.isEventing && DayIndexNumeric.Value == veryLove.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 1)
                    DayPart_Dropdown.SelectedIndex = 0;
                if (DayPart_Dropdown.SelectedIndex == 0)
                {
                    StreamIdea_Label.Text = $"Aww, she really loves you!";
                    MakeActionOrStreamDropdownVisible(false);
                    ParentAction_Dropdown.SelectedIndex = 5;
                    Action_Dropdown.SelectedIndex = 16;
                    IgnoreDMCheck.Enabled = false;
                    EnableActions(false);
                    return;
                }
            }
            if (musicVideo.isEventing && DayIndexNumeric.Value == musicVideo.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 1)
                    DayPart_Dropdown.SelectedIndex = 0;
                if (DayPart_Dropdown.SelectedIndex == 0)
                {
                    StreamIdea_Label.Text = $"Music video unlocked!";
                    MakeActionOrStreamDropdownVisible(false);
                    ParentAction_Dropdown.SelectedIndex = 5;
                    Action_Dropdown.SelectedIndex = 17;
                    IgnoreDMCheck.Enabled = false;
                    EnableActions(false);
                    return;
                }
            }
            EnableActions(true);
            IgnoreDMCheck.Enabled = true;

            void EnableActions(bool toggle)
            {
                DayPart_Dropdown.Enabled = toggle;
                ParentAction_Dropdown.Enabled = toggle;
                Action_Dropdown.Enabled = toggle;
                StreamLevelNumeric.Enabled = toggle;
                StreamTopic_Dropdown.Enabled = toggle;
            }
        }


        private void ForceMandatoryNewAction(TargetActionData action)
        {
            var stress = UnsavedEndingBranchData.isStressed;
            var veryStress = UnsavedEndingBranchData.isReallyStressed;
            var veryLove = UnsavedEndingBranchData.isReallyLove;
            var musicVideo = UnsavedEndingBranchData.isVideo;
            if (stress.isEventing && DayIndexNumeric.Value == stress.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 0 && !IsMandatoryActionExist(stress.DayIndex, 0, CmdType.DarknessS1))
                {
                    action.Command = CmdType.DarknessS1;
                }
                if (DayPart_Dropdown.SelectedIndex == 2 && !IsMandatoryActionExist(stress.DayIndex, 2, CmdType.Darkness_1))
                {
                    action.Command = CmdType.Darkness_1;
                }
            }
            if (veryStress.isEventing && DayIndexNumeric.Value == veryStress.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 0 && !IsMandatoryActionExist(veryStress.DayIndex, 0, CmdType.DarknessS2))
                {
                    action.Command = CmdType.DarknessS2;
                }
                if (DayPart_Dropdown.SelectedIndex == 2 && !IsMandatoryActionExist(veryStress.DayIndex, 2, CmdType.Darkness_2))
                {
                    action.Command = CmdType.Darkness_2;
                }
            }
            if (veryLove.isEventing && DayIndexNumeric.Value == veryLove.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 0 && !IsMandatoryActionExist(veryLove.DayIndex, 0, CmdType.OdekakeZikka))
                {
                    action.Command = CmdType.OdekakeZikka;

                }
            }
            if (musicVideo.isEventing && DayIndexNumeric.Value == musicVideo.DayIndex)
            {
                if (DayPart_Dropdown.SelectedIndex == 0 && !IsMandatoryActionExist(veryLove.DayIndex, 0, CmdType.OdekakeOdaiba))
                {
                    action.Command = CmdType.OdekakeOdaiba;
                    return;
                }
            }
            action.TargetAction.DayIndex = (int)DayIndexNumeric.Value;
            action.TargetAction.DayPart = DayPart_Dropdown.SelectedIndex;

        }

        bool IsMandatoryActionExist(int day, int dayPart, CmdType command)
        {
            return ActionList.Exists(a => a.TargetAction.DayIndex == day && a.TargetAction.DayPart == dayPart && a.Command == command);
        }

        private void EndingToGetOnSelectedIndexChanged(object sender, EventArgs e)
        {
            UnsavedEndingBranchData.EndingBranch.EndingToGet = NSODataManager.EndingsList[EndingToGet_Dropdown.SelectedIndex];
        }

        private void StressfulBreakdown_CheckOnCheckedChanged(object sender, EventArgs e)
        {
            UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown = StressfulBreakdown_Check.Checked;
            InitializeBreakdown();
        }

        private void PreventEditForCertainActionsWhenMultiSelect()
        {
            if (ParentAction_Dropdown.SelectedIndex == 0 || ParentAction_Dropdown.SelectedIndex == 6)
                TargetActionButton.Enabled = false;
            else if (ParentAction_Dropdown.SelectedIndex == 5 && Action_Dropdown.SelectedIndex >= 16)
                TargetActionButton.Enabled = false;
            else { TargetActionButton.Enabled = true; }
        }

        private void ParentAction_DropdownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParentAction_Dropdown.SelectedIndex == -1)
            {
                Action_Dropdown.SelectedIndex = -1;
                TargetActionButton.Enabled = false;
            }
            ChangeActionOptionsByParent();
            ConvertActionChoiceNames();
            if (ParentAction_Dropdown.SelectedIndex == 0)
            {
                SetStreamNumericBasedOnIdea();
            }
            if (SelectedAction == null)
            {
                NewActionParentIndex = ParentAction_Dropdown.SelectedIndex;
            }
            SetStatChangePreview();

        }

        private void ActionListViewOnSelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeActionOptionsByParent();
            TargetActionButton.Enabled = true;
            IgnoreDMCheck.Enabled = true;
            var selectedActions = ActionListView.SelectedIndices;
            if (selectedActions.Count == 1)
            {
                try
                {
                    if (selectedActions[0] >= 0 && SelectedAction != ActionList[selectedActions[0]])
                    {
                        SelectedAction = ActionList[selectedActions[0]];
                        if (MainForm.CurrentEndingTree.isDay2Exp && selectedActions.Count == 1 && ActionList[selectedActions[0]].TargetAction.DayIndex == 2 && ActionList[selectedActions[0]].TargetAction.DayPart == -1)
                        {
                            UpdateEditFieldsWithSelectedAction();
                            DayIndexNumeric.Enabled = false;
                            DayPart_Dropdown.Enabled = false;
                            ConvertActionChoiceNames();
                            return;
                        }
                        if (SelectedAction.TargetAction.DayIndex <= UnsavedEndingBranchData.EndingBranch.StartingDay && (UnsavedEndingBranchData.EndingBranch.StartingDay == 1 || SelectedAction.TargetAction.DayPart == -1))
                        {
                            OnlyDayIndexEditableIfFirstDaySelected();
                            ConvertActionChoiceNames();

                            return;
                        }
                        if (SelectedAction.TargetAction.DayIndex == 15 && SelectedAction.TargetAction.DayPart == 3)
                        {
                            NoEditingBreakdownDay();
                            ConvertActionChoiceNames();

                            return;
                        }
                        UpdateEditFieldsWithSelectedAction();
                        ConvertActionChoiceNames();

                    }
                }
                catch (ArgumentOutOfRangeException) { return; }
                return;
            }
            if (selectedActions.Count > 1)
            {
                OnlyActionEditableIfMultiSelected();
                ConvertActionChoiceNames();

                return;
            }
            UpdateEditFieldsWithNewAction();
            ConvertActionChoiceNames();


            void NoEditingBreakdownDay()
            {
                DayIndexNumeric.Value = SelectedAction.TargetAction.DayIndex;
                DayPart_Dropdown.SelectedIndex = -1;
                ParentAction_Dropdown.SelectedIndex = -1;
                Action_Dropdown.SelectedIndex = -1;
                IgnoreDMCheck.Checked = false;
                DayIndexNumeric.Enabled = false;
                ChangeActionEnableBool(false);
                TargetActionButton.Text = "Save Action";
                TargetActionButton.Enabled = false;
                SetStatChangePreview();
            }


            void OnlyDayIndexEditableIfFirstDaySelected()
            {
                DayIndexNumeric.Enabled = true;
                if (UnsavedEndingBranchData.EndingBranch.StartingDay == 1)
                {

                    DayIndexNumeric.Minimum = 1;
                    DayIndexNumeric.Enabled = false;
                    TargetActionButton.Enabled = false;
                }
                else DayIndexNumeric.Minimum = 2;
                DayIndexNumeric.Value = SelectedAction.TargetAction.DayIndex;
                DayPart_Dropdown.SelectedIndex = -1;
                ParentAction_Dropdown.SelectedIndex = -1;
                Action_Dropdown.SelectedIndex = -1;
                ChangeActionEnableBool(false);
                TargetActionButton.Text = "Save Action";
                SetStatChangePreview();
            }

            void OnlyActionEditableIfMultiSelected()
            {
                if (isDeleting) return;
                if (selectedActions.Contains(0))
                {
                    ChangeActionEnableBool(false);
                    TargetActionButton.Enabled = false;
                    return;
                }
                else
                {
                    ChangeActionEnableBool(true);
                    TargetActionButton.Enabled = true;
                }
                DayIndexNumeric.Enabled = false;

                DayPart_Dropdown.Enabled = false;
                DayPart_Dropdown.SelectedIndex = -1;
                TargetActionButton.Text = "Save Action";
                SetStatChangePreview();
                DisableSubmitIfActionNull();
            }

            void UpdateEditFieldsWithSelectedAction()
            {
                DayIndexNumeric.Enabled = true;
                TargetActionButton.Enabled = true;
                ChangeActionEnableBool(true);
                SetDayMinimum();
                DayIndexNumeric.Value = SelectedAction.TargetAction.DayIndex;
                DayPart_Dropdown.SelectedIndex = SelectedAction.TargetAction.DayPart;
                TargetActionButton.Text = "Save Action";
                ParentAction_Dropdown.SelectedIndex = NSODataManager.ParentActionIndex(SelectedAction.TargetAction.Action);
                if (ParentAction_Dropdown.SelectedIndex != 0)
                {
                    MakeActionOrStreamDropdownVisible(false);

                }
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case 0:
                        MakeActionOrStreamDropdownVisible(true);
                        StreamTopic_Dropdown.SelectedIndex = NSODataManager.StreamTopicList.IndexOf(SelectedAction.TargetAction.Stream);
                        try
                        {
                            if (SelectedAction.Command == CmdType.Error) StreamLevelNumeric.Value = 6;
                            else StreamLevelNumeric.Value = int.Parse(SelectedAction.Command.ToString().Split('_')[1]);
                        }
                        catch { StreamLevelNumeric.Value = 1; }
                        break;
                    case 1:
                        Action_Dropdown.SelectedIndex = NSODataManager.HangOutActionList.IndexOf(SelectedAction.TargetAction.Action);
                        break;
                    case 2:
                        Action_Dropdown.SelectedIndex = NSODataManager.SleepActionList.IndexOf(SelectedAction.TargetAction.Action);
                        break;
                    case 3:
                        Action_Dropdown.SelectedIndex = NSODataManager.DrugActionList.IndexOf(SelectedAction.TargetAction.Action);
                        break;
                    case 4:
                        Action_Dropdown.SelectedIndex = NSODataManager.InternetActionList.IndexOf(SelectedAction.TargetAction.Action);
                        break;
                    case 5:
                        Action_Dropdown.SelectedIndex = NSODataManager.OutsideList.IndexOf(SelectedAction.Command);
                        break;
                    case 6:
                        Action_Dropdown.SelectedIndex = NSODataManager.DarknessList.IndexOf(SelectedAction.Command);
                        break;

                }
                IgnoreDMCheck.Checked = SelectedAction.TargetAction.IgnoreDM;
                DisableSubmitIfActionNull();
                ForceMandatoryEvents();
                SetStatChangePreview();
            }

            void UpdateEditFieldsWithNewAction()
            {
                DayIndexNumeric.Enabled = true;
                ChangeActionEnableBool(true);
                SelectedAction = null;
                SetDayMinimum();
                DayPart_Dropdown.SelectedIndex = NewAction.TargetAction.DayPart;
                DayIndexNumeric.Value = NewAction.TargetAction.DayIndex;
                DisableNewSubmitIfDayExists();
                ParentAction_Dropdown.SelectedIndex = NewActionParentIndex;
                if (ParentAction_Dropdown.SelectedIndex != 0)
                {
                    MakeActionOrStreamDropdownVisible(false);
                }
                TargetActionButton.Text = "Add Action";
                IgnoreDMCheck.Checked = NewAction.TargetAction.IgnoreDM;
                if (NewAction.Command == CmdType.None) return;
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case -1:
                        Action_Dropdown.SelectedIndex = -1;
                        TargetActionButton.Enabled = false;
                        break;
                    case 0:
                        StreamTopic_Dropdown.SelectedIndex = NSODataManager.StreamTopicList.IndexOf(NewAction.TargetAction.Stream);
                        try
                        {
                            if (NewAction.Command == CmdType.Error) StreamLevelNumeric.Value = 6;
                            else StreamLevelNumeric.Value = int.Parse(NewAction.Command.ToString().Split('_')[1]);
                        }
                        catch { StreamLevelNumeric.Value = 1; }
                        MakeActionOrStreamDropdownVisible(true);
                        break;
                    case 1:
                        Action_Dropdown.SelectedIndex = NSODataManager.HangOutActionList.IndexOf(NewAction.TargetAction.Action);
                        break;
                    case 2:
                        Action_Dropdown.SelectedIndex = NSODataManager.SleepActionList.IndexOf(NewAction.TargetAction.Action);
                        break;
                    case 3:
                        Action_Dropdown.SelectedIndex = NSODataManager.DrugActionList.IndexOf(NewAction.TargetAction.Action);
                        break;
                    case 4:
                        Action_Dropdown.SelectedIndex = NSODataManager.InternetActionList.IndexOf(NewAction.TargetAction.Action);
                        break;
                    case 5:
                        Action_Dropdown.SelectedIndex = NSODataManager.OutsideList.IndexOf(NewAction.Command);
                        break;
                    case 6:
                        Action_Dropdown.SelectedIndex = NSODataManager.DarknessList.IndexOf(NewAction.Command);
                        break;
                }
                DisableSubmitIfActionNull();
                ForceMandatoryEvents();
                SetStatChangePreview();
            }

        }

        private void DisableSubmitIfActionNull()
        {
            if (ParentAction_Dropdown.SelectedIndex == -1)
                TargetActionButton.Enabled = false;
            else if (Action_Dropdown.SelectedIndex == -1 && ParentAction_Dropdown.SelectedIndex > 0)
                TargetActionButton.Enabled = false;
            else TargetActionButton.Enabled = true;
        }
        private void MakeActionOrStreamDropdownVisible(bool enableStream)
        {
            switch (enableStream)
            {
                case true:
                    Action_Dropdown.SelectedIndex = -1;
                    Action_Label.Visible = false;
                    Action_Dropdown.Visible = false;
                    StreamLevelNumeric.Visible = true;
                    StreamLevel_Label.Visible = true;
                    StreamTopic_Label.Visible = true;
                    StreamTopic_Dropdown.Visible = true;
                    break;
                default:
                    StreamTopic_Dropdown.SelectedIndex = -1;
                    StreamLevelNumeric.Value = StreamLevelNumeric.Minimum;
                    Action_Label.Visible = true;
                    Action_Dropdown.Visible = true;
                    StreamLevelNumeric.Visible = false;
                    StreamLevel_Label.Visible = false;
                    StreamTopic_Label.Visible = false;
                    StreamTopic_Dropdown.Visible = false;
                    break;
            }
        }

        private void DisableNewSubmitIfDayExists()
        {
            if (ActionList.Exists(a => a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex) && DayPart_Dropdown.SelectedIndex != -1 && ActionListView.SelectedIndices.Count == 0 && ActionListView.Items.Count > 0 && SelectedAction == null)
            {
                TargetActionButton.Enabled = false;
            }
            else TargetActionButton.Enabled = true;
        }
        private void DayIndexNumericOnValueChanged(object sender, EventArgs e)
        {
            if (isReseting) return;
            DisableNewSubmitIfDayExists();
            ForceMandatoryEvents();
            SetStatChangePreview();

        }
        private void DayPart_DropdownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReseting) return;
            DisableNewSubmitIfDayExists();
            SetStatChangePreview();
            ConvertActionChoiceNames();
        }

        private void SaveEndingBranchButtonOnClick(object sender, EventArgs e)
        {
            SaveEndingBranch();

        }

        private bool SaveEndingBranch(bool closeAfter = true)
        {
            UnsavedEndingBranchData.EndingBranch.AllActions = ActionList;
            List<(string, string, string)> errorList = new();
            errorList.AddRange(UnsavedEndingBranchData.ValidateBranch(""));
            int index = SelectedEndingIndex;
            var branchConflicts = MainForm.ValidateFutureBranchStarts(index, UnsavedEndingBranchData);
            if (branchConflicts.Item3 != "") errorList.Add(new("", "", branchConflicts.Item3));
            if (MainForm.CurrentEndingTree.isDay2Exp && ActionList.Exists(a => a.TargetAction.DayIndex == 2 && a.TargetAction.DayPart == -1 && a.Command == CmdType.None))
            {
                errorList.Add(new("", "", "Day 2 Extra Action must have an action if Day 2 Extra Action is enabled."));
            }
            if (errorList.Count > 0)
            {
                BranchErrorDetails errorWindow = new(errorList, true);
                if (errorWindow.ShowDialog() == DialogResult.Yes)
                {
                    ConfirmSaveEndingBranch(closeAfter);
                    return true;
                }
                return false;
            }
            if (branchConflicts.Item2 && isChanged)
            {
                var msgHasFutureBranchStartDays = MessageBox.Show($"Some future branches rely on this branch for stats, stream ideas, etc. By changing this branch, some of those future branches might become broken.\n\nAre you sure you want to proceed?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msgHasFutureBranchStartDays == DialogResult.No)
                    return false;
            }
            (int, int, EndingType) checkEnding = UnsavedEndingBranchData.ExpectedDayOfEnd;
            switch (checkEnding.Item3)
            {
                case EndingType.Ending_None:
                    if (UnsavedEndingBranchData.EndingBranch.EndingToGet == EndingType.Ending_None) break;
                    var msgNoEnding = MessageBox.Show($"No valid ending has been found. \nAre you sure you want to proceed?", "No ending found!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (msgNoEnding == DialogResult.Yes)
                    {
                        UnsavedEndingBranchData.EndingBranch.EndingToGet = EndingType.Ending_None;
                        break;
                    }
                    return false;
                default:
                    if (checkEnding.Item3 != UnsavedEndingBranchData.EndingBranch.EndingToGet)
                    {
                        var msgTitle = "Different ending found!";
                        var msgDesc = $"Selected ending in this branch is \"{NSODataManager.EndingNames[UnsavedEndingBranchData.EndingBranch.EndingToGet]}\". \n\nHowever, a different ending is projected to happen instead: \n\"{NSODataManager.EndingNames[checkEnding.Item3]}\". \n\nDo you want to change to this ending?";
                        if (UnsavedEndingBranchData.EndingBranch.EndingToGet == EndingType.Ending_None)
                        {
                            msgTitle = "Ending found!";
                            msgDesc = $"No ending in this branch is selected. \n\nHowever, an ending is currently projected to happen: \n\"{NSODataManager.EndingNames[checkEnding.Item3]}\". \n\nDo you want to change to this ending?";
                        }
                        var msgDiffEnding = MessageBox.Show(msgDesc, msgTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        switch (msgDiffEnding)
                        {
                            case DialogResult.Yes:
                                UnsavedEndingBranchData.EndingBranch.EndingToGet = checkEnding.Item3;
                                EndingToGet_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(UnsavedEndingBranchData.EndingBranch.EndingToGet);
                                break;
                            case DialogResult.No: break;
                            default: return false;
                        }
                    }
                    break;
            }
            ConfirmSaveEndingBranch(closeAfter);
            return true;
        }

        private void ConfirmSaveEndingBranch(bool closeAfter = true)
        {
            isSaving = true;
            int index = SelectedEndingIndex;
            UnsavedEndingBranchData.EndingBranch.AllActions = ActionList;
            MainForm.CurrentEndingTree.EndingsList[index] = new EndingBranchData(UnsavedEndingBranchData);
            SelectedEndingBranch = MainForm.CurrentEndingTree.EndingsList[index];
            bool changed = isChanged || UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown != SelectedEndingBranch.EndingBranch.IsStressfulBressdown || UnsavedEndingBranchData.IgnoreNightEndings != SelectedEndingBranch.IgnoreNightEndings;
            if (changed && !MainForm.isBranchEdited) MainForm.isBranchEdited = true;
            isChanged = false;
            MainForm.SetEndingListViewData();
            ToggleBranchLabelIfUnsaved();
            if (closeAfter)
            {
                MainForm.SelectedEnding = null;
                Close();
            }
            isSaving = false;
        }

        private bool MsgIfBranchUnsaved(bool closeAfter = true)
        {

            var confirm = MessageBox.Show($"You have unsaved changes in this ending branch. \nDo you want to save this branch?.", "Unsaved changes detected!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                return SaveEndingBranch(closeAfter);
            }
            return confirm != DialogResult.Cancel;
        }

        private void ResetEndingBranch(bool showMsg = true)
        {
            if (showMsg)
            {
                var confirm = MessageBox.Show($"Are you sure you want to reset this ending branch? \nThis will reset it back to before you edited it.\nThis action can't be undone.", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No) return;
            }
            isReseting = true;
            EditHistory.undoActions.Clear();
            EditHistory.redoActions.Clear();
            ActionListView.Items.Clear();
            ActionList.Clear();
            UnsavedEndingBranchData = new EndingBranchData(SelectedEndingBranch);
            ActionList = UnsavedEndingBranchData.EndingBranch.AllActions;
            DayIndexNumeric.Minimum = UnsavedEndingBranchData.EndingBranch.StartingDay;
            SetImportedBaseBranchData();
            isReseting = false;
        }

        private void SetImportedBaseBranchData()
        {
            SetDayMinimum();
            int listCount = ActionList.Count;
            NewAction = new TargetActionData(ActionList[listCount - 1].TargetAction.DayIndex, -1, CmdType.None);
            for (int i = 0; i < listCount; i++)
            {
                AddActionVisualData(ActionList[i], false);
                if (i == listCount - 1)
                {
                    InitializeBreakdown();
                }
            }
            if (ActionList.Count == 1) { DayPart_Dropdown.SelectedIndex = 0; }
            if (ActionList[ActionList.Count - 1].TargetAction.DayIndex == 1)
                DayIndexNumeric.Value = 2;
            else DayIndexNumeric.Value = ActionList[ActionList.Count - 1].TargetAction.DayIndex;
            ForceMandatoryEvents();
            ForceMandatoryNewAction(NewAction);
            EndingToGet_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(UnsavedEndingBranchData.EndingBranch.EndingToGet);
            StressfulBreakdown_Check.Checked = UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown;
            IgnoreNightEnding_Label.Checked = UnsavedEndingBranchData.IgnoreNightEndings;
            isChanged = false;
        }
        private void StreamIdeasButtonOnClick(object sender, EventArgs e)
        {
            if (ideasWindow == null)
            {
                var ideaListView = new StreamIdeasWindow(this);
                ideasWindow = ideaListView;
                ideaListView.Show();
                return;
            }
            ideasWindow.BringToFront();

        }

        private void UsedStream_ButtonOnClick(object sender, EventArgs e)
        {
            if (usedWindow == null)
            {
                var ideaListView = new UsedStreamWindow(this);
                usedWindow = ideaListView;
                ideaListView.Show();
                return;
            }
            usedWindow.BringToFront();
        }

        private void Action_DropdownOnSelectedValueChanged(object sender, EventArgs e)
        {
            if (Action_Dropdown.SelectedIndex == -1)
                TargetActionButton.Enabled = false;
            TargetActionData pastAction = null;
            if (ActionListView.Items.Count > 0 && !ActionListView.SelectedIndices.Contains(0) && DayPart_Dropdown.SelectedIndex > -1)
                pastAction = ActionList[ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value)];
            if (SelectedAction == null)
            {
                NewAction.Command = ConvertActionChoiceToCmd(pastAction);
                NewAction.TargetAction.Action = NSODataManager.CmdToActionConverter(NewAction.Command);
            }
            DisableNewSubmitIfDayExists();
            SetStatChangePreview();
        }

        private void StreamTopic_DropdownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            int index = StreamTopic_Dropdown.SelectedIndex;
            if (index == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) || index == NSODataManager.StreamTopicList.IndexOf(AlphaType.Angel))
            {
                StreamLevelNumeric.Maximum = 6;
            }
            else
            {
                if (StreamLevelNumeric.Value == 6) { StreamLevelNumeric.Value = 5; }
                StreamLevelNumeric.Maximum = 5;
            }
            if (index == -1)
            {
                SetStatChangePreview();
                return;
            }
            SetStreamNumericBasedOnIdea();
            if (SelectedAction == null)
            {
                AlphaType streamTopic = NSODataManager.StreamTopicList[index];
                int streamLevel = (int)StreamLevelNumeric.Value;
                if (index == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && streamLevel == 6)
                {
                    NewAction.Command = CmdType.Error;
                }
                else { NewAction.Command = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevel}"); }
            }
            SetStatChangePreview();
        }

        void SetStreamNumericBasedOnIdea()
        {
            int index = StreamTopic_Dropdown.SelectedIndex;
            if (index == -1 || StreamLevelNumeric.Value == -1)
                return;
            var ideas = UnsavedEndingBranchData.StreamIdeaList;
            AlphaType streamTopic = NSODataManager.StreamTopicList[index];
            StreamIdeaObj idea = ideas.LastOrDefault(i => i.Idea.ToString().Contains(streamTopic.ToString()) && ((i.DayIndex == DayIndexNumeric.Value && i.DayPart < DayPart_Dropdown.SelectedIndex) || i.DayIndex < DayIndexNumeric.Value));
            if (streamTopic == AlphaType.Imbouron && ideas.Exists(i => i.Idea == CmdType.Error && ((i.DayIndex == DayIndexNumeric.Value && i.DayPart < DayPart_Dropdown.SelectedIndex) || i.DayIndex < DayIndexNumeric.Value)))
            {
                StreamLevelNumeric.Value = 6;
            }
            else if (idea != null)
            {
                var streamLevel = int.Parse(idea.Idea.ToString().Split('_')[1]);
                StreamLevelNumeric.Value = streamLevel;
            }
        }
        private void StreamLevelNumericOnValueChanged(object sender, EventArgs e)
        {
            int index = StreamTopic_Dropdown.SelectedIndex;
            if (SelectedAction == null)
            {
                if (index == -1)
                {
                    SetStatChangePreview();
                    return;
                }
                AlphaType streamTopic = NSODataManager.StreamTopicList[index];
                int streamLevel = (int)StreamLevelNumeric.Value;
                if (streamTopic == AlphaType.Imbouron && streamLevel == 6)
                {
                    NewAction.Command = CmdType.Error;
                }
                else { NewAction.Command = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevel}"); }
            }
            SetStatChangePreview();
        }

        private void EndingBranchEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool changed = isChanged || UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown != SelectedEndingBranch.EndingBranch.IsStressfulBressdown || UnsavedEndingBranchData.IgnoreNightEndings != SelectedEndingBranch.IgnoreNightEndings;
            if (changed && !isSaving)
            {
                var confirm = MsgIfBranchUnsaved();
                if (!confirm)
                {
                    e.Cancel = true;
                    return;
                }
            }
            MainForm.SetEndingListViewData(true);
            ideasWindow?.Close();
            usedWindow?.Close();
            Dispose();
        }

        private List<TargetActionData> CreateListCopy()
        {
            List<TargetActionData> copiedActions = new List<TargetActionData>();
            if (ActionListView.SelectedIndices.Count == 0) return null;
            for (int i = 0; i < ActionListView.SelectedIndices.Count; i++)
            {
                TargetActionData copiedAction = ActionList[ActionListView.SelectedIndices[i]];
                if (copiedAction.TargetAction.DayPart == -1 && !(MainForm.CurrentEndingTree.isDay2Exp && ActionList[i].TargetAction.DayIndex == 2 && ActionList[i].TargetAction.DayPart == -1) || copiedAction.TargetAction.DayPart == 3 || copiedAction.TargetAction.DayIndex == 1) continue;
                copiedActions.Add(new TargetActionData(copiedAction.TargetAction.DayIndex, copiedAction.TargetAction.DayPart, copiedAction.Command, copiedAction.TargetAction.IgnoreDM));
            }
            return copiedActions;
        }

        private void ActionListViewOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ActionListView.SelectedIndices.Count > 0)
            {
                DeleteSelectedActions();
            }
        }
        void CopyActions()
        {
            if (ActionListView.SelectedIndices.Count == 0) return;
            var clonedList = CreateListCopy().ToArray();
            Clipboard.SetDataObject(clonedList);
        }
        void CutActions()
        {
            CopyActions();
            DeleteSelectedActions();
        }
        void PasteCopiedActions()
        {
            bool hasPasteLowerThanStart = false;
            object getCopiedData = Clipboard.GetDataObject().GetData(typeof(TargetActionData[]));
            try
            {
                List<ActionHistoryObj> listForUndoHistory = new();

                TargetActionData[] pasteList = getCopiedData as TargetActionData[];
                for (int i = 0; i < pasteList.Length; i++)
                {
                    if (pasteList[i].TargetAction.DayIndex < UnsavedEndingBranchData.EndingBranch.StartingDay)
                    {
                        hasPasteLowerThanStart = true;
                        continue;
                    }

                    if (!MainForm.CurrentEndingTree.isDay2Exp && ActionList[i].TargetAction.DayIndex == 2 && ActionList[i].TargetAction.DayPart == -1) continue;
                    if (ActionList.Exists(a => a.TargetAction.DayPart == pasteList[i].TargetAction.DayPart && a.TargetAction.DayIndex == pasteList[i].TargetAction.DayIndex))
                    {
                        TargetActionData existingAction = ActionList.Find(a => a.TargetAction.DayPart == pasteList[i].TargetAction.DayPart && a.TargetAction.DayIndex == pasteList[i].TargetAction.DayIndex);
                        int actionIndex = ActionList.IndexOf(existingAction);
                        if (!(existingAction.Command == pasteList[i].Command && existingAction.TargetAction.IgnoreDM == pasteList[i].TargetAction.IgnoreDM))
                        {
                            var undoObj = new ActionHistoryObj(EditType.Edit, actionIndex, existingAction, pasteList[i]);
                            listForUndoHistory.Add(undoObj);
                        }
                        ActionListView.Items.RemoveAt(actionIndex);
                        ActionList.RemoveAt(actionIndex);
                        AddActionVisualData(pasteList[i], true);
                        continue;
                    }
                    var undoNewObj = new ActionHistoryObj(EditType.Add, ActionList.Count, pasteList[i]);
                    listForUndoHistory.Add(undoNewObj);
                    AddActionVisualData(pasteList[i], true);
                }
                if (hasPasteLowerThanStart && listForUndoHistory.Count == 0)
                {
                    MessageBox.Show("Could not post anything as the attempted pasted actions have their days earlier than the Starting Day of this branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (listForUndoHistory.Count > 0)
                {
                    isChanged = true;
                    EditHistory.undoActions.Add(listForUndoHistory);
                    EditHistory.redoActions.Clear();
                }

                InitializeBreakdown();
            }
            catch { Console.WriteLine("Not a valid data object."); }
        }
        void PasteAsNewActions()
        {
            object getCopiedData = Clipboard.GetDataObject().GetData(typeof(TargetActionData[]));
            try
            {
                List<ActionHistoryObj> listForUndoHistory = new();

                int latestDay = ActionList[ActionList.Count - 1].TargetAction.DayIndex;
                int latestDayPart = ActionList[ActionList.Count - 1].TargetAction.DayPart;
                TargetActionData[] pasteList = getCopiedData as TargetActionData[];
                for (int i = 0; i < pasteList.Length; i++)
                {

                    if (i == 0) latestDayPart += ActionList[ActionList.Count - 1].CommandResult.daypart == 0 ? 1 : ActionList[ActionList.Count - 1].CommandResult.daypart;
                    else latestDayPart += pasteList[i - 1].CommandResult.daypart;
                    if (latestDayPart >= 3)
                    {
                        latestDayPart = 0;
                        latestDay += 1;
                    }
                    if (latestDay >= 30)
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Could not paste any new actions: \n Number of days would be 30 or more.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    }
                    TargetActionData targetActionData = new TargetActionData(latestDay, latestDayPart, pasteList[i].Command, pasteList[i].TargetAction.IgnoreDM);
                    var undoObj = new ActionHistoryObj(EditType.Add, ActionList.Count, targetActionData);
                    listForUndoHistory.Add(undoObj);
                    AddActionVisualData(targetActionData, true);
                }
                if (listForUndoHistory.Count > 0)
                {
                    isChanged = true;
                    EditHistory.undoActions.Add(listForUndoHistory);
                    EditHistory.redoActions.Clear();
                }

                InitializeBreakdown();
            }
            catch { Console.WriteLine("Not a valid data object."); }
            return;
        }

        void RedoActionEdit()
        {
            if (EditHistory.redoActions.Count == 0) return;
            var redoActions = EditHistory.redoActions[EditHistory.redoActions.Count - 1];
            List<ActionHistoryObj> undoActions = new();
            UseEditHistory(redoActions, undoActions);
            EditHistory.undoActions.Add(undoActions);
            EditHistory.redoActions.Remove(redoActions);
            if (EditHistory.undoActions.Count > 0) isChanged = true;
            InitializeBreakdown();
        }
        void UndoActionEdit()
        {
            if (EditHistory.undoActions.Count == 0) return;
            var undoActions = EditHistory.undoActions[EditHistory.undoActions.Count - 1];
            List<ActionHistoryObj> redoActions = new();
            UseEditHistory(undoActions, redoActions);
            EditHistory.redoActions.Add(redoActions);
            EditHistory.undoActions.Remove(undoActions);
            if (EditHistory.undoActions.Count == 0) isChanged = false;
            InitializeBreakdown();
            return;
        }

        void DeleteSelectedActions()
        {
            isDeleting = true;
            List<ActionHistoryObj> listForUndoHistory = new();
            for (int i = ActionListView.SelectedIndices.Count - 1; i >= 0; i--)
            {
                i = ActionListView.SelectedIndices.Count - 1;
                TargetActionData cutAction = ActionList[ActionListView.SelectedIndices[i]];
                if (cutAction.TargetAction.DayPart == -1 || cutAction.TargetAction.DayIndex == 1) break;
                var undoObj = new ActionHistoryObj(EditType.Delete, ActionListView.SelectedIndices[i], cutAction);
                listForUndoHistory.Add(undoObj);
                ActionList.RemoveAt(ActionListView.SelectedIndices[i]);
                ActionListView.Items.RemoveAt(ActionListView.SelectedIndices[i]);
            }
            if (listForUndoHistory.Count > 0)
            {
                isChanged = true;
                EditHistory.undoActions.Add(listForUndoHistory);
                EditHistory.redoActions.Clear();
            }

            InitializeBreakdown();
            isDeleting = false;
        }

        private void ShowMilestoneTooltip(object sender, MouseEventArgs e)
        {
            var item = ActionListView.GetItemAt(e.X, e.Y);
            if (item != null && item.ToolTipText != "")
            {
                if (MilestoneTooltipText != item.ToolTipText) MilestoneTooltipText = item.ToolTipText;
                tipCoordY = e.Y;
                tipCoordX = e.X;
            }
            else if (((e.X != tipCoordX || e.Y != tipCoordY) && item != null && item.ToolTipText == "") || item == null)
            {
                MilestoneTooltip.Hide(ActionListView);
                MilestoneTooltipText = "";
                tipCoordY = e.Y;
                tipCoordX = e.X;
            }

        }

        private void ActionListViewOnItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            MilestoneTooltip.Show(MilestoneTooltipText, ActionListView, tipCoordX + 35, tipCoordY - 10);
        }

        private void EndingBranchEditorOnClick(object sender, EventArgs e)
        {
            ActionListView.SelectedIndices.Clear();
        }

        private (List<ActionHistoryObj>, List<ActionHistoryObj>) UseEditHistory(List<ActionHistoryObj> listToUse, List<ActionHistoryObj> listToEdit)
        {
            for (int i = listToUse.Count - 1; i >= 0; i--)
            {
                EditType redoType = listToUse[i].EditType;
                switch (redoType)
                {
                    case EditType.Delete:
                        redoType = EditType.Add;
                        break;
                    case EditType.Add:
                        redoType = EditType.Delete;
                        break;
                }

                var redoAction = new ActionHistoryObj(redoType, listToUse[i].ActionIndex, listToUse[i].Action);
                var index = 0;
                index = listToUse[i].ActionIndex;
                if (redoType == EditType.Edit)
                {
                    var act = listToUse[i].ActionAfterEdit;
                    index = ActionList.FindIndex(a => a.TargetAction.DayIndex == act.TargetAction.DayIndex && a.TargetAction.DayPart == act.TargetAction.DayPart && a.Command == act.Command);
                    redoAction.ActionIndex = index;
                    redoAction.Action = new TargetActionData(act);
                    redoAction.ActionAfterEdit = new TargetActionData(listToUse[i].Action);
                }
                listToEdit.Add(redoAction);
                if (listToUse[i].EditType == EditType.Delete)
                {
                    AddActionVisualData(listToUse[i].Action, true);
                    continue;
                }
                ActionList.RemoveAt(index);
                ActionListView.Items.RemoveAt(index);
                if (listToUse[i].EditType == EditType.Add) continue;
                AddActionVisualData(listToUse[i].Action, true);
            }
            return (listToUse, listToEdit);

        }

        private void SwitchToOtherEndingBranch(int branchIndex)
        {
            if (isChanged ||
                UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown != SelectedEndingBranch.EndingBranch.IsStressfulBressdown ||
                UnsavedEndingBranchData.IgnoreNightEndings != SelectedEndingBranch.IgnoreNightEndings)
            {
                var confirm = MsgIfBranchUnsaved(false);
                if (!confirm)
                {
                    return;
                }
            }
            SelectedEndingBranch = MainForm.CurrentEndingTree.EndingsList[branchIndex];
            SelectedEndingIndex = branchIndex;
            ResetEndingBranch(false);
            ActionListView.SelectedIndices.Clear();
            OnBranchChanged?.Invoke(this, null);
        }

        private void SwitchToPreviousBranch()
        {
            int index = SelectedEndingIndex;
            if (index == 0)
                return;
            SwitchToOtherEndingBranch(index - 1);
        }

        private void SwitchToNextBranch()
        {
            var tree = MainForm.CurrentEndingTree;
            int index = SelectedEndingIndex;
            if (index == tree.EndingsList.Count - 1)
                return;
            SwitchToOtherEndingBranch(index + 1);
        }

        private void FollowersDiff_DoubleClick(object sender, EventArgs e)
        {
            var followerString = FollowersDiff.Text.Split(' ');
            var followerNum = followerString[followerString.Length - 1];
            Clipboard.SetData(DataFormats.Text, followerNum);
        }

        private void StressDiff_DoubleClick(object sender, EventArgs e)
        {
            var stressString = StressDiff.Text.Split(' ');
            var stressNum = stressString[stressString.Length - 1];
            Clipboard.SetData(DataFormats.Text, stressNum);
        }

        private void AffectionDiff_DoubleClick(object sender, EventArgs e)
        {
            var affectionString = AffectionDiff.Text.Split(' ');
            var affectionNum = affectionString[affectionString.Length - 1];
            Clipboard.SetData(DataFormats.Text, affectionNum);
        }

        private void DarknessDiff_DoubleClick(object sender, EventArgs e)
        {
            var darknessString = DarknessDiff.Text.Split(' ');
            var darknessNum = darknessString[darknessString.Length - 1];
            Clipboard.SetData(DataFormats.Text, darknessNum);
        }

        private void ActionListView_ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (EditHistory.undoActions.Count == 0) Edit_ContextMenuStrip.Items[0].Enabled = false;
            else Edit_ContextMenuStrip.Items[0].Enabled = true;
            if (EditHistory.redoActions.Count == 0) Edit_ContextMenuStrip.Items[1].Enabled = false;
            else Edit_ContextMenuStrip.Items[1].Enabled = true;
            if (ActionListView.SelectedIndices.Count == 0)
            {
                Edit_ContextMenuStrip.Items[3].Enabled = false;
                Edit_ContextMenuStrip.Items[4].Enabled = false;
            }
            else
            {
                Edit_ContextMenuStrip.Items[3].Enabled = true;
                Edit_ContextMenuStrip.Items[4].Enabled = true;
            }
            if (!Clipboard.GetDataObject().GetDataPresent(typeof(TargetActionData[])))
            {
                Edit_ContextMenuStrip.Items[5].Enabled = false;
                Edit_ContextMenuStrip.Items[6].Enabled = false;
            }
            else
            {
                Edit_ContextMenuStrip.Items[5].Enabled = true;
                Edit_ContextMenuStrip.Items[6].Enabled = true;
            }
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            UndoActionEdit();
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            RedoActionEdit();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            CopyActions();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            CutActions();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            PasteCopiedActions();
        }

        private void PasteNewAction_Click(object sender, EventArgs e)
        {
            PasteAsNewActions();
        }

        private void ResetBranch_MenuItemClick(object sender, EventArgs e)
        {
            ResetEndingBranch();
        }

        private void SaveBranch_MenuItemClick(object sender, EventArgs e)
        {
            SaveEndingBranch(false);
        }

        private void IgnoreNightEnding_Label_CheckedChanged(object sender, EventArgs e)
        {
            UnsavedEndingBranchData.IgnoreNightEndings = IgnoreNightEnding_Label.Checked;
            InitializeBreakdown();
        }

        private void NextEnding_MenuItem_Click(object sender, EventArgs e)
        {
            SwitchToNextBranch();
        }

        private void PreviousBranch_MenuItem_Click(object sender, EventArgs e)
        {
            SwitchToPreviousBranch();
        }

        private void Branch_ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            int index = SelectedEndingIndex;
            if (MainForm.CurrentEndingTree.EndingsList.Count == 1)
            {
                PreviousBranch_MenuItem.Enabled = false;
                NextBranch_MenuItem.Enabled = false;
            }
            else if (index == 0)
            {
                PreviousBranch_MenuItem.Enabled = false;
                NextBranch_MenuItem.Enabled = true;
            }
            else if (index == MainForm.CurrentEndingTree.EndingsList.Count - 1)
            {
                NextBranch_MenuItem.Enabled = false;
                PreviousBranch_MenuItem.Enabled = true;
            }
            else
            {
                PreviousBranch_MenuItem.Enabled = true;
                NextBranch_MenuItem.Enabled = true;
            }
            if (ActionListView.SelectedIndices.Count == 0)
                NewEndingBranchFromSelected_MenuItem.Enabled = false;
            else NewEndingBranchFromSelected_MenuItem.Enabled = true;
        }

        private void CreateNewBranch()
        {
            EndingBranchData newBranch = null;
            int newBranchIndex = SelectedEndingIndex;
            AddEndingBranch newBranchWindow = new(MainForm);
            newBranchWindow.branchEditor = this;
            newBranchWindow.OnNewBranchCreated += () =>
            {
                newBranch = newBranchWindow.NewEnding;
                if (newBranchWindow.isAdvanced)
                    newBranchIndex = newBranchWindow.endingIndex;
                else newBranchIndex = MainForm.CurrentEndingTree.EndingsList.Count - 1;
            };
            newBranchWindow.ShowDialog();
            if (newBranch == null)
            {
                newBranchWindow.Dispose();
                return;
            }
            MainForm.SetEndingListViewData();
            MainForm.isBranchEdited = true;
            SwitchToOtherEndingBranch(newBranchIndex);
            newBranchWindow.Dispose();
        }
        private void CreateNewBranchFromExistingDay()
        {
            if (ActionListView.SelectedItems.Count == 0)
                return;
            if (isChanged ||
    UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown != SelectedEndingBranch.EndingBranch.IsStressfulBressdown ||
    UnsavedEndingBranchData.IgnoreNightEndings != SelectedEndingBranch.IgnoreNightEndings)
            {
                var confirm = MsgIfBranchUnsaved(false);
                if (!confirm)
                {
                    return;
                }
            }
            var selectedDay = FindStartingDayFromSelected();
            if (selectedDay < 2)
            {
                MessageBox.Show("Cannot create new Ending Branch, current selected actions are invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EndingBranchData newBranch = null;
            int newBranchIndex = SelectedEndingIndex;
            int maxBranchIndex = MainForm.CurrentEndingTree.EndingsList.Count + 1;
            AddEndingBranch newBranchWindow = new(MainForm, false, SelectedEndingIndex + 1);
            newBranchWindow.branchEditor = this;
            newBranchWindow.StartingDayNumeric.Value = selectedDay;
            newBranchWindow.StartingDayNumeric.Enabled = false;
            newBranchWindow.InsertAtEndingIndex_Numeric.Minimum = SelectedEndingIndex + 2;
            newBranchWindow.InsertAtEndingIndex_Numeric.Value = SelectedEndingIndex + 2;
            newBranchWindow.Advanced_Button.Enabled = false;
            for (int i = SelectedEndingIndex; i < MainForm.CurrentEndingTree.EndingsList.Count; i++)
            {
                if (i == SelectedEndingIndex) continue;
                var otherBranch = MainForm.CurrentEndingTree.EndingsList[i];
                if (otherBranch.EndingBranch.AllActions.Exists(hasValidStartDay))
                {
                    maxBranchIndex = i + 1;
                    break;
                }
            }
            newBranchWindow.InsertAtEndingIndex_Numeric.Maximum = maxBranchIndex;
            newBranchWindow.OnNewBranchCreated += () =>
            {
                newBranch = newBranchWindow.NewEnding;
                if (newBranchWindow.isAdvanced)
                    newBranchIndex = newBranchWindow.endingIndex;
                else newBranchIndex = MainForm.CurrentEndingTree.EndingsList.Count - 1;
            };
            newBranchWindow.ShowDialog();
            if (newBranch == null)
            {
                newBranchWindow.Dispose();
                return;
            }
            var list = CreateListCopy();
            if (list != null && list.Count > 0)
                newBranch.EndingBranch.AllActions.AddRange(list);
            MainForm.SetEndingListViewData();
            MainForm.isBranchEdited = true;
            SwitchToOtherEndingBranch(newBranchIndex);
            newBranchWindow.Dispose();

            int FindStartingDayFromSelected()
            {
                int startingDay = 2;
                if (ActionListView.SelectedIndices.Count == 1)
                    return SelectedAction.TargetAction.DayIndex;
                List<TargetActionData> list = CreateListCopy();
                if (list.Count == 0)
                    return -1;
                for (int i = 2; i < 30; i++)
                {
                    if (!list.Exists(a => a.TargetAction.DayIndex == i))
                        continue;
                    startingDay = i;
                    break;
                }
                return startingDay;
            }

            bool hasValidStartDay(TargetActionData a)
            {
                return a.TargetAction.DayIndex <= selectedDay || (a.TargetAction.DayIndex == selectedDay - 1 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3);
            }

        }
        private void SaveEndingBranchAndExit_MenuItem_Click(object sender, EventArgs e)
        {
            SaveEndingBranch();
        }

        private void newEndingBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewBranch();
        }

        private void newBranchFromSelectedDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewBranchFromExistingDay();
        }

        private void Edit_ContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            for (int i = 0; i < Edit_ContextMenuStrip.Items.Count; i++)
            {
                Edit_ContextMenuStrip.Items[i].Enabled = true;
            }
        }

        private void Branch_ContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            for (int i = 0; i < Branch_ContextMenuStrip.Items.Count; i++)
            {
                Branch_ContextMenuStrip.Items[i].Enabled = true;
            }
        }
    }
}
