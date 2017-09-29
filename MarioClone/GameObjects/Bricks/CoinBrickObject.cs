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
        public CoinBrickObject(ISprite sprite,  Vector2 position, int drawOrder) : base(sprite,  position, drawOrder)
        {

        }

        public override void Bump()
        {
            // spawn a coin, not implemented yet
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
            BoundingBox.HitBoxDraw(spriteBatch);
        }

        public override bool Update(GameTime gameTime)
        {
            BoundingBox.UpdateHitBox(Position, Sprite);
            return false;
        }
    }
}
