using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class AnimatedSprite : Sprite
    {

        protected int FrameCounter { get; set; }

        protected int CurrentFrame { get; set; }

        protected int StartFrame { get; set; }

        protected int EndFrame { get; set; }

        protected int Rows { get; set; }

        protected int Columns { get; set; }

		private int elapsedTime = 0;
		private int timePerFrame = (1000 / 24); //24 FPS 

        public AnimatedSprite(Texture2D spriteSheet, Rectangle sourceRectangle, int rows, int columns, int startFrame, int endFrame) : 
            base(spriteSheet, sourceRectangle)
        {
            FrameCounter = 0;
            StartFrame = startFrame;
            EndFrame = endFrame;
            CurrentFrame = startFrame;
            Columns = columns;
            Rows = rows;
        }

        private void UpdateSourceRectangle()
        {
            int width = SpriteSheet.Width / Columns;
            int height = SpriteSheet.Height / Rows;
            int row = StartFrame / Columns;
            int column = StartFrame % Columns;
            SourceRectangle = new Rectangle(width * column, height * row, width, height);       
        }

        private void Update(GameTime gameTime)
        {
			elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
			if(elapsedTime > timePerFrame)
			{
				elapsedTime -= timePerFrame;
				CurrentFrame++;
				if (CurrentFrame == EndFrame)
				{
					CurrentFrame = StartFrame;
				}
				UpdateSourceRectangle();
			}


		}

        public override void Draw(SpriteBatch batch, Vector2 position, float layer, GameTime gameTime)
        {
            Update(gameTime);
            base.Draw(batch, position, layer, gameTime);
        }
    }
}
