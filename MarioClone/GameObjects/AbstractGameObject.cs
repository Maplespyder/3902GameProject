using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public enum Facing
    {
        Left,
        Right
    }

    public abstract class AbstractGameObject : IDraw
    {
        public HitBox BoundingBox { get; set; }

        public ISprite Sprite { get; set; }

        public Vector2 Position { get; set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Facing Orientation { get; set; }

        public Vector2 Velocity { get; set; }

        public static bool DrawHitbox { get; set; }

        protected AbstractGameObject(ISprite sprite, Vector2 position, Color hitBoxColor)
        {
            Sprite = sprite;
            Position = position;
            Velocity = new Vector2(0, 0);
            Orientation = Facing.Left;
            Visible = true;
            DrawHitbox = false;
            DrawOrder = 1;
            BoundingBox = new HitBox(0, 0, 0, 0, hitBoxColor);
            if (sprite != null)
            {
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (BoundingBox != null && DrawHitbox)
            {
                BoundingBox.HitBoxDraw(spriteBatch);
            }
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, DrawOrder, gameTime, Orientation);
            }
        }

        public virtual bool Update(GameTime gameTime, float percent)
        {
            if (BoundingBox != null)
            {
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
            return false;
        }

        public static void DisplayHitbox()
        {
            DrawHitbox = !DrawHitbox;
        }

        public virtual void CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {

        }
    }
}
