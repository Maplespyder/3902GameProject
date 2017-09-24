using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class KoopaRunRight : KoopaState
    {
        public KoopaRunRight(KoopaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
            Context.State = new KoopaRunLeft(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }

        public override void BecomeRunRight()
        {
        }

        public override void BecomeDead()
        {
            Context.State = new KoopaDead(Context);
            Context.SpriteFactory = DeadEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }
    }
}
