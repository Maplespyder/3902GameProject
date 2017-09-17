using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public abstract class State<TContext>
        where TContext : class
    {
        protected TContext Context { get; set; }

        public State(TContext context)
        {
            Context = context;
        }

        protected abstract void CheckNextState();
    }
}
