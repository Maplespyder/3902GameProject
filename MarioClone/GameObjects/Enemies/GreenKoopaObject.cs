using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
    public class GreenKoopaObject : AbstractGameObject
    {
        public GreenKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red)
		{
			BoundingBox = new HitBox(-4, -4, -4, -4, Color.Red);
		}
    }
}
