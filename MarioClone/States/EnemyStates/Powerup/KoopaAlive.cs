using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;
using MarioClone.Sounds;
using Microsoft.Xna.Framework.Audio;

namespace MarioClone.States.EnemyStates
{
    class KoopaAlive :EnemyPowerupState
    {
        public KoopaAlive(AbstractEnemy context) : base(context)
        {
            if (Context.Orientation == Facing.Right)
            {
                Context.Velocity = new Vector2(1f, Context.Velocity.Y);
            }
            else if (Context.Orientation == Facing.Left)
            {
                Context.Velocity = new Vector2(-1f, Context.Velocity.Y);
            }

            if (Context is GreenKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.GreenKoopa);
            }
            else if (Context is RedKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.RedKoopa);
            }
        }
        
        public override void BecomeDead()
        {
            SoundPool.Instance.GetAndPlay(SoundType.Stomp);
            Context.PowerupState = new KoopaShell(Context);
        }
    }
}
