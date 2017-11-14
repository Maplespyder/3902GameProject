using Microsoft.Xna.Framework;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public class MarioFire2 : MarioPowerupState
    {
        public MarioFire2(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Fire;
        }

        public override void Enter()
        {
            Context.SpriteFactory = Factories.FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeDead()
        {
            Context.StateMachine.TransitionDead();
        }

        public override void BecomeNormal()
        {
            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X + 10, Context.Position.Y + 8);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X + 6, Context.Position.Y + 8);
            }

            Context.StateMachine.TransitionNormal();
        }
        
        public override void BecomeSuper()
        {
            Context.StateMachine.TransitionSuper();
        }

        public override void BecomeFire() { }

        public override void BecomeStar()
        {
            Context.StateMachine.TransitionFire();
        }

        public override void TakeDamage()
        {
            Context.StateMachine.TransitionSuper();
            BecomeInvincible();
        }

        public override void BecomeInvincible()
        {
            Context.StateMachine.TransitionInvincible();
        }

        public override void Update(GameTime gameTime) { }
    }
}
