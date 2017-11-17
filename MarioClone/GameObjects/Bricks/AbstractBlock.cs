using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public abstract class AbstractBlock : AbstractGameObject
    {
        public int CoinCount { get; set; }
        public PowerUpType ContainedPowerup { get; set; }
		public EnemyType ContainedEnemy { get; set; }

		protected AbstractBlock(ISprite sprite, Vector2 position) : base(sprite, position, Color.Blue)
        {
            CoinCount = 0;
            ContainedPowerup = PowerUpType.None;
        }
        
        public BlockState State { get; set; }
    }
}
