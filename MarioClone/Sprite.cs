using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone
{
    public abstract class Sprite
    {
        public Texture2D Texture { get; protected set; }

        public Vector2 Location { get; protected set; }

        public Vector2 Velocity { get; protected set; }

        public bool Visible { get; protected set; }

        public List<Rectangle> Bounds { get; protected set; }

        public Sprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible)
        {
            Texture = texture;
            Location = location;
            Velocity = velocity;
            Visible = visible;
            Bounds = bounds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(Texture, Location, Color.White);
            }
        }

        public void ToggleSpriteCommand()
        {
            Visible = !Visible;
        }

        public virtual void Update(GameTime gameTime)
        {
            Location = new Vector2(Location.X + Velocity.X, Location.Y + Velocity.Y);
            CheckBounds();
        }

        protected virtual void CheckBounds()
        {
            float newX = 0, newY = 0;
            foreach(var bound in Bounds)
            {
                newX = MathHelper.Clamp(Location.X, bound.X, bound.Right - Texture.Width);
                newY = MathHelper.Clamp(Location.Y, bound.Y, bound.Bottom - Texture.Height);
            }
            Location = new Vector2(newX, newY);
        }
    }
}
