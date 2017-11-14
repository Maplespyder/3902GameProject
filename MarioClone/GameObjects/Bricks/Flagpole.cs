using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public class Flagpole : AbstractBlock
    {
        public Flagpole(ISprite sprite, ISprite sprite2, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-12, -12, 0, 0);

        }
    }
}
