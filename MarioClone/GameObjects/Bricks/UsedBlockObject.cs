using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class UsedBlockObject : AbstractBlock
    {
        public UsedBlockObject(ISprite sprite, Vector2 velocity, Vector2 position, int drawOrder) : base(sprite, velocity, position, drawOrder)
        {

        }
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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime);
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
