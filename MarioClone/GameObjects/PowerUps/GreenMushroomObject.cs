using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class GreenMushroomObject : AbstractGameObject
	{
        public GreenMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }
    }
}
