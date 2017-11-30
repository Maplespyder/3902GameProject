using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MarioClone.Commands;
using System.Collections.Generic;
using MarioClone.GameObjects;

namespace MarioClone.Menu
{
    public class MainMenu : AbstractMenu
    {
        Texture2D background;
        List<Tuple<string, ICommand>> multiplayerSubMenuOptions;
        List<Tuple<string, ICommand>> mainMenuOptions;
        MarioCloneGame game;

        public MainMenu(MarioCloneGame game) : base()
        {
            this.game = game;

            background = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] color = { Color.Black };
            background.SetData(color);

            mainMenuOptions = new List<Tuple<string, ICommand>>();
            mainMenuOptions.Add(new Tuple<string, ICommand>("Single Player Mode", new SinglePlayerSelectedCommand(this)));
            mainMenuOptions.Add(new Tuple<string, ICommand>("Two Player Mode", new MultiplayerSelectedCommand(this)));
            mainMenuOptions.Add(new Tuple<string, ICommand>("Quit Game", new ExitCommand(game)));

            multiplayerSubMenuOptions = new List<Tuple<string, ICommand>>();
            multiplayerSubMenuOptions.Add(new Tuple<string, ICommand>("Score Mode (Unlimited Lives)", new ScoreModeSelectedCommand(this)));
            multiplayerSubMenuOptions.Add(new Tuple<string, ICommand>("Score Mode (Limited Lives)", new ScoreModeWithLivesSelectedCommand(this)));
            multiplayerSubMenuOptions.Add(new Tuple<string, ICommand>("Race to the Flag", new RaceModeSelected(this)));
            multiplayerSubMenuOptions.Add(new Tuple<string, ICommand>("Return to Main Menu", new LeaveMultiplayerMenuCommand(this)));

            menuOptions = mainMenuOptions;
            menuTextPosition = new Vector2(750, 450);
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(background,
                new Rectangle(0, 0, MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth,
                MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight), new Color(Color.White, 150));
            
            int yOffset = 0;
            foreach (Tuple<string, ICommand> tuple in menuOptions)
            {
                string str = (yOffset == SelectedOption) ? "<" + tuple.Item1 + ">" : tuple.Item1;
                spriteBatch.DrawString(font, str, menuTextPosition + new Vector2(0, yOffset * 50), Color.Red);
                yOffset++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            controller.UpdateAndExecuteInputs();
        }

        public void SinglePlayerSelected()
        {
            game.StartSinglePlayerRun();
        }

        public void MultiPlayerSelected()
        {
            menuOptions = multiplayerSubMenuOptions;
            SelectedOption = 0;
        }

        public void LeaveMultiplayerMenu()
        {
            menuOptions = mainMenuOptions;
            SelectedOption = 0;
        }

        public void ScoreModeSelected()
        {
            game.StartMultiPlayerRun(MultiplayerType.Score);
            menuOptions = mainMenuOptions;
            SelectedOption = 0;
        }

        public void ScoreModeWithLivesSelected()
        {
            game.StartMultiPlayerRun(MultiplayerType.ScoreWithLives);
            menuOptions = mainMenuOptions;
            SelectedOption = 0;
        }

        public void RaceModeSelected()
        {
            game.StartMultiPlayerRun(MultiplayerType.Race);
            menuOptions = mainMenuOptions;
            SelectedOption = 0;
        }
    }
}