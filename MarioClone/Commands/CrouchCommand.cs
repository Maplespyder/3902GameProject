using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class CrouchCommand : AbstractCommand<Mario>
    {
        public CrouchCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.state == GameState.Playing)
            {
                Receiver.Crouch(); 
            }
        }
    }
}
