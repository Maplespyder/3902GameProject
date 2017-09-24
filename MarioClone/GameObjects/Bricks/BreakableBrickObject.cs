using MarioClone.Factories;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : AbstractBlock
	{
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

        public BreakableBrickObject(ISprite sprite, Vector2 velocity, Vector2 position) : base( sprite, velocity, position)
        {
            initialPosition = position;
        }

        public void Execute()
        {
			//TODO:s
			//if small
			//set state to Bounce
			//else big
			//set state to Break
        }

        public override void Break()
        {
			//TODO: Have the Brick cease drawing & create 4 nuggets
			//Create nuggets 
			for(int i = 0; i < 4; i++)
			{
				var piece = BlockFactory.Instance.Create(BlockType.BrickPiece, Position); 
				PieceList.Add(piece);
			}
			state = State.Pieces;
        }
		public override void Bounce()
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

		public override bool Update(GameTime gameTime)
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

		public override void Move()
		{
			throw new NotImplementedException();
		}

		public override void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
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

        public override void BecomeVisible()
        {
            //do nothing
        }
    }
}
