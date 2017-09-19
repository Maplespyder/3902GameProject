using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

            //change these for correct values
            //these are all just dummy values
            switch (state)
            {
                case MarioActionState.Idling:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 10, 40, 54));
                case MarioActionState.Walking:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 10, 40, 54),
                        1, 2, 0, 2);
                case MarioActionState.Running:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16),
                        2, 2, 0, 2 * 2);
                case MarioActionState.Jumping:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                case MarioActionState.Falling:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                case MarioActionState.Dying:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                default:
                    //default will be idling
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
            }
        }
    }
}
