using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public enum EnemyType
    {
        Goomba,
        GreenKoopa,
		Piranha,
		Bowser,
        BowserWalk,
		BowserFire,
		BowserIdle
    }

    public static class EnemyFactory
    {
        public static AbstractGameObject Create(EnemyType type, Vector2 position)
        {
            switch(type)
            {
                case EnemyType.Goomba:
                    return new GoombaObject(MovingEnemySpriteFactory.Create(type), position);
                case EnemyType.GreenKoopa:
                    return new GreenKoopaObject(MovingEnemySpriteFactory.Create(type), position);
                case EnemyType.Piranha:
					return new PiranhaObject(MovingEnemySpriteFactory.Create(type), position);
                case EnemyType.BowserWalk:
                     return new BowserObject(MovingEnemySpriteFactory.Create(type), position);
				case EnemyType.BowserIdle:
					return new BowserObject(MovingEnemySpriteFactory.Create(type), position);
				case EnemyType.BowserFire:
					return new BowserObject(MovingEnemySpriteFactory.Create(type), position);
				default:
                    return new GoombaObject(MovingEnemySpriteFactory.Create(type), position);
            }
        }
    }
}
