using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public class Mario : IGameObject, IDraw, IMoveable
    {
        public MarioState State { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }

        public Mario(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            State = new MarioIdle(this);
            Velocity = velocity;
            Position = position;
        }

        public void Move()
        {
            State.Move();
        }

        public void RunLeft()
        {
            State.RunLeft();
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            Sprite.Draw(spriteBatch, Position, layer);
        }
    }
}
