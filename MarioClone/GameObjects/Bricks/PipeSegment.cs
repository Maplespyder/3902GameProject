using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects.Bricks 
{
	public class PipeSegment : AbstractBlock
	{
		public PipeSegment(ISprite sprite, Vector2 position) : base(sprite, position)
		{
			BoundingBox.UpdateOffSets(-4, -4, 0, 0);
		}
	}
}
