using MarioClone.Sprites;
using MarioClone.States;

namespace MarioClone.Factories
{
    public abstract class MarioSpriteFactory
    {
        protected MarioSpriteFactory() { }

        public abstract ISprite Create(MarioActionState state);
    }
}
