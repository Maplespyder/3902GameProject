using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioActionState;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;

namespace MarioClone.States
{
	public class MarioStar : MarioPowerupState
	{
		static MarioStar _state;
		public int StarmanTime { get; set; }
		public static int MaxStarManDuration { get { return 10000; } }

		private MarioStar(Mario context) : base(context)
		{
			Powerup = MarioPowerup.Star;
			StarmanTime = 0;
		}

		public static MarioPowerupState Instance
		{
			get
			{
				if (_state == null)
				{
					_state = new MarioStar(Mario.Instance);
				}
				return _state;
			}
		}

		public override void BecomeStar()
		{

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

        public override void Update(GameTime gameTime)		{
			StarmanTime += gameTime.ElapsedGameTime.Milliseconds;
			if(StarmanTime >= MaxStarManDuration)
			{
				StarmanTime = 0;
				if (Context.PreviousPowerupState is MarioFire)
				{
					BecomeFire();
				}else if(Context.PreviousPowerupState is MarioNormal)
				{
					BecomeNormal();
				}else if(Context.PreviousPowerupState is MarioSuper)
				{
					BecomeSuper();
				}
			}
		}
	}
}
