using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    class ResetLevelCommand : AbstractCommand<MarioCloneGame>
    {
        public ResetLevelCommand(MarioCloneGame receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.ResetLevelCommand(); 
        }
    }
}
