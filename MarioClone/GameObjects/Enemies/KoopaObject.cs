using MarioClone.Factories;
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
    public class KoopaObject : IGameObject, IMoveable
    {
      
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public EnemySpriteFactory SpriteFactory { get; set; }

        public KoopaState State { get; set; }

        public KoopaObject(Vector2 velocity, Vector2 position)
        {
            State = new KoopaRunLeft(this);
            SpriteFactory = MovingEnemySpriteFactory.Instance;
            Sprite = SpriteFactory.Create(EnemyType.GreenKoopa);
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public void BecomeRun()
        {
            State.
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
