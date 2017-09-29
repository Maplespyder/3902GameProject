using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarioClone.GameObjects
{
    public class HiddenBrickObject : AbstractBlock
    {

		public HiddenBrickObject(ISprite sprite, Vector2 position, int drawOrder) : base(sprite, position, drawOrder)
        {
            Visible = false;
        }

		public override bool Update(GameTime gameTime)
        {
            BoundingBox.UpdateHitBox(Position, Sprite);
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
                BoundingBox.HitBoxDraw(spriteBatch);

            }
        }


        public override void Bump()
        {
            Visible = true;
        }
    }
}
