using MarioClone.GameObjects;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public static class MarioFactory
    {
        public static Mario Create(Vector2 position)
        {
            //this implementation is not done in any way
            ISprite marioSprite = SuperMarioSpriteFactory.Instance.Create(MarioActionState.Idling);
            return new Mario(marioSprite, new Vector2(0, 0), position);
        }
    }
}
