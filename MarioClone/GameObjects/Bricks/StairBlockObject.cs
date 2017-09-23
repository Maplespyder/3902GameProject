using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class StairBlockObject : IGameObject
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; }

        public ISprite Sprite { get; protected set; }

        public StairBlockObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, layer, gameTime);
        }
    }
}
