using MarioClone.GameObjects;
using MarioClone.GameObjects.Other;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum ProjectileType
	{
		FireBall,
		BigFireBall
	}

	public static class ProjectileFactory
	{

		public static AbstractGameObject Create(ProjectileType type, AbstractGameObject player, Vector2 position)
		{
			switch (type)
			{
				case ProjectileType.FireBall:
					return new FireBall(ProjectileSpriteFactory.Create(type), player,  position);
				case ProjectileType.BigFireBall:
					return new BigFireBall(ProjectileSpriteFactory.Create(type), player, position);
				default:
					return new FireBall(ProjectileSpriteFactory.Create(type), player, position);
			}
		}
	}
}
