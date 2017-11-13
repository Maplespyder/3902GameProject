using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeSuperMarioCommand : AbstractCommand<Mario>
    {
        public BecomeSuperMarioCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.state == GameState.Playing)
            {
                Receiver.BecomeSuper(); 
            }
        }
    }
}