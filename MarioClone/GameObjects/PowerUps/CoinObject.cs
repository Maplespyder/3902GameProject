using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class CoinObject : AbstractPowerup
    {
        public CoinObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }
    }
}
