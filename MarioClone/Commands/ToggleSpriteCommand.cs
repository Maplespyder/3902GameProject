namespace MarioClone.Commands
{
    public class ToggleSpriteCommand : AbstractCommand<Sprite>
    {
        public ToggleSpriteCommand(Sprite receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.ToggleSpriteCommand();
        }
    }
}
