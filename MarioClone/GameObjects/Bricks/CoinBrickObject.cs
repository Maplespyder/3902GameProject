using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;

namespace MarioClone.GameObjects
{
    public class CoinBrickObject : AbstractBlock
    {
        public CoinBrickObject(ISprite sprite,  Vector2 position) : base(sprite,  position) { }

        public override void Bump()
        {
            // spawn a coin, not implemented yet
            throw new NotImplementedException();
        }
    }
}
