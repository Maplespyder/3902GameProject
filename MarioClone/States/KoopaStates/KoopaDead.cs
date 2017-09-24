using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class KoopaDead : KoopaState
    {
        public KoopaDead(KoopaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
            Context.State = new KoopaRunLeft(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }

        public override void BecomeRunRight()
        {
            Context.State = new KoopaRunRight(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }

        public override void BecomeDead()
        {
        }
    }
}
