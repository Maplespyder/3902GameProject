using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.Factories.Sounds;
using MarioClone.GameObjects;
using MarioClone.States;
using MarioClone.States.EnemyStates;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.Sounds
{
	public class EventSounds
	{

		public EventSounds()
		{
			EventManager.Instance.RaiseMarioPowerupStateEvent += PowerUpStateChangeSound;
			EventManager.Instance.RaiseBrickBumpedEvent += BlockBumpedSound;
			EventManager.Instance.RaiseEnemyDefeatedEvent += EnemyStompSound;
			EventManager.Instance.RaisePowerupCollectedEvent += PowerUpCollectedSound;
			EventManager.Instance.RaiseMarioActionStateEvent += ActionStateChangeSound;
			EventManager.Instance.RaiseRunningOutOfTimeEvent += RunningOutOfTime;
			EventManager.Instance.RaisePlayerWarpingEvent += Warping;
			EventManager.Instance.RaiseFireballFireEvent += FireballFiring;
            EventManager.Instance.RaiseCannonEvent += CannonAlert;
            EventManager.Instance.RaiseTimeRanOutEvent += OutOfTime;
            EventManager.Instance.RaiseEnterBossRoomEvent += EnterBossRoom;
        }

		public void PowerUpStateChangeSound(object sender, MarioPowerupStateEventArgs e)
		{
			if ((e.CurrentPowerupState is MarioInvincibility2))
			{
				SoundPool.Instance.ResumeBackgroundStopSecondaryTrack();
			}
			if (((e.CurrentPowerupState is MarioInvincibility2) && (e.Sender.ActionState is MarioWarp)))
			{
				SoundPool.Instance.GetAndPlay(SoundType.Down, false);
            }
			else if (e.CurrentPowerupState is MarioDead2)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Dead, false);
            }           
			else if (!(e.PreviousPowerupState is MarioInvincibility2))
			{
				SoundPool.Instance.GetAndPlay(SoundType.PowerUp, false);
            }
		}
		public void PowerUpCollectedSound(object sender, PowerupCollectedEventArgs e)
		{
			if (sender is GreenMushroomObject)
			{
                SoundPool.Instance.GetAndPlay(SoundType.UP1, false);
			}
			if (sender is CoinObject)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Coin, false);
            }
		}

		public void FireballFiring(object sender, FireballFireArgs e)
		{
            if(sender is FireBall)
            {
                SoundPool.Instance.GetAndPlay(SoundType.Fireball, false);
            }
            else
            {
                SoundPool.Instance.GetAndPlay(SoundType.BossFireball, false);
            }
        }

		public void ActionStateChangeSound(object sender, MarioActionStateEventArgs e)
		{
			if (e.CurrentActionState is MarioJump2)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Jump, false);
            }

            if (e.CurrentActionState is MarioDash)
            {
                SoundPool.Instance.GetAndPlay(SoundType.Dash, false);
            }
        }

		public void Warping(object sender, PlayerWarpingEventArgs e)
		{
			if(e.WarpExit.LevelArea != 0)
			{
				SoundPool.Instance.ReplaceBackground(SoundType.Underworld);
			}
			else
			{
				SoundPool.Instance.ReplaceBackground(SoundType.Background);
			}
			
		}

		public void RunningOutOfTime(object sender, RunningOutOfTimeArgs e)
		{
			if (e.currentTime > 97)
			{
				SoundPool.Instance.PauseBackground();
				SoundPool.Instance.GetAndPlay(SoundType.Hurryup, false);
            }
			else
			{
				SoundPool.Instance.BackgroundPitch = .3f;
				SoundPool.Instance.ResumeBackground();
			}
		}

        public void OutOfTime(object sender, TimeRanOutEventArgs e)
        {
            SoundPool.Instance.GetAndPlay(SoundType.Hurryup, false);
        }


        public void BlockBumpedSound(object sender, BrickBumpedEventArgs e)
		{
			if (e.BrickBroken)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Break, false);
            }
			else
			{
				if(sender is AbstractBlock)
				{
					AbstractBlock block = (AbstractBlock)sender;
					if (block.ContainedPowerup != PowerUpType.None)
					{
						SoundPool.Instance.GetAndPlay(SoundType.RevealPowerUp, false);
                    }
				}
				SoundPool.Instance.GetAndPlay(SoundType.Bump, false);
            }
		}

        public void CannonAlert(object sender, CannonEventArgs e)
        {
            SoundPool.Instance.GetAndPlay(SoundType.Alert, false);          
        }
        public void EnterBossRoom(object sender, EnterBossRoomEventArgs e)
        {
            if(sender is Mario)
            {
                var mario = (Mario)sender;
                if(mario.Position.X < 19800)
                {
                    SoundPool.Instance.ReplaceBackground(SoundType.Background);
                }
                else
                {
                    SoundPool.Instance.ReplaceBackground(SoundType.Battle);
                }

            }
        }

        public void EnemyStompSound(object sender, EnemyDefeatedEventArgs e)
		{
            if (sender is BowserObject)
            {
                var boss = (BowserObject)sender;
                if (boss.Hits < 1)
                {
                    SoundPool.Instance.GetAndPlay(SoundType.Stomp, false);
                }
                else
                {
                    SoundPool.Instance.GetAndPlay(SoundType.BossHurt, false);
                }
            }
			else if(sender is GoombaObject || sender is PiranhaObject || sender is GreenKoopaObject)
            {
				SoundPool.Instance.GetAndPlay(SoundType.Stomp, false);
            }
		}
	}
}
