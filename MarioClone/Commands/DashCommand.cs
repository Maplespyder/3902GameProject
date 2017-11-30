using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class DashCommand : AbstractCommand<Mario>
    {
        public DashCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.Playing)
            {
                Receiver.Dash();
            }
        }
    }
}
