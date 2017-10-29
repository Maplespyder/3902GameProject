using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
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
        Star,
        Coin,
        None
    }

    public static class PowerUpFactory
    {
        public static AbstractPowerup Create(PowerUpType type, Vector2 position)
        {
            switch (type)
            {
                case PowerUpType.GreenMushroom:
                    return new GreenMushroomObject(PowerUpSpriteFactory.Create(type), position);
                case PowerUpType.RedMushroom:
                    return new RedMushroomObject(PowerUpSpriteFactory.Create(type), position);
                case PowerUpType.Flower:
                    return new FireFlowerObject(PowerUpSpriteFactory.Create(type), position);
                case PowerUpType.Coin:
                    return new CoinObject(PowerUpSpriteFactory.Create(type), position);
                default:
                    return new GreenMushroomObject(PowerUpSpriteFactory.Create(type), position);
            }
        }
    }
}
