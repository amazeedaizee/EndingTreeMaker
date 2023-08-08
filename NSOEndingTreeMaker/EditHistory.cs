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
        public TargetActionData ActionAfterEdit;

        public ActionHistoryObj(EditType editType, int index, TargetActionData action, TargetActionData actionAfterEdit = null)
        {
            EditType = editType;
            this.ActionIndex = index;
            Action = new TargetActionData(action.TargetAction.DayIndex, action.TargetAction.DayPart, action.Command, action.TargetAction.IgnoreDM);
            if (actionAfterEdit != null)
                ActionAfterEdit = new TargetActionData(actionAfterEdit.TargetAction.DayIndex, actionAfterEdit.TargetAction.DayPart, actionAfterEdit.Command, actionAfterEdit.TargetAction.IgnoreDM);
        }
    }
    public class EditHistory
    {
        public List<List<ActionHistoryObj>> undoActions = new();
        public List<List<ActionHistoryObj>> redoActions = new();

        public EditHistory()
        {
        }
    }
}
