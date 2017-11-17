using MarioClone.Factories;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Projectiles
{
	public class FireballPool
	{
		private int availableFireballs;
		public FireballPool()
		{
			availableFireballs = 2;
		}

		public AbstractGameObject GetAndRelease(Mario player, Vector2 position)
		{
			if(availableFireballs > 0)
			{
				availableFireballs--;
				return ProjectileFactory.Create(ProjectileType.FireBall, player, position);
			}
			else
			{
				return null;
			}
		}

		public void Restore()
		{
			availableFireballs++;
		}

	}
}
