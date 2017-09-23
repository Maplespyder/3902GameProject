using MarioClone.Factories;
using MarioClone.GameObjects.Bricks;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class QuestionBlockObject : IMoveable, IGameObject
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; }

		public ISprite Sprite { get; protected set; }

		//THIS IS A TEMPORARY STATE UNTIL REAL STATES ARE MADE//
		public enum State
		{
			Static,
			Used
		}
		private State state = State.Static;
		private UsedBlockObject UsedBlock;


		public QuestionBlockObject(ISprite sprite, Vector2 velocity, Vector2 position)
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
            }else if (state.Equals(State.Used))
			{
				UsedBlockObject.Draw(spriteBatch, Position, layer, gameTime);
			}
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

        public void Move()
        {
            //Nothing
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
