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

        /// <summary>
        /// This is a weak copy constructor, it does NOT copy the pixel data as that was eating up 70% CPU time and causing memory leaks. 
        /// This is just meant to be used to save the old hitbox when checking if a hitbox translated at all.
        /// </summary>
        /// <param name="copy"></param>
        public HitBox(HitBox copy)
        {
            this.LeftX_OffSet = copy.LeftX_OffSet;
            this.RightX_OffSet = copy.RightX_OffSet;

            this.TopY_OffSet = copy.TopY_OffSet;
            this.BottomY_OffSet = copy.BottomY_OffSet;

            this.Dimensions = new Rectangle(copy.Dimensions.X, copy.Dimensions.Y, copy.Dimensions.Width, copy.Dimensions.Height);

            TopLeft = new Point(Dimensions.X, Dimensions.Y);
            TopRight = new Point(Dimensions.X + (Dimensions.Width), Dimensions.Y);
            BottomLeft = new Point(Dimensions.X, Dimensions.Y + Dimensions.Height);
            BottomRight = new Point(Dimensions.X + Dimensions.Width, Dimensions.Y + Dimensions.Height);
        }

        public void UpdateHitBox(Vector2 Position, ISprite Sprite)
		{
			Dimensions = new Rectangle((int)Position.X - LeftX_OffSet, (int)Position.Y - TopY_OffSet,
				Sprite.SourceRectangle.Width + (LeftX_OffSet + RightX_OffSet), Sprite.SourceRectangle.Height + (TopY_OffSet + BottomY_OffSet));

            TopLeft = new Point(Dimensions.X, Dimensions.Y);
            TopRight = new Point(Dimensions.X + (Dimensions.Width), Dimensions.Y);
            BottomLeft = new Point(Dimensions.X, Dimensions.Y + Dimensions.Height);
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

        public override bool Equals(object obj)
        {
            if (obj is HitBox)
            {
                HitBox box = (HitBox)obj;
                return box.Dimensions.X == this.Dimensions.X && box.Dimensions.Y == this.Dimensions.Y
                    && box.Dimensions.Width == this.Dimensions.Width && box.Dimensions.Height == this.Dimensions.Height;
                
            }
            return base.Equals(obj);
        }
    }
}
