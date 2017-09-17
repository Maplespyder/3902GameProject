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
            this.SpriteSheet = spriteSheet;
            this.SourceRectangle = sourceRectangle;
        }

		#region ISprite
		
        /// <summary>
		/// Updates behavior of drawable object (sprite)
		/// </summary>
		public abstract void Update();

		public void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth)
		{
			spriteBatch.Draw(SpriteSheet, Position, SourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepth);
		}

		#endregion

	}
}
