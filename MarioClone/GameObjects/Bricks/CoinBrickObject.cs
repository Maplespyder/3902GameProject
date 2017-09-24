using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;

namespace MarioClone.GameObjects
{
    public class CoinBrickObject : AbstractBlock
    {
        public override void BecomeVisible()
        {
            //do nothing
        }

        public override void Bounce()
        {
            throw new NotImplementedException();
        }

        public override void Break()
        {
            //should transform into used block
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            /*if (state.Equals(State.Static))
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }
            else if (state.Equals(State.Used))
            {
                UsedBlock.Draw(spriteBatch, layer, gameTime);
            }*/
            Sprite.Draw(spriteBatch, Position, layer, gameTime);
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            //For this sprint, the question block only needs to become used 
            BecomeUsed();
        }

        public void BecomeUsed()
        {
            //UsedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, Position);
            //state = State.Used;
        }

        public override bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
