using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using MarioClone.Factories.Enemies;

namespace MarioClone.States
{
    public class KoopaRun : KoopaState
    {
        public KoopaRun(KoopaObject context) : base(context) { }

        public override void BecomeRun()
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
