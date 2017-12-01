using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using MarioClone.EventCenter;
using System;
using MarioClone.Projectiles;
using System.Security.Cryptography;

namespace MarioClone.GameObjects
{
    public class FireCannonBlock : AbstractBlock
    {
        CannonFireBallPool fireballPool;
        int currentFireballCooldown;
        int maxFireballCooldown;

        public FireCannonBlock(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            maxFireballCooldown = 0;
            currentFireballCooldown = 0;
            fireballPool = new CannonFireBallPool(1);
            EventManager.Instance.RaiseCannonEvent += ShootFireball;
        }

        private void ShootFireball(object sender, CannonEventArgs e)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] rand = new byte[1];
            rng.GetBytes(rand);
            maxFireballCooldown = ((rand[0] % 6) * 500);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if(maxFireballCooldown > 0)
            {
                currentFireballCooldown += gameTime.ElapsedGameTime.Milliseconds;
                if(currentFireballCooldown >= maxFireballCooldown)
                {
                    fireballPool.GetAndRelease(this);
                    maxFireballCooldown = 0;
                    currentFireballCooldown = 0;
                }
            }

            fireballPool.Update(gameTime);
            return base.Update(gameTime, percent);
        }
    }
}
