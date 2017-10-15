using MarioClone.GameObjects.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.EnemyStates.Powerup
{
   public abstract class EnemyPowerupState
    {
        public enum EnemyPowerup
        {
            Dead,
            Alive  
        }

        public EnemyPowerup Powerup { get; set; }

        protected AbstractEnemy Context { get; set; }

        protected EnemyPowerupState(AbstractEnemy context)
        {
            Context = context;
        }

        // Behavior/actions

        public abstract void BecomeDead();
        public abstract void BecomeAlive();

    }
}
