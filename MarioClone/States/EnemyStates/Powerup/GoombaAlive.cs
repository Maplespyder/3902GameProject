using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.GameObjects.Enemies;
using MarioClone.States.EnemyStates.Powerup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.EnemyStates.Powerup
{
    public abstract class GoombaAlive : EnemyPowerupState
    {

        static GoombaAlive _state;

        public GoombaAlive(AbstractEnemy context) : base(context)
        {
            Powerup = EnemyPowerup.Alive;
        }

        public static EnemyPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new GoombaAlive(GoombaObject.Instance);
                }
                return _state;
            }
        }

        public override void BecomeDead()
        {
            Context.EnemyState = EnemyPowerupState.GoombaAlive.Instance;  
            Context.SpriteFactory = DeadEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.PowerupState.Powerup);


        }

        public override void BecomeAlive()
        {
            
        }
    }
}
