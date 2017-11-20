using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States
{
    public class GoombaDead : EnemyPowerupState
    {

        public GoombaDead(AbstractEnemy context) : base(context)
        {
            context.IsDead = true;
            Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Goomba);
            Context.PointValue = 0;
			Context.Velocity = new Vector2(0, 0);
            Context.TimeDead = 0;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (Context.Sprite.Finished)
            {
                Context.BoundingBox = new HitBox(-4, -4, -4, -4, Color.Red);
                return true;
            }
            return false;
        }
	}
}
