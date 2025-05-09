﻿using Newtonsoft.Json;
using ngov3;
using System;
using System.Linq;

namespace NSOEndingTreeMaker
{
    [Serializable]
    public class TargetActionData
    {
        public TargetAction_Stub TargetAction;
        public string ActionName;
        public int Followers = 1001;
        public int Stress = 15;
        public int Affection = 40;
        public int Darkness = 15;
        public int StreamStreak = 1;
        public bool PreAlertBonus = false;
        public int Communication = 0;
        public int Experience = 0;
        public int Impact = 0;
        public int GamerGirl = 0;
        public int Cinephile = 0;
        public int RabbitHole = 0;
        public CmdType Command = CmdType.None;
        public CommandAction CommandResult;
        public CmdType StreamIdea = CmdType.None;
        public CmdType MilestoneIdea = CmdType.None;

        [JsonConstructor]
        public TargetActionData() { }

        public TargetActionData(TargetActionData action)
        {
            var t = action.TargetAction;
            TargetAction = new TargetAction_Stub(t.DayIndex, t.DayPart, t.Action, t.IgnoreDM);
            ActionName = action.ActionName;
            Command = action.Command;
            CommandResult = NSOCommandManager.CmdTypeToCommand(Command);
        }
        public TargetActionData(TargetAction_Stub TargetAction, string action, CommandAction command = null)
        {
            this.TargetAction = TargetAction;
            ActionName = action;
            CommandResult = command;
        }

        public TargetActionData(int dayIndex, int dayPart, string action, CommandAction command = null, bool ignoreDM = false)
        {
            TargetAction = new TargetAction_Stub(dayIndex, dayPart, ActionType.None, ignoreDM);
            ActionName = action;
            CommandResult = command;
        }

        public TargetActionData(int dayIndex, int dayPart, CmdType cmd, bool ignoreDM = false)
        {
            ActionType action = NSODataManager.CmdToActionConverter(cmd);
            TargetAction = new TargetAction_Stub(dayIndex, dayPart, action, ignoreDM);
            AlphaType stream;
            if (cmd.ToString().Contains('_'))
            {
                string streamTopic = cmd.ToString().Split('_')[0];
                stream = (AlphaType)Enum.Parse(typeof(AlphaType), streamTopic);
                TargetAction = new TargetAction_Stub(dayIndex, dayPart, stream, ignoreDM);
            }
            else if (cmd == CmdType.Error)
            {
                TargetAction = new TargetAction_Stub(dayIndex, dayPart, AlphaType.Imbouron, ignoreDM);
            }
            else
            {
                TargetAction = new TargetAction_Stub(dayIndex, dayPart, action, ignoreDM);
            }
            ActionName = cmd != CmdType.None ? NSODataManager.CmdName(cmd) : "";
            Command = cmd;
            CommandResult = NSOCommandManager.CmdTypeToCommand(cmd);
        }

        public void SetStatsToMaxOrMin(EndingBranchData branch, bool isStressed, bool isReallyStressed, bool isReallyLove)
        {
            var stressMax = 100;
            var affectionMax = 100;
            if ((isStressed && isReallyStressed) || (branch.isStressed.isEventing && branch.isReallyStressed.isEventing && TargetAction.DayIndex >= branch.isReallyStressed.DayIndex))
                stressMax = 120;
            if (isReallyLove || (branch.isReallyLove.isEventing && TargetAction.DayIndex >= branch.isReallyLove.DayIndex))
                affectionMax = 120;
            Followers = ClampStats(0, Followers, 9999999);
            Stress = ClampStats(0, Stress, stressMax);
            Affection = ClampStats(0, Affection, affectionMax);
            Darkness = ClampStats(0, Darkness, 100);
            StreamStreak = ClampStats(0, StreamStreak, 30);
            Communication = ClampStats(0, Communication, 90);
            Experience = ClampStats(0, Experience, 60);
            Impact = ClampStats(0, Impact, 60);
            GamerGirl = ClampStats(0, GamerGirl, 60);
            Cinephile = ClampStats(0, Cinephile, 60);
            RabbitHole = ClampStats(0, RabbitHole, 100);

            int ClampStats(int min, int num, int max)
            {
                if (num > max) return max;
                if (num < min) return min;
                return num;
            }
        }
        public void ResetActionStats()
        {
            TargetAction.Action = ActionType.None;
            TargetAction.Stream = AlphaType.none;
            TargetAction.IgnoreDM = false;
            ActionName = "";
            Followers = 1001;
            Stress = 15;
            Affection = 40;
            Darkness = 15;
            StreamStreak = 1;
            PreAlertBonus = false;
            Communication = 0;
            Experience = 0;
            Impact = 0;
            GamerGirl = 0;
            Cinephile = 0;
            RabbitHole = 0;
            Command = CmdType.None;
            CommandResult = null;
            StreamIdea = CmdType.None;
            MilestoneIdea = CmdType.None;
        }
        public void ChangeStats(int followers, int stress, int love, int dark, int streamstreak, bool prealert, int communication, int experience, int impact, int game, int movie, int rabbithole)
        {
            Followers = followers;
            Stress = stress;
            Affection = love;
            Darkness = dark;
            StreamStreak = streamstreak;
            PreAlertBonus = prealert;
            Communication = communication;
            Experience = experience;
            Impact = impact;
            GamerGirl = game;
            Cinephile = movie;
            RabbitHole = rabbithole;
        }
    }

    [Serializable]
    public class TargetAction_Stub
    {
        public int DayIndex;
        public int DayPart;
        public ActionType Action;
        public AlphaType Stream;
        public bool IgnoreDM;

        [JsonConstructor]
        public TargetAction_Stub() { }
        public TargetAction_Stub(int dayIndex, int dayPart, AlphaType stream, bool ignoreDM = false)
        {
            DayIndex = dayIndex > 30 ? 30 : dayIndex;
            DayPart = dayPart > 3 ? 3 : dayPart;
            Action = ActionType.Haishin;
            Stream = stream;
            IgnoreDM = ignoreDM;
        }

        public TargetAction_Stub(int dayIndex, int dayPart, ActionType action, bool ignoreDM = false)
        {
            DayIndex = dayIndex > 30 ? 30 : dayIndex;
            DayPart = dayPart > 3 ? 3 : dayPart;
            Action = action;
            Stream = AlphaType.none;
            IgnoreDM = ignoreDM;
        }
    }
}
