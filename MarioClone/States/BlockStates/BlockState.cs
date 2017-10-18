using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.BlockStates
{
    public abstract class BlockState
    {
        public enum BlockStates
        {
            Static,
            Action,
            Used
        }

        public BlockStates State { get; set; }

        protected AbstractBlock Context { get; set; }

        protected BlockState(AbstractBlock context)
        {
            Context = context;
        }

        public abstract void Bump();

        public virtual bool Action(float percent)
        {
            return false;
        }
    }
}
