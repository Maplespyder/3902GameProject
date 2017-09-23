using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class UsedBlockObject : AbstractBlock
    {
        public override void BecomeVisible()
        {
            //do nothing
        }

        public override void Bounce()
        {
            //do nothing
        }

        public override void Break()
        {
            //do nothing
        }

        public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            //do nothing
        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
