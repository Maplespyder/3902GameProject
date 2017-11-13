using MarioClone.Factories.Sounds;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Sounds
{

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
	public SoundEffectInstance mainBackground;
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
			SoundEffect effect = SoundFactory.Instance.Create(sound);
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

		public void Reset()
		{
			mainBackground.Stop();
			mainBackground.Dispose();
			mainBackground = GetAndPlay(SoundType.Background);
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
				PoolList.Add(SoundFactory.Instance.Create(sound));
			}
			PoolList.Add(SoundFactory.Instance.Create(SoundType.Coin));
			PoolList.Add(SoundFactory.Instance.Create(SoundType.Coin));
			//add additional sounds

		}
	}
}
