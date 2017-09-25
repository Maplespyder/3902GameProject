using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States.BlockStates
{
    public class QuestionBlockStatic : BlockState
    {
        public QuestionBlockStatic(AbstractBlock context) : base(context)
        {
            State = BlockStates.Static;
        }

        public override void Bump()
        {
            Context.State = new QuestionBlockAction(Context);
        }
    }
}
