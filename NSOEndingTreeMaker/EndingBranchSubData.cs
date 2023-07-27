using Newtonsoft.Json;
using ngov3;
using System;

namespace NSOEndingTreeMaker
{
    [Serializable]
    public static class EndingBranchSubData
    {
        [Serializable]
        public class StreamIdeaObj
        {
            public int DayIndex;
            public int DayPart;
            public CmdType Idea;

            [JsonConstructor]
            public StreamIdeaObj(int DayIndex, int DayPart, CmdType Idea)
            {
                this.DayIndex = DayIndex;
                this.DayPart = DayPart;
                this.Idea = Idea;
            }
        }
        [Serializable]
        public class StreamUsedObj
        {
            public int DayIndex;
            public CmdType UsedStream;

            [JsonConstructor]
            public StreamUsedObj(int DayIndex, CmdType UsedStream)
            {
                this.DayIndex = DayIndex;
                this.UsedStream = UsedStream;
            }
        }
        [Serializable]
        public class ActionCounter
        {
            public int DayIndex;
            public int DayPart;

            [JsonConstructor]
            public ActionCounter(int DayIndex, int DayPart)
            {
                this.DayIndex = DayIndex;
                this.DayPart = DayPart;
            }
        }

        [Serializable]
        public class EventCounter
        {
            public int DayIndex;
            public bool isEventing;

            [JsonConstructor]
            public EventCounter(int DayIndex, bool isEventing)
            {
                this.DayIndex = DayIndex;
                this.isEventing = isEventing;
            }
        }
    }
}


