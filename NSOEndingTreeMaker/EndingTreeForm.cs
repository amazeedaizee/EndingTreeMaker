﻿using Microsoft.Win32;
using Newtonsoft.Json;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class EndingTreeForm : Form
    {
        internal bool isBranchEdited = false;
        private bool _isNotesEdited = false;
        private bool _isExpEdited = false;
        private bool _currentExp;
        private bool loadingFile = false;
        private string _currentNotes;
        private string _directoryToOpen = Properties.Settings.Default.Directory;
        private string _directoryToExport = Properties.Settings.Default.ExportDirectory;
        private string _currentFile = "";
        private string _recentlyClosed = Properties.Settings.Default.RecentClosedEndingTree;

        public EndingTreeData CurrentEndingTree;
        public EndingBranchData SelectedEnding;

        private Dictionary<int, string> _slots = new Dictionary<int, string>()
            {
                {1, File.Exists(Properties.Settings.Default.SlotOne) ? Properties.Settings.Default.SlotOne : "" },
                {2, File.Exists(Properties.Settings.Default.SlotTwo) ? Properties.Settings.Default.SlotTwo : ""},
                {3, File.Exists(Properties.Settings.Default.SlotThree) ? Properties.Settings.Default.SlotThree : ""},
                {4, File.Exists(Properties.Settings.Default.SlotFour) ? Properties.Settings.Default.SlotFour : ""},
                {5, File.Exists(Properties.Settings.Default.SlotFive) ? Properties.Settings.Default.SlotFive : ""},
            };

        public bool DeletingEndings;

        public EndingTreeData SavedEndingTree;
        public EndingTreeForm()
        {
            InitializeComponent();

        }

        private void ChangeFormTitle()
        {
            if (!string.IsNullOrEmpty(_currentFile))
                Text = $"Main Ending Tree - {Path.GetFileName(_currentFile)}";
            else Text = "Main Ending Tree";
        }
        private string InitializeValidGamePath()
        {
            string gamePath;
            if (Environment.Is64BitOperatingSystem)
            {
                gamePath = (string)RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 1451940", false).GetValue("InstallLocation");
            }
            else
            {
                gamePath = (string)RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 1451940", false).GetValue("InstallLocation");
            }
            return gamePath;
        }

        private string InitializeValidSteamPath()
        {
            string steamPath;
            if (Environment.Is64BitOperatingSystem)
            {
                steamPath = (string)RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Valve\Steam", false).GetValue("SteamExe");
            }
            else
            {
                steamPath = (string)RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Valve\Steam", false).GetValue("SteamExe");
            }
            return steamPath;
        }

        private string PathToGameModOrDocuments()
        {
            string gameModPath = InitializeValidGamePath() + "\\BepInEx\\plugins\\EndingTreeSimulator";
            if (!Directory.Exists(gameModPath)) return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return gameModPath;
        }
        private EndingTreeData MakeFirstTree()
        {
            EndingBranchData TestEndingBranch = new EndingBranchData(1, NGO.EndingType.Ending_None, new List<TargetActionData>()
            {
                new(1, 2, ngov3.CmdType.None),

            });


            EndingTreeData data = new(new() { TestEndingBranch });
            return data;
        }

        public List<(string, string, string)> SetEndingListViewData(bool initializeStartingView = true)
        {
            EndingListView.Items.Clear();
            List<(string, string, string)> errorList = new();
            for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
            {
                var list = new List<(string, string, string)>();
                var branch = CurrentEndingTree.EndingsList[i];
                string futureBranchName = $"Branch {i + 1}. {NSODataManager.EndingNames[branch.EndingBranch.EndingToGet]}";
                if (CurrentEndingTree.isDay2Exp && branch.EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == 2 && a.TargetAction.DayPart == -1 && a.Command == CmdType.None))
                {
                    list.Add(new("", "", "Day 2 Extra Action must have an action if Day 2 Extra Action is enabled."));
                }
                if (i == 0)
                {
                    InitializeEndingList(i, errorList, futureBranchName);
                    continue;
                }
                var startAction = SetStartingAction(branch, false, i - 1);
                branch.EndingBranch.AllActions[0] = startAction;
                if (startAction.Followers == 0)
                    errorList.Add(new(futureBranchName, "", $"Tried to initialize starting stats for this branch's starting day, however no valid day exists to set stats."));
                else branch.EndingBranch.AllActions[0] = startAction;
                ResetStartingDayData(branch, i - 1);
                InitializeEndingList(i, errorList, futureBranchName);
            }
            if (errorList.Count > 0) UnvalidBranches_Label.Visible = true;
            else UnvalidBranches_Label.Visible = false;
            Notes.Text = CurrentEndingTree.Notes;
            //Experiment_Check.Checked = CurrentEndingTree.isDay2Exp;
            ChangeFileLabelIfUnsaved();
            return errorList;

            void InitializeEndingList(int branchIndex, List<(string, string, string)> errorList, string branchName)
            {
                var branch = CurrentEndingTree.EndingsList[branchIndex];
                var list = new List<(string, string, string)>();
                branch.InitializeActionStats();
                AddExpectedErrorToErrorList(branch, list, branchName);
                list.AddRange(branch.ValidateBranch(branchName));
                var customEvents = ValidateCustomEvents(branchIndex);
                if (customEvents.Item2 != "") errorList.Add((branchName, "", customEvents.Item2));
                AddEndingToListView(branch);
                if (list.Count > 0) EndingListView.Items[branchIndex].SubItems[0].Text += " (!)";
                errorList.AddRange(list);
            }
        }

        bool IsExpectedMatchesEndingToGet(EndingBranchData branch)
        {
            if (branch.EndingBranch.AllActions[0].Followers == 0)
                return true;
            if (branch.ExpectedDayOfEnd.Item3 != branch.EndingBranch.EndingToGet)
            {
                if (NSODataManager.IsNightEnding(branch.ExpectedDayOfEnd.Item3) && branch.IgnoreNightEndings)
                    return true;
                else return false;
            }
            return true;
        }

        void AddExpectedErrorToErrorList(EndingBranchData branch, List<(string, string, string)> errorList, string branchName)
        {
            if (!IsExpectedMatchesEndingToGet(branch))
            {
                errorList.Add(new(branchName, "", $"Expected ending in this branch does not match this branch's selected ending.\n\nExpected ending:{NSODataManager.EndingNames[branch.ExpectedDayOfEnd.Item3]} \nSelected ending:{NSODataManager.EndingNames[branch.EndingBranch.EndingToGet]}"));
            }
        }

        public void AddEndingToListView(EndingBranchData endingData)
        {
            bool doesEndExist = NSODataManager.EndingNames.TryGetValue(endingData.EndingBranch.EndingToGet, out string name);
            ListViewItem ending = EndingListView.Items.Add(doesEndExist ? name : "");
            ending.SubItems.Add(endingData.EndingBranch.StartingDay.ToString());
            ending.SubItems.Add(SetLatestDayString(endingData));
            ending.SubItems.Add(endingData.EndingBranch.IsStressfulBressdown ? "Yes" : "");
        }

        public void InsertEndingToListView(EndingBranchData endingData, int index)
        {
            bool doesEndExist = NSODataManager.EndingNames.TryGetValue(endingData.EndingBranch.EndingToGet, out string name);
            ListViewItem ending = EndingListView.Items.Insert(index, doesEndExist ? name : "");
            ending.SubItems.Add(endingData.EndingBranch.StartingDay.ToString());
            ending.SubItems.Add(SetLatestDayString(endingData));
            ending.SubItems.Add(endingData.EndingBranch.IsStressfulBressdown ? "Yes" : "");
        }

        public string SetLatestDayString(EndingBranchData endingData)
        {
            TargetActionData action = endingData.EndingBranch.AllActions[endingData.EndingBranch.AllActions.Count - 1];
            if (action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && !NSODataManager.IsEndingOnSameDay(endingData.ExpectedDayOfEnd.Item3))
                return $"{action.TargetAction.DayIndex + 1}";
            return action.TargetAction.DayIndex.ToString();
        }

        private void MoveBranchToPreviousIndex()
        {
            if (SelectedEnding == null || EndingListView.SelectedIndices.Count == 0 || EndingListView.SelectedIndices[0] <= 1)
                return;
            int branchIndex = CurrentEndingTree.EndingsList.IndexOf(SelectedEnding);
            MoveBranchToNewIndex(SelectedEnding, branchIndex - 1);
        }
        private void MoveBranchToNextIndex()
        {
            int maxListCount = CurrentEndingTree.EndingsList.Count;
            if (SelectedEnding == null || EndingListView.SelectedIndices.Count == 0 || EndingListView.SelectedIndices[0] == maxListCount - 1)
                return;
            int branchIndex = CurrentEndingTree.EndingsList.IndexOf(SelectedEnding);
            MoveBranchToNewIndex(SelectedEnding, branchIndex + 1);
        }
        private void MoveBranchToNewIndex(EndingBranchData endingData, int newIndex)
        {
            int branchIndex = CurrentEndingTree.EndingsList.IndexOf(endingData);
            var newBranch = new EndingBranchData(endingData);
            CurrentEndingTree.EndingsList.RemoveAt(branchIndex);
            EndingListView.Items.RemoveAt(branchIndex);
            CurrentEndingTree.EndingsList.Insert(newIndex, newBranch);
            InsertEndingToListView(newBranch, newIndex);
            SetEndingListViewData();
        }

        private void DeleteSelectedEndingData()
        {
            DeletingEndings = true;
            bool remove = false;
            var selectedEndings = EndingListView.SelectedIndices;
            List<string> depEnds = [];
            if (selectedEndings.Count == 0)
            {
                return;
            }
            if (selectedEndings[0] == 0)
            {
                MessageBox.Show($"Deleting the first branch of the tree isn't allowed.", "No", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DeletingEndings = false;
                return;
            }
            var confirm = MessageBox.Show($"Are you sure you want to delete {(selectedEndings.Count > 1 ? "these endings" : "this ending")}? \n\nThis action can't be undone.", "Delete Ending?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                ResetSelectedEnding();
                return;
            }
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
                            break;
                        }
                        if (!checkIfValid)
                            depEnds.Add($"{j + 1}: {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[j].EndingBranch.EndingToGet]}");

                    }


                }
                if (depEnds.Count > 0)
                {
                    var confirmAgain = MessageBox.Show($"Are you really sure? \n\nOne or more future ending branches relies on only this ({index + 1}: {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[index].EndingBranch.EndingToGet]}) branch for their Starting Days.\n\nDependent Branches:\n\n{string.Join("\n", depEnds)}", "Confirm Again", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmAgain == DialogResult.No)
                    {
                        ResetSelectedEnding();
                        return;
                    }
                }
                CurrentEndingTree.EndingsList.RemoveAt(index);
                EndingListView.Items.RemoveAt(index);
            }
            SetEndingListViewData(true);
            ResetSelectedEnding();

            void ResetSelectedEnding()
            {
                isBranchEdited = true;
                SelectedEnding = null;
                DeleteEndingBranch.Enabled = false;
                EditEndingBranch.Enabled = false;
                MoveBranchDown_Button.Enabled = false;
                MoveBranchUp_Button.Enabled = false;
                DeletingEndings = false;
            }
        }


        public TargetActionData SetStartingAction(EndingBranchData branch, bool isNewEnding = true, int index = -1)
        {
            bool foundValidDay = false;
            var newAction = new TargetActionData(branch.EndingBranch.StartingDay, -1, ngov3.CmdType.None);
            if (index == -1) index = CurrentEndingTree.EndingsList.Count - 1;
            for (int i = index; i >= 0; i--)
            {
                var refBranch = CurrentEndingTree.EndingsList[i];
                List<TargetActionData> actions = refBranch.EndingBranch.AllActions;
                if (branch.EndingBranch.StartingDay > actions[actions.Count - 1].TargetAction.DayIndex)
                {
                    break;
                }
                for (int j = actions.Count - 1; j >= 0; j--)
                {
                    bool foundReferenceDay = actions[j].TargetAction.DayIndex == branch.EndingBranch.StartingDay || (actions[j].TargetAction.DayIndex == (branch.EndingBranch.StartingDay - 1) && actions[j].TargetAction.DayPart + actions[j].CommandResult.daypart >= 3);
                    bool isNoEndingExpected = (refBranch.ExpectedDayOfEnd.Item3 == EndingType.Ending_None || ((actions[j].TargetAction.DayIndex <= refBranch.ExpectedDayOfEnd.Item1 || (actions[j].TargetAction.DayIndex == refBranch.ExpectedDayOfEnd.Item1 && refBranch.ExpectedDayOfEnd.Item2 != 0)) && !(refBranch.isHorror.isEventing && actions[j].TargetAction.DayIndex >= refBranch.isHorror.DayIndex)));
                    if (foundReferenceDay && isNoEndingExpected)
                    {
                        foundValidDay = true;
                    }
                    if (foundValidDay && actions[j].TargetAction.DayIndex == (branch.EndingBranch.StartingDay - 1))
                    {
                        var refAction = actions[j];
                        newAction.Followers = refAction.Followers;
                        newAction.Stress = refAction.Stress;
                        newAction.Affection = refAction.Affection;
                        newAction.Darkness = refAction.Darkness;
                        newAction.StreamStreak = refAction.StreamStreak;
                        //newAction.PreAlertBonus = refAction.PreAlertBonus;
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
            newAction.Followers = 0;
            newAction.Stress = 0;
            newAction.Affection = 0;
            newAction.Darkness = 0;
            newAction.StreamStreak = 0;
            newAction.PreAlertBonus = false;
            newAction.Cinephile = 0;
            newAction.Impact = 0;
            newAction.GamerGirl = 0;
            newAction.Experience = 0;
            newAction.Communication = 0;
            newAction.RabbitHole = 0;
            return newAction;
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
                index = CurrentEndingTree.EndingsList.IndexOf(branch);
            for (int i = index; i >= 0; i--)
            {
                EndingBranchData endingToImport = CurrentEndingTree.EndingsList[i];
                List<TargetActionData> actions = endingToImport.EndingBranch.AllActions;
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

                    if ((endingToImport.hasGalacticRail.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.hasGalacticRail.isEventing)
                        branch.hasGalacticRail = endingToImport.hasGalacticRail;
                    else branch.hasGalacticRail = new(0, false);
                    if ((endingToImport.NoMeds.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.NoMeds.isEventing)
                        branch.NoMeds = endingToImport.NoMeds;
                    else branch.NoMeds = new(0, false);
                    if ((endingToImport.isHorror.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isHorror.isEventing)
                        branch.isHorror = endingToImport.isHorror;
                    else branch.isHorror = new(0, false);
                    if ((endingToImport.isReallyLove.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isReallyLove.isEventing)
                        branch.isReallyLove = endingToImport.isReallyLove;
                    else branch.isReallyLove = new(0, false);
                    if ((endingToImport.isStressed.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isStressed.isEventing)
                        branch.isStressed = endingToImport.isStressed;
                    else branch.isStressed = new(0, false);
                    if ((endingToImport.isReallyStressed.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isReallyStressed.isEventing)
                        branch.isReallyStressed = endingToImport.isReallyStressed;
                    else branch.isReallyStressed = new(0, false);
                    if ((endingToImport.isTrauma.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isTrauma.isEventing)
                        branch.isTrauma = endingToImport.isTrauma;
                    else branch.isTrauma = new(0, false);
                    if ((endingToImport.isVideo.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.isVideo.isEventing)
                        branch.isVideo = endingToImport.isVideo;
                    else branch.isVideo = new(0, false);
                    if ((endingToImport.is150M.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.is150M.isEventing)
                        branch.is150M = endingToImport.is150M;
                    else branch.is150M = new(0, false);
                    if ((endingToImport.is300M.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.is300M.isEventing)
                        branch.is300M = endingToImport.is300M;
                    else branch.is300M = new(0, false);
                    if ((endingToImport.is500M.DayIndex <= branch.EndingBranch.StartingDay) && endingToImport.is500M.isEventing)
                        branch.is500M = endingToImport.is500M;
                    else branch.is500M = new(0, false);
                    if ((endingToImport.isMaxFollowers.DayIndex <= branch.isMaxFollowers.DayIndex) && endingToImport.isMaxFollowers.isEventing)
                        branch.isMaxFollowers = endingToImport.isMaxFollowers;
                    else branch.isMaxFollowers = new(0, false);
                    return;

                }
            }
        }

        public (bool, string) ValidateCustomEvents(int index = -1, EndingBranchData branch = null)
        {
            bool isValidated = true;
            string errorMsg = "";

            if (index >= CurrentEndingTree.EndingsList.Count) index = CurrentEndingTree.EndingsList.Count - 1;
            else if (index < -1) index = 0;
            if (index == -1)
            {
                List<string> endings = [];
                var ends = CurrentEndingTree.EndingsList;
                for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
                {
                    if (ends[i].EndingBranch.AllActions.Exists(a => a.Command == (CmdType)1000))
                    {
                        endings.Add($"Branch {i + 1}. {NSODataManager.EndingNames[ends[i].EndingBranch.EndingToGet]}");
                        break;
                    }



                }
                if (endings.Count > 0)
                {
                    isValidated = false;
                    errorMsg = $"Custom Events are not allowed except in Experimental Mode. \n\nList of branches that contain Custom Events:\n{string.Join("\n", endings)}";
                }
            }
            else
            {
                List<string> customs = [];
                EndingBranchData b = branch ?? CurrentEndingTree.EndingsList[index];
                var acts = b.EndingBranch.AllActions;
                for (int i = 0; i < acts.Count; i++)
                {
                    if (acts[i].Command == (CmdType)1000)
                        customs.Add($"Day {acts[i].TargetAction.DayIndex}, {NSODataManager.DayPartNames[acts[i].TargetAction.DayPart]}: {acts[i].ActionName}");
                }
                if (customs.Count > 0)
                {
                    isValidated = false;
                    errorMsg = $"Custom Events are not allowed except in Experimental Mode. \n\nList of actions that contain Custom Events:\n{string.Join("\n", customs)}";
                }
                ;
            }

            return (isValidated, errorMsg);
        }

        public (bool, bool, string) ValidateFutureBranchStarts(int index, EndingBranchData branch)
        {
            int minStartingDay = branch.EndingBranch.StartingDay;
            bool hasAnyFutureStartDays = false;
            bool isValidated = true;
            string errorMsg = "";
            List<string> endings = new List<string>();
            int oldLatestIndex = CurrentEndingTree.EndingsList[index].EndingBranch.AllActions.Count - 1;
            int oldLatestDay = CurrentEndingTree.EndingsList[index].EndingBranch.AllActions[oldLatestIndex].TargetAction.DayIndex;
            for (int i = index; i < CurrentEndingTree.EndingsList.Count; i++)
            {
                if (i == 0) continue;
                if (i == index) continue;
                var futureBranch = CurrentEndingTree.EndingsList[i];
                int futureStartDay = futureBranch.EndingBranch.StartingDay;
                int latestDay = branch.EndingBranch.AllActions[branch.EndingBranch.AllActions.Count - 1].TargetAction.DayIndex;

                string futureBranchName = $"Branch {i + 1}. {NSODataManager.EndingNames[CurrentEndingTree.EndingsList[i].EndingBranch.EndingToGet]}";


                if (futureStartDay > latestDay && latestDay < oldLatestDay && futureStartDay <= oldLatestDay)
                {
                    isValidated = false;
                    endings.Add(futureBranchName);

                    continue;
                }
                if (futureStartDay <= latestDay && futureStartDay > minStartingDay)
                {
                    hasAnyFutureStartDays = true;
                    break;
                }

            }
            if (endings.Count > 0) errorMsg = $"Other branches' starting days are reliant on a day from this branch that now doesn't exist. \n\nList of branches that relied on this branch:\n{string.Join("\n", endings)}";
            return (isValidated, hasAnyFutureStartDays, errorMsg);
        }

        private bool ValidateEndingTree()
        {

            var errorList = SetEndingListViewData();
            if (errorList.Count > 0)
            {
                bool isConfirm;
                CurrentEndingTree.isBroken = true;
                if (Properties.Settings.Default.ExperimentMode) return true;
                BranchErrorDetails errorDetails = new BranchErrorDetails(errorList, false);
                errorDetails.ErrorIntro.Text = "Branches in this tree contains validation errors. Are you sure you want to proceed?";
                isConfirm = errorDetails.ShowDialog() == DialogResult.Yes ? true : false;
                return isConfirm;
            }
            CurrentEndingTree.isBroken = false;
            return true;
        }

        private void EndingTreeForm_Load(object sender, EventArgs e)
        {
            EndingListView.Capture = true;
            var endingTree = MakeFirstTree();
            CurrentEndingTree = endingTree;
            SetEndingListViewData();
            Experiment_Check.Checked = Properties.Settings.Default.ExperimentMode;
            _currentNotes = CurrentEndingTree.Notes;
            DeleteEndingBranch.Enabled = false;
            EditEndingBranch.Enabled = false;
            MoveBranchDown_Button.Enabled = false;
            MoveBranchUp_Button.Enabled = false;
            OpenRecent_MenuItem.Text += $" {Path.GetFileName(_recentlyClosed)}";
            string gameModPath = InitializeValidGamePath() + @"\BepInEx\plugins\EndingTreeSimulator";
            EndingSim_MenuItem.Visible = Directory.Exists(gameModPath) && File.Exists(gameModPath + @"\EndingTreeSimulator.dll");
            SetEnableBoolForMenuOptions();
        }

        private void AddEndingButton_Click(object sender, EventArgs e)
        {
            AddEndingBranch newEnding = new(this);
            newEnding.ShowDialog();
        }

        private bool SaveExistingEndingTree(string pathToTree)
        {
            if (string.IsNullOrEmpty(pathToTree)) return false;
            if (!ValidateEndingTree()) return false;
            Stream stream;
            var treeData = JsonConvert.SerializeObject(CurrentEndingTree, Formatting.Indented);
            if ((stream = File.Create(pathToTree)) != null)
            {
                stream.Write(Encoding.UTF8.GetBytes(treeData), 0, Encoding.UTF8.GetByteCount(treeData));
                _currentNotes = CurrentEndingTree.Notes;
                _currentExp = CurrentEndingTree.isDay2Exp;
                _isNotesEdited = false;
                isBranchEdited = false;
                stream.Close();
                MessageBox.Show("Successfully saved Ending Tree!", "Success", MessageBoxButtons.OK);
                ChangeFormTitle();
                ChangeFileLabelIfUnsaved();
                return true;
            }
            return false;
        }
        private bool SaveAsEndingTree()
        {
            if (!ValidateEndingTree()) return false;
            Stream stream;
            SaveFileDialog saveEndingTree = new SaveFileDialog();
            saveEndingTree.InitialDirectory = !string.IsNullOrEmpty(_directoryToOpen) ? _directoryToOpen : PathToGameModOrDocuments();
            saveEndingTree.Filter = "JSON File (*.json)|*.json";
            saveEndingTree.FilterIndex = 1;
            saveEndingTree.RestoreDirectory = true;
            saveEndingTree.OverwritePrompt = true;
            if (saveEndingTree.ShowDialog() == DialogResult.OK)
            {
                var treeData = JsonConvert.SerializeObject(CurrentEndingTree, Formatting.Indented);
                if ((stream = File.Create(saveEndingTree.FileName)) != null)
                {
                    _directoryToOpen = Path.GetDirectoryName(saveEndingTree.FileName);
                    Properties.Settings.Default.Directory = _directoryToOpen;
                    stream.Write(Encoding.UTF8.GetBytes(treeData), 0, Encoding.UTF8.GetByteCount(treeData));
                    _currentFile = saveEndingTree.FileName;
                    _currentExp = CurrentEndingTree.isDay2Exp;
                    _currentNotes = CurrentEndingTree.Notes;
                    _isNotesEdited = false;
                    isBranchEdited = false;
                    stream.Close();
                    MessageBox.Show("Successfully saved Ending Tree!", "Success", MessageBoxButtons.OK);
                    ChangeFormTitle();
                    ChangeFileLabelIfUnsaved();
                    return true;
                }
                return false;
            }
            return false;

        }

        private bool LoadExistingEndingTree(string pathToTree)
        {
            if (string.IsNullOrEmpty(pathToTree) || !IsCancelActionDialog()) return false;
            try
            {
                var fileContent = File.OpenRead(pathToTree);
                using (StreamReader reader = new StreamReader(fileContent))
                {
                    loadingFile = true;
                    var importedTreeData = reader.ReadToEnd();
                    _currentFile = pathToTree;
                    ConvertDataToTree(importedTreeData);
                    loadingFile = false;
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Could not open file, either the file is invalid or the file does not represent an Ending Tree.", "Could not read file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadingFile = false;
            return false;


        }

        private void ConvertDataToTree(string data)
        {
            var newTreeData = JsonConvert.DeserializeObject<EndingTreeData>(data);
            CurrentEndingTree = newTreeData;
            _currentNotes = CurrentEndingTree.Notes;
            _currentExp = CurrentEndingTree.isDay2Exp;
            SetEndingListViewData(true);
            DeleteEndingBranch.Enabled = false;
            EditEndingBranch.Enabled = false;
            if (!string.IsNullOrEmpty(_currentFile)) _recentlyClosed = _currentFile;
            Day2ExtraAction();
            _isNotesEdited = false;
            isBranchEdited = false;
            _isExpEdited = false;
            ChangeFormTitle();
            ChangeFileLabelIfUnsaved();
        }

        private bool LoadEndingTree()
        {
            if (!IsCancelActionDialog()) return false;
            OpenFileDialog openEndingTree = new OpenFileDialog();
            openEndingTree.InitialDirectory = !string.IsNullOrEmpty(_directoryToOpen) ? _directoryToOpen : PathToGameModOrDocuments();
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
                        loadingFile = true;
                        var importedTreeData = reader.ReadToEnd();
                        var newTreeData = JsonConvert.DeserializeObject<EndingTreeData>(importedTreeData);
                        CurrentEndingTree = newTreeData;
                        SetEndingListViewData(true);
                        DeleteEndingBranch.Enabled = false;
                        EditEndingBranch.Enabled = false;
                        _currentFile = openEndingTree.FileName;
                        _currentNotes = CurrentEndingTree.Notes;
                        _currentExp = CurrentEndingTree.isDay2Exp;
                        Day2ExtraAction();
                        _isNotesEdited = false;
                        isBranchEdited = false;
                        openEndingTree.Dispose();
                        ChangeFormTitle();
                        ChangeFileLabelIfUnsaved();
                        loadingFile = false;
                        return true;
                    }
                }
                catch { MessageBox.Show("Could not open JSON file, either the JSON file is invalid or the JSON file does not represent an Ending Tree.", "Could not read JSON file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            loadingFile = false;
            return false;
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
                if (selectedEndings[0] == 0)
                {
                    MoveBranchDown_Button.Enabled = false;
                    MoveBranchUp_Button.Enabled = false;
                    return;
                }
                if (selectedEndings[0] != CurrentEndingTree.EndingsList.Count - 1)
                    MoveBranchDown_Button.Enabled = true;
                if (selectedEndings[0] > 1)
                    MoveBranchUp_Button.Enabled = true;
                return;
            }
            if (selectedEndings.Count == 0)
            {
                SelectedEnding = null;
                DeleteEndingBranch.Enabled = false;
                EditEndingBranch.Enabled = false;
                MoveBranchDown_Button.Enabled = false;
                MoveBranchUp_Button.Enabled = false;
                return;
            }
            DeleteEndingBranch.Enabled = true;
            EditEndingBranch.Enabled = false;
            MoveBranchDown_Button.Enabled = false;
            MoveBranchUp_Button.Enabled = false;
        }

        private void EndingListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { DeleteSelectedEndingData(); }
        }

        private void EndingListView_DoubleClick(object sender, EventArgs e)
        {
            ListWhenDoubleClicked();
        }

        private void ListWhenDoubleClicked()
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
            if (CurrentEndingTree.Notes != _currentNotes && !loadingFile) _isNotesEdited = true;
            else _isNotesEdited = false;
            ChangeFileLabelIfUnsaved();
        }

        private void ExportEndingTreeToCSV()
        {
            if (!ConfirmSave()) return;
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
                    saveEndingTree.Dispose();
                }
                MessageBox.Show("Successfully exported Ending Tree!", "Success", MessageBoxButtons.OK);
            }
        }

        private void EndingTreeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsCancelActionDialog())
            {
                e.Cancel = true;
                return;
            }
            SaveNewSlotPathsToSettings();
            if (!string.IsNullOrEmpty(_currentFile))
                Properties.Settings.Default.RecentClosedEndingTree = _currentFile;
            Properties.Settings.Default.Save();
        }

        private void SetEnableBoolForMenuOptions()
        {
            loadTreeFromSlot1_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[1]);
            loadTreeFromSlot2_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[2]);
            loadTreeFromSlot3_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[3]);
            loadTreeFromSlot4_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[4]);
            loadTreeFromSlot5_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[5]);

            SetSlotOne_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[1]);
            SetSlotTwo_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[2]);
            SetSlotThree_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[3]);
            SetSlotFour_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[4]);
            SetSlotFive_MenuItem.Enabled = !string.IsNullOrEmpty(_slots[5]);

            Slot1_Name.Text = !string.IsNullOrEmpty(_slots[1]) ? Path.GetFileName(_slots[1]) : "(none)";
            Slot2_Name.Text = !string.IsNullOrEmpty(_slots[2]) ? Path.GetFileName(_slots[2]) : "(none)";
            Slot3_Name.Text = !string.IsNullOrEmpty(_slots[3]) ? Path.GetFileName(_slots[3]) : "(none)";
            Slot4_Name.Text = !string.IsNullOrEmpty(_slots[4]) ? Path.GetFileName(_slots[4]) : "(none)";
            Slot5_Name.Text = !string.IsNullOrEmpty(_slots[5]) ? Path.GetFileName(_slots[5]) : "(none)";
        }

        private void SavePathToSlot(int slot)
        {
            if (!ConfirmSave()) return;
            _slots[slot] = _currentFile;
            SetEnableBoolForMenuOptions();
        }

        private void SaveNewSlotPathsToSettings()
        {
            Properties.Settings.Default.SlotOne = _slots[1];
            Properties.Settings.Default.SlotTwo = _slots[2];
            Properties.Settings.Default.SlotThree = _slots[3];
            Properties.Settings.Default.SlotFour = _slots[4];
            Properties.Settings.Default.SlotFive = _slots[5];
        }

        private void LoadPathFromSlot(int slot)
        {
            if (string.IsNullOrEmpty(_slots[slot]))
            {
                MessageBox.Show($"Could not load Ending Tree, as nothing is saved in Slot {slot}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadExistingEndingTree(_slots[slot]);
        }

        private void SetTreeAsSimulation(int slot)
        {
            string gameModPath = InitializeValidGamePath() + "\\BepInEx\\plugins\\EndingTreeSimulator";
            if (!Directory.Exists(gameModPath)) return;
            string[] existingJSONs = Directory.GetFiles(gameModPath);
            if (slot == 0)
            {
                if (!ConfirmSave()) return;
            }
            string treepath = slot == 0 ? _currentFile : _slots[slot];
            if (string.IsNullOrEmpty(treepath)) return;
            string fileName = Path.GetFileName(treepath);
            string simulatingTreePath = Path.Combine(gameModPath, fileName);
            foreach (string json in existingJSONs)
            {
                if (json.EndsWith(".json")) File.Delete(json);
            }
            var pathData = File.ReadAllBytes(treepath);
            using (FileStream stream = File.Create(simulatingTreePath))
            {
                stream.Write(pathData, 0, pathData.Length);
                stream.Dispose();
            }
            MessageBox.Show("Successfully set Ending Tree!", "Success", MessageBoxButtons.OK);
        }

        private bool ConfirmSave()
        {
            if (!(isBranchEdited || _isNotesEdited)) return true;
            var confirm = MessageBox.Show($"Saving is required before proceeding. Do you want to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                SaveOrSaveAs();
                return true;
            }
            return false;
        }
        private bool IsCancelActionDialog()
        {
            if (!(isBranchEdited || _isNotesEdited)) return true;
            var confirm = MessageBox.Show($"Ending tree isn't saved yet. Do you want to save?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                SaveOrSaveAs();
                return true;
            }
            if (confirm == DialogResult.No) return true;
            return false;
        }

        internal void ChangeFileLabelIfUnsaved()
        {
            if (isBranchEdited || _isNotesEdited)
                File_MenuItem.Text = "File*";
            else File_MenuItem.Text = "File";
        }

        private void SaveOrSaveAs()
        {
            if (string.IsNullOrEmpty(_currentFile))
            {
                SaveAsEndingTree();
                return;
            }
            SaveExistingEndingTree(_currentFile);
        }
        private void openEndingTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadEndingTree();
        }

        private void openRecentEndingTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadExistingEndingTree(_recentlyClosed);
        }

        private void saveEndingTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveOrSaveAs();
        }

        private void saveEndingTreeAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsEndingTree();
        }

        private void exportTreeAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportEndingTreeToCSV();
        }

        private void openSimulationLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(InitializeValidGamePath() + "\\BepInEx\\plugins\\EndingTreeSimulator\\Logs");
            }
            catch { MessageBox.Show("Could not open the Simulation Logs folder: either the folder doesn't exist, has been moved to another location, or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void SaveToSLot1(object sender, EventArgs e)
        {
            SavePathToSlot(1);
        }

        private void LoadToSlot1(object sender, EventArgs e)
        {
            LoadPathFromSlot(1);
        }

        private void SaveToSlot2(object sender, EventArgs e)
        {
            SavePathToSlot(2);
        }

        private void LoadToSlot2(object sender, EventArgs e)
        {
            LoadPathFromSlot(2);
        }

        private void SaveToSlot3(object sender, EventArgs e)
        {
            SavePathToSlot(3);
        }

        private void LoadToSlot3(object sender, EventArgs e)
        {
            LoadPathFromSlot(3);
        }

        private void SaveToSlot4(object sender, EventArgs e)
        {
            SavePathToSlot(4);
        }

        private void LoadToSLot4(object sender, EventArgs e)
        {
            LoadPathFromSlot(4);
        }

        private void SaveToSlot5(object sender, EventArgs e)
        {
            SavePathToSlot(5);
        }

        private void LoadToSlot5(object sender, EventArgs e)
        {
            LoadPathFromSlot(5);
        }

        private void ResetEndingTree_MenuCLick(object sender, EventArgs e)
        {
            ReloadEndingTree();
        }

        private void ReloadEndingTree()
        {
            var confirm = MessageBox.Show($"Are you sure you want to reset this ending tree? \nThis will reset it back to when it was last saved (or default if it was never saved).\n\nThis action can't be undone.", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                _isNotesEdited = false;
                isBranchEdited = false;
                if (string.IsNullOrEmpty(_currentFile))
                {
                    CurrentEndingTree = MakeFirstTree();
                    SetEndingListViewData();
                    ChangeFormTitle();
                    return;
                }
                LoadExistingEndingTree(_currentFile);
            }
        }

        private void CreateNewEndingTree()
        {
            if (IsCancelActionDialog())
            {
                loadingFile = true;
                _recentlyClosed = _currentFile;
                _currentFile = "";
                CurrentEndingTree = MakeFirstTree();
                _isNotesEdited = false;
                isBranchEdited = false;
                SetEndingListViewData();
                ChangeFormTitle();
                loadingFile = false;
                return;
            }
        }

        private void SetCurrent_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(0);
        }

        private void SetSlot1_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(1);
        }

        private void SetSlot2_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(2);
        }

        private void SetSlot3_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(3);
        }

        private void SetSlot4_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(4);
        }

        private void SetSlot5_MenuItem_Click(object sender, EventArgs e)
        {
            SetTreeAsSimulation(5);
        }

        private void PlayGame_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(InitializeValidGamePath() + @"\Windose.exe");
            }
            catch { MessageBox.Show("Could not open the game from the Steam path: either the game doesn't exist, has been moved to another location, or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void EndingTreeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Shift && e.Control)
                SaveAsEndingTree();
            else if (e.KeyCode == Keys.S && e.Control)
                SaveOrSaveAs();
            else if (e.KeyCode == Keys.O && e.Shift && e.Control)
                LoadExistingEndingTree(_recentlyClosed);
            else if (e.KeyCode == Keys.O && e.Control)
                LoadEndingTree();
            else if (e.KeyCode == Keys.Z && e.Shift && e.Control)
                ReloadEndingTree();
            else if (e.KeyCode == Keys.E && e.Control)
                ExportEndingTreeToCSV();
            else if (e.KeyCode == Keys.F1 && e.Shift)
                SavePathToSlot(1);
            else if (e.KeyCode == Keys.F2 && e.Shift)
                SavePathToSlot(2);
            else if (e.KeyCode == Keys.F3 && e.Shift)
                SavePathToSlot(3);
            else if (e.KeyCode == Keys.F4 && e.Shift)
                SavePathToSlot(4);
            else if (e.KeyCode == Keys.F5)
                SavePathToSlot(5);
            else if (e.KeyCode == Keys.F1)
                LoadPathFromSlot(1);
            else if (e.KeyCode == Keys.F2)
                LoadPathFromSlot(2);
            else if (e.KeyCode == Keys.F3)
                LoadPathFromSlot(3);
            else if (e.KeyCode == Keys.F4)
                LoadPathFromSlot(4);
            else if (e.KeyCode == Keys.F5)
                LoadPathFromSlot(5);
            else if (e.KeyCode == Keys.P && e.Shift && e.Control)
            {
                try
                {
                    Process.Start(InitializeValidSteamPath(), @"steam://rungameid/1451940");
                }
                catch { MessageBox.Show("Could not open the game from the Steam path: either the game doesn't exist, has been moved to another location, or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (e.KeyCode == Keys.P && e.Shift && e.Control && e.Alt)
            {
                try
                {
                    Process.Start(InitializeValidGamePath() + @"\Windose.exe");
                }
                catch { MessageBox.Show("Could not open the game from the Steam path: either the game doesn't exist, has been moved to another location, or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void NewTree_MenuItem(object sender, EventArgs e)
        {
            CreateNewEndingTree();
        }

        private void UnvalidBranch_LabelToggle(object sender, EventArgs e)
        {
            if (CurrentEndingTree == null)
            {
                UnvalidBranches_Label.Visible = false;
                return;
            }
        }

        private void Day2ExtraAction()
        {
            CurrentEndingTree.isDay2Exp = false;
            switch (CurrentEndingTree.isDay2Exp)
            {
                case true:
                    for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
                    {
                        var allActions = CurrentEndingTree.EndingsList[i].EndingBranch.AllActions;
                        if (i == 0 && allActions.Count == 1)
                        {
                            allActions.Add(new TargetActionData(2, -1, CmdType.None));
                            continue;
                        }
                        if (i == 0 && allActions.Count >= 2 && allActions[1].TargetAction.DayIndex != 2 && allActions[1].TargetAction.DayPart != -1)
                        {
                            allActions.Insert(1, new TargetActionData(2, -1, CmdType.None));
                            continue;
                        }
                        if (i == 0 && allActions.Count >= 2 && allActions[1].TargetAction.DayIndex == 2 && allActions[1].TargetAction.DayPart == -1 && !allActions[1].TargetAction.IgnoreDM)
                        {
                            // allActions[1].TargetAction.IgnoreDM = true;
                            continue;
                        }
                        if (allActions[0].TargetAction.DayIndex == 2 && allActions[0].TargetAction.DayPart == -1)
                        {
                            // allActions[0].TargetAction.IgnoreDM = true;
                            continue;
                        }
                    }
                    break;
                case false:
                    for (int i = 0; i < CurrentEndingTree.EndingsList.Count; i++)
                    {
                        var allActions = CurrentEndingTree.EndingsList[i].EndingBranch.AllActions;
                        if (i == 0 && allActions.Count >= 2 && allActions[1].TargetAction.DayIndex == 2 && allActions[1].TargetAction.DayPart == -1)
                        {
                            allActions.RemoveAt(1);
                            continue;
                        }
                        if (allActions[0].TargetAction.DayIndex == 2 && allActions[0].TargetAction.DayPart == -1)
                        {
                            allActions[0].ResetActionStats();
                        }
                    }
                    break;
            }
            SetEndingListViewData();

        }

        private void Day2Exp_Check_MouseClick(object sender, MouseEventArgs e)
        {
            var state = Properties.Settings.Default.ExperimentMode;
            if (!state)
            {
                const string msg = "Are you sure you want to change this variable?\n" +
                   "This will turn off error checking, however you will still be notified if a tree in invalidated.";
                var confirm = MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirm == DialogResult.No)
                {
                    Experiment_Check.Checked = false;
                    return;
                }
                else Experiment_Check.Checked = true;
            }
            else Experiment_Check.Checked = false;
            Properties.Settings.Default.ExperimentMode = Experiment_Check.Checked;
            //CurrentEndingTree.isDay2Exp = Experiment_Check.Checked;
            //if (_currentExp != CurrentEndingTree.isDay2Exp) _isExpEdited = true;
            //else _isExpEdited = false;
            //Day2ExtraAction();
        }

        private void OpenSteamNSO(object sender, EventArgs e)
        {
            try
            {
                Process.Start(InitializeValidSteamPath(), @"steam://rungameid/1451940");
            }
            catch { MessageBox.Show("Could not open the game from the Steam path: either the game doesn't exist, has been moved to another location, or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void MoveBranchDown_Button_Click(object sender, EventArgs e)
        {
            MoveBranchToNextIndex();
        }

        private void MoveBranchUp_Button_Click(object sender, EventArgs e)
        {
            MoveBranchToPreviousIndex();
        }

        private void File_MenuItem_DropDownOpened(object sender, EventArgs e)
        {
            OpenRecent_MenuItem.Text = $"Open Recent Ending Tree: {Path.GetFileName(_recentlyClosed)}"; ;
        }

        private void EndingTreeForm_Click(object sender, EventArgs e)
        {
            EndingListView.SelectedIndices.Clear();
        }

        private void Notes_Click(object sender, EventArgs e)
        {
            EndingListView.SelectedIndices.Clear();
        }

        private void ImportLog_Click(object sender, EventArgs e)
        {
            PlayLogViewer logViewer = new PlayLogViewer(this);
            logViewer.ShowDialog();
        }

        private void EndingListView_DragDrop(object sender, DragEventArgs e)
        {

            var data = e.Data;
            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                var tree = (string[])data.GetData(DataFormats.FileDrop);
                if (tree != null)
                    LoadExistingEndingTree(tree[0]);
            }

        }

        private void EndingTreeForm_DragDrop(object sender, DragEventArgs e)
        {


        }

        private void EndingTreeForm_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void EndingListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
