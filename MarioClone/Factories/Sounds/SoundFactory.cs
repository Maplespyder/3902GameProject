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
        BossFireball,
        BossHurt,
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
		Dead,
        Battle,
        Alert,
        Dash

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
                case SoundType.Dash:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Dash");
                case SoundType.Alert:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Alert");
                case SoundType.Stomp:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Puff");
				case SoundType.UP1:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/PowerUp");
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
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Exploration");
				case SoundType.Underworld:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Exploration");
				case SoundType.Dead:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Puff");
				case SoundType.Hurryup:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Hurry");
				case SoundType.Fireball:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Fireball");
                case SoundType.BossFireball:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/BossFireBall");
                case SoundType.BossHurt:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/BossHurt");
                case SoundType.Battle:
                    return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Battle");
                default:
					return MarioCloneGame.GameContent.Load<SoundEffect>("CustomSounds/Coin");
			}
		}
	}
}
