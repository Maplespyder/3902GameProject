using MarioClone.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MarioClone.Controllers
{
    public enum Modifier
    {
        LeftShift = Keys.LeftShift,
        RightShift = Keys.RightShift,
        LeftControl = Keys.LeftControl,
        RightControl = Keys.RightControl,
        LeftAlt = Keys.LeftAlt,
        RightAlt = Keys.RightAlt
    }

    public class KeyboardController : AbstractController
    {
        private KeyboardState lastState;
        private Dictionary<int, Dictionary<int, ICommand>> inputChordToCommandMap;

        public KeyboardController()
        {
            lastState = new KeyboardState();
            inputChordToCommandMap = new Dictionary<int, Dictionary<int, ICommand>>();
        }

        public override bool AddInputChord(int modifier, int input, ICommand command)
        {
            if (!Enum.IsDefined(typeof(Modifier), modifier))
            {
                throw new NotSupportedException();
            }

            if (inputChordToCommandMap.ContainsKey(modifier))
            {
                if (!inputChordToCommandMap[modifier].ContainsKey(input))
                {
                    inputChordToCommandMap[modifier].Add(input, command);
                    return true;
                }
                return false;
            }
            else
            {
                inputChordToCommandMap.Add(modifier, new Dictionary<int, ICommand>());
                inputChordToCommandMap[modifier].Add(input, command);
            }

            return true;
        }

        public override bool RemoveInputChord(int modifier, int input)
        {
            if (!Enum.IsDefined(typeof(Modifier), modifier))
            {
                throw new NotSupportedException();
            }

            if (inputChordToCommandMap.ContainsKey(modifier))
            {
                if (inputChordToCommandMap[modifier].ContainsKey(input))
                {
                    inputChordToCommandMap[modifier].Remove(input);
                    return true;
                }
            }

            return false;
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
                if (lastState.IsKeyUp(key))
                {
                    ICommand command = null;

                    foreach (Modifier mod in Enum.GetValues(typeof(Modifier)))
                    {
                        if (currentState.IsKeyDown((Keys)mod))
                        {

                            //if a modifier is being held, it should block other kinds of commands
                            //this guarantees that the null check fails when it tries to run a regular command
                            command = new DummyCommand(new DummyClass());
                            if (inputChordToCommandMap.TryGetValue((int)mod, out Dictionary<int, ICommand> temp))
                            {
                                if (temp.TryGetValue((int)key, out command))
                                {
                                    command.InvokeCommand();
                                    break;
                                }
                                else
                                {
                                    //needs to be set here too, if it makes it in here but trygetvalue fails, because
                                    //trygetvalue will set it back to null;
                                    command = new DummyCommand(new DummyClass());
                                }
                            }
                        }
                    }
                    if ((command == null) && InputToCommandMap.TryGetValue((int)key, out command))
                    {
                        command.InvokeCommand();
                    }

                }
            }

            lastState = currentState;
        }
    }
}
