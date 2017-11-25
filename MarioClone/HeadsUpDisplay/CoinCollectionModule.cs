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
        private int coinCount;

        private Vector2 CoinPositionShift { get; set; }

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
            coinSprite = Factories.PowerUpSpriteFactory.Create(Factories.PowerUpType.Coin);
            coinCount = ParentHUD.Player.CoinCount;

            RelativePosition = new Vector2(1180 / 2, 50);
            CoinPositionShift = new Vector2(-40, 69);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.Gold;
                spriteBatch.DrawString(coinsFont, "X " + coinCount, AbsolutePosition, tint);
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
