using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public interface IState<TContext>
    {
        TContext Context { get; }

        void CheckNextState();
    }
}
