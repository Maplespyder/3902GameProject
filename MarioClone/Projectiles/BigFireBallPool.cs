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
	public class BigFireBallPool
	{
		private int availableFireballs;
		private List<BigFireBall> FireBalls = new List<BigFireBall>();
		private List<BigFireBall> RemovedFireBalls = new List<BigFireBall>();
		public BigFireBallPool(int availableBalls)
		{
			availableFireballs = availableBalls;
		}

		public void GetAndRelease(AbstractGameObject player)
		{
			if (availableFireballs > 0)
			{
				availableFireballs--;
				BigFireBall newFireball = (BigFireBall)ProjectileFactory.Create(ProjectileType.BigFireBall, player, GetPosition(player));
				FireBalls.Add(newFireball);
				newFireball.CoolDown = 0;
				GameGrid.Instance.Add(newFireball);
				//EventManager.Instance.TriggerFireballFire(newFireball);
			}
		}

		public Vector2 GetPosition(AbstractGameObject player)
		{
			Vector2 position;
			if (player.Orientation is Facing.Right)
			{
				position = new Vector2(player.Position.X + player.Sprite.SourceRectangle.Width,
						player.Position.Y - player.Sprite.SourceRectangle.Height / 2);
			}
			else
			{
				position = new Vector2(player.Position.X,
						player.Position.Y - player.Sprite.SourceRectangle.Height / 2);
			}
			return position;
		}

		public void Update(GameTime gameTime)
		{
			foreach (BigFireBall fireball in FireBalls)
			{
				fireball.CoolDown += gameTime.ElapsedGameTime.Milliseconds;
				if (fireball.Destroyed)
				{
					GameGrid.Instance.Remove(fireball);
					if (fireball.CoolDown >= 2000)
					{
						RemovedFireBalls.Add(fireball);
					}
				}
			}
			foreach (BigFireBall fireball in RemovedFireBalls)
			{
				FireBalls.Remove(fireball);
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
