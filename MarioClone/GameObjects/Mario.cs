using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarioClone.States.MarioActionState;

namespace MarioClone.GameObjects
{
    public class Mario : AbstractGameObject
    {
        public const float HorizontalMovementSpeed = 1f;
        public const float VerticalMovementSpeed = 1f;
        private static Mario _mario;

        /// <summary>
        /// Do not instantiate Mario more than once. We have to make Mario before
        /// things that reference him use him, because I can't null check this getter.
        /// If you aren't sure what you're doing comes after Mario's creation, then
        /// null check the return on instance.
        /// </summary>
        public static Mario Instance
        {
            get
            {
                return _mario;
            }
        }

        public MarioActionState ActionState { get; set; }

        public MarioActionState PreviousActionState { get; set; }

        public MarioPowerupState PowerupState { get; set; }
        
        public MarioSpriteFactory SpriteFactory { get; set; }
        
        //passing null sprite because mario's states control his sprite
        public Mario(Vector2 position) : base(null, position, Color.Yellow)
        {
            _mario = this;
            PowerupState = MarioNormal.Instance;
            SpriteFactory = NormalMarioSpriteFactory.Instance;
            ActionState = MarioIdle.Instance;
            Sprite = SpriteFactory.Create(MarioAction.Idle);
            PreviousActionState = MarioIdle.Instance;
            Orientation = Facing.Right;
        }

		public void MoveLeft()
		{
			ActionState.BecomeWalk(Facing.Left);
		}

        public void MoveRight()
        {
            ActionState.BecomeWalk(Facing.Right);
        }

		public void BecomeJump()
        {
            ActionState.BecomeJump();
        }

        public void BecomeCrouch()
        {
            ActionState.BecomeCrouch();
        }

        public void BecomeDead()
        {
            PowerupState.BecomeDead();
        }

        public void BecomeNormal()
        {
            PowerupState.BecomeNormal();
        }

        public void BecomeSuper()
        {
            PowerupState.BecomeSuper();
        }

        public void BecomeFire()
        {
            PowerupState.BecomeFire();
        }

        public override bool Update(GameTime gameTime)
        {
            ActionState.UpdateHitBox();
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
            return base.Update(gameTime);
        }
    }
}