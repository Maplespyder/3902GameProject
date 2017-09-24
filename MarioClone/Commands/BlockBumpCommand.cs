using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BlockBumpCommand : AbstractCommand<AbstractBlock>
	{
		public BlockBumpCommand(AbstractBlock receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.Bounce();
		}
	}
}
