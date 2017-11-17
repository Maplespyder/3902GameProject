using MarioClone.GameObjects;
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

		bool Finished { get; }

		void Draw(SpriteBatch spriteBatch, Vector2 position, float layerDepth, GameTime gameTime, Facing facing, float scaling = 1);
		void Draw(SpriteBatch spriteBatch, Vector2 position, float layerDepth, GameTime gameTime, Facing facing, Color color, float scaling = 1);
	}
}
