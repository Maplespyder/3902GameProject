using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
	public class MoveRightCommand : AbstractCommand<Mario>
	{
		public MoveRightCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
            if (MarioCloneGame.State == GameState.Playing)
            {
                Receiver.MoveRight(); 
            }
		}
	}
}
