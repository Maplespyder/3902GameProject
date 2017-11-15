using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.GameObjects;

namespace MarioClone.GameOver
{
    public enum GameOverOptions
    {
        Exit,
        Replay
    }

    public class GameOverScreen
    {
        MarioCloneGame game;
        SpriteFont font;

        public bool Visible { get; set; }

        public GameOverOptions OptionSelected { get; set; }

        public GameOverScreen(MarioCloneGame _game)
        {
            game = _game;
            Visible = false;
            font = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Color tint = Color.White;
                spriteBatch.DrawString(font, "YOU LOST", new Vector2(500, 500), tint);

                if (OptionSelected == GameOverOptions.Exit)
                {
                    spriteBatch.DrawString(font, "REPLAY", new Vector2(500, 600), tint);
                    spriteBatch.DrawString(font, "*EXIT*", new Vector2(500, 700), tint);
                }
                else if (OptionSelected == GameOverOptions.Replay)
                {
                    spriteBatch.DrawString(font, "*REPLAY*", new Vector2(500, 600), tint);
                    spriteBatch.DrawString(font, "EXIT", new Vector2(500, 700), tint);
                }
            }
        }

        public void MenuSelectCommand()
        {
            if (OptionSelected == GameOverOptions.Exit)
            {
                game.ExitCommand();
            }
            else
            {
                Mario.Instance.Lives = 4;
                game.ResetLevelCommand();
                game.SetAsPlaying();
            }
        }

        public void MenuMoveCommand()
        {
            if (OptionSelected == GameOverOptions.Exit)
            {
                OptionSelected = GameOverOptions.Replay;
            }
            else
            {
                OptionSelected = GameOverOptions.Exit;
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
