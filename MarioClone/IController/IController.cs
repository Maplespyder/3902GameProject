using MarioClone.Command;

namespace MarioClone.Controller
{
    public interface IController
	{
        bool AddInputCommand(int input, ICommand command);
        bool RemoveInputCommand(int input);
        void UpdateAndExecuteInputs();
	}
}
