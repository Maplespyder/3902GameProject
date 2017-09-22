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
    public class HiddenBrickObject :  IDraw, IMoveable, IGameObject
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }

        public ISprite Sprite { get; protected set; }

        public HiddenBrickObject(Isprite sprite, Vector2 Velocity, Vector2 Position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;

        }

        public HiddenBrickObject()
        {
        }

        public override void Draw(bool visible, SpriteBatch spriteBatch, float layer, GameTime gameTime)
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

        private void Update(Gametime gametime)
        {
            throw new NotImplementedException();
        }

	}
}
