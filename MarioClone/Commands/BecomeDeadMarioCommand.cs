using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeDeadMarioCommand : AbstractCommand<Mario>
	{
		
		public BecomeDeadMarioCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
            if (MarioCloneGame.state == GameState.Playing)
            {
                Receiver.BecomeDead(); 
            }
		}
	}
}
