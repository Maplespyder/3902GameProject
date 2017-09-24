using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BrickBumpCommand : AbstractCommand<AbstractBlock>
	{
		public BrickBumpCommand(AbstractBlock receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.Bounce();
		}
	}
}
