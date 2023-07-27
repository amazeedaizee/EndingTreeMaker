using Newtonsoft.Json;
using ngov3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class EndingTreeForm : Form
    {
        private string _directoryToOpen = Properties.Settings.Default.Directory;
        private string _directoryToExport = Properties.Settings.Default.ExportDirectory;
        public EndingTreeData CurrentEndingTree;
        public EndingBranchData SelectedEnding;

        public bool DeletingEndings;

        public EndingTreeData SavedEndingTree;
        public EndingTreeForm()
        {
            InitializeComponent();
        }

        private EndingTreeData MakeFirstTree()
        {
            EndingBranchData TestEndingBranch = new EndingBranchData(1, NGO.EndingType.Ending_None, new List<TargetActionData>()
            {
                new(1, 2, ngov3.CmdType.None),

            }, true);


            EndingTreeData data = new(new() { TestEndingBranch });
            return data;
        }

        public void SetEndingListViewData(bool initializeStartingView = true)
        {
            EndingListView.Items.Clear();
            Console.WriteLine(CurrentEndingTree.EndingsList.Count);
            for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
            {
                AddEndingToListView(CurrentEndingTree.EndingsList[i]);
                if (i == 0) continue;
                if (initializeStartingView && CurrentEndingTree.EndingsList[i].EndingBranch.StartingDay > 1)
                {
                    if (SetStartingAction(CurrentEndingTree.EndingsList[i], false, i - 1) == null)
                        throw new Exception($"Tried to initialize starting stats for starting day of a specific ending branch, however no valid day exists to set stats.\n\nSpecific ending branch: \nIndex: {i + 1}. \nEnding To Get: {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[i].EndingBranch.EndingToGet]} \nStarting Day: {CurrentEndingTree.EndingsList[i].EndingBranch.StartingDay} \nIs Stressful Breakdown: {CurrentEndingTree.EndingsList[i].EndingBranch.IsStressfulBressdown}");
                    ResetStartingDayData(CurrentEndingTree.EndingsList[i], i - 1);
                }
            }
            Notes.Text = CurrentEndingTree.Notes;
        }

        public void AddEndingToListView(EndingBranchData endingData)
        {
            bool doesEndExist = NSODataManager.EndingNames.TryGetValue(endingData.EndingBranch.EndingToGet, out string name);
            ListViewItem ending = EndingListView.Items.Add(doesEndExist ? name : "");
            ending.SubItems.Add(endingData.EndingBranch.StartingDay.ToString());
            ending.SubItems.Add(endingData.EndingBranch.AllActions[endingData.EndingBranch.AllActions.Count - 1].TargetAction.DayIndex.ToString());
            ending.SubItems.Add(endingData.EndingBranch.IsStressfulBressdown ? "Yes" : "");
        }

        private void DeleteSelectedEndingData()
        {
            DeletingEndings = true;
            var selectedEndings = EndingListView.SelectedIndices;
            if (selectedEndings.Count == 0)
            {
                return;
            }
            if (selectedEndings[0] == 0)
            {
                MessageBox.Show($"Deleting the first branch of the tree isn't allowed.", "No", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirm = MessageBox.Show($"Are you sure you want to delete {(selectedEndings.Count > 1 ? "these endings" : "this ending")}? \n\nThis action can't be undone.", "Delete Ending?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;
            for (int i = selectedEndings.Count - 1; i >= 0; i--)
            {
                int index = selectedEndings[selectedEndings.Count - 1];
                for (int j = index; j < CurrentEndingTree.EndingsList.Count; j++)
                {
                    if (j == 0) break;
                    if (j == index) continue;
                    if (CurrentEndingTree.EndingsList[j].EndingBranch.StartingDay >= CurrentEndingTree.EndingsList[index].EndingBranch.StartingDay
                        && CurrentEndingTree.EndingsList[j].EndingBranch.StartingDay <= CurrentEndingTree.EndingsList[index].EndingBranch.AllActions[CurrentEndingTree.EndingsList[index].EndingBranch.AllActions.Count - 1].TargetAction.DayIndex)
                    {
                        bool checkIfValid = true;
                        for (int k = index; k >= 0; k--)
                        {
                            if (CurrentEndingTree.EndingsList[index].EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == CurrentEndingTree.EndingsList[k].EndingBranch.StartingDay) &&
                                (CurrentEndingTree.EndingsList[k].EndingBranch.StartingDay - 1) <= CurrentEndingTree.EndingsList[index].EndingBranch.AllActions[CurrentEndingTree.EndingsList[index].EndingBranch.AllActions.Count - 1].TargetAction.DayIndex) continue;
                            checkIfValid = false;
                        }
                        if (!checkIfValid)
                        {
                            MessageBox.Show($"Could not delete an Ending Branch: \n\nOne or more future ending branches relies on only this branch for their Starting Days.\n\nEnding Branch: \n\nBranch {j + 1} \nEnding To Get: {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[index].EndingBranch.EndingToGet]}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                CurrentEndingTree.EndingsList.RemoveAt(index);
                EndingListView.Items.RemoveAt(index);
            }
            SetEndingListViewData(true);
            SelectedEnding = null;
            DeleteEndingBranch.Enabled = false;
            EditEndingBranch.Enabled = false;
            DeletingEndings = false;
        }


        public TargetActionData SetStartingAction(EndingBranchData branch, bool isNewEnding = true, int index = -1)
        {
            bool foundValidDay = false;
            if (index == -1) index = CurrentEndingTree.EndingsList.Count - 1;
            for (int i = index; i >= 0; i--)
            {
                List<TargetActionData> actions = CurrentEndingTree.EndingsList[i].EndingBranch.AllActions;
                if (branch.EndingBranch.StartingDay > actions[actions.Count - 1].TargetAction.DayIndex)
                {
                    if (isNewEnding)
                        return null;
                    else continue;
                }
                for (int j = actions.Count - 1; j >= 0; j--)
                {
                    if (actions[j].TargetAction.DayIndex == branch.EndingBranch.StartingDay)
                    {
                        foundValidDay = true;
                        continue;
                    }
                    if (foundValidDay && actions[j].TargetAction.DayIndex == (branch.EndingBranch.StartingDay - 1))
                    {
                        var refAction = actions[j];
                        var newAction = new TargetActionData(branch.EndingBranch.StartingDay, -1, ngov3.CmdType.None);
                        newAction.Followers = refAction.Followers;
                        newAction.Stress = refAction.Stress;
                        newAction.Affection = refAction.Affection;
                        newAction.Darkness = refAction.Darkness;
                        newAction.StreamStreak = refAction.StreamStreak;
                        newAction.PreAlertBonus = refAction.PreAlertBonus;
                        newAction.Cinephile = refAction.Cinephile;
                        newAction.Impact = refAction.Impact;
                        newAction.GamerGirl = refAction.GamerGirl;
                        newAction.Experience = refAction.Experience;
                        newAction.Communication = refAction.Communication;
                        newAction.RabbitHole = refAction.RabbitHole;
                        return newAction;
                    }
                }
            }
            return null;
        }

        public void SetStartingDayData(EndingBranchData branch)
        {
            for (int i = CurrentEndingTree.EndingsList.Count - 1; i >= 0; i--)
            {
                EndingBranchData endingToImport = CurrentEndingTree.EndingsList[i];
                List<TargetActionData> actions = CurrentEndingTree.EndingsList[i].EndingBranch.AllActions;
                if (actions.Exists(a => a.TargetAction.DayIndex == (branch.EndingBranch.StartingDay - 1)))
                {
                    branch.StreamIdeaList.AddRange(endingToImport.StreamIdeaList.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay));
                    branch.StreamUsedList.AddRange(endingToImport.StreamUsedList.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay));
                    branch.LoveCounter.AddRange(endingToImport.LoveCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay));
                    branch.PsycheCounter.AddRange(endingToImport.PsycheCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay));
                    branch.IgnoreCounter.AddRange(endingToImport.IgnoreCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay));

                    branch.hasGalacticRail = endingToImport.hasGalacticRail;
                    branch.NoMeds = endingToImport.NoMeds;
                    branch.isHorror = endingToImport.isHorror;
                    branch.isReallyLove = endingToImport.isReallyLove;
                    branch.isStressed = endingToImport.isStressed;
                    branch.isReallyStressed = endingToImport.isReallyStressed;
                    branch.isTrauma = endingToImport.isTrauma;
                    branch.isVideo = endingToImport.isVideo;
                    branch.is150M = endingToImport.is150M;
                    branch.is300M = endingToImport.is300M;
                    branch.is500M = endingToImport.is500M;
                    branch.isMaxFollowers = endingToImport.isMaxFollowers;
                    return;
                }

            }
        }

        public void ResetStartingDayData(EndingBranchData branch, int index = -1)
        {
            branch.StreamIdeaList.RemoveAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
            branch.StreamUsedList.RemoveAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
            branch.LoveCounter.RemoveAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
            branch.PsycheCounter.RemoveAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
            branch.IgnoreCounter.RemoveAll(i => i.DayIndex < branch.EndingBranch.StartingDay);

            if (index == -1)
                index = CurrentEndingTree.EndingsList.IndexOf(branch) - 1;
            for (int i = index; i >= 0; i--)
            {
                EndingBranchData endingToImport = CurrentEndingTree.EndingsList[i];
                List<TargetActionData> actions = CurrentEndingTree.EndingsList[i].EndingBranch.AllActions;
                if (actions.Exists(a => a.TargetAction.DayIndex == (branch.EndingBranch.StartingDay - 1)))
                {
                    var newIdeas = endingToImport.StreamIdeaList.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
                    var newUsed = endingToImport.StreamUsedList.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
                    var newSexNum = endingToImport.LoveCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
                    var newPsyNum = endingToImport.PsycheCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay);
                    var newIgnoreNum = endingToImport.IgnoreCounter.FindAll(i => i.DayIndex < branch.EndingBranch.StartingDay);

                    branch.StreamIdeaList.InsertRange(0, newIdeas);
                    branch.StreamUsedList.InsertRange(0, newUsed);
                    branch.LoveCounter.InsertRange(0, newSexNum);
                    branch.PsycheCounter.InsertRange(0, newPsyNum);
                    branch.IgnoreCounter.InsertRange(0, newIgnoreNum);

                    if ((endingToImport.hasGalacticRail.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.hasGalacticRail.isEventing)
                        branch.hasGalacticRail = endingToImport.hasGalacticRail;
                    if ((endingToImport.NoMeds.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.NoMeds.isEventing)
                        branch.NoMeds = endingToImport.NoMeds;
                    if ((endingToImport.isHorror.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isHorror.isEventing)
                        branch.isHorror = endingToImport.isHorror;
                    if ((endingToImport.isReallyLove.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isReallyLove.isEventing)
                        branch.isReallyLove = endingToImport.isReallyLove;
                    if ((endingToImport.isStressed.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isStressed.isEventing)
                        branch.isStressed = endingToImport.isStressed;
                    if ((endingToImport.isReallyStressed.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isReallyStressed.isEventing)
                        branch.isReallyStressed = endingToImport.isReallyStressed;
                    if ((endingToImport.isTrauma.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isTrauma.isEventing)
                        branch.isTrauma = endingToImport.isTrauma;
                    if ((endingToImport.isVideo.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.isVideo.isEventing)
                        branch.isVideo = endingToImport.isVideo;
                    if ((endingToImport.is150M.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.is150M.isEventing)
                        branch.is150M = endingToImport.is150M;
                    if ((endingToImport.is300M.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.is300M.isEventing)
                        branch.is300M = endingToImport.is300M;
                    if ((endingToImport.is500M.DayIndex < branch.EndingBranch.StartingDay) && endingToImport.is500M.isEventing)
                        branch.is500M = endingToImport.is500M;
                    if ((endingToImport.isMaxFollowers.DayIndex < branch.isMaxFollowers.DayIndex) && endingToImport.isMaxFollowers.isEventing) branch.isMaxFollowers = endingToImport.isMaxFollowers;
                    return;

                }
            }
        }

        public List<(string, string)> ValidateEndingBranches(int index, EndingBranchData branch)
        {
            List<(string branch, string errorMsg)> errorBranches = new();
            int oldLatestDay = CurrentEndingTree.EndingsList[index].EndingBranch.AllActions[CurrentEndingTree.EndingsList[index].EndingBranch.AllActions.Count - 1].TargetAction.DayIndex;
            var oldPresentBranch = CurrentEndingTree.EndingsList[index];
            for (int i = index; i < CurrentEndingTree.EndingsList.Count; i++)
            {
                if (i == 0) continue;
                if (i == index) continue;
                var futureBranch = CurrentEndingTree.EndingsList[i];
                int futureStartDay = futureBranch.EndingBranch.StartingDay;
                int futurelatestDay = futureBranch.EndingBranch.AllActions[futureBranch.EndingBranch.AllActions.Count - 1].TargetAction.DayIndex;
                var futureIdeas = futureBranch.StreamIdeaList;
                var futureStreamed = futureBranch.StreamUsedList;
                var presentIdeas_Condensed = branch.StreamIdeaList.FindAll(i => i.DayIndex < futureStartDay);
                var futureIdeas_Condensed = futureBranch.StreamIdeaList.FindAll(i => i.DayIndex < futureStartDay);
                var presentStreamed_Condensed = branch.StreamUsedList.FindAll(i => i.DayIndex < futureStartDay);
                var futureStreamed_Condensed = futureBranch.StreamUsedList.FindAll(i => i.DayIndex < futureStartDay);
                int latestDay = branch.EndingBranch.AllActions[branch.EndingBranch.AllActions.Count - 1].TargetAction.DayIndex;

                var future150M = !branch.is150M.isEventing && oldPresentBranch.is150M.isEventing && futureBranch.is150M.isEventing ? futureBranch.EndingBranch.AllActions.FirstOrDefault(a => a.Followers >= 1500000 && branch.IsNotMidnightEvents(a, branch.hasGalacticRail, branch.is150M, branch.is300M, branch.is500M)) : null;
                var future300M = !branch.is300M.isEventing && oldPresentBranch.is300M.isEventing && futureBranch.is300M.isEventing ? futureBranch.EndingBranch.AllActions.FirstOrDefault(a => (future150M == null || a != future150M) && a.Followers >= 3000000 && branch.IsNotMidnightEvents(a, branch.hasGalacticRail, branch.is150M, branch.is300M, branch.is500M)) : null;
                var future500M = !branch.is500M.isEventing && oldPresentBranch.is500M.isEventing && futureBranch.is500M.isEventing ? futureBranch.EndingBranch.AllActions.FirstOrDefault(a => (future150M == null || a != future150M) && (future300M == null || a != future300M) && a.Followers >= 5000000 && branch.IsNotMidnightEvents(a, branch.hasGalacticRail, branch.is150M, branch.is300M, branch.is500M)) : null;
                var futureMaxFollowers = futureBranch.EndingBranch.AllActions.FirstOrDefault(a => (future150M == null || a != future150M) && (future300M == null || a != future300M) && (future500M == null || a != future500M) && a.Followers >= 9999999 && branch.IsNotMidnightEvents(a, branch.hasGalacticRail, branch.is150M, branch.is300M, branch.is500M));

                string futureBranchName = $"Branch {i + 1}. {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[i].EndingBranch.EndingToGet]}";

                if (futureStartDay > latestDay && latestDay < oldLatestDay && futureStartDay <= oldLatestDay)
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch's starting day (Day {CurrentEndingTree.EndingsList[i].EndingBranch.StartingDay}) is reliant on a day from this branch that now doesn't exist."));
                }

                // Check For Event Counter Desync

                if (oldPresentBranch.isStressed.isEventing && !branch.isStressed.isEventing && (futureBranch.isReallyStressed.isEventing || futureBranch.isHorror.isEventing))
                {
                    string flagName = "Very Stressed";
                    if (futureBranch.isHorror.isEventing) flagName = "Horror";
                    errorBranches.Add(new(futureBranchName, $"Listed branch relies on this branch's Stressed flag, due to the listed branch's {flagName} flag being active. \nThis branch's Stressed flag is currently inactive."));
                }
                if (oldPresentBranch.isStressed.isEventing && branch.isStressed.isEventing && futureBranch.isReallyStressed.isEventing && branch.isStressed.DayIndex >= futureBranch.isReallyStressed.DayIndex)
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch currently relies on this branch's Stressed flag, due to the listed branch's Very Stressed flag being active. \nThis branch's Stressed flag is active a day(s) after or the same day the listed branch's Very Stressed flag became active (Day {branch.isReallyStressed.DayIndex}), which isn't allowed. \nThis branch's Stressed flag became active on Day {branch.isStressed.DayIndex}. \nListed branch's Very Stressed flag became active on Day {futureBranch.isReallyStressed.DayIndex}."));
                }
                if (oldPresentBranch.isStressed.isEventing && branch.isStressed.isEventing && futureBranch.isHorror.isEventing && branch.isStressed.DayIndex >= 25)
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch currently relies on this branch's Stressed flag, due to the listed branch's Horror flag being active. \nThis branch's Stressed flag is active a day(s) after or the same day the listed branch's Horror flag became active (Day {branch.isHorror.DayIndex}), which isn't allowed. \nThis branch's Stressed flag became active on Day {branch.isStressed.DayIndex}."));
                }
                if (branch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1 || a.Command == CmdType.Darkness_1) && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1 || a.Command == CmdType.Darkness_1))
                {
                    int darkDay = branch.EndingBranch.AllActions.Find(a => a.Command == CmdType.DarknessS1 || a.Command == CmdType.Darkness_1).TargetAction.DayIndex;
                    int futureDarkDay = futureBranch.EndingBranch.AllActions.Find(a => a.Command == CmdType.DarknessS1 || a.Command == CmdType.Darkness_1).TargetAction.DayIndex;
                    errorBranches.Add(new(futureBranchName, $"Listed branch has Stressed events even though Stressed events have already been done on previous days in this branch.\nDay Stressed Events were done in this branch: Day {darkDay} \nDay Stressed Events were done in listed branch: Day {futureDarkDay}"));
                }
                if (oldPresentBranch.isReallyStressed.isEventing && !branch.isReallyStressed.isEventing && futureBranch.isHorror.isEventing)
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch currently relies on this branch's Very Stressed flag, due to the listed branch's Horror flag being active. \nThis branch's Very Stressed flag is currently inactive."));
                }
                if (oldPresentBranch.isReallyStressed.isEventing && branch.isReallyStressed.isEventing && futureBranch.isHorror.isEventing && branch.isReallyStressed.DayIndex >= 25)
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch currently relies on this branch's Very Stressed flag, due to the listed branch's Horror flag being active. \nThis branch's Very Stressed flag is active a day(s) after or the same day the listed branch's Horror flag became active (Day {branch.isHorror.DayIndex}), which isn't allowed. \nThis branch's Very Stressed flag became active on Day {branch.isReallyStressed.DayIndex}."));
                }
                if (oldPresentBranch.isReallyStressed.isEventing && !branch.isReallyStressed.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.Stress > 100))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains actions that have more than 100 Stress, even though this branch's Very Stressed flag is inactive."));
                }
                if (branch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS2 || a.Command == CmdType.Darkness_2) && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS2 || a.Command == CmdType.Darkness_2))
                {
                    int darkDay = branch.EndingBranch.AllActions.Find(a => a.Command == CmdType.DarknessS2 || a.Command == CmdType.Darkness_2).TargetAction.DayIndex;
                    int futureDarkDay = futureBranch.EndingBranch.AllActions.Find(a => a.Command == CmdType.DarknessS2 || a.Command == CmdType.Darkness_2).TargetAction.DayIndex;
                    errorBranches.Add(new(futureBranchName, $"Listed branch has Very Stressed events even though Very Stressed events have already been done on previous days in this branch.\n Day Very Stressed Events were done in this branch: Day {darkDay} \nDay Very Stressed Events were done in listed branch: Day {futureDarkDay}"));
                }
                if (oldPresentBranch.isReallyStressed.isEventing && branch.isReallyStressed.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex < branch.isReallyStressed.DayIndex && a.Stress > 100))
                {
                    string allStressMsgs = "";
                    var Stress = futureBranch.EndingBranch.AllActions.FindAll(a => a.TargetAction.DayIndex < branch.isReallyStressed.DayIndex && a.Stress > 100);
                    for (int d = 0; d < Stress.Count; d++)
                    {
                        string stressMsg = $"\nDay {Stress[d].TargetAction.DayIndex}, {NSODataManager.DayPartNames[Stress[d].TargetAction.DayPart]}: {NSODataManager.CmdName(Stress[d].Command)}";
                        allStressMsgs += stressMsg;
                    }
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains actions that have more than 100 Stress before the day this branch's Very Stressed flag became active. (Day {branch.isStressed.DayIndex}) \nActions from listed branch containing more than 100 Stress: {allStressMsgs}"));
                }
                if (oldPresentBranch.isReallyLove.isEventing && !branch.isReallyLove.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeZikka))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains \"Go Out: Ame's Parents\", even though this branch's Visited Parents flag is inactive."));
                }
                if (oldPresentBranch.isReallyLove.isEventing && !branch.isReallyLove.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.Affection > 100))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains actions that have more than 100 Affection, even though this branch's Visited Parents flag is inactive."));
                }
                if (oldPresentBranch.isReallyLove.isEventing && branch.isReallyLove.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex < branch.isReallyLove.DayIndex && a.Affection > 100))
                {
                    string allAffectionMsgs = "";
                    var Affection = futureBranch.EndingBranch.AllActions.FindAll(a => a.TargetAction.DayIndex < branch.isReallyStressed.DayIndex && a.Affection > 100);
                    for (int d = 0; d < Affection.Count; d++)
                    {
                        string affectionMsg = $"\nDay {Affection[d].TargetAction.DayIndex}, {NSODataManager.DayPartNames[Affection[d].TargetAction.DayPart]}: {NSODataManager.CmdName(Affection[d].Command)}";
                        allAffectionMsgs += affectionMsg;
                    }
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains actions that have more than 100 Affection before the day this branch's Visited Parents flag became active. (Day {branch.isReallyLove.DayIndex} \nActions from listed branch containing more than 100 Stress: {allAffectionMsgs}"));
                }
                if (oldPresentBranch.isReallyLove.isEventing && futureStartDay <= 24 && branch.isReallyLove.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == 24) && !futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeZikka))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch does not contain \"Go Out: Ame's Parents\", even though this branch's Visited Parents flag is currently active."));
                }
                if (oldPresentBranch.isVideo.isEventing && !branch.isVideo.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeOdaiba))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains \"Go Out: Music Video\", even though this branch's Did Music Video flag is inactive."));
                }
                if (oldPresentBranch.isVideo.isEventing && futureStartDay <= 27 && branch.isVideo.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == 27) && !futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeOdaiba))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch does not contain \"Go Out: Music Video\", even though this branch's Did Music Video flag is currently active."));
                }
                if (oldPresentBranch.NoMeds.isEventing && branch.NoMeds.isEventing &&
                    futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex >= branch.NoMeds.DayIndex
                    && (a.Command.ToString().Contains("Overdose") || a.Command.ToString().Contains("Happa") || a.Command == CmdType.OkusuriPsyche)))
                {
                    string allDrugMsgs = "";
                    var drugs = futureBranch.EndingBranch.AllActions.FindAll(a => a.TargetAction.DayIndex >= branch.NoMeds.DayIndex
                    && (a.Command.ToString().Contains("Overdose") || a.Command.ToString().Contains("Happa") || a.Command == CmdType.OkusuriPsyche));
                    for (int d = 0; d < drugs.Count; d++)
                    {
                        string drugMsg = $"\nDay {drugs[d].TargetAction.DayIndex}, {NSODataManager.DayPartNames[drugs[d].TargetAction.DayPart]}: {NSODataManager.CmdName(drugs[d].Command)}";
                        allDrugMsgs += drugMsg;
                    }
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains non-moderate medication use after the day this branch's No Meds flag became active (Day {branch.NoMeds.DayIndex}).\nActions from listed branch containing non-moderate medication use: {allDrugMsgs}"));
                }
                if (oldPresentBranch.hasGalacticRail.isEventing && !branch.hasGalacticRail.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeGinga))
                {
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains \"Go Out: Galactic Rail\", even though this branch's Galactic rail flag is inactive."));
                }
                if (oldPresentBranch.hasGalacticRail.isEventing && branch.hasGalacticRail.isEventing && futureBranch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex < branch.hasGalacticRail.DayIndex && a.Command == CmdType.OdekakeGinga))
                {
                    var galaxy = futureBranch.EndingBranch.AllActions.Find(a => a.TargetAction.DayIndex < branch.hasGalacticRail.DayIndex && a.Command == CmdType.OdekakeGinga);
                    errorBranches.Add(new(futureBranchName, $"Listed branch contains \"Go Out: Galactic Rail\" before the day this branch's Galactic rail flag became active. (Day {branch.hasGalacticRail.DayIndex}) \nGalactic Rail action from listed branch: (Day {galaxy.TargetAction.DayIndex}, {NSODataManager.DayPartNames[galaxy.TargetAction.DayPart]}: {NSODataManager.CmdName(galaxy.Command)})"));
                }
                if (futureBranch.isMaxFollowers.isEventing)
                {
                    if (!branch.is150M.isEventing && oldPresentBranch.is150M.isEventing && futureBranch.is150M.isEventing && future150M == null)
                    {
                        errorBranches.Add(new(futureBranchName, $"Listed branch has max followers, and relies on this branch's 300M Extra Milestone flag which now doesn't exist, however no valid day to substitute the missing flag exists on the listed branch."));
                    }
                    if (!branch.is300M.isEventing && oldPresentBranch.is300M.isEventing && futureBranch.is300M.isEventing && future300M == null)
                    {
                        errorBranches.Add(new(futureBranchName, $"Listed branch has max followers, and relies on this branch's 300M Extra Milestone flag which now doesn't exist, however no valid day to substitute the missing flag exists on the listed branch."));
                    }
                    if (!branch.is500M.isEventing && oldPresentBranch.is500M.isEventing && futureBranch.is500M.isEventing && future500M == null)
                    {
                        errorBranches.Add(new(futureBranchName, $"Listed branch has max followers, and relies on this branch's 500M Extra Milestone flag which now doesn't exist, however no valid day to substitute the missing flag exists on the listed branch."));
                    }
                    if (futureMaxFollowers == null && futureBranch.EndingBranch.EndingToGet == NGO.EndingType.Ending_Ideon)
                    {
                        errorBranches.Add(new(futureBranchName, $"Listed branch has max followers, however the day this is on is not valid for the Internet Runaway Angel ending."));
                    }
                }

                // Check For Stream Idea Desync

                for (int j = 0; j < futureStreamed.Count; j++)
                {
                    var oldPresentIdeas = oldPresentBranch.StreamIdeaList.FindAll(i => i.DayIndex <= futureStartDay);
                    var presentStreamIdea = presentIdeas_Condensed.Find(i => i.Idea == futureStreamed[j].UsedStream);
                    var usedStreamIdea = futureIdeas_Condensed.Find(i => i.Idea == futureStreamed[j].UsedStream);
                    if (usedStreamIdea == null)
                        continue;
                    if (futureStreamed[j].UsedStream.ToString().Contains("Darkness"))
                        continue;
                    if (presentStreamIdea == null && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == futureStreamed[j].UsedStream) && oldPresentIdeas.Exists(a => a.Idea == futureStreamed[j].UsedStream))
                        errorBranches.Add(new(futureBranchName, $"Stream from listed branch (Day {futureStreamed[j].DayIndex}, {NSODataManager.CmdName(futureStreamed[j].UsedStream)}) relies on a now non-existent stream idea from this branch."));
                    if (presentStreamIdea != null && presentStreamIdea.DayIndex >= futureStartDay)
                        continue;
                    if (presentStreamIdea != null && presentStreamIdea.DayIndex <= futureStreamed[j].DayIndex)
                        continue;
                    else if (presentStreamIdea != null && presentStreamIdea.DayIndex > futureStreamed[j].DayIndex && futureBranch.EndingBranch.AllActions.Exists(a => a.Command == futureStreamed[j].UsedStream && oldPresentIdeas.Exists(a => a.Idea == futureStreamed[j].UsedStream)))
                        errorBranches.Add(new(futureBranchName, $"Stream from listed branch (Day {futureStreamed[j].DayIndex}, {NSODataManager.CmdName(futureStreamed[j].UsedStream)}) relies on a now delayed stream idea from this branch. \n(Day {presentStreamIdea.DayIndex}, {NSODataManager.DayPartNames[presentStreamIdea.DayPart]}, {NSODataManager.CmdName(presentStreamIdea.Idea)})"));
                }

                // Check For Used Stream Desync

                for (int u = 0; u < futureIdeas.Count; u++)
                {
                    var oldPresentUsed = oldPresentBranch.StreamUsedList.FindAll(i => i.DayIndex <= futureStartDay);
                    bool isIdeaActionExist = futureIdeas.Exists(i => i.DayIndex >= futureStartDay && i.Idea == futureIdeas[u].Idea);
                    if (futureIdeas[u].Idea.ToString().Contains("Darkness_"))
                        continue;
                    if (futureIdeas[u].Idea.ToString().Contains("Angel_"))
                        continue;
                    if (futureIdeas[u].Idea == CmdType.Error && futureStreamed.Exists(i => i.UsedStream == CmdType.Imbouron_5) && oldPresentUsed.Exists(i => i.UsedStream == CmdType.Imbouron_5))
                    {
                        if (isIdeaActionExist && !presentStreamed_Condensed.Exists(i => i.UsedStream == CmdType.Imbouron_5))
                            errorBranches.Add(new(futureBranchName, $"Stream idea from listed branch (Day {futureIdeas[u].DayIndex}, {NSODataManager.DayPartNames[futureIdeas[u].DayPart]}, {NSODataManager.CmdName(futureIdeas[u].Idea)}) relies on a now non-existent streamed stream of a lower level from this branch."));
                        else if (isIdeaActionExist && presentStreamed_Condensed.Exists(i => i.UsedStream == CmdType.Imbouron_5 && i.DayIndex > futureIdeas[u].DayIndex))
                        {
                            var lastConspire = presentStreamed_Condensed.Find(i => i.UsedStream == CmdType.Imbouron_5 && i.DayIndex > futureIdeas[u].DayIndex);
                            errorBranches.Add(new(futureBranchName, $"Stream idea from listed branch (Day {futureIdeas[u].DayIndex}, {NSODataManager.DayPartNames[futureIdeas[u].DayPart]}, {NSODataManager.CmdName(futureIdeas[u].Idea)}) relies on a now delayed streamed stream of a lower level from this branch. \n(Day {lastConspire.DayIndex}, {NSODataManager.CmdName(lastConspire.UsedStream)})"));
                        }
                        continue;
                    }
                    else if (futureIdeas[u].Idea.ToString().Contains("_1")) continue;
                    else if (futureIdeas[u].Idea != CmdType.Error)
                    {
                        var stream = futureIdeas[u].Idea.ToString().Split('_');
                        string streamTopic = stream[0];
                        int streamLevel = (int.Parse(stream[1]) - 1);
                        CmdType pastStream = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevel}");
                        Console.WriteLine($"Past Stream: {NSODataManager.CmdName(pastStream)}");
                        if (futureStreamed.Exists(i => i.UsedStream == pastStream) && oldPresentUsed.Exists(i => i.UsedStream == pastStream))
                        {
                            if (isIdeaActionExist && !presentStreamed_Condensed.Exists(i => i.UsedStream == pastStream) && oldPresentUsed.Exists(i => i.UsedStream == pastStream))
                                errorBranches.Add(new(futureBranchName, $"Stream idea from listed branch (Day {futureIdeas[u].DayIndex}, {NSODataManager.DayPartNames[futureIdeas[u].DayPart]}, {NSODataManager.CmdName(futureIdeas[u].Idea)}) relies on a now non-existent streamed stream of a lower level this branch."));
                            else if (isIdeaActionExist && presentStreamed_Condensed.Exists(i => i.UsedStream == pastStream && i.DayIndex > futureIdeas[u].DayIndex))
                            {
                                var pastStreamed = presentStreamed_Condensed.Find(i => i.UsedStream == CmdType.Imbouron_5 && i.DayIndex > futureIdeas[u].DayIndex);
                                errorBranches.Add(new(futureBranchName, $"Stream idea from listed branch (Day {futureIdeas[u].DayIndex}, {NSODataManager.DayPartNames[futureIdeas[u].DayPart]}, {NSODataManager.CmdName(futureIdeas[u].Idea)}) relies on a now delayed streamed stream of a lower level from this branch. \n(Day {pastStreamed.DayIndex}, {NSODataManager.CmdName(pastStreamed.UsedStream)})"));
                            }
                        }
                    }

                }

                // Check for future streams done during this stream's hiatus period (if applicable)

                if (futureStreamed.Exists(u => u.UsedStream == CmdType.Yamihaishin_3))
                {
                    var hiatus = futureStreamed.Find(u => u.UsedStream == CmdType.Yamihaishin_3);
                    string allStreamMsgs = "";
                    var stream = futureBranch.EndingBranch.AllActions.FindAll(a => a.TargetAction.DayIndex > hiatus.DayIndex && a.TargetAction.DayIndex <= (hiatus.DayIndex + 2) && a.TargetAction.Action == ActionType.Haishin);
                    if (stream.Count > 0)
                    {
                        for (int d = 0; d < stream.Count; d++)
                        {
                            string streamMsg = $"\nDay {stream[d].TargetAction.DayIndex}, {NSODataManager.DayPartNames[stream[d].TargetAction.DayPart]}: {NSODataManager.CmdName(stream[d].Command)}";
                            allStreamMsgs += streamMsg;
                        }
                        errorBranches.Add(new(futureBranchName, $"Listed branch contains streams done during this branch's hiatus period. (Day {hiatus.DayIndex}).\nActions from listed branch containing said streams: {allStreamMsgs}"));
                    }
                }

                if (futureStartDay <= latestDay) break;
            }
            return errorBranches;

        }

        private void EndingTreeForm_Load(object sender, EventArgs e)
        {
            var endingTree = MakeFirstTree();
            CurrentEndingTree = endingTree;
            SetEndingListViewData();
            DeleteEndingBranch.Enabled = false;
            EditEndingBranch.Enabled = false;
        }

        private void AddEndingButton_Click(object sender, EventArgs e)
        {
            AddEndingBranch newEnding = new(this);
            newEnding.ShowDialog();
        }


        private void SaveEndingTreeButton_Click(object sender, EventArgs e)
        {
            Stream stream;
            SaveFileDialog saveEndingTree = new SaveFileDialog();
            saveEndingTree.InitialDirectory = !string.IsNullOrEmpty(_directoryToOpen) ? _directoryToOpen : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveEndingTree.Filter = "JSON File (*.json)|*.json";
            saveEndingTree.FilterIndex = 1;
            saveEndingTree.RestoreDirectory = true;
            saveEndingTree.OverwritePrompt = true;
            if (saveEndingTree.ShowDialog() == DialogResult.OK)
            {
                var treeData = JsonConvert.SerializeObject(CurrentEndingTree, Formatting.Indented);
                if ((stream = saveEndingTree.OpenFile()) != null)
                {
                    _directoryToOpen = Path.GetDirectoryName(saveEndingTree.FileName);
                    Properties.Settings.Default.Directory = _directoryToOpen;
                    stream.Write(Encoding.UTF8.GetBytes(treeData), 0, Encoding.UTF8.GetByteCount(treeData));
                    stream.Close();
                    MessageBox.Show("Successfully saved Ending Tree!", "Success", MessageBoxButtons.OK);
                }
            }


        }

        private void LoadEndingTreeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openEndingTree = new OpenFileDialog();
            openEndingTree.InitialDirectory = !string.IsNullOrEmpty(_directoryToOpen) ? _directoryToOpen : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openEndingTree.Filter = "JSON File (*.json)|*.json";
            openEndingTree.FilterIndex = 1;
            openEndingTree.RestoreDirectory = true;
            if (openEndingTree.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileContent = openEndingTree.OpenFile();
                    _directoryToOpen = Path.GetDirectoryName(openEndingTree.FileName);
                    Properties.Settings.Default.Directory = _directoryToOpen;
                    using (StreamReader reader = new StreamReader(fileContent))
                    {
                        var importedTreeData = reader.ReadToEnd();
                        var newTreeData = JsonConvert.DeserializeObject<EndingTreeData>(importedTreeData);
                        CurrentEndingTree = newTreeData;
                        SetEndingListViewData(true);
                        DeleteEndingBranch.Enabled = false;
                        EditEndingBranch.Enabled = false;
                    }
                }
                catch { MessageBox.Show("Could not open JSON file, either the JSON file is invalid or the JSON file does not represent an Ending Tree.", "Could not read JSON file", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }

        private void DeleteEndingBranch_Click(object sender, EventArgs e)
        {
            DeleteSelectedEndingData();
        }

        private void EditEndingBranch_Click(object sender, EventArgs e)
        {
            var selectedEndings = EndingListView.SelectedIndices;
            if (SelectedEnding == null || selectedEndings.Count == 0 || selectedEndings.Count > 1)
            {
                return;
            }
            EndingBranchEditor editor = new(SelectedEnding, this);
            editor.ShowDialog();
        }

        private void EndingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedEndings = EndingListView.SelectedIndices;
            if (DeletingEndings) { return; }
            if (selectedEndings.Count == 1)
            {
                SelectedEnding = CurrentEndingTree.EndingsList[selectedEndings[0]];
                DeleteEndingBranch.Enabled = true;
                EditEndingBranch.Enabled = true;
                Console.WriteLine("Selected Ending Ignored DM's: " + SelectedEnding.IgnoreCounter.Count);
                return;
            }
            if (selectedEndings.Count == 0)
            {
                SelectedEnding = null;
                DeleteEndingBranch.Enabled = false;
                EditEndingBranch.Enabled = false;
                return;
            }
            DeleteEndingBranch.Enabled = true;
            EditEndingBranch.Enabled = false;
        }

        private void EndingListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { DeleteSelectedEndingData(); }
        }

        private void EndingListView_DoubleClick(object sender, EventArgs e)
        {
            var selectedEndings = EndingListView.SelectedIndices;
            if (SelectedEnding == null || selectedEndings.Count == 0)
            {
                AddEndingBranch newEnding = new(this);
                newEnding.ShowDialog();
                return;
            }
            if (selectedEndings.Count == 0 || selectedEndings.Count > 1)
            {
                EditEndingBranch.Enabled = false;
                return;
            }
            EndingBranchEditor editor = new(SelectedEnding, this);
            editor.ShowDialog();
        }

        private void EndingTreeForm_Leave(object sender, EventArgs e)
        {
            DeleteEndingBranch.Enabled = false;
            EditEndingBranch.Enabled = false;
        }

        private void Notes_TextChanged(object sender, EventArgs e)
        {
            CurrentEndingTree.Notes = Notes.Text;
        }

        private void ExportEndingTreeButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveEndingTree = new SaveFileDialog();
            saveEndingTree.InitialDirectory = !string.IsNullOrEmpty(_directoryToExport) ? _directoryToExport : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveEndingTree.Filter = "CSV File (*.csv)|*.csv";
            saveEndingTree.FilterIndex = 1;
            saveEndingTree.RestoreDirectory = true;
            saveEndingTree.OverwritePrompt = true;
            if (saveEndingTree.ShowDialog() == DialogResult.OK)
            {
                _directoryToExport = Path.GetDirectoryName(saveEndingTree.FileName);
                Properties.Settings.Default.ExportDirectory = _directoryToExport;
                using (var streamWriter = new StreamWriter(saveEndingTree.FileName))
                {
                    streamWriter.WriteLine("Day,DayPart,Action,SkippedDM,Followers,Stress,Affection,Darkness,StreamStreak,PreAlertBonus,GamerGirl,Cinephile,Impact,Experience,Communication,RabbitHole");
                    for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
                    {
                        var branch = CurrentEndingTree.EndingsList[i];
                        for (int j = 0; j < branch.EndingBranch.AllActions.Count; j++)
                        {
                            var action = branch.EndingBranch.AllActions[j];
                            streamWriter.WriteLine($"{action.TargetAction.DayIndex},{NSODataManager.DayPartNames[action.TargetAction.DayPart]},{NSODataManager.CmdName(action.Command)},{(action.TargetAction.IgnoreDM ? "Yes" : "")}," +
                                $"{action.Followers},{action.Stress},{action.Affection},{action.Darkness},{action.StreamStreak},{(action.PreAlertBonus ? "Active" : "")},{action.GamerGirl},{action.Cinephile},{action.Impact},{action.Experience},{action.Communication},{action.RabbitHole}");
                        }
                        streamWriter.WriteLine($",,Expected Ending: {NSODataManager.EndingNames[branch.EndingBranch.EndingToGet]},,,,,,,,,,,,,");
                    }
                }
                MessageBox.Show("Successfully exported Ending Tree!", "Success", MessageBoxButtons.OK);
            }
        }

        private void EndingTreeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
