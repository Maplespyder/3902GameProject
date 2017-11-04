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

        protected virtual void OnRaiseMarioActionStateEvent(MarioActionStateEventArgs e)
        {
            EventHandler<MarioActionStateEventArgs> handler = RaiseMarioActionStateEvent;
            handler(e.Sender, e);
        }

        protected virtual void OnRaiseMarioPowerupStateEvent(MarioPowerupStateEventArgs e)
        {
            EventHandler<MarioPowerupStateEventArgs> handler = RaiseMarioPowerupStateEvent;
            handler(e.Sender, e);
        }

        protected virtual void OnRaisePowerupCollectedEvent(PowerupCollectedEventArgs e)
        {
            EventHandler<PowerupCollectedEventArgs> handler = RaisePowerupCollectedEvent;
            handler(e.Sender, e);
        }

        protected virtual void OnRaiseEnemyDefeatedEvent(EnemyDefeatedEventArgs e)
        {
            EventHandler<EnemyDefeatedEventArgs> handler = RaiseEnemyDefeatedEvent;
            handler(e.Sender, e);
        }


        protected virtual void OnRaiseBadObjectRemovalEvent(BadObjectRemovalEventArgs e)
        {
            EventHandler<BadObjectRemovalEventArgs> handler = RaiseBadObjectRemovalEvent;
            handler(e.Sender, e);
        }
    }
}
