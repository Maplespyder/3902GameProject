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
        public float Volume { get; set; }

	    public SoundEffectInstance MainBackground { get; set; }
	    private SoundEffectInstance SecondaryBackground { get; set; }
	    public float BackgroundPitch { get; set; }

	    private Dictionary<SoundEffectInstance, SoundEffect> PlayingList = new Dictionary<SoundEffectInstance, SoundEffect>();
	    private Dictionary<SoundEffectInstance, SoundEffect> RemoveList = new Dictionary<SoundEffectInstance, SoundEffect>();
        private List<SoundEffectInstance> LoopList = new List<SoundEffectInstance>();
        private List<SoundEffectInstance> RemoveLoopList = new List<SoundEffectInstance>();

        public SoundPool()
		{
            Volume = 1f;
            BackgroundPitch = 0;
			InitializeSoundPool();
			MainBackground = GetAndPlay(SoundType.Background, true);
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
                if (Loop)
                {
                    LoopList.Add(soundEffectInstance);
                }
				return soundEffectInstance;
			}
			return null;
		}

        public void Update()
        {
            foreach (SoundEffectInstance effect in LoopList)
            {
                if(effect.IsDisposed == true)
                {
                    RemoveLoopList.Add(effect);
                }
                else if (effect.State == SoundState.Stopped)
                {
                    effect.Play();
                }
            }
            foreach (SoundEffectInstance effect in RemoveLoopList)
            {
                LoopList.Remove(effect);
            }
            RemoveLoopList.Clear();
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
			MainBackground.Stop();
			MainBackground.Dispose();
			if (SecondaryBackground != null)
			{
				SecondaryBackground.Stop();
				SecondaryBackground.Dispose();
                SecondaryBackground = null;
            }
			PlayingList.Clear();
			PoolList.Clear();
			InitializeSoundPool();
			MainBackground = GetAndPlay(SoundType.Background, true);
            BackgroundPitch = 0;
			MainBackground.Pitch = BackgroundPitch;
		}

		public void PauseBackground()
		{
			MainBackground.Pause();
		}

		public void ResumeBackground()
		{
			if (SecondaryBackground == null)
			{
				MainBackground.Resume();
				MainBackground.Pitch = BackgroundPitch;
			}
		}

		public void ResumeBackgroundStopSecondaryTrack()
		{
			if (SecondaryBackground != null)
			{
				SecondaryBackground.Stop();
				PlayingList.Remove(SecondaryBackground);
				SecondaryBackground.Dispose();
				SecondaryBackground = null;

			}
			MainBackground.Resume();
			MainBackground.Pitch = BackgroundPitch;
		}

		public void ReplaceBackground(SoundType sound)
		{
			MainBackground.Stop();
			MainBackground.Dispose();
			MainBackground = GetAndPlay(sound, true);
			MainBackground.Pitch = BackgroundPitch;
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
