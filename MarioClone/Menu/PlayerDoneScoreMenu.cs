using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.GameObjects;

namespace MarioClone.Menu
{
    public class PlayerDoneScoreMenu : AbstractMenu
    {
        Mario player;
        Texture2D background;

        public PlayerDoneScoreMenu(Mario player)
        {
            background = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
            Color[] color = { Color.Black };
            background.SetData(color);

            this.player = player;
        }

        public override void MenuOptionSelect() { }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(background,
                  new Rectangle(0, 0, MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice.Viewport.Width,
                  MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), new Color(Color.White, 200), 0, new Vector2(0, 0), SpriteEffects.None, DrawOrder);
            
            spriteBatch.DrawString(font, "STATS:", new Vector2(400, 400), Color.White);
            spriteBatch.DrawString(font, "LIVES: " + player.Lives, new Vector2(400, 550), Color.White);
            spriteBatch.DrawString(font, "COINS: " + player.CoinCount, new Vector2(400, 600), Color.White);
            spriteBatch.DrawString(font, "TIME: " + player.Time, new Vector2(400, 650), Color.White);
            spriteBatch.DrawString(font, "SCORE: " + (player.Score + player.Time * 10), new Vector2(400, 700), Color.White);
        }

        public override void Update(GameTime gameTime) { }
    }
}
