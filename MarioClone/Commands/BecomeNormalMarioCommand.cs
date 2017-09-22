﻿using MarioClone.GameObjects;

namespace MarioClone.Commands
{
    public class BecomeNormalMarioCommand : AbstractCommand<Mario>
	{
		
		public BecomeNormalMarioCommand(Mario receiver) : base(receiver) { }

		public override void InvokeCommand()
		{
			Receiver.BecomeNormal();
		}
	}
}
