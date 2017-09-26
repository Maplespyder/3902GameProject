using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class UsedBlockObject : AbstractBlock
    {
        public UsedBlockObject(ISprite sprite,  Vector2 position, int drawOrder) : base(sprite,  position, drawOrder)
        {

        }

        public override void Bump()
        {
            //Do Nothing   
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
            }
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
