using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class AnimatedUnmovingSprite : AnimatedSprite
    {
        public AnimatedUnmovingSprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible, int rows, int columns, float frameTime, int frameHeight, int frameWidth, int frameCount) : base(texture, location, velocity, bounds, visible, rows, columns, frameTime, frameHeight, frameWidth, frameCount)
        {
        }
    }
}