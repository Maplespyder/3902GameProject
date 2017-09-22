using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public enum PowerUpType
    {
        GreenMushroom,
        RedMushroom,
        Flower,
        Star
    }
    public static class PowerUpSpriteFactory
    {
        public static ISprite Create(PowerUpType type)
        {
            switch(type)
            {
                case PowerUpType.GreenMushroom:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(96, 0, 32, 32));
                case PowerUpType.RedMushroom:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(64, 0, 32, 32));
                case PowerUpType.Flower:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(0, 0, 32, 32));
                case PowerUpType.Star:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(32, 0, 32, 32));
                default:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(96, 0, 32, 32));
            }
        }
    }
}
