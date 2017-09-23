using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public class FloorBlockObject : AbstractBlock
    {
        public FloorBlockObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, layer, gameTime);
        }

        public override void Bounce()
        {
            //do nothing
        }

        public override void Break()
        {
            //do nothing
        }

        public override void BecomeVisible()
        {
            //do nothing
        }

        public override void Move()
        {
            //do nothing
        }
    }
}
