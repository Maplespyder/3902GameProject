using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum ProjectileType
	{
		FireBall
	}

	public static class ProjectileFactory
	{

		public static AbstractGameObject Create(ProjectileType type, AbstractGameObject player, Vector2 position)
		{
			switch (type)
			{
				case ProjectileType.FireBall:
					return new FireBall(ProjectileSpriteFactory.Create(type), player,  position);
				default:
					return new FireBall(ProjectileSpriteFactory.Create(type), player, position);
			}
		}
	}
}
