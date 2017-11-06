using MarioClone.EventCenter;
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

            coinsFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Name");
            coinSprite = Factories.PowerUpSpriteFactory.Create(Factories.PowerUpType.Coin);
            coinCount = 0;

            RelativePosition = new Vector2(600, 50);
            CoinPositionShift = new Vector2(-64, 30);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
            
            EventManager.Instance.RaisePowerupCollectedEvent += UpdatePlayerScoreFromPowerup;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                spriteBatch.DrawString(coinsFont, "Coins Collected: " + coinCount, AbsolutePosition, Color.Gold);
                coinSprite.Draw(spriteBatch, AbsolutePosition + CoinPositionShift, DrawOrder, gameTime, Facing.Left);
            }
        }

        public void Update(GameTime gameTime)
        {
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void UpdatePlayerScoreFromPowerup(object sender, PowerupCollectedEventArgs e)
        {
            if (ReferenceEquals(e.Collector, ParentHUD.Player) && e.Sender is CoinObject)
            {
                coinCount++;
                if(coinCount >= 100)
                {
                    //life gain somehow
                    coinCount = 0;
                }
            }
        }

        public void Dispose()
        {
            EventManager.Instance.RaisePowerupCollectedEvent -= UpdatePlayerScoreFromPowerup;
            coinSprite = null;
            ParentHUD = null;
        }
    }
}
