using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.ISprite
{
	class StaticSprite : AbstractDrawable
	{
		public StaticSprite(Texture2D spriteSheet, Rectangle sourceRectangle, Vector2 velocity) : 
            base(spriteSheet, sourceRectangle, velocity)
		{ 

		}

		public override void Update()
		{
            //Nothing
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth)
		{
			spriteBatch.Draw(SpriteSheet, Position, SourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepth);
		}

	}
}
