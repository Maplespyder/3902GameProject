using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects.Bricks
{
    public class PipeTop : AbstractBlock
    {
        public PipeTop(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-4, -4, 0, 0);
        }
    }
}
