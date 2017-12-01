using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioClone.Menu
{
    public class PauseMenu : AbstractMenu
    {
        Texture2D background;

        public PauseMenu(MarioCloneGame game) : base()
        {
            background = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] color = { Color.Black };
            background.SetData(color);

            controller.AddInputCommand((int)Keys.P, new PauseCommand(game));

            menuOptions.Add(new Tuple<string, ICommand>("Resume Game", new PauseCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Restart Level", new ResetLevelCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Return to Main Menu", new ReturnToMainMenuCommand(game)));
            menuOptions.Add(new Tuple<string, ICommand>("Quit Game", new ExitCommand(game)));
            menuTextPosition = new Vector2(750, 450);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(background,
                new Rectangle(0, 0, MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth,
                MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight), new Color(Color.White, 150));

            int yOffset = 0;
            foreach(Tuple<string, ICommand> tuple in menuOptions)
            {
                string str = (yOffset == SelectedOption) ? "<" + tuple.Item1 + ">" : tuple.Item1;
                spriteBatch.DrawString(font, str, menuTextPosition + new Vector2(0, yOffset * 50), Color.White);
                yOffset++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            controller.UpdateAndExecuteInputs();
        }
    }
}
