using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.GameObjects.Other;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Projectiles
{
	public class CannonFireBallPool
	{
		private int availableFireballs;
		private List<CannonFireBall> FireBalls = new List<CannonFireBall>();
		private Dictionary<CannonFireBall, int> CoolDownList = new Dictionary<CannonFireBall, int>();
		private List<CannonFireBall> RemovedFireBalls = new List<CannonFireBall>();
		public CannonFireBallPool(int availableBalls)
		{
			availableFireballs = availableBalls;
		}

		public void GetAndRelease(AbstractGameObject player)
		{
			if (availableFireballs > 0)
			{
				availableFireballs--;
				CannonFireBall newFireball = (CannonFireBall)ProjectileFactory.Create(ProjectileType.CannonFireBall, player, GetPosition(player));
				FireBalls.Add(newFireball);
				CoolDownList.Add(newFireball, 0);
				newFireball.CoolDown = 0;
				GameGrid.Instance.Add(newFireball);
				//EventManager.Instance.TriggerFireballFire(newFireball);
			}
		}

		public Vector2 GetPosition(AbstractGameObject player)
		{
			Vector2 position;
			position = new Vector2(player.Position.X + player.Sprite.SourceRectangle.Width/2,
						player.Position.Y +50);
			return position;
		}

		public void Update(GameTime gameTime)
		{
			foreach (CannonFireBall fireball in FireBalls)
			{
				CoolDownList[fireball] += gameTime.ElapsedGameTime.Milliseconds;
				if (CoolDownList[fireball] >= 8000)
				{
					Restore(gameTime);
					fireball.Destroyed = true;
				}

				if (fireball.Destroyed)
				{
					GameGrid.Instance.Remove(fireball);
					RemovedFireBalls.Add(fireball);
				}
			}
			foreach (CannonFireBall fireball in RemovedFireBalls)
			{
				FireBalls.Remove(fireball);
				CoolDownList.Remove(fireball);
				fireball.Owner = null;
				Restore(gameTime);
			}
			RemovedFireBalls.Clear();

		}

		public void Restore(GameTime gameTime)
		{
			availableFireballs++;
		}

	}
}
