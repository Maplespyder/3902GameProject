using MarioClone.Collision;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioDead2 : MarioPowerupState
    {
        public MarioDead2(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Dead;
        }

        public override void Enter()
        {
            Context.SpriteFactory = Factories.DeadMarioSpriteFactory.Instance;
            Context.StateMachine.TransitionIdle();
            Context.Lives--;
        }

        public override void BecomeDead() { }

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

        public override void Update(GameTime gameTime) { }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            return false;
        }
    }
}
