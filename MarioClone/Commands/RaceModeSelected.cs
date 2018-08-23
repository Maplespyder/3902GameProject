using MarioClone.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class RaceModeSelected : AbstractCommand<MainMenu>
    {
        public RaceModeSelected(MainMenu receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.RaceModeSelected();
        }
    }
}
