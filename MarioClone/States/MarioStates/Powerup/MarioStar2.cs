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
        private int colorChangeDelay;
        public int StarmanTime { get; set; }
        public static int MaxStarManDuration { get { return 10000; } }

        public MarioStar2(Mario context) : base(context)
		{
            Powerup = MarioPowerup.Star;
        }

        public override void Enter()
        {
            colorChangeDelay = 0;
            StarmanTime = 0;
            Context.SpriteTint = Color.Tomato;
        }
        
        public override void Leave()
        {
            Context.SpriteTint = Color.White;
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
            CycleColors();
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

        private void CycleColors()
        {
            colorChangeDelay++;
            if (Context.SpriteTint == Color.Tomato && colorChangeDelay >= 15)
            {
                Context.SpriteTint = Color.Gold;
                colorChangeDelay = 0;
            }
            else if (Context.SpriteTint == Color.Gold && colorChangeDelay >= 15)
            {
                Context.SpriteTint = Color.Orange;
                colorChangeDelay = 0;
            }
            else if (Context.SpriteTint == Color.Orange && colorChangeDelay >= 15)
            {
                Context.SpriteTint = Color.Yellow;
                colorChangeDelay = 0;
            }
            else if (Context.SpriteTint == Color.Yellow && colorChangeDelay >= 15)
            {
                Context.SpriteTint = Color.Tomato;
                colorChangeDelay = 0;
            }
        }
    }
}
