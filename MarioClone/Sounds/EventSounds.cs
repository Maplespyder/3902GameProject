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
		}

		public void PowerUpStateChangeSound(object sender, MarioPowerupStateEventArgs e)
		{
			if(e.PreviousPowerupState is MarioStar2)
			{
				SoundPool.Instance.ResumeBackgroundStopSecondaryTrack();
			}
			if ((e.CurrentPowerupState is MarioInvincibility2))
			{
				SoundPool.Instance.GetAndPlay(SoundType.Down);
			}
			else if (e.CurrentPowerupState is MarioDead2)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Dead);
			}
            else if(e.CurrentPowerupState is MarioStar2)
			{
				SoundPool.Instance.PauseBackgroundPlaySecondaryTrack(SoundType.Starman);
			}
			else if (!(e.PreviousPowerupState is MarioInvincibility2))
			{
				SoundPool.Instance.GetAndPlay(SoundType.PowerUp);
			}
		}
		public void PowerUpCollectedSound(object sender, PowerupCollectedEventArgs e)
		{
			if (sender is GreenMushroomObject)
			{
				SoundPool.Instance.GetAndPlay(SoundType.UP1);
			}
			if (sender is CoinObject)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Coin);
			}
		}
		public void ActionStateChangeSound(object sender, MarioActionStateEventArgs e)
		{
			if (e.CurrentActionState is MarioJump2)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Jump);
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
			if (e.currentTime > 96)
			{
				SoundPool.Instance.PauseBackground();
				SoundPool.Instance.GetAndPlay(SoundType.Hurryup);
			}
			else
			{
				SoundPool.Instance.backgroundPitch = .3f;
				SoundPool.Instance.ResumeBackground();
			}
		}


		public void BlockBumpedSound(object sender, BrickBumpedEventArgs e)
		{
			if (e.BrickBroken)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Break);
			}
			else
			{
				if(sender is AbstractBlock)
				{
					AbstractBlock block = (AbstractBlock)sender;
					if (block.ContainedPowerup != PowerUpType.None)
					{
						SoundPool.Instance.GetAndPlay(SoundType.RevealPowerUp);
					}
				}
				SoundPool.Instance.GetAndPlay(SoundType.Bump);
			}
		}

		public void EnemyStompSound(object sender, EnemyDefeatedEventArgs e)
		{
			if (sender is GreenKoopaObject)
			{
				GreenKoopaObject enemy = (GreenKoopaObject)sender;
				if (enemy.PowerupState is KoopaAlive)
				{
					SoundPool.Instance.GetAndPlay(SoundType.Stomp);
				}
				else if (enemy.PowerupState is KoopaShell)
				{
					SoundPool.Instance.GetAndPlay(SoundType.Kick);
				}

			}
			else if(sender is RedKoopaObject)
			{
				RedKoopaObject enemy = (RedKoopaObject)sender;
				if (enemy.PowerupState is KoopaAlive)
				{
					SoundPool.Instance.GetAndPlay(SoundType.Stomp);
				}
				else if (enemy.PowerupState is KoopaShell)
				{
					SoundPool.Instance.GetAndPlay(SoundType.Kick);
				}

			}
			else if(sender is GoombaObject)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Stomp);
			}
		}
	}
}
