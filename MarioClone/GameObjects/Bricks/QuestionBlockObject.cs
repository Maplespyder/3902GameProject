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
			Used
		}
		private State state = State.Static;
		private IGameObject UsedBlock;

        public QuestionBlockObject(ISprite sprite, Vector2 velocity, Vector2 position, int drawOrder) : base(sprite, velocity, position, drawOrder)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (state.Equals(State.Static))
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
            return false;
        }

        public override void Bounce()
        {
            BecomeUsed();
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
