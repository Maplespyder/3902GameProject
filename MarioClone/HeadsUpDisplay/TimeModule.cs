using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Sprites;
using System;

namespace MarioClone.HeadsUpDisplay
{
    public class TimeModule : HUDModule
    {
        SpriteFont timeFont;
        int timeDelta;
        int currentTime;
        int maxGameTime = 200;
        
        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }
        public Vector2 TimeRelativePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public TimeModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;

            timeFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            currentTime = maxGameTime;
            timeDelta = 0;

            RelativePosition = new Vector2(1350, 10);
            TimeRelativePosition = new Vector2(27, 40);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.Black;
                spriteBatch.DrawString(timeFont, "TIME", AbsolutePosition, tint);
                spriteBatch.DrawString(timeFont, currentTime.ToString(), AbsolutePosition + TimeRelativePosition, tint);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentTime == 0)
            {
                //time is up event
            }
            else
            {
                timeDelta += gameTime.ElapsedGameTime.Milliseconds;
                if (timeDelta >= 1000)
                {
                    if (Math.Floor(Math.Log10(currentTime) + 1) > Math.Floor(Math.Log10(currentTime - 1) + 1))
                    {
                        TimeRelativePosition += new Vector2(29, 0);
                    }
                    currentTime -= 1;
                    timeDelta = 0;
					if(currentTime <= 100)
					{
						//Trigger event
					}
                }
            }
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Dispose()
        {

            ParentHUD = null;
        }
    }
}
