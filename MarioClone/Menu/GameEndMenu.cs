using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MarioClone.Commands;
using System.Collections.Generic;
using MarioClone.GameObjects;

namespace MarioClone.Menu
{
    public class GameEndMenu : AbstractMenu
    {
        bool variablesUndefined;
        Texture2D background;
        string winner;
        List<string> player1Info;
        List<string> player2Info;

        public GameEndMenu(MarioCloneGame game) : base()
        {
            background = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] color = { Color.Black };
            background.SetData(color);

            menuOptions.Add(new Tuple<string, ICommand>("Return To Main Menu", new ReturnToMainMenuCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Play Again", new ResetLevelCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Quit Game", new ExitCommand(game)));
            menuTextPosition = new Vector2(750, 450);

            variablesUndefined = true;
        }

        private void initialize()
        {
            player1Info = new List<string>();
            player2Info = new List<string>();

            Mario player1 = MarioCloneGame.Player1;
            Mario player2 = MarioCloneGame.Player2;

            if(MarioCloneGame.Mode == GameMode.SinglePlayer)
            {
                if(player1.LevelCompleted)
                {
                    winner = "You WON!";
                }
                else
                {
                    winner = "You LOST!";
                }
            }
            else if (!(player1.LevelCompleted || player2.LevelCompleted) && (player1.Time <= 0) && (player2.Time == 0))
            {
                winner = "It was a TIE!";
            }
            //game won't end w/o both having level completed unless they ran out of time or it's race
            else if(player1.LevelCompleted && !player2.LevelCompleted)
            {
                winner = "Player 1 WON";
            }
            else if (player2.LevelCompleted && !player1.LevelCompleted)
            {
                winner = "Player 2 WON";
            }
            else if(MarioCloneGame.MultiplayerMode == MultiplayerType.ScoreWithLives)
            {
                if(player1.Lives <= 0 && player2.Lives <= 0)
                {
                    winner = "It was a TIE!";
                }
                else if(player2.Lives <= 0 || (player1.Score > player2.Score))
                {
                    winner = "Player 1 WON!";
                } 
                else if(player1.Lives <= 0 || (player2.Score > player1.Score))
                {
                    winner = "Player 2 WON!";
                }
                else
                {
                    winner = "It was a TIE!";
                }
            }
            else if(MarioCloneGame.MultiplayerMode == MultiplayerType.Score)
            {
                if (player1.Score > player2.Score)
                {
                    winner = "Player 1 WON!";
                }
                else if(player2.Score > player1.Score)
                {
                    winner = "Player 2 WON!";
                }
                else
                {
                    winner = "It was a TIE!";
                }
            }
            player1Info.Add("PLAYER 1 STATS:");
            player1Info.Add("LIVES: " + player1.Lives);
            player1Info.Add("COINS: " + player1.CoinCount);
            player1Info.Add("TIME: " + player1.Time);
            player1Info.Add("SCORE: " + player1.Score);

            if (MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                player2Info.Add("PLAYER 2 STATS:");
                player2Info.Add("LIVES: " + player2.Lives);
                player2Info.Add("COINS: " + player2.CoinCount);
                player2Info.Add("TIME: " + player2.Time);
                player2Info.Add("SCORE: " + player2.Score);
            }

            variablesUndefined = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(variablesUndefined)
            {
                initialize();
            }

            spriteBatch.Draw(background,
                new Rectangle(0, 0, MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth,
                MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight), new Color(Color.White, 150));
            
            int yOffset = 0;
            foreach(string str in player1Info)
            {
                spriteBatch.DrawString(font, str, menuTextPosition + new Vector2(-500, yOffset * 50), Color.Red);
                yOffset += 1;
            }

            spriteBatch.DrawString(font, winner, menuTextPosition + new Vector2(10, -100), Color.Red);
            yOffset = 0;
            foreach (Tuple<string, ICommand> tuple in menuOptions)
            {
                string str = (yOffset == SelectedOption) ? "<" + tuple.Item1 + ">" : tuple.Item1;
                spriteBatch.DrawString(font, str, menuTextPosition + new Vector2(0, yOffset * 50), Color.Red);
                yOffset++;
            }

            yOffset = 0;
            foreach (string str in player2Info)
            {
                spriteBatch.DrawString(font, str, menuTextPosition + new Vector2(500, yOffset * 50), Color.Red);
                yOffset += 1;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (variablesUndefined)
            {
                initialize();
            }
            controller.UpdateAndExecuteInputs();
        }

        public override void MenuOptionSelect()
        {
            variablesUndefined = true;
            base.MenuOptionSelect();
        }
    }
}