using MarioClone.Commands;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MarioClone.Controllers
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
					if (key.Equals(Keys.P))
					{
						MarioCloneGame.Paused = !MarioCloneGame.Paused;
					}

					if (!MarioCloneGame.Paused)
					{
						ICommand command;
						if (InputToCommandMap.TryGetValue((int)key, out command))
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
