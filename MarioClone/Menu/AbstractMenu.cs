using MarioClone.Controllers;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Menu
{
    public abstract class AbstractMenu : IDraw
    {
        protected SpriteFont font;
        protected AbstractController controller;

        public float DrawOrder { get; set; }

        public bool Visible { get; set; }

        protected AbstractMenu()
        {
            Visible = true;
            DrawOrder = .51f;
            font = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Letter");
            controller = new KeyboardController();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
