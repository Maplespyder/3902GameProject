using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
   public abstract class EnemyPowerupState
    {
        protected AbstractEnemy Context { get; set; }


        protected EnemyPowerupState(AbstractEnemy context)
        {
            Context = context;
        }

        // Behavior/actions

        public abstract void BecomeDead();
        public abstract void BecomeAlive();
        public abstract bool Update(GameTime gameTime, float percent);
		public abstract void BecomeHide();
		public abstract void BecomeReveal();

    }
}
