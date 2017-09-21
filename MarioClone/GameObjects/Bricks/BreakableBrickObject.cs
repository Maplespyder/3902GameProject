using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : BrickObject
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }

        public ISprite Sprite { get; protected set; }

        public BreakableBrickObject(Isprite sprite, Vector2 Velocity, Vector2 Position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;

        }
    }
}
