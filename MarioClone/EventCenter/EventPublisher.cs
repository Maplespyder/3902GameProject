using System;

namespace MarioClone.EventCenter
{
    //generics can have multiple static instances that are exclusive to their generic type
    public partial class EventManager
    {
        static EventManager _instance;
        public static EventManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EventManager();
                }
                return _instance;
            }
        }

        private EventManager() { }

        public event EventHandler<MarioActionStateEventArgs> RaiseMarioActionStateEvent;
        public event EventHandler<MarioPowerupStateEventArgs> RaiseMarioPowerupStateEvent;
        public event EventHandler<PowerupCollectedEventArgs> RaisePowerupCollectedEvent;
        public event EventHandler<EnemyDefeatedEventArgs> RaiseEnemyDefeatedEvent;
        public event EventHandler<BadObjectRemovalEventArgs> RaiseBadObjectRemovalEvent;
        public event EventHandler<BrickBumpedEventArgs> RaiseBrickBumpedEvent;
        public event EventHandler<PlayerWarpingEventArgs> RaisePlayerWarpingEvent;
        public event EventHandler<PlayerKilledBowserEventArgs> RaisePlayerKilledBowserEvent;
		public event EventHandler<RunningOutOfTimeArgs> RaiseRunningOutOfTimeEvent;
		public event EventHandler<FireballFireArgs> RaiseFireballFireEvent;
        public event EventHandler<PlayerDiedEventArgs> RaisePlayerDiedEvent;
        public event EventHandler<PlayerDamagedEventArgs> RaisePlayerDamagedEvent;
        public event EventHandler<TimeRanOutEventArgs> RaiseTimeRanOutEvent;
        public event EventHandler<PlayerHitPoleEventArgs> RaisePlayerHitPoleEvent;
		public event EventHandler<CannonEventArgs> RaiseCannonEvent;


		protected virtual void OnRaiseMarioActionStateEvent(MarioActionStateEventArgs e)
        {
            EventHandler<MarioActionStateEventArgs> handler = RaiseMarioActionStateEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaiseMarioPowerupStateEvent(MarioPowerupStateEventArgs e)
        {
            EventHandler<MarioPowerupStateEventArgs> handler = RaiseMarioPowerupStateEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaisePowerupCollectedEvent(PowerupCollectedEventArgs e)
        {
            EventHandler<PowerupCollectedEventArgs> handler = RaisePowerupCollectedEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaiseEnemyDefeatedEvent(EnemyDefeatedEventArgs e)
        {
            EventHandler<EnemyDefeatedEventArgs> handler = RaiseEnemyDefeatedEvent;
            handler?.Invoke(e.Sender, e);
        }


        protected virtual void OnRaiseBadObjectRemovalEvent(BadObjectRemovalEventArgs e)
        {
            EventHandler<BadObjectRemovalEventArgs> handler = RaiseBadObjectRemovalEvent;
            handler?.Invoke(e.Sender, e);
        }

		protected virtual void OnRaiseBrickBumpedEvent(BrickBumpedEventArgs e)
		{
			EventHandler<BrickBumpedEventArgs> handler = RaiseBrickBumpedEvent;
			handler?.Invoke(e.Sender, e);
		}

        protected virtual void OnRaisePlayerWarpingEvent(PlayerWarpingEventArgs e)
        {
            EventHandler<PlayerWarpingEventArgs> handler = RaisePlayerWarpingEvent;
            handler?.Invoke(e.Sender, e);
        }

		protected virtual void OnRaisePlayerKilledBowserEvent(PlayerKilledBowserEventArgs e)
        {
            EventHandler<PlayerKilledBowserEventArgs> handler = RaisePlayerKilledBowserEvent;
            handler?.Invoke(e.Sender, e);
        }

		protected virtual void OnRaiseFireballFireEvent(FireballFireArgs e)
		{
			EventHandler<FireballFireArgs> handler = RaiseFireballFireEvent;
			handler?.Invoke(e.Sender, e);
		}
        protected virtual void OnRaisePlayerHitPoleEvent(PlayerHitPoleEventArgs e)
        {
            EventHandler<PlayerHitPoleEventArgs> handler = RaisePlayerHitPoleEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaiseRunningOutOfTimeEvent(RunningOutOfTimeArgs e)
		{
			EventHandler<RunningOutOfTimeArgs> handler = RaiseRunningOutOfTimeEvent;
			handler?.Invoke(e.Sender, e);
		}

        protected virtual void OnRaisePlayerDiedEvent(PlayerDiedEventArgs e)
        {
            EventHandler<PlayerDiedEventArgs> handler = RaisePlayerDiedEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaisePlayerDamagedEvent(PlayerDamagedEventArgs e)
        {
            EventHandler<PlayerDamagedEventArgs> handler = RaisePlayerDamagedEvent;
            handler?.Invoke(e.Sender, e);
        }

        protected virtual void OnRaiseTimeRanOutEvent(TimeRanOutEventArgs e)
        {
            EventHandler<TimeRanOutEventArgs> handler = RaiseTimeRanOutEvent;
            handler?.Invoke(e.Sender, e);
        }

		protected virtual void OnRaiseCannonEvent(CannonEventArgs e)
		{
			EventHandler<CannonEventArgs> handler = RaiseCannonEvent;
			handler?.Invoke(e.Sender, e);
		}
	}
}
