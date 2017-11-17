using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.HeadsUpDisplay;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;

namespace MarioClone.EventCenter
{
    public abstract class CustomEventArgs<T> : EventArgs
    {
        public T Sender { get; }

        protected CustomEventArgs(T sender)
        {
            Sender = sender;
        }
    }

    public class MarioActionStateEventArgs : CustomEventArgs<Mario>
    {
        public MarioActionState PreviousActionState { get; }
        public MarioActionState CurrentActionState { get; }

        public MarioActionStateEventArgs(Mario mario) : base(mario)
        {
            PreviousActionState = mario.PreviousActionState;
            CurrentActionState = mario.ActionState;
        }
    }

    public class MarioPowerupStateEventArgs : CustomEventArgs<Mario>
    {
        public MarioPowerupState PreviousPowerupState { get; }
        public MarioPowerupState CurrentPowerupState { get; }

        public MarioPowerupStateEventArgs(Mario mario) : base(mario)
        {
            PreviousPowerupState = mario.PreviousPowerupState;
            CurrentPowerupState = mario.PowerupState;
        }
    }

    public class PowerupCollectedEventArgs : CustomEventArgs<AbstractPowerup>
    {
        public Mario Collector { get; }
        public int PointValue { get; }

        public PowerupCollectedEventArgs(AbstractPowerup powerup, Mario collector) : base(powerup)
        {
            PointValue = powerup.PointValue;
            Collector = collector;
        }
    }

    public class EnemyDefeatedEventArgs : CustomEventArgs<AbstractEnemy>
    {
        public Mario Killer { get; }
        public int BounceCount { get; }
        public int PointValue { get; }

        public EnemyDefeatedEventArgs(AbstractEnemy enemy, Mario killer) : base(enemy)
        {
            Killer = killer;
            PointValue = enemy.PointValue;
            BounceCount = killer.BounceCount;
        }
    }

    public class BadObjectRemovalEventArgs : CustomEventArgs<AbstractGameObject>
    {
        public HitBox LastHitBox { get; }
        public Vector2 Position { get; }
        public BadObjectRemovalEventArgs(AbstractGameObject obj) : base(obj)
        {
            LastHitBox = obj.BoundingBox;
            Position = obj.Position;
        }
    }
    public class BrickBumpedEventArgs : CustomEventArgs<AbstractBlock>
    {
        public PowerUpType PowerupRevealed { get; set; }
        public bool BrickBroken { get; set; }

        public BrickBumpedEventArgs(AbstractBlock obj, PowerUpType powerup, bool broken) : base(obj)
        {
            PowerupRevealed = powerup;
            BrickBroken = broken;
        }
    }

    public class PlayerWarpingEventArgs : CustomEventArgs<PipeTop>
    {
        public PipeTop WarpExit { get; }
        public Mario Warper { get; }

        public PlayerWarpingEventArgs(PipeTop obj, PipeTop exit, Mario player) : base(obj)
        {
            WarpExit = exit;
            Warper = player;
        }
    }
	public class FireballFireArgs : CustomEventArgs<FireBall>
	{
		public FireballFireArgs(FireBall obj) : base(obj)
		{
		}
	}

	public class RunningOutOfTimeArgs : CustomEventArgs<TimeModule>
	{
		public int currentTime { get; }
		public RunningOutOfTimeArgs(TimeModule obj) : base(obj)
		{
			currentTime = obj.CurrentTime;
		}
	}

	public class PlayerHitPoleEventArgs : CustomEventArgs<Mario>
    {
        public int _height { get; }
        public Mario Mario { get; }

        public PlayerHitPoleEventArgs(int height, Mario player) : base(player)
        {
            _height = height;
            Mario = player;
        }
    }
}
