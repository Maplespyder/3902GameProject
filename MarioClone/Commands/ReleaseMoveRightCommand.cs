using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class ReleaseMoveRightCommand : AbstractCommand<Mario>
    {
        public ReleaseMoveRightCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.Playing)
            {
                Receiver.ReleaseMoveRight(); 
            }
        }
    }
}
