using MarioClone.Factories;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public class QuestionBlockObject : AbstractBlock
	{
		//THIS IS A TEMPORARY STATE UNTIL REAL STATES ARE MADE//
		public enum State
		{
			Static,
			Used,
			Bounce
		}
		private State state = State.Static;
		private IGameObject UsedBlock;
		private bool maxHeightReached = false;
		private Vector2 initialPosition;


		public QuestionBlockObject(ISprite sprite,  Vector2 position, int drawOrder) : base(sprite,  position, drawOrder)
        {
			initialPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!(state.Equals(State.Used)))
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
            }else if (state.Equals(State.Used))
			{
				UsedBlock.Draw(spriteBatch, gameTime);
			}
        }

		public void BecomeUsed()
		{
			UsedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, Position);
			state = State.Used;
		}

        public override void Move()
        {
            //Nothing
        }

        public override bool Update(GameTime gameTime)
        {
			if(state == State.Bounce)
			{
				Bounce();
			}
			return false;
        }

        public override void Bounce()
        {
			if (state == State.Static || state == State.Bounce)
			{
				if (Position.Y > (initialPosition.Y - 10) && !maxHeightReached) //if Position hasnt reached max height
				{
					Position = new Vector2(Position.X, Position.Y - 1f);
					if (Position.Y == (initialPosition.Y - 10))
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
					BecomeUsed();
					state = State.Used;
					maxHeightReached = false;
				}
			}
        }

        public override void Break()
        {
            //do nothing
        }

        public override void BecomeVisible()
        {
            //do nothing
        }
    }
}
