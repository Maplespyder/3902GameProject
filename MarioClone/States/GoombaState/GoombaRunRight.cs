using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using MarioClone.Factories.Enemies;

namespace MarioClone.States
{
    public class GoombaRunRight : GoombaState
    {
        public GoombaRunRight(GoombaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
            Context.State = new GoombaRunLeft(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }

        public override void BecomeRunRight()
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
