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
        ISprite HUDBox;
        private int lives;

        private Vector2 MarioPositionShift { get; set; }

        public Vector2 RelativePosition { get; set; }
        public Vector2 TextShift { get; set; }
        public Vector2 AbsolutePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public PlayerLivesModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;

            livesFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            HUDBox = new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("HUDMenuSprites/HUDBox2"), new Rectangle(0, 0, 160, 114));
            marioSprite = Factories.NormalMarioSpriteFactory.Instance.Create(States.MarioAction.Idle);

            lives = ParentHUD.Player.Lives;

            if (MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                RelativePosition = new Vector2(630 / 2, 106);
            }
            else if (MarioCloneGame.Mode == GameMode.SinglePlayer)
            {
                RelativePosition = new Vector2(750, 106);
            }

            TextShift = new Vector2(85, -70);
            MarioPositionShift = new Vector2(20, 40);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.White;
                HUDBox.Draw(spriteBatch, AbsolutePosition, .49f, gameTime, Facing.Left, 1f);
                spriteBatch.DrawString(livesFont, "X" + lives, AbsolutePosition+TextShift, tint);
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
