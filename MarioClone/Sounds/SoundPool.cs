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
    public float Volume = 1f;

	private SoundEffectInstance mainBackground;
	private SoundEffectInstance secondaryBackground;
	public float BackgroundPitch { get; set; }

	private Dictionary<SoundEffectInstance, SoundEffect> PlayingList = new Dictionary<SoundEffectInstance, SoundEffect>();
	private Dictionary<SoundEffectInstance, SoundEffect> RemoveList = new Dictionary<SoundEffectInstance, SoundEffect>();

		public SoundPool()
		{
            BackgroundPitch = 0;
			InitializeSoundPool();
			mainBackground = GetAndPlay(SoundType.Background, true);
		}
		public SoundEffectInstance GetAndPlay(SoundType sound, bool Loop)
		{
			CheckAvailability();
			SoundEffect effect = SoundFactory.Instance.Create(sound);
			if (PoolList.Contains(effect))
			{
				PoolList.Remove(effect);
				SoundEffectInstance soundEffectInstance = effect.CreateInstance();
				PlayingList.Add(soundEffectInstance, effect);
				soundEffectInstance.Play();
                soundEffectInstance.Volume = Volume;
                soundEffectInstance.IsLooped = Loop;
				return soundEffectInstance;
			}
			return null;
		}


        public void CheckAvailability()
		{
			foreach(KeyValuePair<SoundEffectInstance, SoundEffect> effect in PlayingList)
			{
				if(effect.Key.State == SoundState.Stopped)
				{
					RemoveList.Add(effect.Key, effect.Value);
				}
			}
			foreach(KeyValuePair<SoundEffectInstance, SoundEffect> effect in RemoveList)
			{
				PoolList.Add(effect.Value);
				PlayingList.Remove(effect.Key);
			}
			RemoveList.Clear();
		}

		public void MuteCommand()
		{
            Volume = 1 - Volume;
            foreach (SoundEffectInstance effect in PlayingList.Keys)
            {
                effect.Volume = Volume;
            }

    }

		public void Reset()
		{
			mainBackground.Stop();
			mainBackground.Dispose();
			if (secondaryBackground != null)
			{
				secondaryBackground.Stop();
				secondaryBackground.Dispose();
                secondaryBackground = null;
            }
			PlayingList.Clear();
			PoolList.Clear();
			InitializeSoundPool();
			mainBackground = GetAndPlay(SoundType.Background, true);
            BackgroundPitch = 0;
			mainBackground.Pitch = BackgroundPitch;
		}
		public void AddObject(SoundEffect sound)
		{
			if (!PoolList.Contains(sound))
			{
				PoolList.Add(sound);
			}
		}

		public void PauseBackground()
		{
			mainBackground.Pause();
		}

		public void ResumeBackground()
		{
			if (secondaryBackground == null)
			{
				mainBackground.Resume();
				mainBackground.Pitch = BackgroundPitch;
			}
		}

		public void PauseBackgroundPlaySecondaryTrack(SoundType newSound)
		{
			if (secondaryBackground == null)
			{
				mainBackground.Pause();
				secondaryBackground = GetAndPlay(newSound, false);
			}
		}

		public void ResumeBackgroundStopSecondaryTrack()
		{
			if (secondaryBackground != null)
			{
				secondaryBackground.Stop();
				PlayingList.Remove(secondaryBackground);
				secondaryBackground.Dispose();
				secondaryBackground = null;

			}
			mainBackground.Resume();
			mainBackground.Pitch = BackgroundPitch;
		}

		public void ReplaceBackground(SoundType sound)
		{
			mainBackground.Stop();
			mainBackground.Dispose();
			mainBackground = GetAndPlay(sound, true);
			mainBackground.Pitch = BackgroundPitch;
		}

		private void InitializeSoundPool()
		{
			PoolList.Clear();
			//Load in all sound effects via Factory we want 3 times max
			foreach(SoundType sound in Enum.GetValues(typeof(SoundType))){
				SoundEffect effect = SoundFactory.Instance.Create(sound);
				PoolList.Add(effect);
			}
			PoolList.Add(SoundFactory.Instance.Create(SoundType.Coin));
			PoolList.Add(SoundFactory.Instance.Create(SoundType.Coin));
			PoolList.Add(SoundFactory.Instance.Create(SoundType.Fireball));
			//add additional sounds

		}
	}
}
