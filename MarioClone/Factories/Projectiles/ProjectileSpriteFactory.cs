using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
	public class ProjectileSpriteFactory
	{

		public static ISprite Create(ProjectileType type)
		{
			switch (type)
			{
				case ProjectileType.FireBall:
					//make fireball sprite; for brick pieces will suffice
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/BrickPiece"),
					   new Rectangle(0, 0, 32, 32));
				default:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/BrickPiece"),
					   new Rectangle(0, 0, 32, 32));
			}
		}
	}
}
