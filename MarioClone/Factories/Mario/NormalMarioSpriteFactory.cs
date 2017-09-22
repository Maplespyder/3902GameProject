using MarioClone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioClone.States;

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

        public override ISprite Create(MarioActionState state)
        {
            string name = state.GetType().Name;

            //change these for correct values
            //these are all just dummy values
            switch (name)
            {
                case "MarioIdle":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(0, 0, 32, 32));
                case "MarioWalking":
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(0, 0, 32, 32),
                        1, 6, 0, 1, 4);
                case "MarioRunning":
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(128, 0, 32, 32),
                        1, 6, 4, 5, 6);
                case "MarioJumping":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(96, 0, 32, 32));
                case "MarioDying":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(192, 0, 32, 32));
                default:
                    //default will be idling
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(0, 0, 16, 16));
            }
        }
    }
}
