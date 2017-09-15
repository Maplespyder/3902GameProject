using MarioClone.Command;

namespace MarioClone.IController
{
    public interface IController
	{
        bool AddInputCommand(int input, Command.ICommand command);
        bool RemoveInputCommand(int input);
        void UpdateAndExecuteInputs();
	}
}
