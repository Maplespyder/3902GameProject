using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MarioClone.Factories
{
    public static class PowerUpFactory
    {
        public static IGameObject Create(PowerUpType type, Vector2 position)
        {
            switch (type)
            {
                case PowerUpType.GreenMushroom:
                    return new GreenMushroomObject(PowerUpSpriteFactory.Create(type), position);
                case PowerUpType.RedMushroom:
                    return new RedMushroomObject(PowerUpSpriteFactory.Create(type), position);
                case PowerUpType.Flower:
                    return new FirePowerUpObject(PowerUpSpriteFactory.Create(type), position);
                default:
                    return new GreenMushroomObject(PowerUpSpriteFactory.Create(type), position);
            }
        }
    }
}
