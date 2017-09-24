using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeSuperMarioCommand : AbstractCommand<Mario>
    {
        public BecomeSuperMarioCommand(Mario m) : base(m) { }

        public override void InvokeCommand()
        {
            Receiver.BecomeSuper();
        }
    }
}