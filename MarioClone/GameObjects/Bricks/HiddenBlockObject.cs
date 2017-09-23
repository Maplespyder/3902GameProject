using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class HiddenBrickObject : IMoveable, IGameObject
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }

        public ISprite Sprite { get; }

        int IDraw.DrawOrder => throw new NotImplementedException();

        bool IDraw.Visible => throw new NotImplementedException();

        public HiddenBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }
        }
    }
}

