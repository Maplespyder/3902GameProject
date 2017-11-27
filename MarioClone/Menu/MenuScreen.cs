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
    public enum MenuScreenOptions
    {
        Exit,
        Replay
    }

    public class MenuScreen
    {
        MarioCloneGame game;
        SpriteFont font;

        public MenuScreenOptions OptionSelected { get; set; }

        //TODO  change this to single player mario menu screen, and add a different multiplayer mario menu screen
        public MenuScreen(MarioCloneGame _game)
        {
            game = _game;
            font = game.Content.Load<SpriteFont>("Fonts/Letter");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (MarioCloneGame.State == GameState.GameOver)
            {
                spriteBatch.DrawString(font, "YOU LOST", new Vector2(500, 500), Color.White); 
            }
            else if (MarioCloneGame.State == GameState.Win)
            {
                spriteBatch.DrawString(font, "YOU WIN", new Vector2(500, 500), Color.White);
                spriteBatch.DrawString(font, "LIVES: " + MarioCloneGame.Player1.Lives, new Vector2(500, 750), Color.White);
                spriteBatch.DrawString(font, "COINS: " + MarioCloneGame.Player1.CoinCount, new Vector2(500, 800), Color.White);
                spriteBatch.DrawString(font, "TIME: " + MarioCloneGame.Player1.Time, new Vector2(500, 850), Color.White);
                spriteBatch.DrawString(font, "SCORE: " + (MarioCloneGame.Player1.Score + MarioCloneGame.Player1.Time * 10), new Vector2(500, 900), Color.White);
            }

            if (OptionSelected == MenuScreenOptions.Exit)
            {
                spriteBatch.DrawString(font, "REPLAY", new Vector2(500, 600), Color.White);
                spriteBatch.DrawString(font, "*EXIT*", new Vector2(500, 700), Color.White);
            }
            else if (OptionSelected == MenuScreenOptions.Replay)
            {
                spriteBatch.DrawString(font, "*REPLAY*", new Vector2(500, 600), Color.White);
                spriteBatch.DrawString(font, "EXIT", new Vector2(500, 700), Color.White);
            }            
        }

        public void MenuSelectCommand()
        {
            if (OptionSelected == MenuScreenOptions.Exit)
            {
                game.ExitCommand();
            }
            else
            {
                MarioCloneGame.Player1.Lives = 2;
                game.ResetLevelCommand();
                game.SetAsPlaying();
            }
        }

        public void MenuMoveCommand()
        {
            if (OptionSelected == MenuScreenOptions.Exit)
            {
                OptionSelected = MenuScreenOptions.Replay;
            }
            else
            {
                OptionSelected = MenuScreenOptions.Exit;
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Dispose()
        {
            font = null;
        }
    }
}
