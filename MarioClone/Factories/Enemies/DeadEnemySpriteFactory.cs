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
                    return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
                        new Rectangle(0, 0, 90, 92),1,6,0,5,8);
                case EnemyType.GreenKoopa:
					return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
				case EnemyType.RedKoopa:
					return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
				case EnemyType.Piranha:
					return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
                case EnemyType.Bowser:
                    return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
                        new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
                default:
					return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);

			}
        }
    }
}

