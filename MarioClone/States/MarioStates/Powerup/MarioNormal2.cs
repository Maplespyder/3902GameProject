using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioNormal2 : MarioPowerupState
    {
        public MarioNormal2(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Normal;
        }

        public override void Enter()
        {
            Context.SpriteFactory = Factories.NormalMarioSpriteFactory.Instance;
            
            //TODO make this a response to the sprite factory changing on its property?
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeDead()
        {
            Context.StateMachine.TransitionDead();
        }
        
        public override void BecomeNormal() { }

        public override void BecomeSuper()
        {
            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y);
            }

            Context.StateMachine.TransitionSuper();
        }


        public override void BecomeFire()
        {
            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y);
            }

            Context.StateMachine.TransitionFire();
        }

        public override void TakeDamage(AbstractGameObject obj)
        {
            EventCenter.EventManager.Instance.TriggerPlayerDamagedEvent(Context, obj);
            BecomeDead();
        }

        public override void BecomeInvincible() { }

        //TODO figure out what to do with update for all the states
        public override void Update(GameTime gameTime) { }
    }
}
