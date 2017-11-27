using MarioClone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioClone.States;
using static MarioClone.States.MarioActionState;

namespace MarioClone.Factories
{
    public class NormalMarioSpriteFactory : MarioSpriteFactory
    {
        static NormalMarioSpriteFactory _factory;

        private NormalMarioSpriteFactory() : base() { }

        public static MarioSpriteFactory Instance
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new NormalMarioSpriteFactory();
                }
                return _factory;
            }
        }

        public override ISprite Create(MarioAction action)
        {
            switch (action)
            {
                case MarioAction.Idle:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/SmallIdle"), new Rectangle(0, 0, 96, 143),
						1,4,0,3,4);
                case MarioAction.Walk:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/SmallSprint"), new Rectangle(0, 0, 104, 139),
                        1,6,0,5,8);
                /*case MarioAction.RunRight:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(256, 0, 64, 64),
                        1, 6, 4, 5, 6); */
                case MarioAction.Jump:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/SmallJump"), new Rectangle(0, 0, 77, 138),
						1,4,0,3,4);
				case MarioAction.Dead:
					return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
                case MarioAction.Fall:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/SmallFall"), new Rectangle(0, 0, 81, 144));

                default:
					//default will be idling
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/SmallIdle"), new Rectangle(0, 0, 96, 143),
						1, 4, 0, 3, 4);
			}
        }
    }
}