using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;
using MarioClone.GameObjects.Enemies;
using MarioClone.States.EnemyStates.Powerup;

namespace MarioClone.GameObjects
{
    public class GoombaObject : AbstractEnemy 
    {
        private static GoombaObject _goombaObject;

        public EnemyPowerupState PowerupState { get; set; }

        public GoombaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            PowerupState = GoombaAlive.Instance;
            BoundingBox.UpdateOffSets(-4, -4, -4, -4);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }

        public static GoombaObject Instance
        {
            get
            {
                return _goombaObject;
            }
        }

        public override void CollisionResponse(AbstractGameObject gameObject, Side side)
        {
            if (gameObject is Mario)
            {
                if (side.Equals(3) || side.Equals(4))
                {
                    PowerupState.BecomeDead();
                }
            }

        }

        public void BecomeDead()
        {
            PowerupState.BecomeDead();
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            //if goomba is dead for 250 msec
            //change hitbox to null

            return base.Update(gameTime, percent);
        }
    }
}
