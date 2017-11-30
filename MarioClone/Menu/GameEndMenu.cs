using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MarioClone.Commands;
using System.Collections.Generic;

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

            menuOptions.Add(new Tuple<string, ICommand>("Return To Main Menu", new ResetLevelCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Play Again", new ResetLevelCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Quit Game", new ExitCommand(game)));
            menuTextPosition = new Vector2(750, 450);

            variablesUndefined = true;
        }

        private void initialize()
        {
            player1Info = new List<string>();
            player2Info = new List<string>();

            //TODO fix for actual modes
            if ((MarioCloneGame.Player1.Winner && !MarioCloneGame.Player2.Winner) || (MarioCloneGame.Player1.Score > MarioCloneGame.Player2.Score))
            {
                winner = "Player 1 WINS!";
            }
            else if ((MarioCloneGame.Player2.Winner && !MarioCloneGame.Player1.Winner) || (MarioCloneGame.Player2.Score > MarioCloneGame.Player1.Score))
            {
                winner = "Player 2 WINS!";
            }
            else
            {
                winner = "There's a TIE!";
            }

            player1Info.Add("PLAYER 1 STATS:");
            player1Info.Add("LIVES: " + MarioCloneGame.Player1.Lives);
            player1Info.Add("COINS: " + MarioCloneGame.Player1.CoinCount);
            player1Info.Add("TIME: " + MarioCloneGame.Player1.Time);
            player1Info.Add("SCORE: " + (MarioCloneGame.Player1.Score + MarioCloneGame.Player1.Time * 10));

            if (MarioCloneGame.Mode == GameMode.MultiPlayer)
            {
                player2Info.Add("PLAYER 2 STATS:");
                player2Info.Add("LIVES: " + MarioCloneGame.Player2.Lives);
                player2Info.Add("COINS: " + MarioCloneGame.Player2.CoinCount);
                player2Info.Add("TIME: " + MarioCloneGame.Player2.Time);
                player2Info.Add("SCORE: " + (MarioCloneGame.Player2.Score + MarioCloneGame.Player2.Time * 10));
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