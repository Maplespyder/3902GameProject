using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Collision
{
	public class HitBox
	{

		public Rectangle Dimensions { get; protected set; }

		public int X_OffSet { get; protected set; }
		public int Y_OffSet { get; protected set; }

		public HitBox(int x_offSet, int y_offSet)
		{
			X_OffSet = x_offSet;
			Y_OffSet = y_offSet;
		}

		public void UpdateHitBox(Vector2 Position, ISprite Sprite)
		{
			Dimensions = new Rectangle((int)Position.X - X_OffSet, (int)Position.Y - Y_OffSet,
				Sprite.SourceRectangle.Width + (2 * X_OffSet), Sprite.SourceRectangle.Height + (2 * Y_OffSet));
		}
	}
}
