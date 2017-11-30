using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class FireCannonBlock : AbstractBlock
    {
        public FireCannonBlock(ISprite sprite, Vector2 position) : base(sprite, position)
        {
        }
    }
}
