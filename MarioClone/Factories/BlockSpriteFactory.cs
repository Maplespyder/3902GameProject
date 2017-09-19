﻿using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum BlockType
    {
        StairBlock,
        UsedBlock,
        QuestionBlock,
        BrickBlock,
        FloorBlock,
        BrokenBlock
    }

    public abstract class BlockSpriteFactory
    {
        protected BlockSpriteFactory() { }

        public abstract Sprite Create(BlockType type, Vector2 location);
    }
}
