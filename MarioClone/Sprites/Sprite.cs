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

		public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float layerDepth, GameTime gametime)
		{
			spriteBatch.Draw(SpriteSheet, position, SourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
		}

		#endregion

	}
}
