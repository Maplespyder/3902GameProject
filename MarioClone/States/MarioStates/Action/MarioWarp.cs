using MarioClone.GameObjects;
using System;

namespace MarioClone.States
{
    public class MarioWarp : MarioActionState
    {
        public MarioWarp(Mario context) : base(context)
        {
            Action = MarioAction.Warp;
        }

        public override void Enter()
        {
            Context.StateMachine.TransitionInvincible();
        }

        public override void Leave()
        {
            if (Context.PreviousPowerupState is MarioSuper2)
            {
                Context.StateMachine.TransitionSuper();
            }
            else if (Context.PreviousPowerupState is MarioNormal2)
            {

                Context.StateMachine.TransitionNormal();
            }
            else if (Context.PreviousPowerupState is MarioFire2)
            {
                Context.StateMachine.TransitionFire();
            }
            else
            {
                Context.StateMachine.TransitionNormal();
            }
        }
        public override void UpdateHitBox()
        {
            //TODO either change it to steal logic from all the action states, or make it do no updating
            /*if (Context.PowerupState is MarioNormal2)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -56, -1);
            }
            else if (Context.PreviousPowerupState is MarioNormal2)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PreviousPowerupState is MarioSuper2 || Context.PreviousPowerupState is MarioFire2)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -56, -1);
            }*/
        }
    }
}
