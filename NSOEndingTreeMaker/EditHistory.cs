using System.Collections.Generic;

namespace NSOEndingTreeMaker
{
    public enum EditType
    {
        Add, Delete, Edit
    }

    public class ActionHistoryObj
    {
        public EditType EditType;
        public int ActionIndex;
        public TargetActionData Action;

        public ActionHistoryObj(EditType editType, int index, TargetActionData action)
        {
            EditType = editType;
            this.ActionIndex = index;
            Action = new TargetActionData(action.TargetAction.DayIndex, action.TargetAction.DayPart, action.Command, action.TargetAction.IgnoreDM);
        }
    }
    public class EditHistory
    {
        public List<List<ActionHistoryObj>> undoActions = new();
        public List<List<ActionHistoryObj>> redoActions = new();

        public EditHistory()
        {
            undoActions.Capacity = 20;
        }
    }
}
