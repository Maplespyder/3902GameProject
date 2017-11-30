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
        ISprite HUDBox;
        int timeDelta;
        public int CurrentTime { get; set; }
        int maxGameTime = 300;

        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }
        public Vector2 TimeShift { get; set; }
        public Vector2 TimeTextShift { get; set; }
        public Vector2 TimeRelativePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public TimeModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;

            timeFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            HUDBox = new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("HUDMenuSprites/HUDBox2"), new Rectangle(0, 0, 160, 114));
            CurrentTime = maxGameTime;
            timeDelta = 0;

            if(MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                RelativePosition = new Vector2(1440 / 2, 106);
            }
            else if(MarioCloneGame.Mode == GameMode.SinglePlayer)
            {
                RelativePosition = new Vector2(1620, 106);
            }
            TimeShift = new Vector2(45, -55);
            TimeTextShift = new Vector2(35, -90);
            TimeRelativePosition = new Vector2(27, 40);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = Color.White;
                HUDBox.Draw(spriteBatch, AbsolutePosition, .49f, gameTime, Facing.Left, 1f);
                spriteBatch.DrawString(timeFont, "TIME", AbsolutePosition + TimeTextShift, Color.Black);
                spriteBatch.DrawString(timeFont, ParentHUD.Player.Time.ToString(), AbsolutePosition + TimeShift, tint);
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

                if(CurrentTime > 0)
                {
                    CurrentTime -= 1;
                }

                timeDelta = 0;
                if (CurrentTime == 100 || CurrentTime == 97)
                {
                    EventManager.Instance.TriggerRunningOutOfTimeEvent(this);
                }
                else if(CurrentTime <= 0)
                {
                    EventManager.Instance.TriggerTimeRanOutEvent(this, ParentHUD.Player);
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
