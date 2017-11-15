using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public abstract class AbstractEnemy : AbstractGameObject
    {
        public const float EnemyHorizontalMovementSpeed = 1f;
        public bool Gravity { get; set; }

        public static int MaxTimeDead { get { return 250; } }
        public static int MaxTimeShell { get { return 2000; } }
        public static int MaxPiranhaReveal { get { return 4000; } }
        public static int MaxPiranhaHide { get { return 2500; } }
        public int PiranhaCycleTime { get; set; }
        public int TimeDead { get; set; }

        public int PointValue { get; set; }
        public EnemyPowerupState PowerupState { get; internal set; }
        public bool IsDead { get; set; }

        protected AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red)
        {
            IsDead = false;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y * percent);
            bool retVal = PowerupState.Update(gameTime, percent);
            Removed = base.Update(gameTime, percent) || retVal;
            return Removed;
        }

        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            if (obj1 is AbstractBlock && obj1.Visible)
            {
                Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                BoundingBox.UpdateHitBox(Position, Sprite);

            }
            else if (obj2 is AbstractBlock && obj2.Visible)
            {
                Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
        }
    }
}