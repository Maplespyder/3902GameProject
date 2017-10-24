using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.BlockStates
{
    public abstract class BlockState
    {
        protected AbstractBlock Context { get; set; }

        protected BlockState(AbstractBlock context)
        {
            Context = context;
        }

        public virtual void Bump()
        {
            //default do nothing
        }

        public virtual bool Action(float percent, GameTime gameTime)
        {
            return false;
        }
    }
}
