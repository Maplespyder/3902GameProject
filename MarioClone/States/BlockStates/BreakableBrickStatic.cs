using MarioClone.GameObjects;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickStatic : BlockState
    {
        static bool IsMarioNormal
        {
            get
            {
                return Mario.Instance.PowerupState.Powerup == MarioPowerup.Normal;
            }
        }

        static private bool IsMarioSuper
        {
            get
            {
                return Mario.Instance.PowerupState.Powerup == MarioPowerup.Super;
            }
        }

        static bool IsMarioFire
        {
            get
            {
                return Mario.Instance.PowerupState.Powerup == MarioPowerup.Fire;
            }
        }

        public BreakableBrickStatic(AbstractBlock context) : base(context)
        {
            State = BlockStates.Static;
        }

        public override void Bump()
        {
            if (IsMarioNormal)
            {
                Context.State = new BreakableBrickBounce(Context);
            }
            else if (IsMarioSuper || IsMarioFire)
            {
                Context.State = new BreakableBrickBreak(Context);
            }
        }
    }
}
