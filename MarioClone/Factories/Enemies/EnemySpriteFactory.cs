using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public abstract class EnemySpriteFactory
    {
        protected EnemySpriteFactory() { }

        public abstract ISprite Create(EnemyType type);
    }
}
