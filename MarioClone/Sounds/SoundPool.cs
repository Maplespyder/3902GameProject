using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Sounds
{
	public enum SoundType
	{
		Background,
		Stomp,
		Bump,
		UP1,
		DOWN1,
		RevealPowerUp,
		PowerUp,
		Down,
		Jump,
		Coin,
		Kick,
		Break
		
		//more to come
	}

	public class SoundPool
	{
		private static SoundPool _soundPool;
		public static SoundPool Instance
		{
			get
			{
				if (_soundPool == null)
				{
					_soundPool = new SoundPool();
				}
				return _soundPool;
			}
		}

	private List<SoundEffect> PoolList = new List<SoundEffect>();
	public bool Muted { get; set; }
	SoundEffectInstance mainBackground;
	private Dictionary<SoundEffectInstance, SoundEffect> UnPoolList = new Dictionary<SoundEffectInstance, SoundEffect>();
	private Dictionary<SoundEffectInstance, SoundEffect> RemoveList = new Dictionary<SoundEffectInstance, SoundEffect>();

		public SoundPool()
		{
			InitializeSoundPool();
			mainBackground = GetAndPlay(SoundType.Background);
			Muted = false;
		}
		public SoundEffectInstance GetAndPlay(SoundType sound)
		{
			CheckAvailability();
			SoundEffect effect = SoundFactory(sound);
			if (PoolList.Contains(effect) && !Muted)
			{
				PoolList.Remove(effect);
				SoundEffectInstance soundEffectInstance = effect.CreateInstance();
				UnPoolList.Add(soundEffectInstance, effect);
				if (!Muted)
				{
					soundEffectInstance.Play();
				}
				return soundEffectInstance;
			}
			return null;
		}

		public void CheckAvailability()
		{
			foreach(KeyValuePair<SoundEffectInstance, SoundEffect> effect in UnPoolList)
			{
				if(effect.Key.State != SoundState.Playing)
				{
					RemoveList.Add(effect.Key, effect.Value);
				}
			}
			foreach(KeyValuePair<SoundEffectInstance, SoundEffect> effect in RemoveList)
			{
				PoolList.Add(effect.Value);
				UnPoolList.Remove(effect.Key);
			}
			RemoveList.Clear();
		}

		public void MuteCommand()
		{
			Muted = !Muted;
			if (Muted)
			{
				mainBackground.Pause();
			}
			else
			{
				mainBackground.Resume();
			}

		}
		public void AddObject(SoundEffect sound)
		{
			if (!PoolList.Contains(sound))
			{
				PoolList.Add(sound);
			}
		}
		private void InitializeSoundPool()
		{
			//Load in all sound effects via Factory we want 3 times max
			foreach(SoundType sound in Enum.GetValues(typeof(SoundType))){
				PoolList.Add(SoundFactory(sound));
			}
			PoolList.Add(SoundFactory(SoundType.Coin));
			PoolList.Add(SoundFactory(SoundType.Coin));
			//add additional sounds

		}
		public SoundEffect SoundFactory(SoundType sound)
		{
			switch (sound)
			{
				case SoundType.Coin:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Coin");
				case SoundType.Stomp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Stomp");
				case SoundType.UP1:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/1UP");
				case SoundType.DOWN1:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/1DOWN");
				case SoundType.Bump:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Bump");
				case SoundType.PowerUp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/PowerUp");
				case SoundType.Kick:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Kick");
				case SoundType.Break:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Break");
				case SoundType.Jump:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Jump");
				case SoundType.Down:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Down");
				case SoundType.RevealPowerUp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/RevealPowerUp");
				case SoundType.Background:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Overworld");

				default:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Coin");
			}
		}
	}
}
