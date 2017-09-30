using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;
using MarioClone.Collision;

namespace MarioClone.GameObjects
{
    public class GoombaObject : AbstractGameObject
    {
		public GoombaObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red)
        {
			BoundingBox = new HitBox(-4, 0, Color.Red);
        }
    }
}
