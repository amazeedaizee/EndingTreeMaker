using Newtonsoft.Json;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;
using static NSOEndingTreeMaker.EndingBranchSubData;

namespace NSOEndingTreeMaker
{
    [Serializable]
    public class EndingBranchData
    {
        public EndingBranch_Stub EndingBranch;
        public (int, int, EndingType) ExpectedDayOfEnd = new(0, 0, EndingType.Ending_None);
        public List<StreamIdeaObj> StreamIdeaList = new();
        public List<StreamUsedObj> StreamUsedList = new();
        public EventCounter hasGalacticRail = new(0, false);
        public EventCounter NoMeds = new(0, false);
        public EventCounter isReallyLove = new(0, false);
        public EventCounter isStressed = new(0, false);
        public EventCounter isReallyStressed = new(0, false);
        public EventCounter isTrauma = new(0, false);
        public EventCounter isVideo = new(0, false);
        public EventCounter isHorror = new(0, false);
        public EventCounter is150M = new(0, false);
        public EventCounter is300M = new(0, false);
        public EventCounter is500M = new(0, false);
        public EventCounter isMaxFollowers = new(0, false);
        public List<ActionCounter> LoveCounter = new();
        public List<ActionCounter> PsycheCounter = new();
        public List<ActionCounter> IgnoreCounter = new();
        public bool IgnoreNightEndings;

        [JsonConstructor]
        public EndingBranchData() { }

        public EndingBranchData(EndingBranchData endingBranch)
        {
            List<TargetActionData> actionList = CopyActionListFromExistingBranch(endingBranch);
            EndingBranch = new(endingBranch.EndingBranch.StartingDay, endingBranch.EndingBranch.EndingToGet, actionList, endingBranch.EndingBranch.IsStressfulBressdown);
            ExpectedDayOfEnd = new(endingBranch.ExpectedDayOfEnd.Item1, endingBranch.ExpectedDayOfEnd.Item2, endingBranch.ExpectedDayOfEnd.Item3);
            StreamIdeaList = SetStreamIdeasFromExistingBranch(endingBranch);
            StreamUsedList = SetUsedStreamsFromExistingBranch(endingBranch);
            hasGalacticRail = new(endingBranch.hasGalacticRail.DayIndex, endingBranch.hasGalacticRail.isEventing);
            NoMeds = new(endingBranch.NoMeds.DayIndex, endingBranch.NoMeds.isEventing);
            isReallyLove = new(endingBranch.isReallyLove.DayIndex, endingBranch.isReallyLove.isEventing);
            isStressed = new(endingBranch.isStressed.DayIndex, endingBranch.isStressed.isEventing);
            isReallyStressed = new(endingBranch.isReallyStressed.DayIndex, endingBranch.isReallyStressed.isEventing);
            isTrauma = new(endingBranch.isTrauma.DayIndex, endingBranch.isTrauma.isEventing);
            isVideo = new(endingBranch.isVideo.DayIndex, endingBranch.isVideo.isEventing);
            isHorror = new(endingBranch.isHorror.DayIndex, endingBranch.isHorror.isEventing);
            is150M = new(endingBranch.is150M.DayIndex, endingBranch.is150M.isEventing);
            is300M = new(endingBranch.is300M.DayIndex, endingBranch.is300M.isEventing);
            is500M = new(endingBranch.is500M.DayIndex, endingBranch.is500M.isEventing);
            isMaxFollowers = new(endingBranch.isMaxFollowers.DayIndex, endingBranch.isMaxFollowers.isEventing);
            LoveCounter = SetActionCountersFromExistingBranch(endingBranch.LoveCounter);
            PsycheCounter = SetActionCountersFromExistingBranch(endingBranch.PsycheCounter);
            IgnoreCounter = SetActionCountersFromExistingBranch(endingBranch.IgnoreCounter);
            IgnoreNightEndings = endingBranch.IgnoreNightEndings;

        }
        public EndingBranchData(int startDay, EndingType ending, List<TargetActionData> actions, bool isStressBreakdown = false)
        {
            EndingBranch = new EndingBranch_Stub(startDay, ending, actions, isStressBreakdown);
        }

        public List<TargetActionData> CopyActionListFromExistingBranch(EndingBranchData endingBranch)
        {
            var actionList = new List<TargetActionData>();
            var allActions = endingBranch.EndingBranch.AllActions;
            for (int i = 0; i < endingBranch.EndingBranch.AllActions.Count; i++)
            {
                TargetActionData action = new TargetActionData(allActions[i].TargetAction.DayIndex, allActions[i].TargetAction.DayPart, allActions[i].Command, allActions[i].TargetAction.IgnoreDM);
                action.ActionName = allActions[i].ActionName;
                action.ChangeStats(allActions[i].Followers, allActions[i].Stress, allActions[i].Affection, allActions[i].Darkness, allActions[i].StreamStreak, allActions[i].PreAlertBonus, allActions[i].Communication, allActions[i].Experience, allActions[i].Impact, allActions[i].GamerGirl, allActions[i].Cinephile, allActions[i].RabbitHole);
                action.CommandResult = allActions[i].CommandResult;
                action.StreamIdea = allActions[i].StreamIdea;
                action.MilestoneIdea = allActions[i].MilestoneIdea;
                actionList.Add(action);
            }
            return actionList;
        }

        public List<StreamIdeaObj> SetStreamIdeasFromExistingBranch(EndingBranchData endingBranch)
        {
            if (endingBranch == null) throw new ArgumentNullException("Argument branch cannot be null.");
            List<StreamIdeaObj> newStreamIdeas = new();
            var ideas = endingBranch.StreamIdeaList;
            for (int i = 0; i < endingBranch.StreamIdeaList.Count; i++)
            {
                newStreamIdeas.Add(new(ideas[i].DayIndex, ideas[i].DayPart, ideas[i].Idea));
            }
            return newStreamIdeas;
        }

        public List<StreamUsedObj> SetUsedStreamsFromExistingBranch(EndingBranchData endingBranch)
        {
            if (endingBranch == null) throw new ArgumentNullException("Argument branch cannot be null.");
            List<StreamUsedObj> newStreamUsed = new();
            var used = endingBranch.StreamUsedList;
            for (int i = 0; i < endingBranch.StreamUsedList.Count; i++)
            {
                newStreamUsed.Add(new(used[i].DayIndex, used[i].UsedStream));
            }
            return newStreamUsed;
        }


        public List<ActionCounter> SetActionCountersFromExistingBranch(List<ActionCounter> counter)
        {
            List<ActionCounter> newActionCounterList = new();
            for (int i = 0; i < counter.Count; i++)
            {
                newActionCounterList.Add(new(counter[i].DayIndex, counter[i].DayPart));
            }
            return newActionCounterList;
        }
        public List<(string branch, string action, string errorMsg)> ValidateBranch(string branchName)
        {
            List<(string, string action, string errorMsg)> errorList = new();
            ValidateMissingMandatoryEvents(errorList, branchName);
            for (int i = 0; i < EndingBranch.AllActions.Count; i++)
            {
                if (i == 0)
                    ValidateStartingDay(EndingBranch.AllActions[i], errorList, branchName);
                if (i == EndingBranch.AllActions.Count - 1)
                    break;
                if (i > 0)
                {
                    ValidatePreviousDay(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], errorList, branchName);
                    ValidateIgnoreDm(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], errorList, branchName);
                    ValidateNightEvents(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], errorList, branchName);
                }
                ValidateNextDay(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1], errorList, branchName);
                LookForMissingDays(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1], errorList, branchName);
                (bool, string, CmdType) actionValid = ValidateAction(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1]);
                if (!actionValid.Item1)
                {
                    errorList.Add(new(branchName, $"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", $"Action can't be done based on the stats of the previous action. \n\n({actionValid.Item2})"));
                }
                (bool, string) streamValid = ValidateStream(EndingBranch.AllActions[i + 1]);
                if (!streamValid.Item1)
                {
                    errorList.Add(new(branchName, $"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", streamValid.Item2));

                }
                if (EndingBranch.AllActions[i + 1].TargetAction.Action == ActionType.OkusuriHipuronModerate)
                {
                    errorList.Add(new(branchName, $"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", $"{NSODataManager.CmdName(CmdType.OkusuriHipuronModerate)} is currently not supported in the latest version of the game."));
                }
                (bool, string) specialEventValid = ValidateSpecialEvents(EndingBranch.AllActions[i + 1]);
                if (!specialEventValid.Item1)
                {
                    errorList.Add(new(branchName, $"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", specialEventValid.Item2));
                }
            }
            return errorList;
        }

        private void ValidateStartingDay(TargetActionData presentAction, List<(string, string, string)> errorList, string branchName)
        {
            if (presentAction.Followers <= 0)
            {
                errorList.Add(new(branchName, "", $"This branch's Starting Day is invalid."));
                return;
            }
        }
        private void ValidateIgnoreDm(TargetActionData pastAction, TargetActionData presentAction, List<(string, string, string)> errorList, string branchName)
        {
            var excludedCmds = new List<CmdType>() { CmdType.DarknessS2, CmdType.Darkness_1, CmdType.Darkness_2, CmdType.OdekakeOdaiba, CmdType.OdekakeZikka };
            if (!presentAction.TargetAction.IgnoreDM) return;
            if (pastAction.Stress >= 80 && presentAction.MilestoneIdea != CmdType.None)
            {
                if ((presentAction.TargetAction.DayPart == 0 && IsNotFixedEvents(pastAction, isTrauma.isEventing, isReallyLove.isEventing, isVideo.isEventing) && presentAction.MilestoneIdea == CmdType.None) || (presentAction.TargetAction.DayPart > 0 && presentAction.TargetAction.DayPart != 3))
                    errorList.Add(new(branchName, $"Day {presentAction.TargetAction.DayIndex}, {NSODataManager.DayPartNames[presentAction.TargetAction.DayPart]}: {NSODataManager.CmdName(presentAction.Command)}", $"Can't ignore DM if there's no DM to ignore (Ame won't send a DM if her Stress is 80 or more at the start of a part of a day, save for any fixed or milestone events at Noon.)"));
                return;
            }
            if (excludedCmds.Exists(c => c == presentAction.Command))
            {
                errorList.Add(new(branchName, $"Day {presentAction.TargetAction.DayIndex}, {NSODataManager.DayPartNames[presentAction.TargetAction.DayPart]}: {NSODataManager.CmdName(presentAction.Command)}", $"This action event is excluded from Ignore DM."));
                return;
            }
        }
        private void ValidateNextDay(TargetActionData presentAction, TargetActionData futureAction, List<(string, string, string)> errorList, string branchName)
        {
            if (presentAction.CommandResult.daypart == 0) return;
            if (presentAction.TargetAction.DayIndex == 1) return;
            int dayPassing;
            dayPassing = presentAction.TargetAction.DayPart + presentAction.CommandResult.daypart;
            if (presentAction.TargetAction.DayPart == -1 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart != 0)
            {
                errorList.Add(new(branchName, "", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (presentAction.TargetAction.DayPart == -1 && futureAction.TargetAction.DayIndex != presentAction.TargetAction.DayIndex)
            {
                errorList.Add(new(branchName, "", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (dayPassing > 2 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && presentAction.TargetAction.DayPart == 1 && futureAction.TargetAction.DayPart == 2)
            {
                errorList.Add(new(branchName, "", $"Day {presentAction.TargetAction.DayIndex} contains a Night Action even though the Dusk action would move it to next day."));
                return;
            }
            if (dayPassing == 2 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart == 1 && EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && a.TargetAction.DayPart == 2))
            {
                errorList.Add(new(branchName, "", $"Day {presentAction.TargetAction.DayIndex} contains a Dusk Action even though the Noon action would move it to Night."));
                return;
            }
            if (dayPassing <= 2 && presentAction.TargetAction.DayPart != -1 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart != dayPassing)
            {
                errorList.Add(new(branchName, "", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (dayPassing <= 2 && presentAction.TargetAction.DayPart != -1 && futureAction.TargetAction.DayIndex >= (presentAction.TargetAction.DayIndex + 1))
            {
                errorList.Add(new(branchName, "", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
        }

        private void ValidatePreviousDay(TargetActionData pastAction, TargetActionData presentAction, List<(string, string, string)> errorList, string branchName)
        {
            if (pastAction.CommandResult.daypart == 0) return;
            int dayPassing;
            dayPassing = pastAction.TargetAction.DayPart + pastAction.CommandResult.daypart;
            if (presentAction.TargetAction.DayPart > 0 && pastAction.TargetAction.DayIndex != presentAction.TargetAction.DayIndex)
            {
                errorList.Add(new(branchName, "", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
            }
        }

        private void LookForMissingDays(TargetActionData presentAction, TargetActionData futureAction, List<(string, string, string)> errorList, string branchName)
        {
            int presentDay = presentAction.TargetAction.DayIndex;
            if (futureAction.TargetAction.DayIndex <= presentDay) return;
            for (int i = presentDay; i < futureAction.TargetAction.DayIndex; i++)
            {
                if (EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == i)) continue;
                errorList.Add(new(branchName, "", $"No parts of Day {i} exist."));
            }

        }
        private (bool, string, CmdType) ValidateAction(TargetActionData presentAction, TargetActionData futureAction)
        {

            switch (futureAction.Command)
            {
                case CmdType.PlayIchatukuTalk:
                    if (presentAction.Affection >= 40) return (false, "Previous action must end with under 40 Affection.", futureAction.Command);
                    break;
                case CmdType.PlayIchatukuIchatuku:
                    if (presentAction.Affection < 40 || presentAction.Affection >= 80) return (false, "Previous action must end with Affection between 40-80, excluding 80.", futureAction.Command);
                    break;
                case CmdType.PlayIchatukuKizu:
                    if (presentAction.Affection < 80) return (false, "Previous action must end with 80 or more Affection", futureAction.Command);
                    break;
                case CmdType.PlayMakeLove:
                    if (presentAction.TargetAction.DayPart == 0 && (presentAction.TargetAction.Action.ToString().Contains("Overdose") || presentAction.TargetAction.Action == ActionType.OkusuriHappa || presentAction.TargetAction.Action == ActionType.OkusuriPsyche))
                        return (false, "Previous action must not be any action that involves non-moderate drug use.", futureAction.Command);
                    break;
                case CmdType.PlayKimeLove:
                    if (presentAction.TargetAction.DayPart == 0 && presentAction.TargetAction.Action == ActionType.OkusuriHappa) break;
                    if (presentAction.TargetAction.DayPart == 0 && presentAction.TargetAction.Action == ActionType.OkusuriPsyche) break;
                    if (presentAction.TargetAction.DayPart == 0 && presentAction.TargetAction.Action.ToString().Contains("Overdose")) break;
                    return (false, "Previous action must be any action that involves non-moderate drug use.", futureAction.Command);
                case CmdType.SleepToTwilight1:
                    if (futureAction.TargetAction.DayPart > 0) return (false, "This action is not done during Noon.", futureAction.Command);
                    break;
                case CmdType.SleepToNight2:
                    if (futureAction.TargetAction.DayPart > 0) return (false, "This action is not done during Noon.", futureAction.Command);
                    break;
                case CmdType.SleepToNight1:
                    if (futureAction.TargetAction.DayPart != 1) return (false, "This action is not done during Dusk.", futureAction.Command);
                    break;
                case CmdType.SleepToTomorrow3:
                    if (futureAction.TargetAction.DayPart > 0) return (false, "This action is not done during Noon.", futureAction.Command);
                    break;
                case CmdType.SleepToTomorrow2:
                    if (futureAction.TargetAction.DayPart != 1) return (false, "This action is not done during Dusk.", futureAction.Command);
                    break;
                case CmdType.SleepToTomorrow1:
                    if (futureAction.TargetAction.DayPart != 2) return (false, "This action is not done during Night.", futureAction.Command);
                    break;
                case CmdType.OkusuriPuronModerate:
                    if (presentAction.Darkness < 20) return (false, "Previous action must end with under 20 Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriHipuronModerate:
                    if (presentAction.Darkness < 40) return (false, "Previous action must end with under 40 Darkness", futureAction.Command);
                    break;
                case CmdType.OkusuriDaypassOverdoseY1:
                    if (presentAction.Darkness >= 20) return (false, "Previous action must end with under 20 Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriDaypassOverdoseY2:
                    if (presentAction.Darkness >= 40 || presentAction.Darkness < 20) return (false, "Previous action must end with Darkness between 20-40, excluding 40.", futureAction.Command);
                    break;
                case CmdType.OkusuriDaypassOverdoseY3:
                    if (presentAction.Darkness >= 60 || presentAction.Darkness < 40) return (false, "Previous action must end with Darkness between 40-60, excluding 60.", futureAction.Command);
                    break;
                case CmdType.OkusuriDaypassOverdoseY4:
                case CmdType.OkusuriDaypassOverdoseY5:
                    if (presentAction.Darkness < 60) return (false, "Previous action must end with 60 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriPuronOverdoseY2:
                    if (presentAction.Darkness >= 40 || presentAction.Darkness < 20) return (false, "Previous action must end with Darkness between 20-40, excluding 40.", futureAction.Command);
                    break;
                case CmdType.OkusuriPuronOverdoseY3:
                    if (presentAction.Darkness >= 60 || presentAction.Darkness < 40) return (false, "Previous action must end with Darkness between 40-60, excluding 60.", futureAction.Command);
                    break;
                case CmdType.OkusuriPuronOverdoseY4:
                case CmdType.OkusuriPuronOverdoseY5:
                    if (presentAction.Darkness < 60) return (false, "Previous action must end with 60 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriHiPuronOverdoseY3:
                    if (presentAction.Darkness >= 60 || presentAction.Darkness < 40) return (false, "Previous action must end with Darkness between 40-60, excluding 60.", futureAction.Command);
                    break;
                case CmdType.OkusuriHiPuronOverdoseY4:
                case CmdType.OkusuriHiPuronOverdoseY5:
                    if (presentAction.Darkness < 60) return (false, "Previous action must end with 60 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriHappaY4:
                    if (presentAction.Darkness >= 80 || presentAction.Darkness < 60) return (false, "Previous action must end with Darkness between 60-80, excluding 80.", futureAction.Command);
                    break;
                case CmdType.OkusuriHappaY5:
                    if (presentAction.Darkness < 80) return (false, "Previous action must end with 80 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.OkusuriPsyche:
                    if (presentAction.Darkness < 80) return (false, "Previous action must end with 80 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.InternetPoketterF0Y12:
                    if (presentAction.Followers >= 10000) return (false, "Previous action must end with under 10000 Followers.", futureAction.Command);
                    break;
                case CmdType.InternetPoketterF1Y12:
                    if (presentAction.Followers >= 100000 || presentAction.Followers < 10000 || (presentAction.Followers >= 10000 && presentAction.Followers < 100000 && presentAction.Darkness >= 40))
                        return (false, "Previous action must end with Followers between 10000-100000, excluding 100000, and under 40 Darkness.", futureAction.Command);
                    break;
                case CmdType.InternetPoketterPoem:
                    if (presentAction.Followers < 100000) return (false, "Previous action must end with 100000 or more Followers.", futureAction.Command);
                    break;
                case CmdType.InternetPoketterF0Y45:
                    if (presentAction.Followers >= 100000 || ((presentAction.Followers < 10000 && presentAction.Darkness < 20) && !(presentAction.Followers < 100000 && presentAction.Followers >= 10000 && presentAction.Darkness >= 40 && presentAction.Darkness < 60)))
                        return (false, "Previous action must end with: Followers under 10000 with Darkness 20 or over, or Followers between 10000-100000 excluding 100000 with Darkness between 40-60, excluding 60.", futureAction.Command);
                    break;
                case CmdType.InternetPoketterF1Y45:
                    if (presentAction.Followers >= 100000 || !(presentAction.Followers >= 10000 && presentAction.Followers < 100000 && presentAction.Darkness >= 60))
                        return (false, "Previous action must end with Followers between 10000 and 100000, excluding 100000, and 80 or more Darkness.", futureAction.Command);
                    break;
                case CmdType.Internet2chY12:
                    if (presentAction.Darkness >= 40) return (false, "Previous action must end with under 40 Darkness", futureAction.Command);
                    break;
                case CmdType.Internet2chY3:
                    if (presentAction.Darkness < 40 || presentAction.Darkness >= 60) return (false, "Previous action must end with Darkness between 40-60, excluding 60.", futureAction.Command);
                    break;
                case CmdType.Internet2chY45:
                    if (presentAction.Darkness < 60) return (false, "Previous action must end with 60 or more Darkness", futureAction.Command);
                    break;
                default: break;
            }
            return (true, "", futureAction.Command);
        }

        private (bool, string) ValidateStream(TargetActionData action)
        {
            if (action.TargetAction.Action != ActionType.Haishin) return (true, "");
            if (action.Command == CmdType.Darkness_1 || action.Command == CmdType.Darkness_2) return (true, "");
            bool ideaExists = StreamIdeaList.Exists(i => i.Idea == action.Command);
            var breakdownThree = StreamUsedList.FirstOrDefault(u => u.UsedStream == CmdType.Yamihaishin_3);
            var cultStreamIdea = StreamIdeaList.FirstOrDefault(u => u.Idea == CmdType.Error);
            if (action.TargetAction.DayPart != 2)

                return (false, $"You can only stream at night.");

            if (!ideaExists)

                return (false, $"This stream's idea has not been found. You can only stream that once you found that idea.");

            var idea = StreamIdeaList.FirstOrDefault(i => i.Idea == action.Command);
            if (idea.DayIndex > action.TargetAction.DayIndex && idea.Idea == action.Command)

                return (false, $"This stream's idea has been found after that stream has been done, which is not allowed. \n\nDay Streamed: {action.TargetAction.DayIndex} \nDay Idea was found: {idea.DayIndex}");

            if (StreamUsedList.Exists(u => u.DayIndex < action.TargetAction.DayIndex && u.UsedStream == action.Command))
            {
                var usedStream = StreamUsedList.Find(u => u.DayIndex < action.TargetAction.DayIndex && u.UsedStream == action.Command);
                return (false, $"You can't do this stream more than once. \n\nStream first done on: Day {usedStream.DayIndex}");
            }

            if (breakdownThree != null && action.TargetAction.DayIndex > breakdownThree.DayIndex)
            {
                if (action.TargetAction.DayIndex == (breakdownThree.DayIndex + 1))
                    return (false, $"You can't stream anything after doing Breakdown Stream 3. You can stream after one day.");
                if (action.TargetAction.DayIndex == (breakdownThree.DayIndex + 2))
                    return (false, $"You can't stream anything after doing Breakdown Stream 3. You can stream tomorrow.");
            }
            if (cultStreamIdea != null && action.TargetAction.DayIndex > cultStreamIdea.DayIndex && action.Command != CmdType.Error)
            {
                return (false, $"You can't stream anything other than Conspiracy Stream 6 after getting the idea for Conspiracy Stream 6. \n\nDay Conspiracy Stream 6 was found: {cultStreamIdea.DayIndex}, on {NSODataManager.DayPartNames[cultStreamIdea.DayPart]}");
            }
            if (action.Command.ToString().StartsWith("Angel"))
            {
                var stream = action.Command.ToString().Split('_');
                int streamLevel = int.Parse(stream[1]);
                bool mileExists = StreamIdeaList.Exists(i => i.Idea.ToString().StartsWith("Angel") && int.Parse(i.Idea.ToString().Split('_')[1]) > streamLevel);
                var milestoneHigher = StreamIdeaList.FirstOrDefault(i => i.Idea.ToString().StartsWith("Angel") && int.Parse(i.Idea.ToString().Split('_')[1]) > streamLevel);
                if (mileExists && action.TargetAction.DayIndex > milestoneHigher.DayIndex) return (false, $"You can't do Milestone Streams that are levels lower than the highest leveled Milestone Stream idea.");
            }
            return (true, "");
        }

        private (bool, string) ValidateSpecialEvents(TargetActionData action)
        {
            bool isStress = this.isStressed.isEventing && action.TargetAction.DayIndex >= this.isStressed.DayIndex && isStressed.DayIndex >= EndingBranch.StartingDay;
            bool isReallyStress = this.isReallyStressed.isEventing && action.TargetAction.DayIndex >= this.isReallyStressed.DayIndex && isReallyStressed.DayIndex >= EndingBranch.StartingDay;
            bool isNoMeds = this.NoMeds.isEventing && action.TargetAction.DayIndex >= this.NoMeds.DayIndex && NoMeds.DayIndex >= EndingBranch.StartingDay;
            int actionIndex = EndingBranch.AllActions.IndexOf(action);
            if (isNoMeds)
            {
                if (NSODataManager.IsOverdoseAction(action))
                    return (false, $"Drugs cannot be taken after Day {NoMeds.DayIndex - 1} (the day you did Internet Angel 5.) ");
            }
            if (action.Command == CmdType.OdekakeZikka)
            {
                if (action.TargetAction.DayIndex != 24 && action.TargetAction.DayIndex != 0)
                    return (false, $"Going to {NSODataManager.CmdName(action.Command)} can only be done on Day 24 at Noon.");
                if (EndingBranch.AllActions[actionIndex - 1].Affection < 80)
                    return (false, $"Going to {NSODataManager.CmdName(action.Command)} can only be done when the previous day ends at Affection 80 or higher.");
            }
            if (action.Command == CmdType.OdekakeOdaiba)
            {
                if (action.TargetAction.DayIndex != 27 && action.TargetAction.DayIndex != 0)
                    return (false, $"Doing the {NSODataManager.CmdName(action.Command)} can only be done on Day 27 at Noon.");
                if (EndingBranch.AllActions[actionIndex - 1].Followers < 500000)
                    return (false, $"Going to {NSODataManager.CmdName(action.Command)} can only be done when the previous day ends at 500000 Followers or more.");
            }
            if (action.Command == CmdType.DarknessS1)
            {
                if (!isStressed.isEventing)
                    return (false, $"Ame is not that stressed on Day {action.TargetAction.DayIndex}.");
                if (isStress && action.TargetAction.DayIndex != isStressed.DayIndex)
                    return (false, $"Ame will only {NSODataManager.CmdName(action.Command)} on Day {isStressed.DayIndex}.");
                if (action.TargetAction.DayPart != 0)
                    return (false, $"Ame will only {NSODataManager.CmdName(action.Command)} at Noon.");
                if (actionIndex == EndingBranch.AllActions.Count - 1 || EndingBranch.AllActions[actionIndex + 1].Command != CmdType.Darkness_1)
                    return (false, $"{NSODataManager.CmdName(action.Command)} must be succeeded with the {NSODataManager.CmdName(CmdType.Darkness_1)} stream at night.");
            }
            if (action.Command == CmdType.DarknessS2)
            {
                if (!isStressed.isEventing)
                    return (false, $"Ame is not that stressed on Day {action.TargetAction.DayIndex}.");
                if (isStress && !isReallyStressed.isEventing)
                    return (false, $"Ame is stressed but not very stressed on Day {action.TargetAction.DayIndex}.");
                if (isStress && isStressed.DayIndex >= EndingBranch.StartingDay && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1))
                    return (false, $"Ame has to {NSODataManager.CmdName(CmdType.DarknessS1)} first on Day {isStressed.DayIndex} at Noon.");
                if (isStress && EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1 && a.TargetAction.DayIndex >= action.TargetAction.DayIndex))
                    return (false, $"{NSODataManager.CmdName(CmdType.DarknessS1)} must be on Day {isStressed.DayIndex} at Noon, before {NSODataManager.CmdName(action.Command)}.");
                if (isReallyStress && action.TargetAction.DayIndex != isReallyStressed.DayIndex)
                    return (false, $"Ame will only {NSODataManager.CmdName(action.Command)} on Day {isReallyStressed.DayIndex}.");
                if (action.TargetAction.DayPart != 0)
                    return (false, $"Ame will only {NSODataManager.CmdName(action.Command)} at Noon.");
                if (actionIndex == EndingBranch.AllActions.Count - 1 || EndingBranch.AllActions[actionIndex + 1].Command != CmdType.Darkness_2)
                    return (false, $"{NSODataManager.CmdName(action.Command)} must be succeeded with the {NSODataManager.CmdName(CmdType.Darkness_2)} stream at night.");
            }
            if (action.Command == CmdType.OdekakeGinga)
            {
                if (!StreamUsedList.Exists(c => c.UsedStream == CmdType.Kaidan_5))
                    return (false, "Netlore 5 has not been streamed. You need Netlore 5 to go to the Galactic Rail.");
                if (StreamUsedList.Exists(c => c.UsedStream == CmdType.Kaidan_5 && c.DayIndex >= action.TargetAction.DayIndex))
                    return (false, "Netlore 5 has to be done before going to the Galactic Rail.");
                if (!hasGalacticRail.isEventing)
                    return (false, "Netlore 5 has been done, but Galactic Rail has not been found yet due to a midnight event from the previous day.");
                if (hasGalacticRail.isEventing && action.TargetAction.DayIndex < hasGalacticRail.DayIndex)
                    return (false, "You can't go to the Galactic Rail before it has been found.");
                if (!NSODataManager.IsOverdoseAction(EndingBranch.AllActions[actionIndex - 1]))
                    return (false, "Drugs have to be overdosed on Noon on the same day before going into the Galactic Rail at night.");
                if (action.TargetAction.DayPart != 2)
                    return (false, "The Galactic Rail can only be visited at night.");
            }
            return (true, "");
        }

        public void ValidateMissingMandatoryEvents(List<(string, string, string)> errorList, string branchName)
        {
            List<(string, string, string)> eventErrors = new();

            if (isStressed.isEventing && isStressed.DayIndex >= EndingBranch.StartingDay && !DoesEventConflictExpectedEnd(isStressed.DayIndex))
            {
                var stress = EndingBranch.AllActions.FirstOrDefault(a => a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isStressed.DayIndex);
                if (stress == null || (stress.Command != CmdType.DarknessS1 && (!isReallyStressed.isEventing || stress.TargetAction.DayIndex < isReallyStressed.DayIndex)))
                    eventErrors.Add(new(branchName, "", $"Ame is stressed. You must do {NSODataManager.CmdName(CmdType.DarknessS1)} on Day {isStressed.DayIndex} at Noon."));
            }
            if (isStressed.isEventing && isReallyStressed.isEventing && isReallyStressed.DayIndex >= EndingBranch.StartingDay && !DoesEventConflictExpectedEnd(isReallyStressed.DayIndex))
            {
                var reallyStress = EndingBranch.AllActions.FirstOrDefault(a => a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isReallyStressed.DayIndex);
                if (reallyStress == null || reallyStress.Command != CmdType.DarknessS2)
                    eventErrors.Add(new(branchName, "", $"Ame is really stressed. You must do {NSODataManager.CmdName(CmdType.DarknessS2)} on Day {isReallyStressed.DayIndex} at Noon."));
            }
            else if (isReallyLove.isEventing && isReallyLove.DayIndex >= EndingBranch.StartingDay && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeZikka && a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isReallyLove.DayIndex) && !DoesEventConflictExpectedEnd(isReallyLove.DayIndex))
            {
                eventErrors.Add(new(branchName, "", $"Going to {NSODataManager.CmdName(CmdType.OdekakeZikka)} has to be done on Day 24 at Noon."));
            }
            else if (isVideo.isEventing && isVideo.DayIndex >= EndingBranch.StartingDay && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeOdaiba && a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isVideo.DayIndex) && !DoesEventConflictExpectedEnd(isVideo.DayIndex))
            {
                eventErrors.Add(new(branchName, "", $"Doing the {NSODataManager.CmdName(CmdType.OdekakeOdaiba)} has to be done on Day 27 at Noon."));
            }
            errorList.InsertRange(0, eventErrors);

            bool DoesEventConflictExpectedEnd(int dayIndex)
            {
                if (ExpectedDayOfEnd.Item3 == EndingType.Ending_None)
                    return false;
                else return dayIndex >= ExpectedDayOfEnd.Item1;
            }
        }

        public bool IsNotFixedEvents(TargetActionData action, bool isTrauma, bool isParents, bool isVideo)
        {
            int nextDay = action.TargetAction.DayIndex + 1;
            if (action.TargetAction.DayPart == -1) nextDay--;
            if (action.TargetAction.DayPart + action.CommandResult.daypart < 3 && action.TargetAction.DayPart != -1)
                return false;
            if (this.isTrauma.isEventing && action.TargetAction.DayIndex >= this.isTrauma.DayIndex) isTrauma = true;
            if (this.isReallyLove.isEventing && action.TargetAction.DayIndex >= this.isReallyLove.DayIndex) isParents = true;
            if (this.isVideo.isEventing && action.TargetAction.DayIndex >= this.isVideo.DayIndex) isVideo = true;
            if (nextDay == 5 || nextDay == 20)
            {
                return false;
            }
            if (nextDay == 16 && isTrauma)
            {
                return false;
            }
            if (nextDay == 24 && isParents)
            {
                return false;
            }
            if ((nextDay == 27 || nextDay == 29) && isVideo)
            {
                return false;
            }
            return true;
        }

        public bool IsNotMidnightEvents(TargetActionData action, EventCounter hasGalaxy, EventCounter is150M, EventCounter is300M, EventCounter is500M)
        {
            return IsNotMidnightEvents(action, (hasGalaxy.DayIndex, hasGalaxy.isEventing), (is150M.DayIndex, is150M.isEventing), (is300M.DayIndex, is300M.isEventing), (is500M.DayIndex, is500M.isEventing));
        }

        public bool IsNotMidnightEvents(TargetActionData action, (int, bool) hasGalaxy, (int, bool) is150M, (int, bool) is300M, (int, bool) is500M)
        {
            if (this.hasGalacticRail.isEventing && action.TargetAction.DayIndex >= this.hasGalacticRail.DayIndex)
            {
                hasGalaxy.Item2 = true;
                hasGalaxy.Item1 = this.hasGalacticRail.DayIndex;
            }
            if (this.is150M.isEventing && action.TargetAction.DayIndex >= this.is150M.DayIndex)
            {
                is150M.Item2 = true;
                is150M.Item1 = this.is150M.DayIndex;
            }
            if (this.is300M.isEventing && action.TargetAction.DayIndex >= this.is300M.DayIndex)
            {
                is300M.Item2 = true;
                is300M.Item1 = this.is300M.DayIndex;
            }
            if (this.is500M.isEventing && action.TargetAction.DayIndex >= this.is500M.DayIndex)
            {
                is500M.Item2 = true;
                is500M.Item1 = this.is500M.DayIndex;
            }
            if (action.TargetAction.DayPart + action.CommandResult.daypart < 3)
                return false;
            if (action.TargetAction.DayIndex == 17)
                return false;
            if (action.TargetAction.DayIndex == 22)
                return false;
            if (action.TargetAction.DayIndex == 23 && action.Affection >= 80)
                return false;
            if (action.TargetAction.DayIndex == 27)
                return false;
            if (this.StreamUsedList.Exists(s => s.DayIndex == action.TargetAction.DayIndex && s.UsedStream == CmdType.Kaidan_5) && hasGalaxy.Item2 && action.TargetAction.DayIndex == (hasGalaxy.Item1 - 1))
                return false;
            if (is150M.Item2 && action.TargetAction.DayIndex == (is150M.Item1 - 1))
                return false;
            if (is300M.Item2 && action.TargetAction.DayIndex == (is300M.Item1 - 1))
                return false;
            if (is500M.Item2 && action.TargetAction.DayIndex == (is500M.Item1 - 1))
                return false;
            return true;
        }

        public bool IsNotStressEvents(TargetActionData action, EventCounter isStressed, EventCounter isReallyStressed)
        {
            return IsNotStressEvents(action, (isStressed.DayIndex, isStressed.isEventing), (isReallyStressed.DayIndex, isReallyStressed.isEventing));
        }

        public bool IsNotStressEvents(TargetActionData action, (int, bool) isStressed, (int, bool) isReallyStressed)
        {
            if (this.isStressed.isEventing && action.TargetAction.DayIndex >= this.isStressed.DayIndex)
            {
                isStressed.Item2 = true;
                isStressed.Item1 = this.isStressed.DayIndex;
            }
            if (this.isReallyStressed.isEventing && action.TargetAction.DayIndex >= this.isReallyStressed.DayIndex)
            {
                isReallyStressed.Item2 = true;
                isReallyStressed.Item1 = this.isReallyStressed.DayIndex;
            }
            int nextDay = action.TargetAction.DayIndex + 1;
            if (action.TargetAction.DayPart == -1) nextDay--;
            if (isStressed.Item2 && isStressed.Item1 == nextDay)
            {
                return false;
            }
            if (isReallyStressed.Item2 && isReallyStressed.Item1 == nextDay)
            {
                return false;
            }
            return true;
        }

        public void InitializeActionStats(EndingBranchEditor branchWindow = null)
        {
            var branch = this;
            int startingDay = branch.EndingBranch.StartingDay;
            int latestDay = EndingBranch.AllActions[EndingBranch.AllActions.Count - 1].TargetAction.DayIndex;
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
            for (int i = 0; i < EndingBranch.AllActions.Count; i++)
            {
                if (i == 0)
                {
                    if (EndingBranch.AllActions[0].TargetAction.DayIndex == 1)
                    {
                        EndingBranch.AllActions[0].ResetActionStats();
                    }
                    EndingBranch.AllActions[i].CommandResult = new CommandAction();
                    SetStressFlag(EndingBranch.AllActions[i]);
                    expectedEnding = branch.CheckIfEndingAchieved(null, EndingBranch.AllActions[i], ReallyStressed.Item2, Horror.Item2, VisitParents.Item2, NoMeds.Item2, isMaxFollowers.Item2, IgnoreNightEndings);
                    continue;
                }
                ChangePresentCmdBasedOnPastStats(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i]);
                if (EndingBranch.AllActions[i].TargetAction.DayPart != 3) EndingBranch.AllActions[i].CommandResult = NSOCommandManager.CmdTypeToCommand(EndingBranch.AllActions[i].Command);
                NSOCommandManager.CalculateStats(this, EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], NoMeds.Item2);
                AddIdeaOrUsedStream(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i]);
                EndingBranch.AllActions[i].SetStatsToMaxOrMin(this, Stressed.Item2, ReallyStressed.Item2, VisitParents.Item2);
                SetActionCounterFlags(EndingBranch.AllActions[i]);
                SetMidnightEventFlags(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i]);
                SetExtraMilestoneFlags(EndingBranch.AllActions[i]);
                if (EndingBranch.AllActions[i].TargetAction.DayIndex == 24 && (EndingBranch.AllActions[i].TargetAction.DayPart + EndingBranch.AllActions[i].CommandResult.daypart) >= 3)
                {
                    if (EndingBranch.AllActions[i].Stress >= 80 && ((ReallyStressed.Item2 && ReallyStressed.Item1 < 25) || (branch.isReallyStressed.isEventing && branch.isReallyStressed.DayIndex < 25)) && !Horror.Item2)
                    {
                        Horror.Item1 = 25;
                        Horror.Item2 = true;
                    }
                }
                SetStressFlag(EndingBranch.AllActions[i]);
                if (branch.StreamUsedList.Exists(u => u.DayIndex <= EndingBranch.AllActions[i].TargetAction.DayIndex && u.UsedStream == CmdType.Angel_5) && !ReallyStressed.Item2 && !NoMeds.Item2)
                {
                    NoMeds.Item1 = EndingBranch.AllActions[i].TargetAction.DayIndex + 1;
                    NoMeds.Item2 = true;
                }
                EndingBranch.AllActions[i].MilestoneIdea = CmdType.None;
                if (branch.IsNotFixedEvents(EndingBranch.AllActions[i - 1], Trauma.Item2, VisitParents.Item2, MusicVideo.Item2) && branch.IsNotStressEvents(EndingBranch.AllActions[i - 1], Stressed, ReallyStressed))
                {
                    if (ReallyStressed.Item2 && EndingBranch.AllActions[i].TargetAction.DayIndex >= ReallyStressed.Item1 && EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS2 && a.TargetAction.DayIndex == ReallyStressed.Item1)) isDarkAngel = true;
                    NSODataManager.InitializeMilestoneIdea(EndingBranch.AllActions[i], branch, isDarkAngel);
                }
                if (expectedEnding.Item1 == 0)
                {
                    expectedEnding = branch.CheckIfEndingAchieved(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], ReallyStressed.Item2, Horror.Item2, VisitParents.Item2, NoMeds.Item2, isMaxFollowers.Item2, IgnoreNightEndings);
                    ExpectedDayOfEnd = expectedEnding;
                }

                if (i > 0) branchWindow?.EditActionVisualData(EndingBranch.AllActions[i - 1]);
                branchWindow?.EditActionVisualData(EndingBranch.AllActions[i]);
                branchWindow?.ideasWindow?.UpdateFoundIdeas();
                branchWindow?.usedWindow?.UpdateUsed();
            }
            SetNewEventFlags();
            branchWindow?.SetEventInfo(branch);
            branchWindow?.SetExtraMilestoneGraphic(branch);
            branchWindow?.SetGuessedEnding(branch, expectedEnding);
            branchWindow?.SetActionCounterText(branch);

            void SetActionCounterFlags(TargetActionData action)
            {
                if (action.TargetAction.Action == ActionType.PlayMakeLove)
                {
                    branch.LoveCounter.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart));
                }
                if (action.TargetAction.Action == ActionType.OkusuriPsyche)
                {
                    branch.PsycheCounter.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart));
                }
                if (action.TargetAction.IgnoreDM)
                {
                    branch.IgnoreCounter.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart));
                    action.Stress += 4;
                    action.Affection += -5;
                    action.SetStatsToMaxOrMin(this, Stressed.Item2, ReallyStressed.Item2, VisitParents.Item2);
                }
            }

            void SetMidnightEventFlags(TargetActionData pastAction, TargetActionData action)
            {
                if (action.TargetAction.DayIndex == 15 && action.TargetAction.DayPart == 3)
                {
                    if (pastAction.Affection >= 60 && pastAction.Darkness >= 60 && !Trauma.Item2)
                    {
                        Trauma.Item1 = action.TargetAction.DayIndex + 1;
                        Trauma.Item2 = true;
                    }
                }
                if (action.TargetAction.DayIndex == 23 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && action.Affection >= 80 && !VisitParents.Item2)
                {
                    VisitParents.Item1 = action.TargetAction.DayIndex + 1;
                    VisitParents.Item2 = true;

                }
                if (action.TargetAction.DayIndex == 26 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3)
                {
                    if (action.Followers >= 500000 && !MusicVideo.Item2)
                    {
                        MusicVideo.Item1 = action.TargetAction.DayIndex + 1;
                        MusicVideo.Item2 = true;
                    }
                }
                if (branch.StreamUsedList.Exists(s => s.DayIndex <= action.TargetAction.DayIndex && s.UsedStream == CmdType.Kaidan_5) && branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M) && !HasGalaxy.Item2)
                {
                    HasGalaxy.Item1 = action.TargetAction.DayIndex + 1;
                    if (branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M))
                    {
                        HasGalaxy.Item2 = true;
                    }

                }
            }

            void SetExtraMilestoneFlags(TargetActionData action)
            {
                if (action.Followers >= 1500000 && !is150M.Item2 && branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M))
                {
                    is150M.Item1 = action.TargetAction.DayIndex + 1;
                    is150M.Item2 = true;
                }
                if (action.Followers >= 3000000 && !is300M.Item2 && branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M))
                {
                    is300M.Item1 = action.TargetAction.DayIndex + 1;
                    is300M.Item2 = true;
                }
                if (action.Followers >= 5000000 && !is500M.Item2 && branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M))
                {
                    is500M.Item1 = action.TargetAction.DayIndex + 1;
                    is500M.Item2 = true;
                }
                if (action.Followers >= 9999999 && branch.IsNotMidnightEvents(action, HasGalaxy, is150M, is300M, is500M))
                {
                    isMaxFollowers.Item1 = action.TargetAction.DayIndex;
                    isMaxFollowers.Item2 = true;
                }
            }
            void SetStressFlag(TargetActionData action)
            {
                bool isNotBeforeBreakdownDay = !(action.TargetAction.DayIndex == 15 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && action.TargetAction.DayPart < 3);
                if (action.Stress >= 100
                   && isNotBeforeBreakdownDay
                  && (Stressed.Item2 || (isStressed.isEventing && isStressed.DayIndex <= EndingBranch.StartingDay && action.TargetAction.DayIndex != isStressed.DayIndex))
                  && !ReallyStressed.Item2
                  && branch.IsNotFixedEvents(action, Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    ReallyStressed.Item1 = action.TargetAction.DayIndex + 1;
                    ReallyStressed.Item2 = true;
                }
                if (action.Stress >= 80 && !Stressed.Item2 && isNotBeforeBreakdownDay && branch.IsNotFixedEvents(action, Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    if (aboutToStress)
                    {
                        Stressed.Item1 = action.TargetAction.DayIndex + 1;
                        Stressed.Item2 = true;
                    }
                    else aboutToStress = true;
                }
                if (action.Stress < 80 && isNotBeforeBreakdownDay && aboutToStress && !Stressed.Item2 && branch.IsNotFixedEvents(action, Trauma.Item2, VisitParents.Item2, MusicVideo.Item2))
                {
                    aboutToStress = false;
                }
            }
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
        }

        private void ChangePresentCmdBasedOnPastStats(TargetActionData pastAction, TargetActionData presentAction)
        {
            var a = presentAction.TargetAction.Action;
            if (presentAction.TargetAction.DayPart == 3) return;
            if (a == ActionType.PlayIchatuku)
            {
                if (pastAction.Affection < 40)
                    presentAction.Command = CmdType.PlayIchatukuTalk;
                else if (pastAction.Affection >= 80)
                    presentAction.Command = CmdType.PlayIchatukuKizu;
                else
                    presentAction.Command = CmdType.PlayIchatukuIchatuku;
            }
            if (a == ActionType.PlayMakeLove)
            {
                if (pastAction.TargetAction.DayPart == 0 && (pastAction.TargetAction.Action.ToString().Contains("Overdose") || pastAction.TargetAction.Action == ActionType.OkusuriHappa || pastAction.TargetAction.Action == ActionType.OkusuriPsyche))
                    presentAction.Command = CmdType.PlayKimeLove;
                else
                    presentAction.Command = CmdType.PlayMakeLove;
            }
            if (a == ActionType.SleepToNight)
            {
                if (presentAction.TargetAction.DayPart == 0)
                    presentAction.Command = CmdType.SleepToNight2;
                else
                    presentAction.Command = CmdType.SleepToNight1;
            }
            if (a == ActionType.SleepToTomorrow)
            {
                if (presentAction.TargetAction.DayPart == 0)
                    presentAction.Command = CmdType.SleepToTomorrow3;
                else if (presentAction.TargetAction.DayPart == 1)
                    presentAction.Command = CmdType.SleepToTomorrow2;
                else
                    presentAction.Command = CmdType.SleepToTomorrow1;
            }
            if (a == ActionType.OkusuriDaypassOverdose)
            {
                if (pastAction.Darkness < 20)
                    presentAction.Command = CmdType.OkusuriDaypassOverdoseY1;
                else if (pastAction.Darkness >= 20 && pastAction.Darkness < 40)
                    presentAction.Command = CmdType.OkusuriDaypassOverdoseY2;
                else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                    presentAction.Command = CmdType.OkusuriDaypassOverdoseY3;
                else presentAction.Command = CmdType.OkusuriDaypassOverdoseY4;
            }
            if (a == ActionType.OkusuriPuronOverdose)
            {
                if (pastAction.Darkness < 40)
                    presentAction.Command = CmdType.OkusuriPuronOverdoseY2;
                else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                    presentAction.Command = CmdType.OkusuriPuronOverdoseY3;
                else
                    presentAction.Command = CmdType.OkusuriPuronOverdoseY4;
            }
            if (a == ActionType.OkusuriHiPuronOverdose)
            {
                if (pastAction.Darkness < 60)
                    presentAction.Command = CmdType.OkusuriHiPuronOverdoseY3;
                else
                    presentAction.Command = CmdType.OkusuriHiPuronOverdoseY4;
            }
            if (a == ActionType.OkusuriHappa)
            {
                if (pastAction.Darkness < 80)
                    presentAction.Command = CmdType.OkusuriHappaY4;
                else presentAction.Command = CmdType.OkusuriHappaY5;
            }
            if (a == ActionType.InternetPoketter)
            {
                if (pastAction.Followers < 10000 && pastAction.Darkness < 20)
                    presentAction.Command = CmdType.InternetPoketterF0Y12;
                else if ((pastAction.Followers < 10000 && pastAction.Darkness >= 20) || (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness >= 40 && pastAction.Darkness < 60))
                    presentAction.Command = CmdType.InternetPoketterF0Y45;
                else if (pastAction.Followers < 100000 && pastAction.Followers >= 10000 && pastAction.Darkness < 40)
                    presentAction.Command = CmdType.InternetPoketterF1Y12;
                else if (pastAction.Followers >= 10000 && pastAction.Followers < 100000 && pastAction.Darkness >= 60)
                    presentAction.Command = CmdType.InternetPoketterF1Y45;
                else presentAction.Command = CmdType.InternetPoketterPoem;
            }
            if (a == ActionType.Internet2ch)
            {
                if (pastAction.Darkness < 40)
                    presentAction.Command = CmdType.Internet2chY12;
                else if (pastAction.Darkness >= 40 && pastAction.Darkness < 60)
                    presentAction.Command = CmdType.Internet2chY3;
                else presentAction.Command = CmdType.Internet2chY45;
            }
            presentAction.CommandResult = NSOCommandManager.CmdTypeToCommand(presentAction.Command);
            presentAction.ActionName = NSODataManager.CmdName(presentAction.Command);
        }

        public void AddIdeaOrUsedStream(TargetActionData pastAction, TargetActionData presentAction)
        {
            if (presentAction.TargetAction.Action == ActionType.Haishin)
            {
                if (!StreamUsedList.Exists(u => u.UsedStream == presentAction.Command))
                    StreamUsedList.Add(new(presentAction.TargetAction.DayIndex, presentAction.Command));
            }
            else
            {
                var idea = NSODataManager.ActionToStreamIdea(pastAction, presentAction, this);
                if (idea != (0, 0, CmdType.None))
                {
                    StreamIdeaList.Add(new(idea.DayIndex, idea.DayPart, idea.Idea));
                }
                presentAction.StreamIdea = idea.Idea;
                if ((presentAction.TargetAction.DayPart == 2 || (presentAction.TargetAction.DayPart < 2 && presentAction.TargetAction.DayPart + presentAction.CommandResult.daypart >= 3)) && !(EndingBranch.StartingDay == 1 && presentAction.TargetAction.DayIndex == 2))
                    presentAction.StreamStreak = 0;
            }
        }

        public bool StreamIdeaExistsBeforeAction(TargetActionData action, CmdType idea)
        {
            if (!idea.ToString().Contains('_') && idea != CmdType.Error)
                throw new ArgumentOutOfRangeException($"This CmdType isn't a stream...! {idea}");
            var isExist = StreamIdeaList.Exists(c => ((c.DayIndex == action.TargetAction.DayIndex && c.DayPart <= action.TargetAction.DayPart) || c.DayIndex < action.TargetAction.DayIndex) && c.Idea == idea);
            return isExist;
        }

        public bool UsedStreamExistsBeforeAction(TargetActionData action, CmdType idea)
        {

            if (!idea.ToString().Contains('_') && idea != CmdType.Error)
                throw new ArgumentOutOfRangeException($"This CmdType isn't a stream! {idea}");
            var isExist = StreamUsedList.Exists(c => c.DayIndex < action.TargetAction.DayIndex && c.UsedStream == idea);
            return isExist;
        }

        public void ValidateNightEvents(TargetActionData pastAction, TargetActionData action, List<(string branch, string action, string errorMsg)> errorList, string branchName)
        {
            bool isVeryVeryStressed = false;
            bool isVeryLove = false;
            ActionCounter paperDay = new(30, 0);
            if (PsycheCounter.Count >= 5) paperDay = PsycheCounter[4];
            if (!action.TargetAction.IgnoreDM)
                return;
            if (isReallyStressed.isEventing && isReallyStressed.DayIndex <= action.TargetAction.DayIndex)
                isVeryVeryStressed = true;
            if (isReallyLove.isEventing && isReallyLove.DayIndex <= action.TargetAction.DayIndex)
                isVeryLove = true;
            if (!IgnoreNightEndings) return;
            if (pastAction.TargetAction.DayPart + pastAction.CommandResult.daypart == 2 && pastAction.Command != CmdType.DarknessS1 && pastAction.Command != CmdType.DarknessS2)
            {
                if (isVeryVeryStressed && pastAction.Stress == 120)
                {
                    errorList.Add(new(branchName, $"Day {action.TargetAction.DayIndex}, {NSODataManager.DayPartNames[action.TargetAction.DayPart]}: {NSODataManager.CmdName(action.Command)}", $"Can't ignore DM in this action since this will trigger Bomber Girl, and Ignore Night Endings is enabled."));
                    return;
                }
                if (pastAction.Darkness == 0)
                {
                    errorList.Add(new(branchName, $"Day {action.TargetAction.DayIndex}, {NSODataManager.DayPartNames[action.TargetAction.DayPart]}: {NSODataManager.CmdName(action.Command)}", $"Can't ignore DM in this action since this will trigger Normie Life, and Ignore Night Endings is enabled."));
                    return;
                }
                if ((!isVeryLove && pastAction.Affection == 100) || (isVeryLove && pastAction.Affection == 120))
                {
                    errorList.Add(new(branchName, $"Day {action.TargetAction.DayIndex}, {NSODataManager.DayPartNames[action.TargetAction.DayPart]}: {NSODataManager.CmdName(action.Command)}", $"Can't ignore DM in this action since this will trigger Ground Control To Psychoelectric Angel, and Ignore Night Endings is enabled."));
                    return;
                }
                if (pastAction.Affection == 0)
                {
                    errorList.Add(new(branchName, $"Day {action.TargetAction.DayIndex}, {NSODataManager.DayPartNames[action.TargetAction.DayPart]}: {NSODataManager.CmdName(action.Command)}", $"Can't ignore DM in this action since this will trigger Cucked, and Ignore Night Endings is enabled."));
                    return;
                }
                if (paperDay.DayIndex != 30 && pastAction.TargetAction.DayIndex == paperDay.DayIndex && pastAction.TargetAction.DayPart == paperDay.DayPart)
                {
                    errorList.Add(new(branchName, $"Day {action.TargetAction.DayIndex}, {NSODataManager.DayPartNames[action.TargetAction.DayPart]}: {NSODataManager.CmdName(action.Command)}", $"Can't ignore DM in this action since this will trigger Rainbow Girl, and Ignore Night Endings is enabled."));
                    return;
                }
            }
        }

        public (int, int, EndingType) CheckIfEndingAchieved(TargetActionData pastAction, TargetActionData action, bool isVeryVeryStressed, bool isHorror, bool isVeryLove, bool isAngelFive, bool isMaxFollows, bool isSkipNightEnds = false)
        {
            ActionCounter ignoreDay = new(30, 0);
            ActionCounter loveDay = new(30, 0);
            ActionCounter paperDay = new(30, 0);
            (int, int, EndingType) nightEnd = (0, 0, EndingType.Ending_None);

            var isCultStreamIdeaExists = StreamIdeaList.Exists(u => u.Idea == CmdType.Error);

            if (IgnoreCounter.Count >= 5) ignoreDay = IgnoreCounter[4];
            if (LoveCounter.Count >= 7) loveDay = LoveCounter[6];
            if (PsycheCounter.Count >= 5) paperDay = PsycheCounter[4];

            if (isReallyStressed.isEventing && isReallyStressed.DayIndex <= action.TargetAction.DayIndex)
                isVeryVeryStressed = true;
            if (this.isHorror.isEventing && (this.isHorror.DayIndex <= action.TargetAction.DayIndex || (action.TargetAction.DayIndex == 24 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3)))
                isHorror = true;
            if (isReallyLove.isEventing && isReallyLove.DayIndex <= action.TargetAction.DayIndex)
                isVeryLove = true;
            if (NoMeds.isEventing && NoMeds.DayIndex <= action.TargetAction.DayIndex)
                isAngelFive = true;
            if (isMaxFollowers.isEventing && isMaxFollowers.DayIndex <= action.TargetAction.DayIndex)
                isMaxFollows = true;
            if (loveDay.DayIndex != 30 && action.TargetAction.DayIndex == loveDay.DayIndex && action.TargetAction.DayPart == loveDay.DayPart)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Lust);
            if (ignoreDay.DayIndex != 30 && action.TargetAction.DayIndex == ignoreDay.DayIndex && action.TargetAction.DayPart == ignoreDay.DayPart)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Jine);
            if (NSODataManager.IsOverdoseAction(pastAction) &&
                StreamUsedList.Exists(a => a.DayIndex < action.TargetAction.DayIndex && a.UsedStream == CmdType.Kaidan_5) && action.Command == CmdType.OdekakeGinga && action.TargetAction.DayPart == 2)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Ginga);
            if (action.TargetAction.DayIndex == 10 && action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && action.Followers < 10000)
                return (action.TargetAction.DayIndex, 3, EndingType.Ending_Jikka);
            if (action.Followers >= 9999999 && isMaxFollows && IsNotMidnightEvents(action, (hasGalacticRail.DayIndex, hasGalacticRail.isEventing), (is150M.DayIndex, is150M.isEventing), (is300M.DayIndex, is300M.isEventing), (is500M.DayIndex, is500M.isEventing)))
                return (action.TargetAction.DayIndex, 3, EndingType.Ending_Ideon);
            if (action.Command == CmdType.Error)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Kyouso);
            bool isOrIsFollowingNight = (action.TargetAction.DayPart + action.CommandResult.daypart == 2 && !EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == action.TargetAction.DayIndex && a.TargetAction.DayPart == 2)) || action.TargetAction.DayPart == 2;
            if (action.TargetAction.DayPart + action.CommandResult.daypart == 2 && !action.Command.ToString().Contains("Darkness"))
            {
                if (nightEnd.Item3 == EndingType.Ending_None)
                    nightEnd = CheckIfNightEndingAchieved();
                if (!isSkipNightEnds && nightEnd.Item3 != EndingType.Ending_None)
                {
                    return nightEnd;
                }
            }
            if (isOrIsFollowingNight && paperDay.DayIndex != 30 && (action.TargetAction.DayIndex > paperDay.DayIndex || (action.TargetAction.DayIndex == paperDay.DayIndex && (action.TargetAction.DayPart > paperDay.DayPart || (action.TargetAction.DayPart == paperDay.DayPart && action.TargetAction.DayPart == 0)))) && !StreamIdeaExistsBeforeAction(action, CmdType.Error) && nightEnd.Item3 == EndingType.Ending_None)
                return (action.TargetAction.DayIndex, 2, EndingType.Ending_Meta);
            if (action.Command == CmdType.Yamihaishin_5)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Yami);
            if (action.Command == CmdType.Hnahaisin_5)
                return (action.TargetAction.DayIndex, action.TargetAction.DayPart + 1, EndingType.Ending_Av);
            if (isVeryVeryStressed && action.Command == CmdType.Angel_6)
                return (action.TargetAction.DayIndex + 1, 0, EndingType.Ending_DarkAngel);
            if (isVeryVeryStressed && isHorror && action.Stress >= 80)
                return (25, 0, EndingType.Ending_KowaiInternet);
            if (action.TargetAction.DayIndex == 29 && (action.TargetAction.DayPart + action.CommandResult.daypart >= 3))
            {
                if (isCultStreamIdeaExists)
                    return (30, 0, EndingType.Ending_Kyouso);
                if (action.Followers >= 1000000 && action.Affection >= 80 && action.Darkness >= 80 && isAngelFive)
                    return (30, 0, EndingType.Ending_Grand);
                if (action.Followers >= 1000000 && action.Affection >= 80)
                    return (30, 0, EndingType.Ending_Happy);
                if (action.Followers >= 500000 && action.Affection >= 80)
                    return (30, 0, EndingType.Ending_Normal);
                if (action.Followers >= 500000)
                    return (30, 0, EndingType.Ending_Yarisute);
                if (action.Affection >= 60 && action.Darkness >= 60)
                    return (30, 0, EndingType.Ending_Needy);
                if (action.Affection < 60 && action.Darkness >= 60)
                    return (30, 0, EndingType.Ending_Sucide);
                if (action.Affection >= 60 && action.Darkness < 60)
                    return (30, 0, EndingType.Ending_Work);
                if (action.Affection < 60 && action.Darkness < 60)
                    return (30, 0, EndingType.Ending_Bad);
            }

            return (0, 0, EndingType.Ending_None);

            (int, int, EndingType) CheckIfNightEndingAchieved()
            {
                if (isVeryVeryStressed && action.Stress == 120)
                    return (action.TargetAction.DayIndex, 2, EndingType.Ending_Stressful);
                if (action.Darkness == 0)
                    return (action.TargetAction.DayIndex, 2, EndingType.Ending_Healthy);
                if ((!isVeryLove && action.Affection == 100) || (isVeryLove && action.Affection == 120))
                    return (action.TargetAction.DayIndex, 2, EndingType.Ending_Sukisuki);
                if (action.Affection == 0)
                    return (action.TargetAction.DayIndex, 2, EndingType.Ending_Ntr);
                return (0, 0, EndingType.Ending_None);
            }
        }
    }

    public class EndingBranch_Stub
    {
        public int StartingDay = 1;
        public EndingType EndingToGet = EndingType.Ending_None;
        public bool IsStressfulBressdown = false;
        public List<TargetActionData> AllActions = new();

        [JsonConstructor]
        public EndingBranch_Stub() { }
        public EndingBranch_Stub(int startDay, EndingType ending, List<TargetActionData> actions, bool isStressBreakdown = false)
        {
            StartingDay = startDay;
            EndingToGet = ending;
            AllActions.AddRange(actions);
            IsStressfulBressdown = isStressBreakdown;
        }
    }

}
