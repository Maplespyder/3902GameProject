using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeFireMarioCommand : AbstractCommand<Mario>
    {

        public BecomeFireMarioCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.BecomeFire();
        }
    }
}
