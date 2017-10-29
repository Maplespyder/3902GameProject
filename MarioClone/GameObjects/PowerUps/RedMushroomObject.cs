using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class RedMushroomObject : AbstractPowerup
	{
        public RedMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }
    }
}
