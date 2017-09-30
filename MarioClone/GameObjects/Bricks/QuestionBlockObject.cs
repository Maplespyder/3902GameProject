using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MarioClone.States.BlockStates.BlockState;

namespace MarioClone.GameObjects
{
    public class QuestionBlockObject : AbstractBlock
	{

		public QuestionBlockObject(ISprite sprite,  Vector2 position) : base(sprite,  position)
        {
            State = new QuestionBlockStatic(this);
        }

		public override bool Update(GameTime gameTime)
        {
			BoundingBox.UpdateHitBox(Position, Sprite);
			return State.Action();
        }

        public override void Bump()
        {
            State.Bump();
        }
    }
}
