using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class PipeTop : AbstractBlock
    {
        public PipeTop WarpEnd { get; set; }
        public int LevelArea { get; set; }

        public PipeTop(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-4, -4, 0, 0);
            WarpEnd = null;
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if(gameObject is Mario && side == Side.Top)
            {
                if(((Mario)gameObject).ActionState.Action == States.MarioAction.Crouch && WarpEnd != null)
                {
                    EventManager.Instance.TriggerPlayerWarpingEvent(this, WarpEnd, (Mario)gameObject);
                }
            }
            return base.CollisionResponse(gameObject, side, gameTime);
        }
    }
}
