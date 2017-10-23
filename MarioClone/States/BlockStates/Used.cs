using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using MarioClone.Factories;
using Microsoft.Xna.Framework;

namespace MarioClone.States.BlockStates
{
    public class Used : BlockState
    {
        public Used(AbstractBlock context) : base(context)
        {
            context.Sprite = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.UsedBlock);
        }
    }
}
