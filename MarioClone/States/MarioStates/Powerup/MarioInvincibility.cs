using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioActionState;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;

namespace MarioClone.States
{
    public class MarioInvincibility : MarioPowerupState
    {
        static MarioInvincibility _state;

        public int InvincibleTime { get; private set; }

        public static int MaxInvincibleDuration { get { return 10; } }


        private MarioInvincibility(Mario context) : base(context)
        {
            InvincibleTime = 0;
        }

        public static MarioPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioInvincibility(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeStar()
        {
            Context.PowerupState = MarioStar.Instance;
        }

        public override void BecomeDead()
        {
            Context.PowerupState = MarioDead.Instance;
            Context.ActionState = MarioIdle.Instance;
            Context.SpriteFactory = DeadMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;
            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
            if (Context.ActionState.Action == MarioAction.Crouch)
            {
                return;
            }
            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X + 10, Context.Position.Y + 8);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X + 6, Context.Position.Y + 8);
            }
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = MarioSuper.Instance;
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
            if (MarioAction.Crouch == Context.ActionState.Action)
            {
                return;
            }

            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X - 10, Context.Position.Y - 8);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X - 6, Context.Position.Y - 8);
            }

        }

        public override void TakeDamage()
        {
        }

        public override void BecomeInvincible()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            InvincibleTime += gameTime.ElapsedGameTime.Milliseconds;
            if (InvincibleTime >= MaxInvincibleDuration)
            {
                InvincibleTime = 0;
                
                BecomeNormal();

                if (Context.PreviousPowerupState is MarioFire)
                {
                    BecomeNormal();
                }
                else if (Context.PreviousPowerupState is MarioSuper)
                {
                    BecomeNormal();
                }
                else if (Context.PreviousPowerupState is MarioStar)
                {
                    BecomeStar();
                }
            }

        }
    }
}

