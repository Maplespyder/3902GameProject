using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class AnimatedMovingSprite : AnimatedSprite
    {
        public AnimatedMovingSprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible, int rows, int columns, float frameTime, int frameHeight, int frameWidth, int frameCount) : base(texture, location, velocity, bounds, visible, rows, columns, frameTime, frameHeight, frameWidth, frameCount)
        {
            Velocity = new Vector2(50, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Location.X < 100)
            {
                Velocity = new Vector2(10, 0);
            }
            else if (Location.X > 500)
            {
                Velocity = new Vector2(-10, 0);
            }
        }
    }
}