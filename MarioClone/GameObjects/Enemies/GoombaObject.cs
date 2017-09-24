using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;

namespace MarioClone.GameObjects
{
    public class GoombaObject : IGameObject, IMoveable
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public EnemySpriteFactory SpriteFactory { get; set; }

        public GoombaState State { get; set; }

        public GoombaObject(Vector2 velocity, Vector2 position)
        {
            State = new GoombaRunLeft(this);
            SpriteFactory = MovingEnemySpriteFactory.Instance;
            Sprite = SpriteFactory.Create(EnemyType.Goomba);
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public void BecomeRunLeft()
        {
            State.BecomeRunLeft();
        }

        public void BecomeRunRight()
        {
            State.BecomeRunRight();
        }

        public void BecomeDead()
        {
            State.BecomeDead();
        }


        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }
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
