using MarioClone.Collision;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects
{
    public class GreenMushroomObject : AbstractPowerup
	{
        public const float GravityAcceleration = 0.4f;
        public bool Gravity { get; set; }
        public GreenMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) {
            Velocity = new Vector2(2f, 0);
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario)
            {
                isCollided = true;
            }
            else if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Velocity = new Vector2(Velocity.X, 0);
                    Gravity = false;
                }
                else if (side == Side.Left)
                {
                    Velocity = new Vector2(2, Velocity.Y);
                }
                else if (side == Side.Right)
                {
                    Velocity = new Vector2(-2, Velocity.Y);
                }
            }

            return true;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GravityAcceleration);
            }
            Gravity = true;

            return isCollided || base.Update(gameTime, percent);
        }

        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            if (State is MushroomMovingState)
            {
                if (!(obj1 is GreenMushroomObject))
                {
                    if (obj1 is AbstractBlock || obj1 is Mario)
                    {
                        Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                        BoundingBox.UpdateHitBox(Position, Sprite);
                    }
                }
                else
                {
                    if (obj2 is AbstractBlock || obj2 is Mario)
                    {
                        Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                        BoundingBox.UpdateHitBox(Position, Sprite);
                    }
                } 
            }
        }
    }
}