using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;
using MarioClone.Collision;

namespace MarioClone.GameObjects
{
    public class GoombaObject : IGameObject, IMoveable, ICollidable
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public EnemySpriteFactory SpriteFactory { get; protected set; }

		public HitBox BoundingBox { get; protected set; }


		public GoombaObject(Vector2 velocity, Vector2 position)
        {
            SpriteFactory = MovingEnemySpriteFactory.Instance;
            Sprite = SpriteFactory.Create(EnemyType.Goomba);
			BoundingBox = new HitBox(-4, 0, Color.Red);
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
				Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
                BoundingBox.HitBoxDraw(spriteBatch);
			}
        }
   
        public bool Update(GameTime gameTime)
        {
			BoundingBox.UpdateHitBox(Position, Sprite);
			return false;
        }
    }
}
