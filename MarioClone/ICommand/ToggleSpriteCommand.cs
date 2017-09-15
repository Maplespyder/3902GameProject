using MarioClone.Sprite;

namespace MarioClone.Command
{
    public class ToggleSpriteCommand : AbstractCommand<ISprite>
    {
        public ToggleSpriteCommand(ISprite receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.ToggleSpriteCommand();
        }
    }
}
