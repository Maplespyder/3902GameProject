 using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MarioClone.Collision;
using MarioClone.EventCenter;

namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : AbstractBlock
	{
		public List<AbstractGameObject> PieceList = new List<AbstractGameObject>();
		public BreakableBrickObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            State = new BreakableBrickStatic(this);
            EventManager.Instance.RaisePlayerWarpingEvent += WarpToNewLocation;
        }

		public override bool Update(GameTime gameTime, float percent)
        {
            bool retVal = State.Action(percent, gameTime);
            if (Visible)
            {
                retVal |= base.Update(gameTime, percent);
            }
			return retVal;
        }

		public override void Draw(SpriteBatch spriteBatch,  GameTime gameTime)
		{
            base.Draw(spriteBatch, gameTime);
			foreach (BrickPieceObject piece in PieceList)
			{
				piece.Draw(spriteBatch,  gameTime);
			}
		}

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario && side == Side.Bottom)
            {
                State.Bump((Mario)gameObject);
                return true;
            }
            return false;
        }

        public void WarpToNewLocation(object sender, PlayerWarpingEventArgs e)
        {
            /*if(e.WarpExit.LevelArea != 0)
            {             
                BlockFactory.SpriteFactory = SubThemedBlockSpriteFactory.Instance;
            }            
            else
            {
                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;
            }*/
        }
    }
}
