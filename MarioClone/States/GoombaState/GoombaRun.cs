using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using MarioClone.Factories.Enemies;

namespace MarioClone.States
{
    public class GoombaRun : GoombaState
    {
        public GoombaRun(GoombaObject context) : base(context) { }

        public override void BecomeRun()
        {
        }

        public override void BecomeDead()
        {
            Context.State = new GoombaDead(Context);
            Context.SpriteFactory = DeadEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }
    }
}
