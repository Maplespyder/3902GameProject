using MarioClone.EventCenter;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.HeadsUpDisplay
{
    public class PlayerScoreModule : HUDModule
    {
        SpriteFont pointsFont;
        private int playerScore;

        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public PlayerScoreModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;
            pointsFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            playerScore = 0;

            RelativePosition = new Vector2(130, 50);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);

            EventManager.Instance.RaiseEnemyDefeatedEvent += UpdatePlayerScoreFromEnemy;
            EventManager.Instance.RaisePowerupCollectedEvent += UpdatePlayerScoreFromPowerup;
            EventManager.Instance.RaisePlayerHitPoleEvent += UpdatePlayerScoreFromFlagHit;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.Red;
                spriteBatch.DrawString(pointsFont, String.Format("{0:000000}", playerScore), AbsolutePosition, tint);
            }
        }

        public void Update(GameTime gameTime)
        {
            //TODO figure out how to add time to score
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void UpdatePlayerScoreFromEnemy(object sender, EnemyDefeatedEventArgs e)
        {
            if(ReferenceEquals(e.Killer, ParentHUD.Player))
            {
                playerScore += e.PointValue + (50 * e.BounceCount);
            }
        }


        public void UpdatePlayerScoreFromPowerup(object sender, PowerupCollectedEventArgs e)
        {
            if (ReferenceEquals(e.Collector, ParentHUD.Player))
            {
                playerScore += e.PointValue;
            }
        }

        public void UpdatePlayerScoreFromFlagHit(object sender, PlayerHitPoleEventArgs e)
        {
            if (ReferenceEquals(e.Mario, ParentHUD.Player))
            { 
                playerScore += e._height;   
            }
        }

        public void Dispose()
        {
            EventManager.Instance.RaiseEnemyDefeatedEvent -= UpdatePlayerScoreFromEnemy;
            EventManager.Instance.RaisePowerupCollectedEvent -= UpdatePlayerScoreFromPowerup;
            EventManager.Instance.RaisePlayerHitPoleEvent -= UpdatePlayerScoreFromFlagHit;
            pointsFont = null;
            ParentHUD = null;
        }
    }
}
