using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class CoinObject : AbstractPowerup
    {

        public CoinObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario && (side == Side.Top || side == Side.Bottom || side == Side.Right || side == Side.Left))
            {
                isCollided = true;
            }
            return true;

        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (isCollided)
             {
                 return true;
             }
             base.Update(gameTime, percent);
             return false;
           
        }
    }


}
