using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    class CoinBrickObject : IGameObject, IMoveable
    {
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public enum State
        {
            Static,
            Used
        }
        private State state = State.Static;
        private UsedBlockObject UsedBlock;


        public CoinBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (state.Equals(State.Static))
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }
            else if (state.Equals(State.Used))
            {
                UsedBlockObject.Draw(spriteBatch, Position, layer, gameTime);
            }
        }

        public void Move()
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
            //UsedBlock = MarioFactory.Create(BlockType UsedBlock, Position);
            state = State.Used;
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
