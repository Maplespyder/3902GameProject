using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public abstract class AbstractGameObject : IGameObject
    {
        public HitBox BoundingBox { get; protected set; }

        public ISprite Sprite { get; set; }

        public Vector2 Position { get; set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Facing Orientation { get; set; }

        public Vector2 Velocity { get; set; }

        public AbstractGameObject(ISprite sprite, Vector2 position, Color hitBoxColor)
        {
            Sprite = sprite;
            Position = position;
            Velocity = new Vector2(0, 0);
            Orientation = Facing.Left;
            Visible = true;
            DrawOrder = 1;
            BoundingBox = new HitBox(0, 0, 0, 0, hitBoxColor);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, DrawOrder, gameTime, Orientation);
                BoundingBox.HitBoxDraw(spriteBatch);
            }
        }

        public virtual bool Update(GameTime gameTime)
        {
            BoundingBox.UpdateHitBox(Position, Sprite);
            return false;
        }
    }
}
