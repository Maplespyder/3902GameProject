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
    public class KoopaObject : IGameObject, IMoveable, IDraw
    {
        //
        // TODO: Add constructor logic here
        //
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

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
