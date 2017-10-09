using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States.BlockStates
{
    public class Used : BlockState
    {
        public Used(AbstractBlock context) : base(context)
        {
            context.Sprite = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.UsedBlock);
        }

        public override void Bump()
        {
            // no bump on used blocks
        }

        public override bool Action(float percent)
        {
            return false;
        }
    }
}
