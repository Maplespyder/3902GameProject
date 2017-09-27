using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class DummyClass
    {

    }

    public class DummyCommand : AbstractCommand<DummyClass>
    {
        public DummyCommand(DummyClass empty) : base(empty) { }

        public override void InvokeCommand()
        {
            
        }
    }
}
