using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public abstract class AbstractBlock : IGameObject, IMoveable
    {

        protected AbstractBlock(ISprite sprite, Vector2 position, int drawOrder)
        {
            Sprite = sprite;
            Velocity = new Vector2(0,0);
            Position = position;
            Visible = true;
            DrawOrder = drawOrder;
        }
		public void UpdateBoundingBox()
		{
			BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.SourceRectangle.Width,
				Sprite.SourceRectangle.Height);
		}

		public abstract void Bump();

        public BlockState State { get; set; }
		public Rectangle BoundingBox { get; protected set; }

		public Vector2 Position { get; set; }

        public Vector2 Velocity { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public abstract void Draw(SpriteBatch spriteBatch,  GameTime gameTime);

        public abstract bool Update(GameTime gameTime);
    }
}
