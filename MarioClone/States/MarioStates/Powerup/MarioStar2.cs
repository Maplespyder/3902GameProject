using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.GameObjects;
using MarioClone.Collision;

namespace MarioClone.States
{
    public class MarioStar2 : MarioPowerupState
    {
        public int StarmanTime { get; set; }
        public static int MaxStarManDuration { get { return 10000; } }

        public MarioStar2(Mario context) : base(context)
		{
            Powerup = MarioPowerup.Star;
        }

        public override void Enter()
        {
            StarmanTime = 0;
        }
        
        public override void BecomeDead()
        {
            Context.StateMachine.TransitionDead();
        }

        public override void BecomeNormal()
        {
            Context.StateMachine.TransitionNormal();
        }
        
        public override void BecomeSuper()
        {
            Context.StateMachine.TransitionSuper();
        }
        
        public override void BecomeFire()
        {
            Context.StateMachine.TransitionFire();
        }

        public override void BecomeStar()
        {
            Context.StateMachine.TransitionStar();
        }
        
        public override void TakeDamage() { }
        public override void BecomeInvincible() { }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is StarmanObject)
            {
                BecomeStar();
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            StarmanTime += gameTime.ElapsedGameTime.Milliseconds;
            if (StarmanTime >= MaxStarManDuration)
            {
                if (Context.PreviousPowerupState is MarioFire2)
                {
                    BecomeFire();
                }
                else if (Context.PreviousPowerupState is MarioNormal2)
                {
                    BecomeNormal();
                }
                else if (Context.PreviousPowerupState is MarioSuper2)
                {
                    BecomeSuper();
                }
                else
                {
                    BecomeSuper();
                }
            }
        }
    }
}
