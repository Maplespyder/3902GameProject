using MarioClone.GameObjects;
using MarioClone.Sprites;
using MarioClone.States;
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
            return new Mario(new Vector2(0, 0), position);
        }
    }
}
