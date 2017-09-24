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
            Vector2 velocity = new Vector2(0, 0);
            switch(type)
            {
                case EnemyType.Goomba:
                    return new GoombaObject(velocity, position);
                case EnemyType.GreenKoopa:
                    return new KoopaObject(velocity, position);
                case EnemyType.RedKoopa:
                    return new KoopaObject(velocity, position);
                default:
                    return new GoombaObject(velocity, position);
            }
        }
    }
}
