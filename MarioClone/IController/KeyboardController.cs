using Microsoft.Xna.Framework.Input;
using MarioClone.ICommand;
using System.Collections.Generic;

namespace MarioClone.IController
{
    public class KeyboardController : AbstractController
	{
        protected KeyboardState lastState;

        public KeyboardController()
        {
            lastState = new KeyboardState();
        }

        /// <summary>
        /// Executes the commands associated with all keyboard inputs received
        /// since this method was last called.
        /// </summary>
		public override void UpdateAndExecuteInputs()
		{
            KeyboardState currentState = Keyboard.GetState();
            foreach (Keys key in currentState.GetPressedKeys())
            {
                if(lastState.IsKeyUp(key))
                {
                    ICommand.ICommand command;
                    if(InputToCommandMap.TryGetValue((int)key, out command))
                    {
                        command.InvokeCommand();
                    }
                }
            }

            lastState = currentState;
		}
    }
}
