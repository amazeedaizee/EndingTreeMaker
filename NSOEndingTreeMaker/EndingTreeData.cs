﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace NSOEndingTreeMaker
{
    public class EndingTreeData
    {
        public List<EndingBranchData> EndingsList = new();
        public bool isDay2Exp;
        public bool isBroken;
        public string Notes = "";

        public EndingTreeData(List<EndingBranchData> endingsList)
        {
            EndingsList = endingsList;
        }

        [JsonConstructor]
        public EndingTreeData(List<EndingBranchData> endingsList, string notes)
        {
            EndingsList.AddRange(endingsList);
            Notes = notes;
        }

    }
}
