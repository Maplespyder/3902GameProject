using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public abstract class AbstractEnemy : AbstractGameObject
    {
        public static int MaxTimeDead { get { return 250; } }
        public static int MaxTimeShell {  get { return 1000;  } }
        public int TimeDead { get; set; }
        public const float EnemyHorizontalMovementSpeed = 1f;
        protected AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red) { }
        public EnemyPowerupState PowerupState { get; internal set; }
    }
}