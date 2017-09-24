using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
	public class BreakableBrickBumpCommand : AbstractCommand<BreakableBrickObject>
	{
		public BreakableBrickBumpCommand(BreakableBrickObject receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.HitByMario();
		}
	}
}
