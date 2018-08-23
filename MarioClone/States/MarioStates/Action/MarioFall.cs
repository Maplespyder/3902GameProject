//using MarioClone.GameObjects;
//using MarioClone.Factories;
//using System;
//using static MarioClone.States.MarioPowerupState;
//using Microsoft.Xna.Framework;

//namespace MarioClone.States
//{
//    public class MarioFall : MarioActionState
//    {
//        static MarioFall _state;

//        private MarioFall(Mario context) : base(context)
//        {
//            Action = MarioAction.Fall;
//        }

//        public static MarioActionState Instance
//        {
//            get
//            {
//                if (_state == null)
//                {
//                    _state = new MarioFall(Mario.Instance);
//                }
//                return _state;
//            }
//        }

//        public override void UpdateHitBox()
//        {
//            if (Context.PowerupState is MarioNormal)
//            {
//                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
//            }
//            else if (Context.PowerupState is MarioSuper || Context.PowerupState is MarioFire)
//            {
//                Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
//            }
//        }

//        public override void Walk(Facing orientation)
//        {
//            Context.Velocity = orientation == Facing.Left ? new Vector2(-Mario.HorizontalMovementSpeed, Context.Velocity.Y) : new Vector2(Mario.HorizontalMovementSpeed, Context.Velocity.Y);
//            Context.Orientation = orientation;
//        }

//        public override void ReleaseWalk(Facing orientation)
//        {
//            if (orientation == Facing.Left && Context.Velocity.X < 0 || orientation == Facing.Right && Context.Velocity.X > 0)
//            {
//                Context.Velocity = new Vector2(0, Context.Velocity.Y);
//            }
//        }
//    }
//}
