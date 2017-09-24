using MarioClone.Commands;

namespace MarioClone
{
    public interface IController
	{
        bool AddInputCommand(int input, ICommand command);
        bool RemoveInputCommand(int input);
        void UpdateAndExecuteInputs();
	}
}
