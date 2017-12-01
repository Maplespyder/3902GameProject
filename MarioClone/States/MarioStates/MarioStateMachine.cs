using MarioClone.EventCenter;
using MarioClone.GameObjects;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioStateMachine
    {
        private Dictionary<MarioAction, MarioActionState> actionStates;
        private Dictionary<MarioPowerup, MarioPowerupState> powerupStates;
        private MarioActionState currentActionState;
        private MarioPowerupState currentPowerupState;

        public Mario Player { get; }


        public MarioActionState CurrentActionState
        {
            get
            {
                return currentActionState;
            }
            private set
            {
                currentActionState.Leave();
                PreviousActionState = currentActionState;
                currentActionState = value;
                currentActionState.Enter();
                EventManager.Instance.TriggerMarioActionStateChangedEvent(Player);
            }
        }

        public MarioPowerupState CurrentPowerupState
        {
            get
            {
                return currentPowerupState;
            }
            private set
            {
                //the comparision to the value was intentionally left
                //off of this property, as opposed to action state

                currentPowerupState.Leave();
                PreviousPowerupState = currentPowerupState;
                currentPowerupState = value;
                currentPowerupState.Enter();
                CurrentActionState.UpdateHitBox();

                EventManager.Instance.TriggerMarioPowerupStateChangedEvent(Player);
            }
        }

        public MarioPowerupState PreviousPowerupState { get; private set; }

        public MarioActionState PreviousActionState { get; private set; }

        //do not call this constructor without also calling begin, or you'll get null reference on mario if
        //you're trying to start up for the first time, or undefined behavior otherwise
        public MarioStateMachine(Mario player)
        {
            Player = player;
            initializeActionStates();
            initializePowerupStates();
        }

        //call this after the constructor or with reset, nowhere else
        public void Begin()
        {
            currentActionState = actionStates[MarioAction.Idle];
            PreviousActionState = currentActionState;

            currentPowerupState = powerupStates[MarioPowerup.Normal];
            PreviousPowerupState = currentPowerupState;

            CurrentPowerupState.Enter();
            CurrentActionState.Enter();

            TransitionInvincible();
        }

        //call this before begin, nowhere else
        public void Reset()
        {
            CurrentPowerupState.Leave();
            CurrentActionState.Leave();
        }

        private void initializeActionStates()
        {
            actionStates = new Dictionary<MarioAction, MarioActionState>();
            actionStates.Add(MarioAction.Idle, new MarioIdle2(Player));
            actionStates.Add(MarioAction.Walk, new MarioWalk2(Player));
            actionStates.Add(MarioAction.Jump, new MarioJump2(Player));
            actionStates.Add(MarioAction.Fall, new MarioFall2(Player));
            actionStates.Add(MarioAction.Crouch, new MarioCrouch2(Player));
            actionStates.Add(MarioAction.Warp, new MarioWarp(Player));
            actionStates.Add(MarioAction.Dash, new MarioDash(Player));
        }

        private void initializePowerupStates()
        {
            powerupStates = new Dictionary<MarioPowerup, MarioPowerupState>();
            powerupStates.Add(MarioPowerup.Dead, new MarioDead2(Player));
            powerupStates.Add(MarioPowerup.Normal, new MarioNormal2(Player));
            powerupStates.Add(MarioPowerup.Super, new MarioSuper2(Player));
            powerupStates.Add(MarioPowerup.Fire, new MarioFire2(Player));
            powerupStates.Add(MarioPowerup.Invincible, new MarioInvincibility2(Player));
        }

        /*private MarioPowerupState GetDowngradedPowerup()
        {
            switch (CurrentPowerupState.Powerup)
            {
                case MarioPowerup.Normal:
                    return powerupStates[MarioPowerup.Dead];
                case MarioPowerup.Super:
                    return powerupStates[MarioPowerup.Normal];
                case MarioPowerup.Fire:
                    return powerupStates[MarioPowerup.Super];
                default:
                    return powerupStates[CurrentPowerupState.Powerup];
            }
        }*/

        public void TransitionDead()
        {
            CurrentPowerupState = powerupStates[MarioPowerup.Dead];
        }

        public void TransitionInvincible()
        {
            CurrentPowerupState = powerupStates[MarioPowerup.Invincible];
        }

        public void TransitionNormal()
        {
            if (CurrentActionState is MarioCrouch2)
            {
                TransitionIdle();
            }
            CurrentPowerupState = powerupStates[MarioPowerup.Normal];
        }

        public void TransitionSuper()
        {
            CurrentPowerupState = powerupStates[MarioPowerup.Super];
        }

        public void TransitionFire()
        {
            CurrentPowerupState = powerupStates[MarioPowerup.Fire];
        }

        public void TransitionIdle()
        {
            CurrentActionState = actionStates[MarioAction.Idle];
        }

        public void TransitionCrouch()
        {
            //TODO add mario dead action state?
            if(!(CurrentPowerupState is MarioNormal2 || CurrentPowerupState is MarioDead2) 
                && !(PreviousPowerupState is MarioNormal2 && CurrentPowerupState is MarioInvincibility2))
            {
                CurrentActionState = actionStates[MarioAction.Crouch];
            }
        }

        public void TransitionDash()
        {
            if (!(CurrentPowerupState is MarioDead2) && !(Player.DashRecharge < Mario.MaxDashCooldown))
            {
                CurrentActionState = actionStates[MarioAction.Dash]; 
            }
        }

        public void TransitionWalk()
        {
            if (!(CurrentPowerupState is MarioDead2))
            {
                CurrentActionState = actionStates[MarioAction.Walk];
            }
        }

        public void TransitionJump()
        {
            if (!(CurrentPowerupState is MarioDead2))
            {
                CurrentActionState = actionStates[MarioAction.Jump];
            }
        }

        public void TransitionFall()
        {
            if (!(CurrentPowerupState is MarioDead2))
            {
                CurrentActionState = actionStates[MarioAction.Fall];
            }
        }

        public void TransitionWarp()
        {
            if (!(CurrentPowerupState is MarioDead2))
            {
                CurrentActionState = actionStates[MarioAction.Warp];
            }
        }

        public void UpdateDash(GameTime gameTime)
        {
            if (Player.DashRecharge < Mario.MaxDashCooldown)
            {
                Player.DashRecharge += gameTime.ElapsedGameTime.Milliseconds;
                if(Player.DashRecharge > Mario.MaxDashCooldown)
                {
                    Player.DashRecharge = Mario.MaxDashCooldown;
                }
            }
            if (CurrentActionState is MarioDash)
            {
                if (((MarioDash)currentActionState).DashFinished)
                {
                    if (PreviousActionState is MarioIdle2 || PreviousActionState is MarioWalk2)
                    {
                        TransitionIdle(); 
                    }
                    else
                    {
                        TransitionFall();
                    }
                }
            }
        }
    }
}
