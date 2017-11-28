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

            if (MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                RelativePosition = new Vector2(130 / 2, 50);
            }
            else if (MarioCloneGame.Mode == GameMode.SinglePlayer)
            {
                RelativePosition = new Vector2(130, 50);
            }
            
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);

            EventManager.Instance.RaiseEnemyDefeatedEvent += UpdatePlayerScoreFromEnemy;
            EventManager.Instance.RaisePowerupCollectedEvent += UpdatePlayerScoreFromPowerup;
            EventManager.Instance.RaisePlayerHitPoleEvent += UpdatePlayerScoreFromFlagHit;
            EventManager.Instance.RaisePlayerDamagedEvent += UpdatePlayerScoreFromPlayerDamage;
            EventManager.Instance.RaisePlayerDiedEvent += UpdatePlayerScoreFromPlayerDeath;
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
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
            ParentHUD.Player.Score = playerScore;
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

        public void UpdatePlayerScoreFromPlayerDamage(object sender, PlayerDamagedEventArgs e)
        {
            if(ReferenceEquals(e.DamagedPlayer, ParentHUD.Player))
            {
                if(e.Damager is AbstractEnemy)
                {
                    playerScore = clamp(playerScore - 250);
                }
            }
            else if(e.DamagedPlayer != null)
            {
                playerScore += 700;
            }
        }

        public void UpdatePlayerScoreFromPlayerDeath(object sender, PlayerDiedEventArgs e)
        {

            if (ReferenceEquals(e.DeadPlayer, ParentHUD.Player))
            {
                playerScore = clamp(playerScore - 350);
            }
        }

        private int clamp(int score)
        {
            return score < 0 ? 0 : score;
        }

        public void Dispose()
        {
            EventManager.Instance.RaiseEnemyDefeatedEvent -= UpdatePlayerScoreFromEnemy;
            EventManager.Instance.RaisePowerupCollectedEvent -= UpdatePlayerScoreFromPowerup;
            EventManager.Instance.RaisePlayerHitPoleEvent -= UpdatePlayerScoreFromFlagHit;
            EventManager.Instance.RaisePlayerDamagedEvent -= UpdatePlayerScoreFromPlayerDamage;
            EventManager.Instance.RaisePlayerDiedEvent -= UpdatePlayerScoreFromPlayerDeath;
            pointsFont = null;
            ParentHUD = null;
        }
    }
}
