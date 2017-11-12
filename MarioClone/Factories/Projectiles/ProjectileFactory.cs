using MarioClone.GameObjects;
using MarioClone.GameObjects.Other;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories.Projectiles
{
	public enum ProjectileType
	{
		FireBall
	}

	public class ProjectileFactory
	{

		public static AbstractGameObject Create(ProjectileType type, Vector2 position)
		{
			switch (type)
			{
				case ProjectileType.FireBall:
					return new FireBall(ProjectileSpriteFactory.Create(type), position);
				default:
					return new FireBall(ProjectileSpriteFactory.Create(type), position);
			}
		}
	}
}
