using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
	class SingleLoopAnimatedSprite : Sprite
	{

		protected int CurrentFrame { get; set; }

		protected int StartFrame { get; set; }

		protected int EndFrame { get; set; }

		protected int Rows { get; set; }

		protected int Columns { get; set; }

		private int elapsedTime = 0;
		private int timePerFrame;
        private int lastTime;

        public SingleLoopAnimatedSprite(Texture2D spriteSheet, Rectangle sourceRectangle, int rows, int columns, int startFrame, int endFrame, int fps) :
			base(spriteSheet, sourceRectangle)
		{
			timePerFrame = (1000 / fps);
			Finished = false;
			StartFrame = startFrame;
			EndFrame = endFrame;
			CurrentFrame = startFrame;
			Columns = columns;
			Rows = rows;
		}

		private void UpdateSourceRectangle()
		{
			int FrameWidth = SpriteSheet.Width / Columns;
			int FrameHeight = SpriteSheet.Height / Rows;
			int row = CurrentFrame / Columns;
			int column = CurrentFrame % Columns;
			SourceRectangle = new Rectangle(FrameWidth * column, FrameHeight * row, FrameWidth, FrameHeight);
		}

		private void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds != lastTime)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime > timePerFrame)
                {
                    elapsedTime -= timePerFrame;
                    CurrentFrame++;
                    if (CurrentFrame > EndFrame)
                    {
                        Finished = true;
                    }
                    UpdateSourceRectangle();
                }
            }
            lastTime = gameTime.TotalGameTime.Milliseconds;
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth, GameTime gameTime, Facing facing, float scaling = 1)
		{
			Update(gameTime);
			base.Draw(spriteBatch, Position, LayerDepth, gameTime, facing, scaling);
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth, GameTime gameTime, Facing facing, Color color, float scaling = 1)
		{
			Update(gameTime);
			base.Draw(spriteBatch, Position, LayerDepth, gameTime, facing, color, scaling);
		}
	}
}
