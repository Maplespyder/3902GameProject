using MarioClone.Sprites;

namespace MarioClone.Factories
{
    public abstract class MarioSpriteFactory
    {
        public enum MarioActionState
        {
            Idling,
            Walking,
            Running,
            Jumping,
            Falling,
            Crouching,
            Dying,
            ShootingFireball
        }

        protected MarioSpriteFactory() { }

        public abstract ISprite Create(MarioActionState state);
    }
}
