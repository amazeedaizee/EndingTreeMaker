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

        [JsonConstructor]
        public EndingBranchData() { }

        public EndingBranchData(EndingBranchData endingBranch)
        {
            List<TargetActionData> actionList = CopyActionListFromExistingBranch(endingBranch);
            EndingBranch = new(endingBranch.EndingBranch.StartingDay, endingBranch.EndingBranch.EndingToGet, actionList, endingBranch.EndingBranch.IsStressfulBressdown);
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
        public List<(string action, string errorMsg)> ValidateBranch()
        {
            List<(string action, string errorMsg)> errorList = new();
            for (int i = 0; i < EndingBranch.AllActions.Count; i++)
            {
                if (i == EndingBranch.AllActions.Count - 1)
                {
                    break;
                }
                if (i > 0) ValidatePreviousDay(EndingBranch.AllActions[i - 1], EndingBranch.AllActions[i], errorList);
                ValidateNextDay(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1], errorList);
                LookForMissingDays(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1], errorList);
                (bool, string, CmdType) actionValid = ValidateAction(EndingBranch.AllActions[i], EndingBranch.AllActions[i + 1]);
                if (!actionValid.Item1)
                {
                    errorList.Add(new($"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", $"Action can't be done based on the stats of the previous action. \n\n({actionValid.Item2})"));
                }
                (bool, string) streamValid = ValidateStream(EndingBranch.AllActions[i + 1]);
                if (!streamValid.Item1)
                {
                    errorList.Add(new($"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", streamValid.Item2));

                }
                if (EndingBranch.AllActions[i + 1].TargetAction.Action == ActionType.OkusuriHipuronModerate)
                {
                    errorList.Add(new($"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", $"{NSODataManager.CmdName(CmdType.OkusuriHipuronModerate)} is currently not supported in the latest version of the game."));
                }
                (bool, string) specialEventValid = ValidateSpecialEvents(EndingBranch.AllActions[i + 1]);
                if (!specialEventValid.Item1)
                {
                    errorList.Add(new($"Day {EndingBranch.AllActions[i + 1].TargetAction.DayIndex}, {NSODataManager.DayPartNames[EndingBranch.AllActions[i + 1].TargetAction.DayPart]}: {NSODataManager.CmdName(actionValid.Item3)}", specialEventValid.Item2));
                }
            }
            ValidateMissingMandatoryEvents(errorList);
            return errorList;
        }

        private void ValidateNextDay(TargetActionData presentAction, TargetActionData futureAction, List<(string, string)> errorList)
        {
            if (presentAction.CommandResult == null) return;
            if (presentAction.TargetAction.DayIndex == 1) return;
            int dayPassing;
            dayPassing = presentAction.TargetAction.DayPart + presentAction.CommandResult.daypart;
            if (presentAction.TargetAction.DayPart == -1 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart != 0)
            {
                errorList.Add(new("", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (presentAction.TargetAction.DayPart == -1 && futureAction.TargetAction.DayIndex != presentAction.TargetAction.DayIndex)
            {
                errorList.Add(new("", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (dayPassing > 2 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && presentAction.TargetAction.DayPart == 1 && futureAction.TargetAction.DayPart == 2)
            {
                errorList.Add(new("", $"Day {presentAction.TargetAction.DayIndex} contains a Night Action even though the Dusk action would move it to next day."));
                return;
            }
            if (dayPassing == 2 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart == 1 && EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && a.TargetAction.DayPart == 2))
            {
                errorList.Add(new("", $"Day {presentAction.TargetAction.DayIndex} contains a Dusk Action even though the Noon action would move it to Night."));
                return;
            }
            if (dayPassing <= 2 && presentAction.TargetAction.DayPart != -1 && futureAction.TargetAction.DayIndex == presentAction.TargetAction.DayIndex && futureAction.TargetAction.DayPart != dayPassing)
            {
                errorList.Add(new("", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
            if (dayPassing <= 2 && presentAction.TargetAction.DayPart != -1 && futureAction.TargetAction.DayIndex >= (presentAction.TargetAction.DayIndex + 1))
            {
                errorList.Add(new("", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
                return;
            }
        }

        private void ValidatePreviousDay(TargetActionData pastAction, TargetActionData presentAction, List<(string, string)> errorList)
        {
            if (pastAction.CommandResult == null) return;
            int dayPassing;
            dayPassing = pastAction.TargetAction.DayPart + pastAction.CommandResult.daypart;
            if (presentAction.TargetAction.DayPart > 0 && pastAction.TargetAction.DayIndex != presentAction.TargetAction.DayIndex)
            {
                errorList.Add(new("", $"Missing some parts of the day on Day {presentAction.TargetAction.DayIndex}"));
            }
        }

        private void LookForMissingDays(TargetActionData presentAction, TargetActionData futureAction, List<(string, string)> errorList)
        {
            int presentDay = presentAction.TargetAction.DayIndex;
            if (futureAction.TargetAction.DayIndex <= presentDay) return;
            for (int i = presentDay; i < futureAction.TargetAction.DayIndex; i++)
            {
                Console.WriteLine($"Detected Day {i}");
                if (EndingBranch.AllActions.Exists(a => a.TargetAction.DayIndex == i)) continue;
                errorList.Add(new("", $"No parts of Day {i} exist."));
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
                    if (futureAction.TargetAction.DayPart != 0) return (false, "This action is not done during Noon.", futureAction.Command);
                    break;
                case CmdType.SleepToNight2:
                    if (futureAction.TargetAction.DayPart != 0) return (false, "This action is not done during Noon.", futureAction.Command);
                    break;
                case CmdType.SleepToNight1:
                    if (futureAction.TargetAction.DayPart != 1) return (false, "This action is not done during Dusk.", futureAction.Command);
                    break;
                case CmdType.SleepToTomorrow3:
                    if (futureAction.TargetAction.DayPart != 0) return (false, "This action is not done during Noon.", futureAction.Command);
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
                    if (presentAction.Followers >= 100000 || (presentAction.Followers < 10000 && presentAction.Darkness < 20) || !(presentAction.Followers < 100000 && presentAction.Followers >= 10000 && presentAction.Darkness >= 40 && presentAction.Darkness < 60))
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

            if (EndingBranch.AllActions.FindAll(i => i.Command == action.Command).Count > 1)
                return (false, $"You can't do this stream more than once.");
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
            int actionIndex = EndingBranch.AllActions.IndexOf(action);
            if (NoMeds.isEventing)
            {
                if ((action.TargetAction.Action == ActionType.OkusuriHappa || action.TargetAction.Action == ActionType.OkusuriPsyche || action.TargetAction.Action.ToString().Contains("Overdose")) && action.TargetAction.DayIndex > NoMeds.DayIndex)
                    return (false, $"Drugs cannot be taken after Day {NoMeds.DayIndex} (the day you did Internet Angel 5.) ");
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
                if (isStressed.isEventing && action.TargetAction.DayIndex != isStressed.DayIndex)
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
                if (isStressed.isEventing && !isReallyStressed.isEventing)
                    return (false, $"Ame is stressed but not very stressed on Day {action.TargetAction.DayIndex}.");
                if (isStressed.isEventing && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1))
                    return (false, $"Ame has to {NSODataManager.CmdName(CmdType.DarknessS1)} first on Day {isStressed.DayIndex} at Noon.");
                if (isStressed.isEventing && EndingBranch.AllActions.Exists(a => a.Command == CmdType.DarknessS1 && a.TargetAction.DayIndex >= action.TargetAction.DayIndex))
                    return (false, $"{NSODataManager.CmdName(CmdType.DarknessS1)} must be on Day {isStressed.DayIndex} at Noon, before {NSODataManager.CmdName(action.Command)}.");
                if (isReallyStressed.isEventing && action.TargetAction.DayIndex != isReallyStressed.DayIndex)
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
                if (!((EndingBranch.AllActions[actionIndex - 1].TargetAction.Action == ActionType.OkusuriHappa || EndingBranch.AllActions[actionIndex - 1].TargetAction.Action == ActionType.OkusuriPsyche || EndingBranch.AllActions[actionIndex - 1].TargetAction.Action.ToString().Contains("Overdose")) && EndingBranch.AllActions[actionIndex - 1].TargetAction.DayPart == 0 && EndingBranch.AllActions[actionIndex - 1].TargetAction.DayIndex == action.TargetAction.DayIndex))
                    return (false, "Drugs have to be overdosed on Noon on the same day before going into the Galactic Rail at night.");
                if (action.TargetAction.DayPart != 2)
                    return (false, "The Galactic Rail can only be visited at night.");
            }
            return (true, "");
        }

        public void ValidateMissingMandatoryEvents(List<(string, string)> errorList)
        {
            List<(string, string)> eventErrors = new();
            if (isStressed.isEventing && isStressed.DayIndex >= EndingBranch.StartingDay)
            {
                var stress = EndingBranch.AllActions.FirstOrDefault(a => a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isStressed.DayIndex);
                if (stress == null || (stress.Command != CmdType.DarknessS1 && (!isReallyStressed.isEventing || stress.TargetAction.DayIndex < isReallyStressed.DayIndex)))
                    eventErrors.Add(new("", $"Ame is stressed. You must do {NSODataManager.CmdName(CmdType.DarknessS1)} on Day {isStressed.DayIndex} at Noon."));
            }
            if (isStressed.isEventing && isReallyStressed.isEventing && isReallyStressed.DayIndex >= EndingBranch.StartingDay)
            {
                var reallyStress = EndingBranch.AllActions.FirstOrDefault(a => a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isReallyStressed.DayIndex);
                if (reallyStress == null || reallyStress.Command != CmdType.DarknessS2)
                    eventErrors.Add(new("", $"Ame is really stressed. You must do {NSODataManager.CmdName(CmdType.DarknessS2)} on Day {isReallyStressed.DayIndex} at Noon."));
            }
            else if (isReallyLove.isEventing && isReallyLove.DayIndex >= EndingBranch.StartingDay && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeZikka && a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isReallyLove.DayIndex))
            {
                eventErrors.Add(new("", $"Going to {NSODataManager.CmdName(CmdType.OdekakeZikka)} has to be done on Day 24 at Noon."));
            }
            else if (isVideo.isEventing && isVideo.DayIndex >= EndingBranch.StartingDay && !EndingBranch.AllActions.Exists(a => a.Command == CmdType.OdekakeOdaiba && a.TargetAction.DayPart == 0 && a.TargetAction.DayIndex == isVideo.DayIndex))
            {
                eventErrors.Add(new("", $"Doing the {NSODataManager.CmdName(CmdType.OdekakeOdaiba)} has to be done on Day 27 at Noon."));
            }
            errorList.InsertRange(0, eventErrors);
        }

        public bool IsNotFixedEvents(TargetActionData action, bool isTrauma, bool isParents, bool isVideo)
        {
            int nextDay = action.TargetAction.DayIndex + 1;
            if (action.TargetAction.DayPart == -1) nextDay--;
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
