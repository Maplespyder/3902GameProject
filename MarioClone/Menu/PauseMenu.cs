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
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(background,
                new Rectangle(0, 0, MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth,
                MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight), new Color(Color.White, 100));
        }

        public override void Update(GameTime gameTime)
        {
            controller.UpdateAndExecuteInputs();
        }
    }
}
