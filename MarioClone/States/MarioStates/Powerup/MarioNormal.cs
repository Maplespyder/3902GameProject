//using MarioClone.GameObjects;
//using MarioClone.Factories;
//using Microsoft.Xna.Framework;
//using MarioClone.Sounds;

//namespace MarioClone.States
//{
//    public class MarioNormal : MarioPowerupState
//    {
//        static MarioNormal _state;

//        private MarioNormal(Mario context) : base(context)
//        {
//            Powerup = MarioPowerup.Normal;
//        }

//        public static MarioPowerupState Instance
//        {
//            get
//            {
//                if (_state == null)
//                {
//                    _state = new MarioNormal(Mario.Instance);
//                }
//                return _state;
//            }
//        }

//        public override void BecomeDead()
//        {
//            Context.Velocity = new Vector2(0, 0);
//            Context.PowerupState = MarioDead.Instance;
//            Context.ActionState = MarioIdle.Instance;
//            Context.SpriteFactory = DeadMarioSpriteFactory.Instance;
//            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
//        }

//        public override void BecomeNormal()
//        {
//            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
//        }

//		public override void BecomeStar()
//		{
//			Context.PowerupState = MarioStar.Instance;
//		}

//		public override void BecomeSuper()
//        {
//            Context.PowerupState = MarioSuper.Instance;
//            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
//            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
//			if (MarioAction.Crouch == Context.ActionState.Action)
//            {
//                return;
//            }

//            if (Context.Orientation == Facing.Left)
//            {
//                Context.Position = new Vector2(Context.Position.X - 10, Context.Position.Y - 8);
//            }
//            else
//            {
//                Context.Position = new Vector2(Context.Position.X - 6, Context.Position.Y - 8);
//            }
            
//        }

//        public override void BecomeFire()
//        {
//            Context.PowerupState = MarioFire.Instance;
//            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
//            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
//			if (MarioAction.Crouch == Context.ActionState.Action)
//            {
//                return;
//            }

//            if (Context.Orientation == Facing.Left)
//            {
//                Context.Position = new Vector2(Context.Position.X - 10, Context.Position.Y - 8);
//            }
//            else
//            {
//                Context.Position = new Vector2(Context.Position.X - 6, Context.Position.Y - 8);
//            }
//        }

//        public override void BecomeInvincible()
//        {
//            TakeDamage();
//        }

//        public override void TakeDamage()
//        {
//            BecomeDead();
//        }
//		public override void Update(GameTime gameTime)
//		{
//		}
//	}
//}
