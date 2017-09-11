using MarioClone.ICommand;

namespace MarioClone.IController
{
    public interface IController
	{
        bool AddInputCommand(int input, ICommand.ICommand command);
        bool RemoveInputCommand(int input);
        void UpdateAndExecuteInputs();
	}
}
