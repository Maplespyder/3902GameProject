using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.EventCenter
{
    public partial class EventManager
    {
        public void TriggerMarioActionStateChangedEvent(Mario mario)
        {
            MarioActionStateEventArgs args = new MarioActionStateEventArgs(mario);
            OnRaiseMarioActionStateEvent(args);
        }

        public void TriggerMarioPowerupStateChangedEvent(Mario mario)
        {
            MarioPowerupStateEventArgs args = new MarioPowerupStateEventArgs(mario);
            OnRaiseMarioPowerupStateEvent(args);
        }

        public void TriggerPowerupCollectedEvent(AbstractPowerup powerup, Mario mario)
        {
            PowerupCollectedEventArgs args = new PowerupCollectedEventArgs(powerup, mario);
            OnRaisePowerupCollectedEvent(args);
        }

        public void TriggerEnemyDefeatedEvent(AbstractEnemy enemy, Mario mario)
        {
            EnemyDefeatedEventArgs args = new EnemyDefeatedEventArgs(enemy, mario);
            OnRaiseEnemyDefeatedEvent(args);
        }

        public void TriggerBadObjectRemovalEvent(AbstractGameObject obj)
        {
            BadObjectRemovalEventArgs args = new BadObjectRemovalEventArgs(obj);
            OnRaiseBadObjectRemovalEvent(args);
        }

        public void TriggerBrickBumpedEvent(AbstractBlock block, PowerUpType powerup, bool broken)
        {
            BrickBumpedEventArgs args = new BrickBumpedEventArgs(block, powerup, broken);
            OnRaiseBrickBumpedEvent(args);
        }

        public void TriggerPlayerWarpingEvent(PipeTop entrance, PipeTop exit, Mario player)
        {
            PlayerWarpingEventArgs args = new PlayerWarpingEventArgs(entrance, exit, player);
            OnRaisePlayerWarpingEvent(args);
        }

		public void TriggerPlayerHitPoleEvent(int height, Mario player)
        {
            PlayerHitPoleEventArgs args = new PlayerHitPoleEventArgs(height, player);
            OnRaisePlayerHitPoleEvent(args);
        }
    }
}
