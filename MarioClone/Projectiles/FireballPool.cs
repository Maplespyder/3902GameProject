using MarioClone.Collision;
using MarioClone.EventCenter;
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
        private List<FireBall> FireBalls = new List<FireBall>();
        private List<FireBall> RemovedFireBalls = new List<FireBall>();

        public FireballPool(int availableBalls)
		{
			availableFireballs = availableBalls;
		}

		public void GetAndRelease(AbstractGameObject player)
		{
			if(availableFireballs > 0)
			{
				availableFireballs--;
				FireBall newFireball = (FireBall)ProjectileFactory.Create(ProjectileType.FireBall, player, GetPosition(player));
                FireBalls.Add(newFireball);
                GameGrid.Instance.Add(newFireball);
                EventManager.Instance.TriggerFireballFire(newFireball);
            }
		}

        public Vector2 GetPosition(AbstractGameObject player)
        {
            Vector2 position; 
            if (player.Orientation is Facing.Right)
            {
                position =  new Vector2(player.Position.X + player.Sprite.SourceRectangle.Width,
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
            foreach (FireBall fireball in FireBalls)
            {
                if (fireball.Destroyed)
                {
                    RemovedFireBalls.Add(fireball);
                    GameGrid.Instance.Remove(fireball);
                }
            }
            foreach (FireBall fireball in RemovedFireBalls)
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
