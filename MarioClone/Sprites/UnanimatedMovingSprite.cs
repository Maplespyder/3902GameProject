using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class UnanimatedMovingSprite : Sprite
    {
        public UnanimatedMovingSprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible) : base(texture, location, new Rectangle(0, 0, 0, 0), velocity, bounds, visible)
        {
            Velocity = new Vector2(0, 50);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Location.Y < 100)
            {
                Velocity = new Vector2(0, 10);
            }
            else if (Location.Y > 300)
            {
                Velocity = new Vector2(0, -10);
            }
        }
    }
}