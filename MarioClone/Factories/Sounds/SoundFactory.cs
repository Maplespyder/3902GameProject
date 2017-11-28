using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories.Sounds
{
	public enum SoundType
	{
		Background,
		Hurryup,
		Underworld,
		Fireball,
		CourseClear,
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
		Break, 
		Dead

		//more to come
	}
	public class SoundFactory
	{

		private static SoundFactory _factory;
		public static SoundFactory Instance
		{
			get
			{
				if (_factory == null)
				{
					_factory = new SoundFactory();
				}
				return _factory;
			}
		}

		public SoundEffect Create(SoundType sound)
		{
			switch (sound)
			{
				case SoundType.Coin:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Coin");
				case SoundType.Stomp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Puff");
				case SoundType.UP1:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/1UP");
				case SoundType.DOWN1:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Hurt");
				case SoundType.Bump:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/MetalHit");
				case SoundType.PowerUp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/PowerUp");
				case SoundType.Kick:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Kick");
				case SoundType.Break:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Crumble");
				case SoundType.Jump:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Hop");
				case SoundType.Down:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Hurt");
                case SoundType.RevealPowerUp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Reveal");
				case SoundType.Background:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Underground");
				case SoundType.Underworld:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Underground");
				case SoundType.Dead:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Puff");
				case SoundType.Hurryup:
					return MarioCloneGame.GameContent.Load<SoundEffect>("SoundEffects/Hurryup");
				case SoundType.Fireball:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Fireball");
				default:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Coin");
			}
		}
	}
}
