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
		private bool maxHeightReached = false;

		//THIS IS A TEMPORARY STATE UNTIL REAL STATES ARE MADE//
		public enum State
		{
			Bounce,
			Break,
			Pieces,
			Static
		}
		private State state = State.Static;

        public BreakableBrickObject(ISprite sprite, Vector2 position, int drawOrder) : base( sprite, position, drawOrder)
        {
            initialPosition = position;
        }

        public override void Break()
        {
            List<Vector2> velocityList = new List<Vector2>
            {
                new Vector2(1, 0),
                new Vector2(-1, 0),
                new Vector2(-2, 0),
                new Vector2(2, 0)
            };

            for (int i = 0; i < 4; i++)
			{
				var piece = (BrickPieceObject)BlockFactory.Instance.Create(BlockType.BrickPiece, Position); 
				PieceList.Add(piece);
				piece.ChangeVelocity(velocityList[i]);
			}
			state = State.Pieces;
        }
		public override void Bounce()
		{
			if (!(Mario.Instance.PowerupState.Powerup == States.MarioPowerupState.MarioPowerup.Normal) && !(state == State.Pieces))
			{
				state = State.Break;
			}
			else if(state == State.Bounce || state == State.Static)
			{
				if(Position.Y > (initialPosition.Y - 10) && !maxHeightReached) //if Position hasnt reached max height
				{
					Position = new Vector2(Position.X, Position.Y - 1f);
					if(Position.Y == (initialPosition.Y - 10))
					{
						maxHeightReached = true;
					}
				}
				else //lower back down to normal height otherwise
				{
					Position = new Vector2(Position.X, Position.Y + 1f);
				}
				state = State.Bounce;
				if (Position.Y == initialPosition.Y) //back to static position
				{
					state = State.Static;
					maxHeightReached = false;
				}
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

		public override void Draw(SpriteBatch spriteBatch,  GameTime gameTime)
		{
			if (state.Equals(State.Static) || state.Equals(State.Bounce)) //draw if bounce or static 
			{
				Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
			}
			else if(state == State.Pieces)
			{
				foreach (BrickPieceObject piece in PieceList)
				{
					piece.Draw(spriteBatch,  gameTime);
				}
			}
		}

        public override void BecomeVisible()
        {
            //do nothing
        }
    }
}
