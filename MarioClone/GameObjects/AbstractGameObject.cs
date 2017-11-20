using MarioClone.Collision;
using MarioClone.EventCenter;
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
        public virtual HitBox BoundingBox { get; set; }

        public virtual ISprite Sprite { get; set; }

        private Vector2 position;
        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                if(BoundingBox != null)
                {
                    position = CollisionManager.ScreenClamp(value, BoundingBox.Dimensions);
                }
                else if(Sprite != null)
                {
                    position = CollisionManager.ScreenClamp(value, new Rectangle(0, 0, Sprite.SourceRectangle.Width, Sprite.SourceRectangle.Height));
                }
                else
                {
                    position = CollisionManager.ScreenClamp(value, new Rectangle(0, 0, 0, 0));
                }
                if(position != value)
                {
                    //do something later?
                }

				if (Sprite != null && BoundingBox !=null )
				{
					BoundingBox.UpdateHitBox(Position, Sprite);
				}
			}
        }

        protected bool Removed { get; set; }

        public virtual float DrawOrder { get; set; }

        public virtual bool Visible { get; set; }

        public Facing Orientation { get; set; }

        public virtual Vector2 Velocity { get; set; }

        public static bool DrawHitbox { get; set; }

        protected AbstractGameObject(ISprite sprite, Vector2 position, Color hitBoxColor)
        {
			BoundingBox = new HitBox(0, 0, 0, 0, hitBoxColor);
			Sprite = sprite;
            Position = position;
            Velocity = new Vector2(0, 0);
            Orientation = Facing.Right;
            Visible = true;
            Removed = false;
            DrawOrder = .5f;
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

            if (Removed)
            {
                EventManager.Instance.TriggerBadObjectRemovalEvent(this);
                return true;
            }

            return false;
        }

        public static void DisplayHitbox()
        {
            DrawHitbox = !DrawHitbox;
        }

        /// <summary>
        /// returns true if the collision was meaningful/did something that would have to be rechecked
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="side"></param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            return false;
        }

        public virtual void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            //most things don't actually fix any clipping
        }
    }
}
