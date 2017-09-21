using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
	public class StaticBrickObject : BrickObject
    {
        public StaticBrickObject(Isprite sprite, Vector2 position)
        {
            Position = position;
            Sprite = sprite;
        }
    }
}
