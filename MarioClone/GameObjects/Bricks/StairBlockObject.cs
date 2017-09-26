using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class StairBlockObject : AbstractBlock
    {
        public StairBlockObject(ISprite sprite,  Vector2 position, int drawOrder) : base(sprite,  position, drawOrder)
        {

        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch,  GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
        }

        public override void Bump()
        {
            // do nothing
        }

        public override void Move()
        {
            //do nothing
        }
    }
}
