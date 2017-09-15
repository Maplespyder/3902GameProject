namespace MarioClone.Command
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
