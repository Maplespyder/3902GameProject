using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class PipeSegment : AbstractBlock
	{
		public PipeSegment(ISprite sprite, Vector2 position) : base(sprite, position)
		{
			BoundingBox.UpdateOffSets(-4, -4, 0, 0);
			DrawOrder = .51f;
		}
	}
}
