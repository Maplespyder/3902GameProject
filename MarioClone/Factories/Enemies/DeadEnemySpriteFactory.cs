using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public static class DeadEnemySpriteFactory
    {
        /// <summary>
        /// do not call for koopas, the dead koopa sprite sheet doesn't exist yet
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISprite Create(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Goomba:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(128, 0, 64, 62));
                case EnemyType.GreenKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/GreenKoopa"),
                        new Rectangle(0, 0, 64, 64));
                case EnemyType.RedKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/RedKoopa"),
                        new Rectangle(0, 0, 64, 64));
                default:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(128, 0, 64, 64));
            }
        }
    }
}

