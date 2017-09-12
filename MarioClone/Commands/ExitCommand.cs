using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    class ExitCommand : AbstractCommand<MarioCloneGame>
    {
        public ExitCommand(MarioCloneGame receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.ExitCommand();
        }
    }
}
