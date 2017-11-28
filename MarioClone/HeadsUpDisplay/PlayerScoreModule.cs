using MarioClone.EventCenter;
using MarioClone.GameObjects;
using MarioClone.Sprites;
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
        SpriteFont scoreFont;
        private int playerScore;
        ISprite HUDBox;

        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }
        public Vector2 ScoreShift { get; set; }
        public Vector2 PointShift { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public PlayerScoreModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;
            pointsFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            scoreFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            HUDBox = new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("HUDMenuSprites/HUDBox1"), new Rectangle(0, 0, 233, 114));
            playerScore = 0;

            ScoreShift = new Vector2(45, -90);
            PointShift = new Vector2(40, -55);
            RelativePosition = new Vector2(90 / 2, 106);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);

            EventManager.Instance.RaiseEnemyDefeatedEvent += UpdatePlayerScoreFromEnemy;
            EventManager.Instance.RaisePowerupCollectedEvent += UpdatePlayerScoreFromPowerup;
            EventManager.Instance.RaisePlayerHitPoleEvent += UpdatePlayerScoreFromFlagHit;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = ParentHUD.Underground ? Color.White : Color.White;
                HUDBox.Draw(spriteBatch, AbsolutePosition, .49f, gameTime, Facing.Left, 1f);
                spriteBatch.DrawString(pointsFont, String.Format("{0:000000}", playerScore), AbsolutePosition + PointShift, tint);
                spriteBatch.DrawString(scoreFont, "SCORE", AbsolutePosition + ScoreShift, Color.Black);
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
