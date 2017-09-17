using MarioClone.Sprites;
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

    public abstract class EnemySpriteFactory
    {
        protected EnemySpriteFactory() { }

        public abstract Sprite Create(EnemyType type, Vector2 position);
    }
}
