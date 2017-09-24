using MarioClone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioClone.States;
using static MarioClone.States.MarioActionState;

namespace MarioClone.Factories
{
    public class FireMarioSpriteFactory : MarioSpriteFactory
    {
        static FireMarioSpriteFactory _factory;

        private FireMarioSpriteFactory() : base() { }

        public static MarioSpriteFactory Instance
        {
            get
            { 
                if(_factory == null)
                {
                    _factory = new FireMarioSpriteFactory();
                }
                return _factory;
            }
        }

        public override ISprite Create(MarioAction action)
        {
            switch (action)
            {
                case MarioAction.Idle:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(0, 0, 48, 64));
                case MarioAction.Walk:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(0, 0, 48, 64),
                        1, 8, 0, 1, 4);
                /*case MarioAction.Run:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(0, 0, 48, 64),
                        1, 8, 5, 7, 6);*/
                case MarioAction.Jump:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(144, 0, 48, 64));
                case MarioAction.Crouch:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(192, 0, 48, 64));
                //case "MarioFalling":
                //    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(192, 0, 48, 64));
                default:
                    //default will be idling
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(0, 0, 48, 64));

            }

        }
    }
}
