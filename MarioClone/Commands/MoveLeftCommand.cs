using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
	public class MoveLeftCommand : AbstractCommand<Mario>
	{
		public MoveLeftCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.MoveLeft();
		}
	}
}
