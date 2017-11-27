using MarioClone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioClone.States;
using static MarioClone.States.MarioActionState;

namespace MarioClone.Factories
{
    public class SuperMarioSpriteFactory : MarioSpriteFactory
    {
        static SuperMarioSpriteFactory _factory;

        private SuperMarioSpriteFactory() : base() { }

        public static MarioSpriteFactory Instance
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new SuperMarioSpriteFactory();
                }
                return _factory;
            }
        }

        public override ISprite Create(MarioAction action)
        {
            switch (action)
            {
				case MarioAction.Idle:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigIdle"), new Rectangle(0, 0, 112, 164), 1, 4, 0, 3, 4);
				case MarioAction.Walk:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigSprint"), new Rectangle(0, 0, 124, 164),
						1, 6, 0, 5, 8);
				case MarioAction.Jump:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigJump"), new Rectangle(0, 0, 97, 162),
						1, 4, 0, 3, 4);
				case MarioAction.Crouch:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigCrouch"), new Rectangle(0, 0, 113, 109));
				case MarioAction.Fall:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigFall2"), new Rectangle(0, 0, 96, 171));
				default:
					//default will be idling
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigIdle"), new Rectangle(0, 0, 112, 164), 1, 4, 0, 3, 4);
			}

        }
    }
}
