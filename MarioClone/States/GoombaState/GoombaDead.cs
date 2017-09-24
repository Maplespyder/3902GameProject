using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class GoombaDead : GoombaState
    {
        public GoombaDead(GoombaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
            Context.State = new GoombaRunLeft(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }

        public override void BecomeRunRight()
        {
            Context.State = new GoombaRunRight(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }

        public override void BecomeDead()
        {
        }
    }
}
