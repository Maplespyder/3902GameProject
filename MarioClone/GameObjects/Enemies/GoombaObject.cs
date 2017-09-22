using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class GoombaObject : IGameObject, IMoveable
    {
      
        public ISprite Sprite => throw new NotImplementedException();

        public Vector2 Position => throw new NotImplementedException();

        public Vector2 Velocity => throw new NotImplementedException();

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
