using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Sounds;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class CoinObject : AbstractPowerup
    {
        public CoinObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green)
        {
            PointValue = 200;
        }

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
		{
			return base.CollisionResponse(gameObject, side, gameTime);
		}
	}
}
