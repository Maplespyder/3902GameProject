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
        public HiddenBrickObject(ISprite sprite, Vector2 velocity, Vector2 position, int drawOrder) : base(sprite, velocity, position, drawOrder)
        {
            Visible = false;
        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch,  GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime);
            }
        }

        public override void Move()
        {
            throw new NotImplementedException();
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
            Visible = true;
        }
    }
}
