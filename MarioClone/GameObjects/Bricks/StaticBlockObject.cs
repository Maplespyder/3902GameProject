using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;

namespace MarioClone.GameObjects
{
    public class StaticBlockObject : AbstractBlock
    {
        public StaticBlockObject(ISprite sprite, Vector2 position) : base(sprite, position) { }
    }
}
