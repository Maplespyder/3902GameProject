using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioClone.Factories
{
    public static class MovingEnemySpriteFactory
    {
        public static ISprite Create(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Goomba:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(0, 0, 64, 64), 1, 3, 0, 1, 4);
                case EnemyType.GreenKoopa:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/GreenKoopa"),
                        new Rectangle(0, 0, 64, 64), 1, 4, 0, 1, 4);
                case EnemyType.RedKoopa:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/RedKoopa"),
                        new Rectangle(0, 0, 64, 64), 1, 4, 0, 1, 4);
                case EnemyType.GreenKoopaShell:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/GreenKoopaShell"),
                        new Rectangle(0, 0, 72, 68));
                case EnemyType.RedKoopaShell:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/RedKoopaShell"),
                       new Rectangle(0, 0, 72, 68));
                case EnemyType.Piranha:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Plant"),
						new Rectangle(0, 0, 64, 100), 1, 2, 0, 1, 4);
				default:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(0, 0, 64, 64), 1, 3, 0, 1, 4);
            }
        }
    }
}
