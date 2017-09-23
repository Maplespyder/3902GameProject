using MarioClone.Factories;
using MarioClone.GameObjects.Bricks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : IGameObject, IMoveable
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

		private List<IGameObject> PieceList = new List<IGameObject>();
		List<BrickPieceObject> InVisiblePieces = new List<BrickPieceObject>();
		private Vector2 initialPosition;

		//THIS IS A TEMPORARY STATE UNTIL REAL STATES ARE MADE//
		public enum State
		{
			Bounce,
			Break,
			Pieces,
			Static
		}
		private State state = State.Static;

		public BreakableBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
			initialPosition = position;
			Visible = true;
        }

        public void Execute()
        {
			//TODO:s
			//if small
			//set state to Bounce
			//else big
			//set state to Break
        }

        public void Break()
        {
			//TODO: Have the Brick cease drawing & create 4 nuggets
			//Create nuggets 
			for(int i = 0; i < 4; i++)
			{
				var piece = MarioFactory.Create(BlockType BrokenBlock, Position); 
				PieceList.Add(piece);
			}
			state = State.Pieces;
        }
		public void Bounce()
		{
			if(Position.Y < (initialPosition.Y + ((initialPosition.Y) / 2f)))
			{
				Position = new Vector2(Position.X, Position.Y + .1f); //Movement speed will need be tested
			}
		}

		public bool Pieces(GameTime gameTime)
		{
            bool disposeMe = false;
			foreach(BrickPieceObject piece in PieceList)
			{
				if (piece.Update(gameTime))
				{
					InVisiblePieces.Add(piece);
				}
			}

			//Remove pieces from PieceList
			foreach(BrickPieceObject piece in InVisiblePieces)
			{
				if (PieceList.Contains(piece))
				{
					PieceList.Remove(piece);
				}
			}

			//If all pieces are gone
			if(PieceList.Count == 0)
			{
                disposeMe = true;
			}

            return disposeMe;
		}

		public bool Update(GameTime gameTime)
        {
            bool disposeMe = false;
			//If state bounce, call bounce()
			if (state.Equals(State.Bounce))
			{
				Bounce();
			}
			else if (state.Equals(State.Break))
			{
				Break();
			}
            else if (state.Equals(State.Pieces))
			{
                if(Pieces(gameTime))
                {
                    disposeMe = true;
                }
			}

            return disposeMe;
        }

		public void Move()
		{
			throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
		{
			if (state.Equals(State.Static) || state.Equals(State.Bounce)) //draw if bounce or static 
			{
				Sprite.Draw(spriteBatch, Position, layer, gameTime);
			}
			else if(state.Equals(State.Pieces)) 
			{
				foreach (BrickPieceObject piece in PieceList)
				{
					piece.Draw(spriteBatch, layer, gameTime);
				}
			}
		}
	}
}
