using ngov3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    public partial class ActionImporter : Form
    {
        private CustomActionEditor _owner;
        private ActionType _currAction;
        private AlphaType _currStreamTopic;
        private CommandAction selected = null;
        private List<CommandAction> commands;
        public ActionImporter(CustomActionEditor cusWin, ActionType action, AlphaType stream = AlphaType.none)
        {
            _owner = cusWin;
            _currAction = action;
            _currStreamTopic = stream;

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActionImporter_Load(object sender, EventArgs e)
        {
            if (_currAction == ActionType.Haishin)
            {
                commands = NSOCommandManager.StreamTopicToCommands(_currStreamTopic);
            }
            else commands = NSOCommandManager.ActionTypeToCommands(_currAction);
            CommandList.Items.Clear();
            foreach (var c in commands)
            {
                if (_currAction.ToString().Contains("Odekake"))
                {
                    var name = _currAction.ToString().Replace("Odekake", "");
                    if (name.Contains("Park")) {
                        CommandList.Items.Add("Hikarigaoka Park");
                    }
                    else CommandList.Items.Add(name);

                }
                else CommandList.Items.Add(c.name);
            }
            if (commands.Count > 0) selected = commands[0];
        }

        private void CommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commands.Count == 0) return;
            selected = commands[CommandList.SelectedIndex];
            FollowersNumUD.Value = selected.followers;
            StressNumUD.Value = selected.stress;
            AffectionNumUD.Value = selected.affection;
            DarknessNumUD.Value = selected.darkness;
            ImpactNumUD.Value = selected.impact;
            ExperienceNumUD.Value = selected.experience;
            StreakNumUD.Value = selected.streamstreak;
            AlertBonusCheck.Checked = selected.prebonus > 0;
            GameNumUD.Value = selected.gamer;
            MovieNumUD.Value = selected.movie;
            CommNumUD.Value = selected.communication;
            RabbitHoleNumUD.Value = selected.tinfoil;
            DayPassNumUD.Value = selected.daypart;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _owner.SetCustomStatChanges(selected);
            Close();
        }
    }
}
