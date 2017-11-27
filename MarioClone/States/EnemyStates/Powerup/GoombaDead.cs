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
			Context.Gravity = false;
            Context.TimeDead = 0;
			Context.Position = new Vector2(Context.Position.X + (Context.Sprite.SourceRectangle.Width/2), Context.Position.Y);
			Context.BoundingBox = null;
		}

        public override bool Update(GameTime gameTime, float percent)
        {
            if (Context.Sprite.Finished)
            {
				int x = Context.Sprite.SourceRectangle.Width / 2;
				int y = Context.Sprite.SourceRectangle.Height / 2;
				Context.BoundingBox = new HitBox(-x, -x, -y, -y, Color.Red);
                return true;
            }
            return false;
        }
	}
}
