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
	public static class ProjectileSpriteFactory
	{

		public static ISprite Create(ProjectileType type)
		{
			switch (type)
			{
				case ProjectileType.FireBall:
					//make fireball sprite; for brick pieces will suffice
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Fireball"),
					   new Rectangle(0, 0, 32, 36),1,4,0,3,10);
				default:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Fireball"),
					   new Rectangle(0, 0, 32, 36), 1, 4, 0, 3, 10);
			}
		}
	}
}
