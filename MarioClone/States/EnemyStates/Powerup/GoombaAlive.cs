using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;

namespace MarioClone.States
{
    public class GoombaAlive : EnemyPowerupState
    {
        public GoombaAlive(AbstractEnemy context) : base(context) { }
        
        public override void BecomeDead()
        {
            Context.PowerupState = new GoombaDead(Context);
		}
    }
}
