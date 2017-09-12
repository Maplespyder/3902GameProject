using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioClone.Controllers
{
    public class GamepadController : AbstractController
    {
        private GamePadState lastState;

        public GamepadController(PlayerIndex playerIndex)
        {
            lastState = GamePad.GetState(playerIndex);
            if(!lastState.IsConnected)
            {
                lastState = new GamePadState();
            }
        }

        /// <summary>
        /// Executes the commands associated with all gamepad inputs received
        /// since this method was last called, if the gamepad is connected.
        /// </summary>
        public override void UpdateAndExecuteInputs()
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            if (currentState.IsConnected)
            {
                Buttons[] buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                foreach (Buttons button in buttonList)
                {
                    if (lastState.IsButtonUp(button) && currentState.IsButtonDown(button))
                    {
                        ICommand command;
                        if(InputToCommandMap.TryGetValue((int)(button), out command))
                        {
                            command.InvokeCommand();
                        }
                    }
                }
            }

            lastState = currentState;
        }
    }
}
