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
        RedKoopa
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
                case EnemyType.RedKoopa:
                    return new RedKoopaObject(MovingEnemySpriteFactory.Create(type), position);
                default:
                    return new GoombaObject(MovingEnemySpriteFactory.Create(type), position);
            }
        }
    }
}
