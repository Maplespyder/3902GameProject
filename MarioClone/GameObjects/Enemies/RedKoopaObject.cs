using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class RedKoopaObject : AbstractEnemy

    {
        public RedKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
            PowerupState = new KoopaAlive(this);
            Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);
            PointValue = 300;
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario)
            {
                if (side.Equals(Side.Top))
                {
                    PowerupState.BecomeDead();
                    TimeDead = 0;
                    return true;
                }
            }
            else if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Gravity = false;
                    Velocity = new Vector2(Velocity.X, 0);
                }
                else if (side == Side.Left)
                {
                    Velocity = new Vector2(EnemyHorizontalMovementSpeed, Velocity.Y);
                    Orientation = Facing.Right;
                }
                else if (side == Side.Right)
                {
                    Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);
                    Orientation = Facing.Left;
                }
            }

            return false;
        }
    }
}
