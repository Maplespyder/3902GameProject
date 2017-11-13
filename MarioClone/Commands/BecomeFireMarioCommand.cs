using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeFireMarioCommand : AbstractCommand<Mario>
    {

        public BecomeFireMarioCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.state == GameState.Playing)
            {
                Receiver.BecomeFire(); 
            }
        }
    }
}
