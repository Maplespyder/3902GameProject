using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class GoombaDead : GoombaState
    {
        public GoombaDead(GoombaObject context) : base(context) { }

        public override void BecomeRun()
        {
            Context.State = new GoombaRun(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }

        public override void BecomeDead()
        {
        }
    }
}
