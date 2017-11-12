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
