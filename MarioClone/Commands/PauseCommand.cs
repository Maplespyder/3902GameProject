using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    class PauseCommand : AbstractCommand<MarioCloneGame>
    {
        public PauseCommand(MarioCloneGame receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.PauseCommand();
        }
    }
}
