using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
	public class FireBallCommand : AbstractCommand<Mario>
	{
		public FireBallCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.FireBall();
		}
	}
}
