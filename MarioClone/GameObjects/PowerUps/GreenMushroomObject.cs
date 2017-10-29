using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects
{
    public class GreenMushroomObject : AbstractPowerup
	{
        public GreenMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }
    }
}
