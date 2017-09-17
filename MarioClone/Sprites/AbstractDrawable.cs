using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.ISprite
{
	public abstract class AbstractDrawable : IDrawable
	{
		private Vector2 _velocity;
		private Texture2D _spriteSheet;
		private Rectangle _sourceRectangle;


        #region Properties

        protected Rectangle SourceRectangle
		{
			get { return _sourceRectangle; }
			set { _sourceRectangle = value; }
		}

		public Texture2D SpriteSheet
		{
			get { return _spriteSheet; }
			set { _spriteSheet = value; }
		}

		public Vector2 Velocity
		{
			get{ return _velocity; }
			set { _velocity = value; }
		}

        #endregion

        protected AbstractDrawable(Texture2D spriteSheet, Rectangle sourceRectangle, Vector2 velocity)
        {
            this._spriteSheet = spriteSheet;
            this._velocity = velocity;
            this._sourceRectangle = sourceRectangle;
        }

		#region IDrawable
		
        /// <summary>
		/// Updates behavior of drawable object (sprite)
		/// </summary>
		public abstract void Update();

		public abstract void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth);

		#endregion

	}
}
