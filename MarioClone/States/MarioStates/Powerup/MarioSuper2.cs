using MarioClone.Factories;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioSuper2 : MarioPowerupState
    {
        public MarioSuper2(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Super;
        }

        public override void Enter()
        {
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
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
        
        public override void BecomeSuper() { }

        public override void BecomeFire()
        {
            Context.StateMachine.TransitionFire();
        }

    
        public override void TakeDamage(AbstractGameObject obj)
        {
            EventCenter.EventManager.Instance.TriggerPlayerDamagedEvent(Context, obj);
            Context.StateMachine.TransitionNormal();
            BecomeInvincible();
        }

        public override void BecomeInvincible()
        {
            Context.StateMachine.TransitionInvincible();
        }

        public override void Update(GameTime gameTime) { }
    }
}
