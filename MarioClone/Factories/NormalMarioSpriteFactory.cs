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
            var name = state.GetType().Name;

            //change these for correct values
            //these are all just dummy values
            switch (name)
            {
                case "MarioIdle":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 10, 40, 54));
                case "MarioWalking":
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 10, 40, 54),
                        1, 2, 0, 2);
                case "MarioRunning":
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16),
                        2, 2, 0, 2 * 2);
                case "MarioJumping":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                case "MarioFalling":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                case "MarioDying":
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
                default:
                    //default will be idling
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/NormalMario"), new Rectangle(0, 0, 16, 16));
            }
        }
    }
}
