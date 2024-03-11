using Newtonsoft.Json;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static NSOEndingTreeMaker.ClassesFromPlayLogs;

namespace NSOEndingTreeMaker
{
    public partial class PlayLogViewer : Form
    {
        string _directoryToOpen;

        EndingTreeForm treeForm;

        List<EndingBranchData> dataOneEnds = new List<EndingBranchData>();
        List<EndingBranchData> dataTwoEnds = new List<EndingBranchData>();
        List<EndingBranchData> dataThreeEnds = new List<EndingBranchData>();

        public PlayLogViewer(EndingTreeForm treeForm)
        {
            _directoryToOpen = Properties.Settings.Default.PlayLogDirectory;
            InitializeComponent();
            this.treeForm = treeForm;
        }

        public bool LoadPlayLog(out PlaythroughLog log)
        {
            log = null;
            OpenFileDialog openPlayLog = new OpenFileDialog();
            openPlayLog.InitialDirectory = !string.IsNullOrEmpty(_directoryToOpen) ? _directoryToOpen : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openPlayLog.Filter = "AME File (*.ame)|*.ame";
            openPlayLog.FilterIndex = 1;
            openPlayLog.RestoreDirectory = true;
            if (openPlayLog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileContent = openPlayLog.OpenFile();
                    _directoryToOpen = Path.GetDirectoryName(openPlayLog.FileName);
                    Properties.Settings.Default.PlayLogDirectory = _directoryToOpen;
                    using (StreamReader reader = new StreamReader(fileContent))
                    {
                        var importedLog = reader.ReadToEnd();
                        log = JsonConvert.DeserializeObject<PlaythroughLog>(importedLog);
                        openPlayLog.Dispose();
                        return true;
                    }
                }
                catch { MessageBox.Show("Could not open JSON file, either the JSON file is invalid or the JSON file does not represent an Ending Tree.", "Could not read JSON file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            return false;
        }

        void AppendLogToStart()
        {
            PlaythroughLog importedLog;
            List<EndingBranchData> dataOnes;
            List<EndingBranchData> dataTwos;
            List<EndingBranchData> dataThrees;
            if (LoadPlayLog(out importedLog))
            {
                dataOnes = ImportBranchesFromLog(importedLog, 1);
                dataTwos = ImportBranchesFromLog(importedLog, 2);
                dataThrees = ImportBranchesFromLog(importedLog, 3);
                dataOneEnds.InsertRange(0, dataOnes);
                dataTwoEnds.InsertRange(0, dataTwos);
                dataThreeEnds.InsertRange(0, dataThrees);
                InitializeLists();
            }
        }

        void AppendLogToEnd()
        {
            PlaythroughLog importedLog;
            List<EndingBranchData> dataOnes;
            List<EndingBranchData> dataTwos;
            List<EndingBranchData> dataThrees;
            if (LoadPlayLog(out importedLog))
            {
                dataOnes = ImportBranchesFromLog(importedLog, 1);
                dataTwos = ImportBranchesFromLog(importedLog, 2);
                dataThrees = ImportBranchesFromLog(importedLog, 3);
                dataOneEnds.AddRange(dataOnes);
                dataTwoEnds.AddRange(dataTwos);
                dataThreeEnds.AddRange(dataThrees);
                InitializeLists();
            }
        }

        void FillActionPreviewList(EndingBranchData data)
        {
            LogDayListView.Items.Clear();
            for (int i = 0; i < data.EndingBranch.AllActions.Count; i++)
            {
                var action = data.EndingBranch.AllActions[i];
                ListViewItem item = new ListViewItem(action.TargetAction.DayIndex.ToString());
                item.SubItems.Add(NSODataManager.DayPartNames[action.TargetAction.DayPart]);
                item.SubItems.Add(action.ActionName);
                item.SubItems.Add(action.TargetAction.IgnoreDM ? "Yes" : "");
                LogDayListView.Items.Add(item);
            }
        }

        void InitializeLists()
        {
            ListAllEnds(DataOneEndings, 1);
            ListAllEnds(DataTwoEndings, 2);
            ListAllEnds(DataThreeEndings, 3);
        }

        void ClearLists()
        {
            dataOneEnds.Clear();
            dataTwoEnds.Clear();
            dataThreeEnds.Clear();
            DataOneEndings.Items.Clear();
            DataTwoEndings.Items.Clear();
            DataThreeEndings.Items.Clear();
        }

        void ListAllEnds(ListView list, int data)
        {
            List<EndingBranchData> dataEnds = null;
            switch (data)
            {
                case 1:
                    dataEnds = dataOneEnds;
                    break;
                case 2:
                    dataEnds = dataTwoEnds;
                    break;
                case 3:
                    dataEnds = dataThreeEnds;
                    break;
            }
            for (int i = 0; i < dataEnds.Count; i++)
            {
                bool doesEndExist = NSODataManager.EndingNames.TryGetValue(dataEnds[i].EndingBranch.EndingToGet, out string name);
                ListViewItem ending = list.Items.Add(doesEndExist ? name : "");
                ending.SubItems.Add(dataEnds[i].EndingBranch.StartingDay.ToString());
                ending.SubItems.Add(SetLatestDayString(dataEnds[i]));
                ending.SubItems.Add(dataEnds[i].EndingBranch.IsStressfulBressdown ? "Yes" : "");
            }
            string SetLatestDayString(EndingBranchData endingData)
            {
                TargetActionData action = endingData.EndingBranch.AllActions[endingData.EndingBranch.AllActions.Count - 1];
                if (action.TargetAction.DayPart + action.CommandResult.daypart >= 3 && !NSODataManager.IsEndingOnSameDay(endingData.ExpectedDayOfEnd.Item3))
                    return $"{action.TargetAction.DayIndex + 1}";
                return action.TargetAction.DayIndex.ToString();
            }
        }

        List<EndingBranchData> ImportBranchesFromLog(PlaythroughLog log, int data)
        {
            List<EndingBranchData> branches = new List<EndingBranchData>();
            switch (data)
            {
                case 1:
                    for (int i = 0; i < log.DataOnes.Count; i++)
                    {
                        branches.Add(ImportPlayData(log.DataOnes[i]));
                    }
                    break;
                case 2:
                    for (int i = 0; i < log.DataTwos.Count; i++)
                    {
                        branches.Add(ImportPlayData(log.DataTwos[i]));
                    }
                    break;
                case 3:
                    for (int i = 0; i < log.DataThrees.Count; i++)
                    {
                        branches.Add(ImportPlayData(log.DataThrees[i]));
                    }
                    break;
            }
            return branches;
        }

        EndingBranchData ImportPlayData(DataInfo data)
        {
            int dataLen = data.Days.Count;
            int commandLen = data.Days[dataLen - 1].Commands.Count;
            EndingType ending = data.Days[dataLen - 1].Commands[commandLen - 1].Ending;
            return new EndingBranchData(data.Days[0].Day, ending, ImportActionsFromDays(data.Days));
        }

        void TransferDataToTree(int data)
        {
            List<EndingBranchData> dataEnds = null;
            switch (data)
            {
                case 1:
                    dataEnds = dataOneEnds;
                    break;
                case 2:
                    dataEnds = dataTwoEnds;
                    break;
                case 3:
                    dataEnds = dataThreeEnds;
                    break;
            }
            if (dataEnds == null) return;
            if (MessageBox.Show("This will import the current selected data to an Ending Tree. \nAny existing data in the current Tree will be overwritten. \n\nContinue?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                treeForm.CurrentEndingTree = new EndingTreeData(dataEnds);
                treeForm.SetEndingListViewData();
                treeForm.isBranchEdited = true;
                treeForm.ChangeFileLabelIfUnsaved();
                MessageBox.Show("Log succesfully imported!");
            }
        }

        List<TargetActionData> ImportActionsFromDays(List<DayInfo> days)
        {
            List<TargetActionData> targetActions = new List<TargetActionData>();
            CmdType cmd;
            for (int i = 0; i < days.Count; i++)
            {
                for (int j = 0; j < days[i].Commands.Count; j++)
                {
                    var com = days[i].Commands[j];
                    if (com.Command == CmdType.None && com.Ending == EndingType.Ending_Kyouso)
                    {
                        cmd = CmdType.Error;
                    }
                    else cmd = com.Command;
                    targetActions.Add(new TargetActionData
                        (days[i].Day, com.DayPart, cmd, com.SkippedDM));
                }
            }
            return targetActions;
        }

        private void DataOneEndings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataOneEndings.SelectedIndices.Count > 0)
            {
                FillActionPreviewList(dataOneEnds[DataOneEndings.SelectedIndices[0]]);
            }
            else if (DataOneEndings.SelectedItems.Count == 0
                && DataTwoEndings.SelectedItems.Count == 0
                && DataThreeEndings.SelectedItems.Count == 0
                )
            {
                LogDayListView.Items.Clear();
            }

        }

        private void DataTwoEndings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataTwoEndings.SelectedIndices.Count > 0)
            {
                FillActionPreviewList(dataTwoEnds[DataTwoEndings.SelectedIndices[0]]);
            }
            else if (DataOneEndings.SelectedItems.Count == 0
                && DataTwoEndings.SelectedItems.Count == 0
                && DataThreeEndings.SelectedItems.Count == 0
                )
            {
                LogDayListView.Items.Clear();
            }
        }

        private void DataThreeEndings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataThreeEndings.SelectedIndices.Count > 0)
            {
                FillActionPreviewList(dataThreeEnds[DataThreeEndings.SelectedIndices[0]]);
            }
            {
                LogDayListView.Items.Clear();
            }
        }

        private void AppendToStart_Click(object sender, EventArgs e)
        {
            AppendLogToStart();
        }

        private void AppendToEnd_Click(object sender, EventArgs e)
        {
            AppendLogToEnd();
        }

        private void ResetLogView_Click(object sender, EventArgs e)
        {
            ClearLists();
        }

        private void ImportOne_Click(object sender, EventArgs e)
        {
            TransferDataToTree(1);
        }

        private void ImportTwo_Click(object sender, EventArgs e)
        {
            TransferDataToTree(2);
        }

        private void ImportThree_Click(object sender, EventArgs e)
        {
            TransferDataToTree(3);
        }

        private void PlayLogViewer_Click(object sender, EventArgs e)
        {
            LogDayListView.Items.Clear();
        }

        private void PlayLogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
    }
}
