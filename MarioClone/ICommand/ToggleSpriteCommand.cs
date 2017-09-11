namespace MarioClone.ICommand
{
    public class ToggleSpriteCommand : AbstractCommand<ISprite.ISprite>
    {
        public ToggleSpriteCommand(ISprite.ISprite receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.ToggleSpriteCommand();
        }
    }
}
