using MarioClone.Commands;
using MarioClone.Controllers;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MarioClone.Menu
{
    public abstract class AbstractMenu : IDraw
    {
        protected int SelectedOption { get; set; }
        protected List<Tuple<string, ICommand>> menuOptions { get; set; }

        protected SpriteFont font { get; set; }
        protected Vector2 menuTextPosition { get; set; }
        protected AbstractController controller { get; set; }

        public float DrawOrder { get; set; }

        public bool Visible { get; set; }

        protected AbstractMenu()
        {
            SelectedOption = 0;
            DrawOrder = .48f;
            font = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");

            controller = new KeyboardController();
            controller.AddInputCommand((int)Keys.Down, new MenuOptionDownCommand(this));
            controller.AddInputCommand((int)Keys.Up, new MenuOptionUpCommand(this));
            controller.AddInputCommand((int)Keys.Enter, new MenuSelectOptionCommand(this));

            menuOptions = new List<Tuple<string, ICommand>>();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public virtual void MenuOptionDown()
        {
            SelectedOption = (SelectedOption + 1) % menuOptions.Count;
        }

        public virtual void MenuOptionUp()
        {
            SelectedOption = Math.Abs((SelectedOption - 1)) % menuOptions.Count;
        }

        public virtual void MenuOptionSelect()
        {
            menuOptions[SelectedOption].Item2.InvokeCommand();
        }
    }
}
