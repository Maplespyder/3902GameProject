using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class MoveLeftCommand : AbstractCommand<Mario>
	{
		public MoveLeftCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
            if (MarioCloneGame.State == GameState.Playing)
            {
                Receiver.MoveLeft(); 
            }
		}
	}
}
