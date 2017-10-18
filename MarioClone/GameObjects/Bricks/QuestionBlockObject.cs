using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class QuestionBlockObject : AbstractBlock
	{

		public QuestionBlockObject(ISprite sprite,  Vector2 position) : base(sprite,  position)
        {
            State = new QuestionBlockStatic(this);
        }

		public override bool Update(GameTime gameTime, float percent)
        {
			BoundingBox.UpdateHitBox(Position, Sprite);
			return State.Action(percent);
        }

        public override void Bump()
        {
            State.Bump();
        }

        public override void CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            //if (gameObject is Mario && side == Side.Bottom)
            //{
            //    State.Bump();
            //}
        }
    }
}
