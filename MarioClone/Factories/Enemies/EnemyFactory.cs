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

    public class EnemyFactory
    {
        static EnemyFactory _factory;

        public static EnemyFactory Instance
        {
            get
            {
                if(_factory == null)
                {
                    _factory = new EnemyFactory();
                }
                return _factory;
            }
        }

        public IGameObject Create(EnemyType type, Vector2 position)
        {
            switch(type)
            {
                case EnemyType.Goomba:
                    return new GoombaObject();
                case EnemyType.GreenKoopa:
                    return new KoopaObject();
                case EnemyType.RedKoopa:
                    return new KoopaObject();
                default:
                    return new GoombaObject();
            }
        }
    }
}
