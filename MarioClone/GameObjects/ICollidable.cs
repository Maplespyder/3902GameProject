using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
	public interface ICollidable
	{ 
		Rectangle BoundingBox { get; }

		int xCoordinate { get; }
		int yCoordinate { get; }
		int offSet { get; }

		void UpdateBoundingBox();
	}
}
