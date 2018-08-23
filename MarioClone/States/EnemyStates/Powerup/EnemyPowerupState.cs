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

        public virtual bool Update(GameTime gameTime, float percent)
        {
            return false;
        }

        public virtual void BecomeDead() { }
        public virtual void BecomeAlive() { }
		public virtual void BecomeHide() { }
		public virtual void BecomeReveal() { }

    }
}
