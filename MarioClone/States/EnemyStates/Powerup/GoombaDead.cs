using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.GameObjects.Enemies;
using MarioClone.States.EnemyStates.Powerup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.EnemyStates
{
    public class GoombaDead : EnemyPowerupState
    {
        static GoombaDead _state;

        public GoombaDead(AbstractEnemy context) : base(context)
        {
            Powerup = EnemyPowerup.Dead;
        }


        public static EnemyPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new GoombaDead(GoombaObject.Instance);
                }
                return _state;
            }
        }
        public override void BecomeDead()
        {

        }

        public override void BecomeAlive()
        {
            Context.PowerupState = GoombaAlive.Instance;
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.PowerupState.Powerup);
        }
     



    }
}
