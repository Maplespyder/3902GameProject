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

		public int LeftX_OffSet { get; protected set; }
        public int RightX_OffSet { get; protected set; }
        public int TopY_OffSet { get; protected set; }
        public int BottomY_OffSet { get; protected set; }

        public Point TopLeft { get; protected set; }
        public Point TopRight { get; protected set; }
        public Point BottomLeft { get; protected set; }
        public Point BottomRight { get; protected set; }


        public HitBox(int leftX_offSet, int rightX_offset, int topY_offSet, int bottomY_offset, Color hitBoxColor)
		{
			LeftX_OffSet = leftX_offSet;
            RightX_OffSet = rightX_offset;
            TopY_OffSet = topY_offSet;
			BottomY_OffSet = bottomY_offset;


            pixel = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { hitBoxColor });
		}

		public void UpdateHitBox(Vector2 Position, ISprite Sprite)
		{
			Dimensions = new Rectangle((int)Position.X - LeftX_OffSet, (int)Position.Y - TopY_OffSet,
				Sprite.SourceRectangle.Width + (LeftX_OffSet + RightX_OffSet), Sprite.SourceRectangle.Height + (TopY_OffSet + BottomY_OffSet));

            TopLeft = new Point(Dimensions.X, Dimensions.Y);
            TopRight = new Point(Dimensions.X + (Dimensions.Width), Dimensions.Y);
            BottomLeft = new Point(Dimensions.X + Dimensions.Y + Dimensions.Height);
            BottomRight = new Point(Dimensions.X + Dimensions.Width, Dimensions.Y + Dimensions.Height);
        }

        public void UpdateOffSets(int leftXOffSet, int rightXOffSet, int topYOffSet, int bottomYOffSet)
        {
            LeftX_OffSet = leftXOffSet;
            RightX_OffSet = rightXOffSet;
            TopY_OffSet = topYOffSet;
            BottomY_OffSet = bottomYOffSet;
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
