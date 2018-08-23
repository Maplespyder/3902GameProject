using MarioClone.Sounds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
	public class MuteCommand : AbstractCommand<SoundPool>
	{
		public MuteCommand(SoundPool receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.MuteCommand();
		}
	}
}
