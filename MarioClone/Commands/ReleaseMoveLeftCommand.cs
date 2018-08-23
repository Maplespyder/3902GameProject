using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class ReleaseMoveLeftCommand : AbstractCommand<Mario>
    {
        public ReleaseMoveLeftCommand(Mario receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.Playing)
            {
                Receiver.ReleaseMoveLeft(); 
            }
        }
    }
}
