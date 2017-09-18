using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Sprites
{
	public interface ISprite
	{
        Rectangle SourceRectangle { get; }

        Texture2D SpriteSheet { get; }

        void Draw(SpriteBatch spriteBatch, Vector2 Position, float LayerDepth, GameTime gametime);
	}

}
