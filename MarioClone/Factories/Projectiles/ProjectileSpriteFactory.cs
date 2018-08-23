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
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/FireBall"),
					   new Rectangle(0, 0, 32, 36),1,4,0,3,10);
				case ProjectileType.BigFireBall:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BigFireBall"),
                       new Rectangle(0, 0, 180, 60), 6, 1, 0, 5, 10);
				case ProjectileType.CannonFireBall:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CannonFireBall"),
					   new Rectangle(0, 0, 31, 64), 1, 9, 0, 3, 12);
				default:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Fireball"),
                       new Rectangle(0, 0, 32, 36), 1, 4, 0, 3, 10);
            }
		}
	}
}
