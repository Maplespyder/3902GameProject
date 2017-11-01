using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MarioClone.Commands;

namespace MarioClone.Controllers
{
    public class GamepadController : AbstractController
    {
        private GamePadState lastState;
        private PlayerIndex player;

        public GamepadController(PlayerIndex playerIndex)
        {
            player = playerIndex;
            lastState = GamePad.GetState(playerIndex);
            if(!lastState.IsConnected)
            {
                lastState = new GamePadState();
            }
        }

        public override bool AddInputChord(int modifier, int input, ICommand command)
        {
            return true;
        }

        public override bool RemoveInputChord(int modifier, int input)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands associated with all gamepad inputs received
        /// since this method was last called, if the gamepad is connected.
        /// </summary>
        public override void UpdateAndExecuteInputs()
        {
            GamePadState currentState = GamePad.GetState(player);

            Buttons[] buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

            if (currentState.IsButtonDown(Buttons.Start)) 
            {
                MarioCloneGame.Paused = !MarioCloneGame.Paused;
            }

            if (!MarioCloneGame.Paused)
            {
                foreach (Buttons button in buttonList)
                {
                    if (lastState.IsButtonDown(button))
                    {
                        ICommand command = null;
                        if (currentState.IsButtonUp(button))
                        {
                            if ((command == null) && ReleasedInputToCommandMap.TryGetValue((int)button, out command))
                            {
                                command.InvokeCommand();
                            }
                        } 
                    }
                }

                if (currentState.IsConnected)
                {
                    foreach (Buttons button in buttonList)
                    {
                        if (lastState.IsButtonUp(button) && currentState.IsButtonDown(button))
                        {
                            if (InputToCommandMap.TryGetValue((int)(button), out ICommand command))
                            {
                                command.InvokeCommand();
                            }
                        }
                    }
                }
            }
            lastState = currentState;
        }
    }
}
