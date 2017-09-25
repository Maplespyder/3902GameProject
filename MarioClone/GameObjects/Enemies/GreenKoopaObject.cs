using MarioClone.Factories;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
    public class GreenKoopaObject : IGameObject, IMoveable
    {
      
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public EnemySpriteFactory SpriteFactory { get; set; }

        public GreenKoopaObject(Vector2 velocity, Vector2 position)
        {
            SpriteFactory = MovingEnemySpriteFactory.Instance;
            Sprite = SpriteFactory.Create(EnemyType.GreenKoopa);
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, DrawOrder, gameTime, Facing.Left);
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
