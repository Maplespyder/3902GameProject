using MarioClone.Collision;
using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public abstract class AbstractBlock : AbstractGameObject
    {

        protected AbstractBlock(ISprite sprite, Vector2 position) : base(sprite, position, Color.Blue) { }

		public virtual void Bump()
        {
        }

        public BlockState State { get; set; }
    }
}
