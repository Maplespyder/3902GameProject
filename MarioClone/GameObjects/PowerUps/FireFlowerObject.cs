using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects
{
    public class FireFlowerObject : AbstractPowerup
	{
        public FireFlowerObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green)
        {
            PointValue = 2000;
            BoundingBox.UpdateOffSets(-1, -1, 0, 0);
            BoundingBox.UpdateHitBox(Position, Sprite);
			DrawOrder = .51f;
		}
    }
}
