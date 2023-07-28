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
        public EndingBranchData UnsavedEndingBranchData;
        public List<TargetActionData> ActionList = new();

        public (int DayIndex, int DayPart, EndingType ending) ExpectedEnding;

        public TargetActionData SelectedAction;
        public TargetActionData NewAction;
        public int NewActionParentIndex = -1;

        public EditHistory EditHistory = new();

        private bool isDeleting;

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
        }

        private void AddActionVisualData(TargetActionData action, bool addAlsoToActionList)
        {
            //ActionListView.BeginUpdate();
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

        private void EditActionVisualData(TargetActionData action)
        {
            Console.WriteLine(ActionList.Count);
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
            if (actionData.TargetAction.DayPart == -1)
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
                int index = MainForm.CurrentEndingTree.EndingsList.IndexOf(SelectedEndingBranch);
                var newFirstDay = MainForm.SetStartingAction(UnsavedEndingBranchData, false, index - 1);
                MainForm.ResetStartingDayData(UnsavedEndingBranchData, index - 1);
                ActionList[0] = newFirstDay;
                ActionList[0].TargetAction.DayPart = -1;
                EditActionVisualData(ActionList[0]);
                InitializeBreakdown();
            }
        }

        private void InitializeBreakdown()
        {
            TargetActionData breakdown = ActionList.FirstOrDefault(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart == 3);
            if (breakdown == null)
            {
                if (ActionList.Exists(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3))
                {
                    InitializeActionStats();
                    TargetActionData dayBeforeBreak = ActionList.Find(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3 && a.TargetAction.DayPart != 3);
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
                InitializeActionStats();
                return;
            }
            if (breakdown != null)
            {
                breakdown.CommandResult = new CommandAction();
                if (!ActionList.Exists(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart != 3 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3 && a.TargetAction.DayPart != 3))
                {
                    int index = ActionList.IndexOf(breakdown);
                    ActionList.RemoveAt(index);
                    ActionListView.Items.RemoveAt(index);
                    InitializeActionStats();
                    return;
                }
                TargetActionData dayBeforeBreak = ActionList.Find(a => a.TargetAction.DayIndex == 15 && a.TargetAction.DayPart != 3 && a.TargetAction.DayPart + a.CommandResult.daypart >= 3 && a.TargetAction.DayPart != 3);
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
            InitializeActionStats();
        }
        private void InitializeActionStats()
        {
            var branch = UnsavedEndingBranchData;
            int startingDay = branch.EndingBranch.StartingDay;
            int latestDay = ActionList[ActionList.Count - 1].TargetAction.DayIndex;
            int startIdeaReset = branch.StreamIdeaList.RemoveAll(u => u.DayIndex >= startingDay);
            int startUsedReset = branch.StreamUsedList.RemoveAll(u => u.DayIndex >= startingDay);
            int startSexCountReset = branch.LoveCounter.RemoveAll(u => u.DayIndex >= startingDay);
            int startPaperCountReset = branch.PsycheCounter.RemoveAll(u => u.DayIndex >= startingDay);
            int startIgnoreCountReset = branch.IgnoreCounter.RemoveAll(u => u.DayIndex >= startingDay);
            bool aboutToStress = false;
            bool isDarkAngel = false;
            (int, int, EndingType) expectedEnding = (0, 0, EndingType.Ending_None);
            (int, bool) HasGalaxy = (0, false);
            (int, bool) NoMeds = (0, false);
            (int, bool) Stressed = (0, false);
            (int, bool) ReallyStressed = (0, false);
            (int, bool) VisitParents = (0, false);
            (int, bool) Trauma = (0, false);
            (int, bool) MusicVideo = (0, false);
            (int, bool) Horror = (0, false);
            (int, bool) is150M = (0, false);
            (int, bool) is300M = (0, false);
            (int, bool) is500M = (0, false);
            (int, bool) isMaxFollowers = (0, false);

            if (!branch.StreamIdeaList.Exists(i => i.Idea == CmdType.Zatudan_1))
                branch.StreamIdeaList.Insert(0, new(1, 2, CmdType.Zatudan_1));


            for (int i = 0; i < ActionList.Count; i++)
            {
                if (i == 0) { continue; }
                if (ActionList[i].TargetAction.DayPart != 3) ActionList[i].CommandResult = NSOCommandManager.CmdTypeToCommand(ActionList[i].Command);
                NSOCommandManager.CalculateStats(ActionList[i - 1], ActionList[i]);
                if (ActionList[i].Followers > 9999999) { ActionList[i].Followers = 9999999; }
                if (ActionList[i].Stress >= 100)
                {
                    if (ActionList[i].Stress >= 120 && branch.isStressed.isEventing && branch.isReallyStressed.isEventing && ActionList[i].TargetAction.DayIndex >= branch.isReallyStressed.DayIndex)
                    {
                        ActionList[i].Stress = 120;
                    }
                    else if (!branch.isReallyStressed.isEventing || (branch.isReallyStressed.isEventing && ActionList[i].TargetAction.DayIndex < branch.isReallyStressed.DayIndex))
                        ActionList[i].Stress = 100;
                }
                if (ActionList[i].Affection >= 100)
                {
                    if (ActionList[i].Affection >= 120 && branch.isReallyLove.isEventing && ActionList[i].TargetAction.DayIndex >= branch.isReallyLove.DayIndex)
                    {
                        ActionList[i].Affection = 120;
                    }
                    else if (!branch.isReallyLove.isEventing || (branch.isReallyLove.isEventing && ActionList[i].TargetAction.DayIndex < branch.isReallyLove.DayIndex)) { ActionList[i].Affection = 100; }
                }
                if (ActionList[i].Darkness > 100) { ActionList[i].Darkness = 100; }
                if (ActionList[i].TargetAction.Action == ActionType.Haishin)
                {
                    if (!branch.StreamUsedList.Exists(u => u.UsedStream == ActionList[i].Command))
                        branch.StreamUsedList.Add(new(ActionList[i].TargetAction.DayIndex, ActionList[i].Command));
                }
                else
                {
                    var idea = NSODataManager.ActionToStreamIdea(ActionList[i - 1], ActionList[i], branch);
                    if (idea != (0, 0, CmdType.None))
                    {
                        branch.StreamIdeaList.Add(new(idea.DayIndex, idea.DayPart, idea.Idea));
                    }
                    ActionList[i].StreamIdea = idea.Idea;
                    if (ActionList[i].TargetAction.DayPart == 2 || (ActionList[i].TargetAction.DayPart < 2 && ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart >= 3))
                        ActionList[i].StreamStreak = 0;
                }
                if (ActionList[i].TargetAction.Action == ActionType.PlayMakeLove)
                {
                    branch.LoveCounter.Add(new(ActionList[i].TargetAction.DayIndex, ActionList[i].TargetAction.DayPart));
                }
                if (ActionList[i].TargetAction.Action == ActionType.OkusuriPsyche)
                {
                    branch.PsycheCounter.Add(new(ActionList[i].TargetAction.DayIndex, ActionList[i].TargetAction.DayPart));
                }
                if (ActionList[i].TargetAction.DayIndex == 15 && ActionList[i].TargetAction.DayPart == 3)
                {
                    if (ActionList[i - 1].Affection >= 60 && ActionList[i - 1].Darkness >= 60 && !Trauma.Item2)
                    {
                        Trauma.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                        Trauma.Item2 = true;
                    }
                }
                if (ActionList[i].TargetAction.DayIndex == 23 && ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart >= 3 && ActionList[i].Affection >= 80 && !VisitParents.Item2)
                {

                    VisitParents.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    VisitParents.Item2 = true;

                }
                if (ActionList[i].TargetAction.DayIndex == 26 && ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart >= 3)
                {
                    if (ActionList[i].Followers >= 500000 && !MusicVideo.Item2)
                    {
                        MusicVideo.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                        MusicVideo.Item2 = true;
                    }
                }
                if (branch.StreamUsedList.Exists(s => s.DayIndex >= ActionList[i].TargetAction.DayIndex && s.UsedStream == CmdType.Kaidan_5) && branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M) && !HasGalaxy.Item2)
                {
                    HasGalaxy.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    if (branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M))
                    {
                        HasGalaxy.Item2 = true;
                    }
                }
                if (ActionList[i].Followers >= 1500000 && !is150M.Item2 && branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M))
                {
                    is150M.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    is150M.Item2 = true;
                }
                if (ActionList[i].Followers >= 3000000 && !is300M.Item2 && branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M))
                {
                    is300M.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    is300M.Item2 = true;
                }
                if (ActionList[i].Followers >= 5000000 && !is500M.Item2 && branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M))
                {
                    is500M.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    is500M.Item2 = true;
                }
                if (ActionList[i].Followers >= 9999999 && branch.IsNotMidnightEvents(ActionList[i], HasGalaxy, is150M, is300M, is500M))
                {
                    isMaxFollowers.Item1 = ActionList[i].TargetAction.DayIndex;
                    isMaxFollowers.Item2 = true;
                }
                if (ActionList[i].TargetAction.DayIndex == 24 && (ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart) >= 3)
                {
                    if (ActionList[i].Stress >= 80 && branch.isStressed.isEventing && branch.isReallyStressed.isEventing && !Horror.Item2)
                    {
                        Horror.Item1 = 25;
                        Horror.Item2 = true;
                    }
                }
                if (ActionList[i].Stress >= 100 && ((ActionList[i].TargetAction.DayIndex != 15 && (ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart) >= 3) || (ActionList[i].TargetAction.DayIndex == 15 && ActionList[i].TargetAction.DayPart == 3)) && Stressed.Item2 && !ReallyStressed.Item2 && branch.IsNotFixedEvents(ActionList[i], Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    ReallyStressed.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    ReallyStressed.Item2 = true;
                }
                if (ActionList[i].Stress >= 80 && !Stressed.Item2 && ((ActionList[i].TargetAction.DayIndex != 15 && (ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart) >= 3) || (ActionList[i].TargetAction.DayIndex == 15 && ActionList[i].TargetAction.DayPart == 3)) && branch.IsNotFixedEvents(ActionList[i], Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    if (aboutToStress)
                    {
                        Stressed.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                        Stressed.Item2 = true;
                    }
                    else { aboutToStress = true; }
                }
                if (ActionList[i].Stress < 80 && ((ActionList[i].TargetAction.DayIndex != 15 && ActionList[i].TargetAction.DayPart + ActionList[i].CommandResult.daypart >= 3) || (ActionList[i].TargetAction.DayIndex == 15 && ActionList[i].TargetAction.DayPart == 3)) && aboutToStress && !Stressed.Item2 && branch.IsNotFixedEvents(ActionList[i], Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    aboutToStress = false;
                }
                if (branch.StreamUsedList.Exists(u => u.DayIndex <= ActionList[i].TargetAction.DayIndex && u.UsedStream == CmdType.Angel_5) && !ReallyStressed.Item2 && !NoMeds.Item2)
                {
                    NoMeds.Item1 = ActionList[i].TargetAction.DayIndex + 1;
                    NoMeds.Item2 = true;
                }
                if (branch.IsNotFixedEvents(ActionList[i - 1], Trauma.Item2, VisitParents.Item2, MusicVideo.Item2) && branch.IsNotStressEvents(ActionList[i - 1], Stressed, ReallyStressed))
                {
                    if (ReallyStressed.Item2 && ActionList.Exists(a => a.Command == CmdType.DarknessS2 && a.TargetAction.DayIndex == ReallyStressed.Item1) && i > ReallyStressed.Item1) isDarkAngel = true;
                    NSODataManager.InitializeMilestoneIdea(ActionList[i], branch, isDarkAngel);
                }
                if (ActionList[i].TargetAction.IgnoreDM)
                {
                    branch.IgnoreCounter.Add(new(ActionList[i].TargetAction.DayIndex, ActionList[i].TargetAction.DayPart));
                    if (ActionList[i].Stress >= 120 && branch.isStressed.isEventing && branch.isReallyStressed.isEventing && ActionList[i].TargetAction.DayIndex >= branch.isReallyStressed.DayIndex) ActionList[i].Stress = 120;
                    else if (ActionList[i].Stress >= 100 && (!branch.isReallyStressed.isEventing || (branch.isReallyStressed.isEventing && ActionList[i].TargetAction.DayIndex >= branch.isReallyStressed.DayIndex))) ActionList[i].Stress = 100;
                    else ActionList[i].Stress += 4;
                    if (ActionList[i].Affection <= 4) ActionList[i].Affection = 0;
                    else ActionList[i].Affection -= 5;
                }
                if (expectedEnding.Item1 == 0)
                    expectedEnding = CheckIfEndingAchieved(ActionList[i - 1], ActionList[i], ReallyStressed.Item2, Horror.Item2, VisitParents.Item2, NoMeds.Item2);
                EditActionVisualData(ActionList[i - 1]);
                EditActionVisualData(ActionList[i]);
                ideasWindow?.UpdateFoundIdeas();
                usedWindow?.UpdateUsed();
            }

            SetNewEventFlags();
            SetEventInfo();
            SetExtraMilestoneGraphic();
            SetGuessedEnding();
            SetActionCounterText();


            void SetNewEventFlags()
            {
                if (!(branch.hasGalacticRail.DayIndex <= startingDay && branch.hasGalacticRail.DayIndex > 0)) branch.hasGalacticRail = new(HasGalaxy.Item1, HasGalaxy.Item2);
                if (!(branch.NoMeds.DayIndex <= startingDay && branch.NoMeds.DayIndex > 0)) branch.NoMeds = new(NoMeds.Item1, NoMeds.Item2);
                if (!(branch.isReallyLove.DayIndex <= startingDay && branch.isReallyLove.DayIndex > 0)) branch.isReallyLove = new(VisitParents.Item1, VisitParents.Item2);
                if (!(branch.isStressed.DayIndex <= startingDay && branch.isStressed.DayIndex > 0)) branch.isStressed = new(Stressed.Item1, Stressed.Item2);
                if (!(branch.isReallyStressed.DayIndex <= startingDay && branch.isReallyStressed.DayIndex > 0)) branch.isReallyStressed = new(ReallyStressed.Item1, ReallyStressed.Item2);
                if (!(branch.isHorror.DayIndex <= startingDay && branch.isHorror.DayIndex > 0)) branch.isHorror = new(Horror.Item1, Horror.Item2);
                if (!(branch.isVideo.DayIndex <= startingDay && branch.isVideo.DayIndex > 0)) branch.isVideo = new(MusicVideo.Item1, MusicVideo.Item2);
                if (!(branch.isTrauma.DayIndex <= startingDay && branch.isTrauma.DayIndex > 0)) branch.isTrauma = new(Trauma.Item1, Trauma.Item2);
                if (!(branch.is150M.DayIndex <= startingDay && branch.is150M.DayIndex > 0)) branch.is150M = new(is150M.Item1, is150M.Item2);
                if (!(branch.is300M.DayIndex <= startingDay && branch.is300M.DayIndex > 0)) branch.is300M = new(is300M.Item1, is300M.Item2);
                if (!(branch.is500M.DayIndex <= startingDay && branch.is500M.DayIndex > 0)) branch.is500M = new(is500M.Item1, is500M.Item2);
                if (!(branch.isMaxFollowers.DayIndex <= startingDay && branch.isMaxFollowers.DayIndex > 0)) branch.isMaxFollowers = new(isMaxFollowers.Item1, isMaxFollowers.Item2);
            }
            void SetEventInfo()
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
                Vomited_ToolTip.SetToolTip(Vomited_Bool, "Only true if Ame vomits on Day 25, if on Day 24 she has 80+ stress, and her stress maximum (not current stress) is 120. ");
                Trauma_Bool.Checked = branch.isTrauma.isEventing;
                Trauma_Tooltip.SetToolTip(Trauma_Bool, "Only true if Ame trauma dumps on you on Day 15 instead of breaking down.");
                Parents_Bool.Checked = branch.isReallyLove.isEventing;
                Parents_Tooltip.SetToolTip(Parents_Bool, "Only true if Ame visits her parents on Day 24, if at the end of day 23 she has 80+ affection.");
                MV_bool.Checked = branch.isVideo.isEventing;
                MV_Tooltip.SetToolTip(MV_bool, "Only true if Ame goes out to record her MV on Day 27, if she has 500000 or more followers.");
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

            void SetExtraMilestoneGraphic()
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

            void SetGuessedEnding()
            {

                ExpectedEnding = expectedEnding;
                int endedDay = ExpectedEnding.DayIndex;
                int endedDayPart = ExpectedEnding.DayPart;
                if (ExpectedEnding.ending == EndingType.Ending_KowaiInternet)
                {
                    endedDay = 25;
                    endedDayPart = 0;
                }
                if (ExpectedEnding.ending == EndingType.Ending_Grand ||
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
                if (ExpectedEnding.ending == EndingType.Ending_Stressful ||
                   ExpectedEnding.ending == EndingType.Ending_Healthy ||
                   ExpectedEnding.ending == EndingType.Ending_Sukisuki ||
                   ExpectedEnding.ending == EndingType.Ending_Ntr ||
                   ExpectedEnding.ending == EndingType.Ending_Meta)
                {

                    endedDayPart = 2;
                }
                if (ExpectedEnding.ending == EndingType.Ending_Jikka ||
                  ExpectedEnding.ending == EndingType.Ending_Ideon)
                {
                    endedDayPart = 3;
                }
                EndingGuesser.Text = ExpectedEnding.ending == EndingType.Ending_None ? "Projected Ending : None" : $"Projected Ending : {NSODataManager.EndingNames[ExpectedEnding.ending]} on Day {endedDay}, {NSODataManager.DayPartNames[endedDayPart]}";
            }

            void SetActionCounterText()
            {
                LoveCount.Text = $"Love Counter: {branch.LoveCounter.Count}";
                PaperCount.Text = $"Paper Counter: {branch.PsycheCounter.Count}";
                IgnoredCount.Text = $"Ignored DM's: {branch.IgnoreCounter.Count}";
                LoveCounter_Tooltip.SetToolTip(LoveCount, $"{(branch.LoveCounter.Count > 0 ? "Times you ***:" : "")}" + SetActionCounterListTooltip(branch.LoveCounter));
                PsycheCounter_Tooltip.SetToolTip(PaperCount, $"{(branch.PsycheCounter.Count > 0 ? "Times you took Magic Paper:" : "")}" + SetActionCounterListTooltip(branch.PsycheCounter));
                IgnoreCounter_Tooltip.SetToolTip(IgnoredCount, $"{(branch.IgnoreCounter.Count > 0 ? "Times you ignored a DM:" : "")}" + SetActionCounterListTooltip(branch.IgnoreCounter));
            }

            string SetActionCounterListTooltip(List<ActionCounter> countList)
            {
                var countString = "";
                if (countList.Count == 0) return countString;
                for (int i = 0; i < countList.Count; i++)
                {
                    countString += $"\nDay {countList[i].DayIndex}, {NSODataManager.DayPartNames[countList[i].DayPart]}";
                }
                return countString;
            }
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
                    actionOptions.Add("Spend Time Together - Cuddle");
                    actionOptions.Add("Spend Time Together - Pity Party");
                    actionOptions.Add("*** - ***");
                    actionOptions.Add("*** - Have Chem***");
                    break;
                case 2:
                    actionOptions.Add("Sleep To Dusk");
                    actionOptions.Add("Sleep To Night (Noon)");
                    actionOptions.Add("Sleep To Night (Dusk)");
                    actionOptions.Add("Sleep To Tomorrow (Noon)");
                    actionOptions.Add("Sleep To Tomorrow (Dusk)");
                    actionOptions.Add("Sleep To Tomorrow (Night)");
                    break;
                case 3:
                    actionOptions.Add("Prescription (normal dose)");
                    actionOptions.Add("Prescription GO! (Strongest)");
                    actionOptions.Add("Prescription GO! (Dylsem)");
                    actionOptions.Add("Prescription GO! (Embian)");
                    actionOptions.Add("Prescription GO! (Grass/Paper)");
                    actionOptions.Add("OTC (normal dose)");
                    actionOptions.Add("OTC GO! (Strongest)");
                    actionOptions.Add("OTC GO! (Embian)");
                    actionOptions.Add("OTC GO! (Grass/Paper)");
                    actionOptions.Add("Sleeping Pills GO! (Strongest)");
                    actionOptions.Add("Sleeping Pills GO! (Grass/Paper)");
                    actionOptions.Add("Magic Grass GO! (Strongest)");
                    actionOptions.Add("Magic Grass GO! (Paper)");
                    actionOptions.Add("Magic Paper GO!");
                    break;
                case 4:
                    actionOptions.Add("Social Media - Daily Tweet");
                    actionOptions.Add("Social Media - Send Business Tweet");
                    actionOptions.Add("Social Media - Muse");
                    actionOptions.Add("Social Media - Vent On Main");
                    actionOptions.Add("Social Media - Bash Others");
                    actionOptions.Add("Vanity Search");
                    actionOptions.Add("Video Streaming");
                    actionOptions.Add("/st/ - Search Opinions");
                    actionOptions.Add("/st/ - Go Undercover");
                    actionOptions.Add("/st/ - Stir Shit");
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



        private (int, int, EndingType) CheckIfEndingAchieved(TargetActionData pastAction, TargetActionData action, bool isVeryVeryStressed, bool isHorror, bool isVeryLove, bool isAngelFive)
        {
            ActionCounter ignoreDay = new(30, 0);
            ActionCounter loveDay = new(30, 0);
            ActionCounter paperDay = new(30, 0);

            var branch = UnsavedEndingBranchData;
            var isCultStreamIdeaExists = branch.StreamIdeaList.Exists(u => u.Idea == CmdType.Error);

            if (branch.IgnoreCounter.Count >= 5) ignoreDay = branch.IgnoreCounter[4];
            if (branch.LoveCounter.Count >= 7) loveDay = branch.LoveCounter[6];
            if (branch.PsycheCounter.Count >= 5) paperDay = branch.PsycheCounter[4];

            if (branch.isReallyStressed.isEventing && branch.isReallyStressed.DayIndex <= action.TargetAction.DayIndex)
                isVeryVeryStressed = true;
            if (branch.isHorror.isEventing && (branch.isHorror.DayIndex <= action.TargetAction.DayIndex || pastAction.TargetAction.DayIndex == 24 && pastAction.TargetAction.DayPart + pastAction.CommandResult.daypart >= 3))
                isHorror = true;
            if (branch.isReallyLove.isEventing && branch.isReallyLove.DayIndex <= action.TargetAction.DayIndex)
                isVeryLove = true;
            if (branch.NoMeds.isEventing && branch.NoMeds.DayIndex <= action.TargetAction.DayIndex)
                isAngelFive = true;
            if (isVeryVeryStressed && isHorror && action.Stress >= 80)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_KowaiInternet);
            if (loveDay.DayIndex != 30 && action.TargetAction.DayIndex == loveDay.DayIndex && action.TargetAction.DayPart == loveDay.DayPart)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Lust);
            if (ignoreDay.DayIndex != 30 && action.TargetAction.DayIndex == ignoreDay.DayIndex && action.TargetAction.DayPart == ignoreDay.DayPart)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Jine);
            if (isVeryVeryStressed && action.Command == CmdType.Angel_6)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_DarkAngel);
            if (action.Command == CmdType.Error)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Kyouso);
            if (action.Command == CmdType.Yamihaishin_5)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Yami);
            if (action.Command == CmdType.Hnahaisin_5)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Av);
            if (action.TargetAction.DayPart + action.CommandResult.daypart == 2)
            {
                if (isVeryVeryStressed && action.Stress == 120)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Stressful);
                if (action.Darkness == 0)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Healthy);
                if ((!isVeryLove && action.Affection == 100) || (isVeryLove && action.Affection == 120))
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Sukisuki);
                if (action.Affection == 0)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Ntr);
                if (paperDay.DayIndex != 30 && action.TargetAction.DayIndex == paperDay.DayIndex && action.TargetAction.DayPart == paperDay.DayPart)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Meta);
            }
            if (pastAction.TargetAction.Action.ToString().Contains("Okusuri") &&
                pastAction.TargetAction.Action != ActionType.OkusuriPuronModerate &&
                pastAction.TargetAction.Action != ActionType.OkusuriHipuronModerate &&
                pastAction.TargetAction.Action != ActionType.OkusuriDaypassModerate &&
                branch.StreamUsedList.Exists(a => a.DayIndex < action.TargetAction.DayIndex && a.UsedStream == CmdType.Kaidan_5) && action.Command == CmdType.OdekakeGinga && action.TargetAction.DayPart == 2)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Ginga);
            if (action.TargetAction.DayIndex == 10 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && action.Followers < 10000)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Jikka);
            if (action.Followers >= 9999999 && branch.isMaxFollowers.isEventing && branch.isMaxFollowers.DayIndex == action.TargetAction.DayIndex && UnsavedEndingBranchData.IsNotMidnightEvents(action, (branch.hasGalacticRail.DayIndex, branch.hasGalacticRail.isEventing), (branch.is150M.DayIndex, branch.is150M.isEventing), (branch.is300M.DayIndex, branch.is300M.isEventing), (branch.is500M.DayIndex, branch.is500M.isEventing)))
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Ideon);
            if (action.TargetAction.DayIndex == 29 && (action.TargetAction.DayPart + action.CommandResult.daypart >= 3))
            {
                if (isCultStreamIdeaExists)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Kyouso);
                if (action.Followers >= 1000000 && action.Affection >= 80 && action.Darkness >= 80 && isAngelFive)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Grand);
                if (action.Followers >= 1000000 && action.Affection >= 80)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Happy);
                if (action.Followers >= 500000 && action.Affection >= 80)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Normal);
                if (action.Followers >= 500000)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Yarisute);
                if (action.Affection >= 60 && action.Darkness >= 60)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Needy);
                if (action.Affection < 60 && action.Darkness >= 60)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Sucide);
                if (action.Affection >= 60 && action.Darkness < 60)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Work);
                if (action.Affection < 60 && action.Darkness < 60)
                    return (action.TargetAction.DayIndex, action.TargetAction.DayPart, EndingType.Ending_Bad);
            }

            return (0, 0, EndingType.Ending_None);
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
                return;
            }
            if (SelectedAction != null && (SelectedAction.TargetAction.DayPart == -1 || SelectedAction.TargetAction.DayIndex == 1))
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
                TargetActionData pastAction = ActionList[ActionList.FindLastIndex(a => (a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart < DayPart_Dropdown.SelectedIndex) || a.TargetAction.DayIndex < DayIndexNumeric.Value)];
                CommandAction command = SelectedAction != null ? SelectedAction.CommandResult : NSOCommandManager.CmdTypeToCommand(NewAction.Command);
                CmdType stream = CmdType.Error;
                if (ParentAction_Dropdown.SelectedIndex == 0 && !(StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && StreamLevelNumeric.Value == 6)) stream = (CmdType)Enum.Parse(typeof(CmdType), $"{NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex]}_{StreamLevelNumeric.Value}");
                int followerCalc = ParentAction_Dropdown.SelectedIndex == 0 ? NSOCommandManager.CalculateFollowers(pastAction, new TargetActionData((int)DayIndexNumeric.Value, DayPart_Dropdown.SelectedIndex, stream)) : 0;
                int followerResult = pastAction.Followers + followerCalc;
                int stressResult = pastAction.Stress + command.stress;
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
            void SetStreamIdeaPreview(TargetActionData pastAction)
            {
                var ideaAction = new TargetActionData((int)DayIndexNumeric.Value, DayPart_Dropdown.SelectedIndex, CmdType.None, IgnoreDMCheck.Checked);
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        ideaAction.Command = NSODataManager.HangOutList[Action_Dropdown.SelectedIndex];
                        break;
                    case 2:
                        ideaAction.Command = NSODataManager.SleepList[Action_Dropdown.SelectedIndex];
                        break;
                    case 3:
                        ideaAction.Command = NSODataManager.DrugList[Action_Dropdown.SelectedIndex];
                        break;
                    case 4:
                        ideaAction.Command = NSODataManager.InternetList[Action_Dropdown.SelectedIndex];
                        break;
                    case 5:
                        ideaAction.Command = NSODataManager.OutsideList[Action_Dropdown.SelectedIndex];
                        break;
                    case 6:
                        ideaAction.Command = NSODataManager.DarknessList[Action_Dropdown.SelectedIndex];
                        break;

                }
                ideaAction.TargetAction.Action = NSODataManager.CmdToActionConverter(ideaAction.Command);
                var (DayIndex, DayPart, Idea) = NSODataManager.ActionToStreamIdea(pastAction, ideaAction, UnsavedEndingBranchData);


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
            }
        }
        private void EndingBranchEditorOnLoad(object sender, EventArgs e)
        {
            DayIndexNumeric.Value = ActionList[ActionList.Count - 1].TargetAction.DayIndex;
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
            SetDayMinimum();
            int listCount = ActionList.Count;
            for (int i = 0; i < listCount; i++)
            {
                AddActionVisualData(ActionList[i], false);
                if (i == listCount - 1)
                {
                    InitializeBreakdown();
                }
            }
            if (ActionList.Count == 1) { DayPart_Dropdown.SelectedIndex = 0; }
            //ActionListView.EndUpdate();
            EndingToGet_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(UnsavedEndingBranchData.EndingBranch.EndingToGet);
            StressfulBreakdown_Check.Checked = UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown;

        }


        private void IgnoreDmCheck_CheckedChanged(object sender, EventArgs e)
        {
            SetStatChangePreview();
        }

        private void TargetActionButtonOnClick(object sender, EventArgs e)
        {
            CmdType command = CmdType.None;
            var selectedActions = ActionListView.SelectedIndices;
            if (DayPart_Dropdown.SelectedIndex == -1 && (selectedActions.Count == 0 || (selectedActions.Count == 1 && ActionListView.SelectedIndices[0] != 0)))
            {
                MessageBox.Show("Time of Day cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ParentAction_Dropdown.SelectedIndex > 0 && Action_Dropdown.SelectedIndex == -1 && (selectedActions.Count == 0 || (selectedActions.Count > 0 && ActionListView.SelectedIndices[0] != 0)))
            {
                MessageBox.Show("Action cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (ParentAction_Dropdown.SelectedIndex)
            {
                case -1:
                    if (selectedActions.Count > 0 && ActionListView.SelectedIndices[0] == 0) break; 
                    MessageBox.Show("Parent action cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case 0:
                    if (StreamTopic_Dropdown.SelectedIndex == -1)
                    {
                        MessageBox.Show("Stream topic cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (StreamTopic_Dropdown.SelectedIndex == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && StreamLevelNumeric.Value == 6)
                    {
                        command = CmdType.Error;
                        break;
                    }
                    command = (CmdType)Enum.Parse(typeof(CmdType), $"{NSODataManager.StreamTopicList[StreamTopic_Dropdown.SelectedIndex]}_{StreamLevelNumeric.Value}");
                    break;
                case 1:
                    command = NSODataManager.HangOutList[Action_Dropdown.SelectedIndex];
                    break;
                case 2:
                    command = NSODataManager.SleepList[Action_Dropdown.SelectedIndex];
                    break;
                case 3:
                    command = NSODataManager.DrugList[Action_Dropdown.SelectedIndex];
                    break;
                case 4:
                    command = NSODataManager.InternetList[Action_Dropdown.SelectedIndex];
                    break;
                case 5:
                    command = NSODataManager.OutsideList[Action_Dropdown.SelectedIndex];
                    break;
                case 6:
                    command = NSODataManager.DarknessList[Action_Dropdown.SelectedIndex];
                    break;
            }
            if (SelectedAction != null)
            {
                if (selectedActions.Count == 1 && selectedActions[0] == 0)
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
                    EditHistory.undoActions.Add(new() { new(EditType.Edit, index, actionToUndo, moveSelected) });
                    EditHistory.redoActions.Clear();
                }
                ActionListView.Items.RemoveAt(index);
                ActionList.RemoveAt(index);
                AddActionVisualData(moveSelected, true);
                InitializeBreakdown();
                ChangeNumeralDropdowns(moveSelected);
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
                    if (selectedDatas[i].TargetAction.DayPart == -1 || selectedDatas[i].TargetAction.DayIndex == 1)
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
                    EditHistory.undoActions.Add(listForUndoHistory);
                    EditHistory.redoActions.Clear();
                }
                InitializeBreakdown();
            }

            void ChangeNumeralDropdowns(TargetActionData action)
            {
                ActionListView.SelectedIndices.Clear();
                int nextDayPart = action.TargetAction.DayPart;
                int nextDay = action.TargetAction.DayIndex;
                nextDayPart += action.CommandResult.daypart;
                if (nextDayPart >= 3)
                {
                    if (nextDay >= DayIndexNumeric.Maximum) return;
                    nextDay++;
                    DayIndexNumeric.Value = nextDay;
                    DayPart_Dropdown.SelectedIndex = 0;
                    return;
                }
                DayPart_Dropdown.SelectedIndex = nextDayPart;
            }

            bool InitializeSelectedStartDay()
            {
                if (DayIndexNumeric.Value == 1)
                {
                    TargetActionData dayOneAction = new(1, 2, CmdType.None);
                    ActionList[0] = dayOneAction;
                    InitializeActionStats();
                }
                else
                {
                    if (ActionList.Exists(a => a.TargetAction.DayIndex == DayIndexNumeric.Value) && ActionList.Count > 1 && DayIndexNumeric.Value > ActionList[1].TargetAction.DayIndex)
                    {
                        MessageBox.Show("Can't set the starting day higher than other action days.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    int pastStartingDay = UnsavedEndingBranchData.EndingBranch.StartingDay;
                    int index = MainForm.CurrentEndingTree.EndingsList.IndexOf(SelectedEndingBranch);
                    UnsavedEndingBranchData.EndingBranch.StartingDay = (int)DayIndexNumeric.Value;
                    var newAction = MainForm.SetStartingAction(UnsavedEndingBranchData, false, index - 1);
                    if (newAction == null)
                    {
                        UnsavedEndingBranchData.EndingBranch.StartingDay = pastStartingDay;
                        MessageBox.Show("Could not edit the starting day in the initializing action. \nEither the day does not exist in the branch list, or is currently inaccessible based on the previous branches.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    MainForm.ResetStartingDayData(UnsavedEndingBranchData, index - 1);
                    if (ActionList[0].TargetAction.DayIndex != newAction.TargetAction.DayIndex)
                    {
                        EditHistory.undoActions.Add(new() { new ActionHistoryObj(EditType.Edit, 0, ActionList[0], newAction) });
                        EditHistory.redoActions.Clear();
                    }
                    ActionList[0] = newAction;
                    ActionList[selectedActions[0]].TargetAction.DayPart = -1;
                    EditActionVisualData(ActionList[0]);
                    InitializeActionStats();
                }
                return true;
            }
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


        private void ParentAction_DropdownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            Action_Dropdown.SelectedIndex = -1;
            ChangeActionOptionsByParent();
            if (SelectedAction == null)
            {
                NewActionParentIndex = ParentAction_Dropdown.SelectedIndex;
            }
            SetStatChangePreview();

        }

        private void ActionListViewOnSelectedIndexChanged(object sender, EventArgs e)
        {
            TargetActionButton.Enabled = true;
            var selectedActions = ActionListView.SelectedIndices;
            if (selectedActions.Count == 1)
            {             
                try
                {
                    if (selectedActions[0] >= 0 && SelectedAction != ActionList[selectedActions[0]])
                    {
                        SelectedAction = ActionList[selectedActions[0]];
                        if (SelectedAction.TargetAction.DayIndex == UnsavedEndingBranchData.EndingBranch.StartingDay && (UnsavedEndingBranchData.EndingBranch.StartingDay == 1 || SelectedAction.TargetAction.DayPart == -1))
                        {
                            OnlyDayIndexEditableIfFirstDaySelected();
                            return;
                        }
                        if (SelectedAction.TargetAction.DayIndex == 15 && SelectedAction.TargetAction.DayPart == 3)
                        {
                            NoEditingBreakdownDay();
                            return;
                        }
                        UpdateEditFieldsWithSelectedAction();
                    }
                }
                catch (ArgumentOutOfRangeException) { return; }
                return;
            }
            if (selectedActions.Count > 1)
            {
                OnlyActionEditableIfMultiSelected();
                return;
            }
            UpdateEditFieldsWithNewAction();

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
                DayIndexNumeric.Enabled = false;
                ChangeActionEnableBool(true);
                DayPart_Dropdown.Enabled = false;
                DayPart_Dropdown.SelectedIndex = -1;
                TargetActionButton.Text = "Save Action";
                SetStatChangePreview();
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
                    Action_Label.Visible = true;
                    Action_Dropdown.Visible = true;
                    StreamLevelNumeric.Visible = false;
                    StreamLevel_Label.Visible = false;
                    StreamTopic_Label.Visible = false;
                    StreamTopic_Dropdown.Visible = false;
                    StreamTopic_Dropdown.SelectedIndex = -1;
                    StreamLevelNumeric.Value = StreamLevelNumeric.Minimum;

                }
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case 0:
                        Action_Label.Visible = false;
                        Action_Dropdown.Visible = false;
                        StreamLevelNumeric.Visible = true;
                        StreamLevel_Label.Visible = true;
                        StreamTopic_Label.Visible = true;
                        StreamTopic_Dropdown.Visible = true;
                        Action_Dropdown.SelectedIndex = -1;
                        StreamTopic_Dropdown.SelectedIndex = NSODataManager.StreamTopicList.IndexOf(SelectedAction.TargetAction.Stream);
                        try
                        {
                            if (SelectedAction.Command == CmdType.Error) StreamLevelNumeric.Value = 6;
                            else StreamLevelNumeric.Value = int.Parse(SelectedAction.Command.ToString().Split('_')[1]);
                        }
                        catch { StreamLevelNumeric.Value = 1; }
                        break;
                    case 1:
                        Action_Dropdown.SelectedIndex = NSODataManager.HangOutList.IndexOf(SelectedAction.Command);
                        break;
                    case 2:
                        Action_Dropdown.SelectedIndex = NSODataManager.SleepList.IndexOf(SelectedAction.Command);
                        break;
                    case 3:
                        Action_Dropdown.SelectedIndex = NSODataManager.DrugList.IndexOf(SelectedAction.Command);
                        break;
                    case 4:
                        Action_Dropdown.SelectedIndex = NSODataManager.InternetList.IndexOf(SelectedAction.Command);
                        break;
                    case 5:
                        Action_Dropdown.SelectedIndex = NSODataManager.OutsideList.IndexOf(SelectedAction.Command);
                        break;
                    case 6:
                        Action_Dropdown.SelectedIndex = NSODataManager.DarknessList.IndexOf(SelectedAction.Command);
                        break;

                }
                IgnoreDMCheck.Checked = SelectedAction.TargetAction.IgnoreDM;
                SetStatChangePreview();
            }

            void UpdateEditFieldsWithNewAction()
            {
                DayIndexNumeric.Enabled = true;
                ChangeActionEnableBool(true);
                SelectedAction = null;
                SetDayMinimum();
                DayPart_Dropdown.SelectedIndex = NewAction.TargetAction.DayPart;
                ParentAction_Dropdown.SelectedIndex = NewActionParentIndex;
                if (ParentAction_Dropdown.SelectedIndex != 0)
                {
                    StreamTopic_Dropdown.SelectedIndex = -1;
                    StreamLevelNumeric.Value = StreamLevelNumeric.Minimum;
                    Action_Label.Visible = true;
                    Action_Dropdown.Visible = true;
                    StreamLevelNumeric.Visible = false;
                    StreamLevel_Label.Visible = false;
                    StreamTopic_Label.Visible = false;
                    StreamTopic_Dropdown.Visible = false;
                }
                TargetActionButton.Text = "Add Action";
                IgnoreDMCheck.Checked = NewAction.TargetAction.IgnoreDM;
                if (NewAction.Command == CmdType.None) return;
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case -1:
                        Action_Dropdown.SelectedIndex = -1;
                        break;
                    case 0:
                        Action_Dropdown.SelectedIndex = -1;
                        StreamTopic_Dropdown.SelectedIndex = NSODataManager.StreamTopicList.IndexOf(NewAction.TargetAction.Stream);
                        try
                        {
                            if (NewAction.Command == CmdType.Error) StreamLevelNumeric.Value = 6;
                            else StreamLevelNumeric.Value = int.Parse(NewAction.Command.ToString().Split('_')[1]);
                        }
                        catch { StreamLevelNumeric.Value = 1; }
                        Action_Label.Visible = false;
                        Action_Dropdown.Visible = false;
                        StreamLevelNumeric.Visible = true;
                        StreamLevel_Label.Visible = true;
                        StreamTopic_Label.Visible = true;
                        StreamTopic_Dropdown.Visible = true;
                        break;
                    case 1:
                        Action_Dropdown.SelectedIndex = NSODataManager.HangOutList.IndexOf(NewAction.Command);
                        break;
                    case 2:
                        Action_Dropdown.SelectedIndex = NSODataManager.SleepList.IndexOf(NewAction.Command);
                        break;
                    case 3:
                        Action_Dropdown.SelectedIndex = NSODataManager.DrugList.IndexOf(NewAction.Command);
                        break;
                    case 4:
                        Action_Dropdown.SelectedIndex = NSODataManager.InternetList.IndexOf(NewAction.Command);
                        break;
                    case 5:
                        Action_Dropdown.SelectedIndex = NSODataManager.OutsideList.IndexOf(NewAction.Command);
                        break;
                    case 6:
                        Action_Dropdown.SelectedIndex = NSODataManager.DarknessList.IndexOf(NewAction.Command);
                        break;
                }
                SetStatChangePreview();
            }
        }

        private void DayIndexNumericOnValueChanged(object sender, EventArgs e)
        {
            if (ActionList.Exists(a => a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex) && DayPart_Dropdown.SelectedIndex != -1 && ActionListView.SelectedIndices.Count == 0)
            {
                int index = ActionList.FindIndex(a => a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex);
                ActionListView.SelectedIndices.Add(index);
            }
            SetStatChangePreview();

        }
        private void DayPart_DropdownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActionList.Exists(a => a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex) && DayPart_Dropdown.SelectedIndex != -1 && ActionListView.SelectedIndices.Count == 0)
            {
                int index = ActionList.FindIndex(a => a.TargetAction.DayIndex == DayIndexNumeric.Value && a.TargetAction.DayPart == DayPart_Dropdown.SelectedIndex);
                ActionListView.SelectedIndices.Add(index);
            }
            SetStatChangePreview();
        }

        private void SaveEndingBranchButtonOnClick(object sender, EventArgs e)
        {
            SaveEndingBranch();

        }

        private void SaveEndingBranch()
        {
            UnsavedEndingBranchData.EndingBranch.AllActions = ActionList;
            var validBranch = UnsavedEndingBranchData.ValidateBranch();
            var branchConflicts = MainForm.ValidateFutureEndingBranches(MainForm.CurrentEndingTree.EndingsList.IndexOf(SelectedEndingBranch), UnsavedEndingBranchData);
            if (validBranch.Count > 0)
            {
                BranchErrorDetails errorWindow = new(validBranch, true);
                errorWindow.Show();
                return;
            }
            else if (branchConflicts.Count > 0)
            {
                BranchErrorDetails errorWindow = new(branchConflicts, true);
                errorWindow.Show();
                return;
            }
            (int, int, EndingType) checkEnding = ExpectedEnding;
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
                    return;
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
                        var msgDiffEnding = MessageBox.Show(msgDesc, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        switch (msgDiffEnding)
                        {
                            case DialogResult.Yes:
                                UnsavedEndingBranchData.EndingBranch.EndingToGet = checkEnding.Item3;
                                EndingToGet_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(UnsavedEndingBranchData.EndingBranch.EndingToGet); ;
                                break;
                            default: return;
                        }
                    }
                    break;
            }
            if (checkEnding.Item1 != 0)
            {
                if (!(ActionList[ActionList.Count - 1].TargetAction.DayIndex == checkEnding.Item1 && ActionList[ActionList.Count - 1].TargetAction.DayPart == checkEnding.Item2))
                {
                    var extraActions = MessageBox.Show($"Extra actions found. Ending branches must end on the action that will achieve the selected ending. Do you want to remove the extra actions?", "Extra actions found!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (extraActions)
                    {
                        case DialogResult.Yes:
                            for (int i = ActionList.Count - 1; i >= 0; i--)
                            {
                                i = ActionList.Count - 1;
                                if (ActionList[i].TargetAction.DayIndex == checkEnding.Item1 && ActionList[i].TargetAction.DayPart == checkEnding.Item2) break;
                                ActionListView.Items.RemoveAt(i);
                                ActionList.RemoveAt(i);
                            }
                            break;
                        default: return;
                    }
                }
            }
            int index = MainForm.CurrentEndingTree.EndingsList.IndexOf(SelectedEndingBranch);
            UnsavedEndingBranchData.EndingBranch.AllActions = ActionList;
            MainForm.CurrentEndingTree.EndingsList[index] = UnsavedEndingBranchData;
            MainForm.SelectedEnding = null;
            Close();
        }

        private void ResetEndingBranch()
        {
            var confirm = MessageBox.Show($"Are you sure you want to reset this ending branch? \nThis will reset it back to before you edited it.\nThis action can't be undone.", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;
            EditHistory.undoActions.Clear();
            EditHistory.redoActions.Clear();
            ActionList.Clear();
            ActionListView.Items.Clear();
            UnsavedEndingBranchData = new EndingBranchData(SelectedEndingBranch);
            ActionList = UnsavedEndingBranchData.EndingBranch.AllActions;
            SetDayMinimum();
            NewAction = new TargetActionData(ActionList[ActionList.Count - 1].TargetAction.DayIndex, -1, CmdType.None);
            int listCount = ActionList.Count;
            for (int i = 0; i < listCount; i++)
            {
                AddActionVisualData(ActionList[i], false);
                if (i == listCount - 1)
                {
                    InitializeBreakdown();
                }
            }
            //ActionListView.EndUpdate();
            EndingToGet_Dropdown.SelectedIndex = NSODataManager.EndingsList.IndexOf(UnsavedEndingBranchData.EndingBranch.EndingToGet);
            StressfulBreakdown_Check.Checked = UnsavedEndingBranchData.EndingBranch.IsStressfulBressdown;
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
            if (SelectedAction == null)
            {
                if (Action_Dropdown.SelectedIndex == -1) { return; }
                switch (ParentAction_Dropdown.SelectedIndex)
                {
                    case -1:
                        NewAction.Command = CmdType.None;
                        break;
                    case 0:
                        break;
                    case 1:
                        NewAction.Command = NSODataManager.HangOutList[Action_Dropdown.SelectedIndex];
                        break;
                    case 2:
                        NewAction.Command = NSODataManager.SleepList[Action_Dropdown.SelectedIndex];
                        break;
                    case 3:
                        NewAction.Command = NSODataManager.DrugList[Action_Dropdown.SelectedIndex];
                        break;
                    case 4:
                        NewAction.Command = NSODataManager.InternetList[Action_Dropdown.SelectedIndex];
                        break;
                    case 5:
                        NewAction.Command = NSODataManager.OutsideList[Action_Dropdown.SelectedIndex];
                        break;
                    case 6:
                        NewAction.Command = NSODataManager.DarknessList[Action_Dropdown.SelectedIndex];
                        break;
                }
            }
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
            if (SelectedAction == null)
            {
                if (index == -1)
                {
                    SetStatChangePreview();
                    return;
                }
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
                if (index == NSODataManager.StreamTopicList.IndexOf(AlphaType.Imbouron) && streamLevel == 6)
                {
                    NewAction.Command = CmdType.Error;
                }
                else { NewAction.Command = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevel}"); }
            }
            SetStatChangePreview();
        }

        private void EndingBranchEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SetEndingListViewData(true);
            if (ideasWindow != null) { ideasWindow.Close(); }
            if (usedWindow != null) { usedWindow.Close(); }
        }

        private List<TargetActionData> CreateListCopy()
        {
            List<TargetActionData> copiedActions = new List<TargetActionData>();
            if (ActionListView.SelectedIndices.Count == 0) return null;
            for (int i = 0; i < ActionListView.SelectedIndices.Count; i++)
            {
                TargetActionData copiedAction = ActionList[ActionListView.SelectedIndices[i]];
                if (copiedAction.TargetAction.DayPart == -1 || copiedAction.TargetAction.DayPart == 3 || copiedAction.TargetAction.DayIndex == 1) continue;
                copiedActions.Add(new TargetActionData(copiedAction.TargetAction.DayIndex, copiedAction.TargetAction.DayPart, copiedAction.Command, copiedAction.TargetAction.IgnoreDM));
            }
            return copiedActions;
        }

        private void ActionListView_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void ActionListViewOnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ActionListViewOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ActionListView.SelectedIndices.Count > 0)
            {
                DeleteSelectedActions();
            }
            if (!e.Control) return;
            if (e.KeyCode == Keys.C && !e.Shift)
            {
                if (ActionListView.SelectedIndices.Count > 0)
                {
                    CopyActions();
                }
                return;
            }
            if (e.KeyCode == Keys.X && !e.Shift)
            {
                if (ActionListView.SelectedIndices.Count > 0)
                {
                    CutActions();
                }
                return;
            }
            if (e.KeyCode == Keys.V && !e.Shift)
            {
                PasteCopiedActions();
                return;
            }
            else if (e.KeyCode == Keys.V)
            {
                PasteAsNewActions();
                return;
            }
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.Z)
            {
                ResetEndingBranch();
            }
            if ((e.Control && e.Shift && e.KeyCode == Keys.Z) || e.KeyCode == Keys.Y && !e.Shift)
            {
                RedoActionEdit();
                return;
            }
            else if (!e.Shift && e.KeyCode == Keys.Z)
            {
                UndoActionEdit();
                return;
            }
            if (e.KeyCode == Keys.S && !e.Shift)
            {
                SaveEndingBranch();
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
            object getCopiedData = Clipboard.GetDataObject().GetData(typeof(TargetActionData[]));
            try
            {
                List<ActionHistoryObj> listForUndoHistory = new();

                TargetActionData[] pasteList = getCopiedData as TargetActionData[];
                for (int i = 0; i < pasteList.Length; i++)
                {
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
                if (listForUndoHistory.Count > 0)
                {
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
                    if (i == 0) latestDayPart += ActionList[ActionList.Count - 1].CommandResult.daypart;
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
                EditHistory.undoActions.Add(listForUndoHistory);
                EditHistory.redoActions.Clear();
            }

            InitializeBreakdown();
            isDeleting = false;
        }


        private void LoveCount_Click(object sender, EventArgs e)
        {

        }

        private void PaperCount_Click(object sender, EventArgs e)
        {

        }

        private void NoMeds_Bool_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MV_bool_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BonusStatDelta_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EndingBranchEditorOnClick(object sender, EventArgs e)
        {
            ActionListView.SelectedIndices.Clear();
        }

        private void StreamLevel_Label_Click(object sender, EventArgs e)
        {

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

        private void resetEndingBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetEndingBranch();
        }

        private void saveEndingBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEndingBranch();
        }
    }
}
