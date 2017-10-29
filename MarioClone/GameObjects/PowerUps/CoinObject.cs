using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Collision;

namespace MarioClone.GameObjects
{
    public class CoinObject : AbstractPowerup
    {

        public CoinObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }
    }
}
