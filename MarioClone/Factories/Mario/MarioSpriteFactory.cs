using MarioClone.Sprites;
using MarioClone.States;
using static MarioClone.States.MarioActionState;

namespace MarioClone.Factories
{
    public abstract class MarioSpriteFactory
    {
        protected MarioSpriteFactory() { }

        public abstract ISprite Create(MarioAction action);
    }
}
