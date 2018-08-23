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
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireIdle"), new Rectangle(0, 0, 114, 167),1,4,0,3,4);
                case MarioAction.Walk:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireSprint"), new Rectangle(0, 0, 124, 166),
						1, 6, 0, 5, 8);
                /*case MarioAction.Run:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SuperMario"), new Rectangle(0, 0, 96, 128),
                        1, 8, 5, 7, 6);*/
                case MarioAction.Fall:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireFall"), new Rectangle(0, 0, 96, 180),1,4,0,3,6);
                case MarioAction.Jump:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireJump"), new Rectangle(0, 0, 97, 167),1,4,0,3,4);
                case MarioAction.Crouch:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireCrouch"), new Rectangle(0, 0, 113, 109),1,4,0,3,4);
                case MarioAction.Dash:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireSprint"), new Rectangle(0, 0, 124, 166),
                        1, 6, 0, 5, 8);

                default:
                    //default will be idling
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireIdle"), new Rectangle(0, 0, 96, 128));

            }

        }
    }
}
