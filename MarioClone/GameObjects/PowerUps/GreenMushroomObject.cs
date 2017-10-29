using MarioClone.Collision;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects
{
    public class GreenMushroomObject : AbstractPowerup
	{
        private readonly float GravityAcceleration = .4f;
        
        public bool Gravity { get; set; }

        public GreenMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green)
        {
            Velocity = new Vector2(2f, 0);
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

        public override void FixClipping(Vector2 correction)
        {
            Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
            BoundingBox.UpdateHitBox(Position, Sprite);
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
                else if (side == Side.Left || side == Side.Right)
                {
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                }
            }

            return true;
        }
    }
}