using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using MarioClone.Factories.Enemies;

namespace MarioClone.States
{
    public class KoopaRunLeft : KoopaState
    {
        public KoopaRunLeft(KoopaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
        }

        public override void BecomeRunRight()
        {
            Context.State = new KoopaRunRight(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }

        public override void BecomeDead()
        {
            Context.State = new KoopaDead(Context);
            Context.SpriteFactory = DeadEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.GreenKoopa);
        }
    }
}
