using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;
using System.Security.Cryptography;

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
        public int Hits { get; set; }

        public int PointValue { get; set; }
        public EnemyPowerupState PowerupState { get; internal set; }
        public bool IsDead { get; set; }
        private byte[] random = new Byte[1];
        private int timer = 0;
        private int maxTimer;
        protected AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red)
        {
            IsDead = false;
            maxTimer = 5000;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > 5000) {
                timer = 0;
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
                if (random[0] < 190)
                {
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                    Orientation = 1 - Orientation;
                    maxTimer = ((random[0] % 3) + 3) *1000;
                }
            }

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