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
        public StairBlockObject(ISprite sprite, Vector2 velocity, Vector2 position)
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
