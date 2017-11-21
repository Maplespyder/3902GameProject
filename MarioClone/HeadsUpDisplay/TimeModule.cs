using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Sprites;
using System;
using MarioClone.EventCenter;
using MarioClone.GameObjects;

namespace MarioClone.HeadsUpDisplay
{
    public class TimeModule : HUDModule
    {
        SpriteFont timeFont;
        int timeDelta;
        public int CurrentTime { get; set; }
        int maxGameTime = 400;

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
            CurrentTime = maxGameTime;
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
                spriteBatch.DrawString(timeFont, CurrentTime.ToString(), AbsolutePosition + TimeRelativePosition, tint);
            }
        }

        public void Update(GameTime gameTime)
        {
            //TODO replace with "time is up" event
            ParentHUD.Player.Time = CurrentTime;
            timeDelta += gameTime.ElapsedGameTime.Milliseconds;
            if (timeDelta >= 1000)
            {
                if (Math.Floor(Math.Log10(CurrentTime) + 1) > Math.Floor(Math.Log10(CurrentTime - 1) + 1))
                {
                    TimeRelativePosition += new Vector2(29, 0);
                }
                CurrentTime -= 1;
                timeDelta = 0;
                if (CurrentTime == 100 || CurrentTime == 97)
                {
                    EventManager.Instance.TriggerRunningOutOfTimeEvent(this);
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
