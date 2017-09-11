using System.Collections.Generic;
using MarioClone.ICommand;

namespace MarioClone.IController
{
    public abstract class AbstractController : IController
    {
        private Dictionary<int, ICommand.ICommand> inputToCommandMap;

        protected AbstractController()
        {
            inputToCommandMap = new Dictionary<int, ICommand.ICommand>();
        }

        protected Dictionary<int, ICommand.ICommand> InputToCommandMap
        {
            get { return inputToCommandMap; }
        }

        /// <summary>
        /// Adds the given input/command key value pair to the input/command
        /// mapping. Returns true if successfully added, and false otherwise.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool AddInputCommand(int input, ICommand.ICommand command)
        {
            if (!inputToCommandMap.ContainsKey(input))
            {
                inputToCommandMap.Add(input, command);
                return true;
            }
            return false;
        }

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

        /// <summary>
        /// Executes the commands associated with all inputs received
        /// since this method was last called.
        /// </summary>
        public abstract void UpdateAndExecuteInputs();
    }
}
