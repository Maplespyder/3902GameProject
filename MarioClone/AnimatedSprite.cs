using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone
{
    public abstract class AnimatedSprite : Sprite
    {
        public int Rows { get; protected set; }

        public int Columns { get; protected set; }

        public float FrameTime { get; protected set; }

        public int FrameHeight { get; protected set; }

        public int FrameWidth { get; protected set; }

        public int FrameIndex { get; protected set; }

        public int FrameCount { get; protected set; }

        protected float ElapsedTime { get; set; }

        public AnimatedSprite(Texture2D texture, Vector2 location, Vector2 velocity, List<Rectangle> bounds, bool visible, int rows, int columns, float frameTime, int frameHeight, int frameWidth, int frameCount) : base(texture, location, velocity, bounds, visible)
        {
            Rows = rows;
            Columns = columns;
            FrameTime = frameTime;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            FrameIndex = 0;
            FrameCount = frameCount;
            ElapsedTime = 0.0f;
        }

        public void ResetAnimation()
        {
            FrameIndex = 0;
            ElapsedTime = 0.0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var row = FrameIndex / Columns;
            var column = FrameIndex % Columns;

            var source = new Rectangle(column * FrameWidth, row * FrameHeight, FrameWidth, FrameHeight);

            if (Visible)
            {
                spriteBatch.Draw(Texture, Location, source, Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (ElapsedTime > FrameTime)
            {
                ElapsedTime -= FrameTime;
                FrameIndex = (FrameIndex + 1) % FrameCount;
            }

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            float newX = 0, newY = 0;
            foreach (var bound in Bounds)
            {
                newX = MathHelper.Clamp(Location.X, bound.X, bound.Right - FrameWidth);
                newY = MathHelper.Clamp(Location.Y, bound.Y, bound.Bottom - FrameHeight);
            }
            Location = new Vector2(newX, newY);
        }
    }
}
