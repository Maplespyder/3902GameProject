using MarioClone.Sprites;

namespace MarioClone.Factories
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

    public abstract class MarioSpriteFactory
    {
        protected MarioSpriteFactory() { }

        public abstract ISprite Create(MarioActionState state);
    }
}
