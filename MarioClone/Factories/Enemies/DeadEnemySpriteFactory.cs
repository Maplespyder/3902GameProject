using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories.Enemies
{
    public class DeadEnemySpriteFactory : EnemySpriteFactory
    {
        static DeadEnemySpriteFactory _factory;
        private static DeadEnemySpriteFactory Instance
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new DeadEnemySpriteFactory();
                }
                return _factory;
            }
        }

        private DeadEnemySpriteFactory() { }

        /// <summary>
        /// do not call for koopas, the dead koopa sprite sheet doesn't exist yet
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override ISprite Create(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Goomba:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(86, 0, 42, 32));
                case EnemyType.GreenKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/GreenKoopa"),
                        new Rectangle(0, 0, 32, 32));
                case EnemyType.RedKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/RedKoopa"),
                        new Rectangle(0, 0, 32, 32));
                default:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(86, 0, 42, 32));
            }
        }
    }
}

