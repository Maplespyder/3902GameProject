using MarioClone.Commands;

namespace MarioClone
{
    public interface IController
	{
        bool AddInputChord(int modifier, int input, ICommand command);
        bool AddInputCommand(int input, ICommand command);
        bool RemoveInputChord(int modifier, int input);
        bool RemoveInputCommand(int input);
        void UpdateAndExecuteInputs();
	}
}
