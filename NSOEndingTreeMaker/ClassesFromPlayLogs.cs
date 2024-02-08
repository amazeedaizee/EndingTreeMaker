using NGO;
using ngov3;
using System;
using System.Collections.Generic;

namespace NSOEndingTreeMaker
{
    public class ClassesFromPlayLogs
    {
        [Serializable]
        public class DataInfo
        {
            public int SaveNum = 0;
            public List<DayInfo> Days = new List<DayInfo>();
        }

        [Serializable]
        public class CommandInfo
        {
            public EndingType Ending = EndingType.Ending_None;
            public int DayPart = 0;
            public bool SkippedDM = false;
            public CmdType Command = CmdType.None;
        }

        [Serializable]
        public class DayInfo
        {
            public string DayEventName = "";
            public string MidnightEventName = "";
            public int Day = 0;
            public ResultInfo startingStats = null;
            public ResultInfo endingStats;
            public List<CommandInfo> Commands = new List<CommandInfo>();

        }

        [Serializable]
        public class ResultInfo
        {
            public int Followers = 0;
            public int Stress = 0;
            public int Affection = 0;
            public int Darkness = 0;
            public int StreamStreak = 0;
            public bool PreAlertBonus = false;
            public int Communication = 0;
            public int Experience = 0;
            public int Impact = 0;
            public int GamerGirl = 0;
            public int Cinephile = 0;
            public int RabbitHole = 0;
            public int LoveCounter = 0;
            public int IgnoreCounter = 0;
            public int PsycheCounter = 0;
        }

        [Serializable]
        public class PlaythroughLog
        {
            public List<DataInfo> DataOnes = new List<DataInfo>();
            public List<DataInfo> DataTwos = new List<DataInfo>();
            public List<DataInfo> DataThrees = new List<DataInfo>();
        }
    }
}
