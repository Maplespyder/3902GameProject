using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : IGameObject
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }

        public ISprite Sprite { get; protected set; }

        public BreakableBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;

        }

        public void BrickBump()
        {

        }

        public void BrickBreak()
        {
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
