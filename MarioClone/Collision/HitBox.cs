using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Collision
{
	public class HitBox
	{
        Texture2D pixel;

		public Rectangle Dimensions { get; protected set; }

		public int X_OffSet { get; protected set; }
		public int Y_OffSet { get; protected set; }

		public HitBox(int x_offSet, int y_offSet, Color hitBoxColor)
		{
			X_OffSet = x_offSet;
			Y_OffSet = y_offSet;
            pixel = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { hitBoxColor });
		}

		public void UpdateHitBox(Vector2 Position, ISprite Sprite)
		{
			Dimensions = new Rectangle((int)Position.X - X_OffSet, (int)Position.Y - Y_OffSet,
				Sprite.SourceRectangle.Width + (2 * X_OffSet), Sprite.SourceRectangle.Height + (2 * Y_OffSet));
		}

        public void HitBoxDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(Dimensions.X, Dimensions.Y, Dimensions.Width, 2), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(Dimensions.X, Dimensions.Y, 2, Dimensions.Height), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(Dimensions.X + Dimensions.Width, Dimensions.Y, 2, Dimensions.Height), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(Dimensions.X, Dimensions.Y + Dimensions.Height, Dimensions.Width, 2), Color.White);
        }
	}
}
