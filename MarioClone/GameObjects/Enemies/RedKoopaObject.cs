using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.GameObjects.Enemies;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
    public class RedKoopaObject : AbstractEnemy

    {
        public RedKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-4, -4, -4, -4);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }
    }
}
