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
            if (Mario.Instance == null)
            {
                return new Mario(new Vector2(0, 0), position);
            }
            return Mario.Instance;
        }
    }
}
