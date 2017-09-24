using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using MarioClone.Factories.Enemies;

namespace MarioClone.States
{
    public class GoombaRunLeft : GoombaState
    {
        public GoombaRunLeft(GoombaObject context) : base(context) { }

        public override void BecomeRunLeft()
        {
        }

        public override void BecomeRunRight()
        {
            Context.State = new GoombaRunRight(Context);
            Context.SpriteFactory = MovingEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }

        public override void BecomeDead()
        {
            Context.State = new GoombaDead(Context);
            Context.SpriteFactory = DeadEnemySpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(EnemyType.Goomba);
        }
    }
}
