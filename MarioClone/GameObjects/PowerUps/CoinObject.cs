using MarioClone.Collision;
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
			if (gameObject is Mario)
			{
				SoundPool.Instance.GetAndPlay(SoundType.Coin);
				isCollided = true;
			}
			return State.CollisionResponse(gameObject) || isCollided;
		}
	}
}
