using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
	public class BrickObject : IGameObject, IDraw

	{
        Vector2 Position { get; }
        int DrawOrder { get; }
        bool Visible { get; }

        public BrickObject(Isprite sprite, Vector2 position)
        {
            Position = position;
            Sprite = sprite;
        }

        void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, layer, gameTime);
        }
    }
}
