using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class HiddenBrickObject : AbstractBlock
    {
        public HiddenBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = false;
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
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

        public override bool Update(GameTime gametime)
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
