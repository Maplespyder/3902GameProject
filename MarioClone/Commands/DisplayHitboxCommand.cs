using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class DisplayHitboxCommand : AbstractCommand<AbstractGameObject>
    {
        public DisplayHitboxCommand() : base(null) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.Playing)
            {
                AbstractGameObject.DisplayHitbox(); 
            }
        }
    }
}
