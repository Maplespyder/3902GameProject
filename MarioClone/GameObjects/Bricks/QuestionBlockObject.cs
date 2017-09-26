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
        public QuestionBlockObject(ISprite sprite,  Vector2 position, int drawOrder) : base(sprite,  position, drawOrder)
        {
            State = new QuestionBlockStatic(this);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, DrawOrder, gameTime, Facing.Left);
            }
        }

        public override void Move()
        {
            //Nothing
        }

        public override bool Update(GameTime gameTime)
        {
			return State.Action();
        }

        public override void Bump()
        {
            State.Bump();
        }
    }
}
