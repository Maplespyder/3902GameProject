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
    public class QuestionBlockObject : IMoveable, IGameObject, IDraw
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }


        void Draw(bool visible, SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (visible)
            {
                sprite.Draw(spritebatch, position, layer, gametime);
            }
        }

        public void move()
        {
            throw new NotImplementedException();
        }

        public void Update(Gametime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
