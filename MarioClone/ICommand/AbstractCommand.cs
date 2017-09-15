using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Command
{
    /// <summary>
    /// Abstract class that provides a default constructor, and an 
    /// abstract version of the InvokeCommand ICommand interface method.
    /// </summary>
	public abstract class AbstractCommand<TReceiver> : ICommand
	{
		private TReceiver receiver;
		protected AbstractCommand(TReceiver receiver)
		{
			this.receiver = receiver;
		}

        protected TReceiver Receiver
        {
            get { return receiver; }
        }

		public abstract void InvokeCommand();
	}
}
