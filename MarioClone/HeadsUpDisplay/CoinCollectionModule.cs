using MarioClone.GameObjects;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.HeadsUpDisplay
{
    public class CoinCollectionModule : HUDModule
    {
        SpriteFont coinsFont;
        ISprite coinSprite;
        ISprite HUDBox;
        private int coinCount;

        private Vector2 CoinPositionShift { get; set; }
        private Vector2 CoinTextShift { get; set; }
        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public CoinCollectionModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;

            coinsFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            HUDBox = new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("HUDMenuSprites/HUDBox2"), new Rectangle(0, 0, 160, 114));
            coinSprite = Factories.PowerUpSpriteFactory.Create(Factories.PowerUpType.Coin);
            coinCount = ParentHUD.Player.CoinCount;

            if (MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                RelativePosition = new Vector2(1030 / 2, 106);
            }
            else if (MarioCloneGame.Mode == GameMode.SinglePlayer)
            {
                RelativePosition = new Vector2(1250, 106);
            }

            CoinPositionShift = new Vector2(20, -10);
            CoinTextShift = new Vector2(70, -70);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = Color.White;
                HUDBox.Draw(spriteBatch, AbsolutePosition, .49f, gameTime, Facing.Left, 1f);
                spriteBatch.DrawString(coinsFont, "X " + coinCount, AbsolutePosition+CoinTextShift, tint);
                coinSprite.Draw(spriteBatch, AbsolutePosition + CoinPositionShift, DrawOrder, gameTime, Facing.Left, 0.6f);
            }
        }

        public void Update(GameTime gameTime)
        {
            coinCount = ParentHUD.Player.CoinCount;
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Dispose()
        {
            coinSprite = null;
            ParentHUD = null;
        }
    }
}
