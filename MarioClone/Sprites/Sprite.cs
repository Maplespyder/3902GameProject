using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprites
{
	public abstract class Sprite : ISprite
	{

        #region Properties

        public Rectangle SourceRectangle { get; protected set; }

		public Texture2D SpriteSheet { get; protected set; }

		#endregion

		protected Sprite(Texture2D spriteSheet, Rectangle sourceRectangle)
        {
            SpriteSheet = spriteSheet;
            SourceRectangle = sourceRectangle;
        }

		#region ISprite

		public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float layerDepth, GameTime gametime, Facing facing)
		{
            SpriteEffects flip = (facing == Facing.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(SpriteSheet, position, SourceRectangle, Color.White, 0, Vector2.Zero, 1, flip, layerDepth);
		}

		#endregion

	}
}
