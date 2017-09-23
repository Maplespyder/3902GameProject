using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class CoinBrickObject : AbstractBlock
    {
        public override void BecomeVisible()
        {
            //do nothing
        }

        public override void Bounce()
        {
            throw new NotImplementedException();
        }

        public override void Break()
        {
            //should transform into used block
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
