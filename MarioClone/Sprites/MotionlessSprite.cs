using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class MotionlessSprite : Sprite
    {
        public MotionlessSprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible) : base(texture, location, velocity, bounds, visible)
        {
        }
    }
}
