using MarioClone.Commands;
using System.Collections.Generic;

namespace MarioClone.Controllers
{
    public abstract class AbstractController
    {
        private Dictionary<int, ICommand> inputToCommandMap;
        private Dictionary<int, ICommand> releasedInputToCommandMap;
        protected AbstractController()
        {
            inputToCommandMap = new Dictionary<int, ICommand>();
            releasedInputToCommandMap = new Dictionary<int, ICommand>();
        }

        protected Dictionary<int, ICommand> InputToCommandMap
        {
            get { return inputToCommandMap; }
        }

        protected Dictionary<int, ICommand> ReleasedInputToCommandMap
        {
            get { return releasedInputToCommandMap; }
        }

        public abstract bool AddInputChord(int modifier, int input, ICommand command);

        /// <summary>
        /// Adds the given input/command key value pair to the input/command
        /// mapping. Returns true if successfully added, and false otherwise.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool AddInputCommand(int input, ICommand command)
        {
            if (!inputToCommandMap.ContainsKey(input))
            {
                inputToCommandMap.Add(input, command);
                return true;
            }
            return false;
        }

        public bool AddReleasedInputCommand(int input, ICommand command)
        {
            if (!ReleasedInputToCommandMap.ContainsKey(input))
            {
                ReleasedInputToCommandMap.Add(input, command);
                return true;
            }
            return false;
        }

        public abstract bool RemoveInputChord(int modifier, int input);

        /// <summary>
        /// Removes the given input/command key value pair in the input/command
        /// mapping. Returns true if successfully removed, and false otherwise.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool RemoveInputCommand(int input)
        {
            if (inputToCommandMap.ContainsKey(input))
            {
                inputToCommandMap.Remove(input);
                return true;
            }
            return false;
        }

        public bool RemoveReleasedInputCommand(int input)
        {
            if (ReleasedInputToCommandMap.ContainsKey(input))
            {
                ReleasedInputToCommandMap.Remove(input);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executes the commands associated with all inputs received
        /// since this method was last called.
        /// </summary>
        public abstract void UpdateAndExecuteInputs();
    }
}
