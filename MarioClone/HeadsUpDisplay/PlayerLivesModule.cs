using MarioClone.GameObjects;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.HeadsUpDisplay
{
    public class PlayerLivesModule : HUDModule
    {
        SpriteFont livesFont;
        ISprite marioSprite;
        private int lives;

        private Vector2 MarioPositionShift { get; set; }

        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public PlayerLivesModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;

            livesFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            marioSprite = Factories.NormalMarioSpriteFactory.Instance.Create(States.MarioAction.Idle);
            lives = ParentHUD.Player.Lives;

            RelativePosition = new Vector2(600, 50);
            MarioPositionShift = new Vector2(-46, 69);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.Green;
                spriteBatch.DrawString(livesFont, "X " + lives, AbsolutePosition, tint);
                marioSprite.Draw(spriteBatch, AbsolutePosition + MarioPositionShift, DrawOrder, gameTime, Facing.Left, 0.6f);
            }
        }

        public void Update(GameTime gameTime)
        {
            lives = ParentHUD.Player.Lives;
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Dispose()
        {
            marioSprite = null;
            ParentHUD = null;
        }
    }
}
