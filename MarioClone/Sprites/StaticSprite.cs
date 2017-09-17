using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Sprites
{
	class StaticSprite : Sprite
	{
		public StaticSprite(Texture2D spriteSheet, Rectangle sourceRectangle) : 
            base(spriteSheet, sourceRectangle)
		{ 

		}

		public override void Update()
		{
            //Nothing
		}

	}
}
