using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
    class AnimatedSprite : Sprite
    {

        private int frameCounter = 0;
        private int currentFrame;
        private int StartFrame;
        private int EndFrame;
        private int Rows;
        private int Columns;

        public AnimatedSprite(Texture2D spriteSheet, Rectangle sourceRectangle, int rows, int columns, int startFrame, int endFrame) : 
            base(spriteSheet, sourceRectangle)
        {
            StartFrame = startFrame;
            EndFrame = endFrame;
            currentFrame = startFrame;
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

        private void Update()
        {
            if(frameCounter == 10)
            {
                frameCounter = 0;
                currentFrame++;
                if(currentFrame == EndFrame)
                {
                    currentFrame = StartFrame;
                }
                UpdateSourceRectangle();
            }
            frameCounter++;      
        }

        public override void Draw(SpriteBatch batch, Vector2 position, float layer)
        {
            Update();
            base.Draw(batch, position, layer);
        }
    }
}
