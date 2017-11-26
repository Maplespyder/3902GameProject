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
    public static class PowerUpSpriteFactory
    {
        public static ISprite Create(PowerUpType type)
        {
            switch(type)
            {
                case PowerUpType.GreenMushroom:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(192, 0, 64, 64));
                case PowerUpType.RedMushroom:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Berry"), new Rectangle(0, 0, 64, 64));
                case PowerUpType.Flower:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Meat"), new Rectangle(0, 0, 64, 52));
                case PowerUpType.Coin:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Coin"), new Rectangle(0, 0, 64, 64), 1, 4, 0, 3, 4);
                default:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet"), new Rectangle(192, 0, 64, 64));
            }
        }
    }
}
