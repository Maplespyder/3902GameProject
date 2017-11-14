using System;
using Microsoft.Xna.Framework;
using MarioClone.GameObjects;
using MarioClone.Collision;

namespace MarioClone.States
{
    public class MarioInvincibility2 : MarioPowerupState
    {
        public int InvincibleTime { get; private set; }
        public static int MaxInvincibleDuration { get { return 3000; } }
        
        public MarioInvincibility2(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Invincible;
        }

        public override void Enter()
        {
            InvincibleTime = 0;
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

        public override void Update(GameTime gameTime)
        {
            InvincibleTime += gameTime.ElapsedGameTime.Milliseconds;
            if (InvincibleTime >= MaxInvincibleDuration)
            {
                if (Context.PreviousPowerupState is MarioSuper2)
                {
                    BecomeSuper();
                }
                else if (Context.PreviousPowerupState is MarioNormal2)
                {
                    BecomeNormal();
                }
                else if(Context.PreviousPowerupState is MarioFire2)
                {
                    BecomeFire();
                }
                else
                {
                    //shouldn't happen but we need an out
                    BecomeNormal();
                }
            }
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            return false;
        }
    }
}
