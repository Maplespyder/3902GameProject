using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.Sounds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickBreak : BlockState
    {
        new BreakableBrickObject Context;

        public BreakableBrickBreak(BreakableBrickObject context) : base(context)
        {
            Context = context;
            Context.BoundingBox = null;

            EventManager.Instance.TriggerBrickBumpedEvent(Context, Context.ContainedPowerup, true);

			Context.Visible = false;
            List<Vector2> velocityList = new List<Vector2>
            {
                new Vector2(1, 0),
                new Vector2(-1, 0),
                new Vector2(-2, 0),
                new Vector2(2, 0)
            };

            for (int i = 0; i < 4; i++)
            {
                var piece = (BrickPieceObject)BlockFactory.Instance.Create(BlockType.BrickPiece, context.Position);
                Context.PieceList.Add(piece);
                piece.ChangeVelocity(velocityList[i]);
            }
        }

        public override bool Action(float percent, GameTime gameTime)
        {
            List<BrickPieceObject> invisiblePiece = new List<BrickPieceObject>();
            bool disposeMe = false;
            foreach (BrickPieceObject piece in Context.PieceList)
            {
                if (piece.Update(gameTime, percent))
                {
                    invisiblePiece.Add(piece);
                }
            }

            //Remove pieces from PieceList
            foreach (BrickPieceObject piece in invisiblePiece)
            {
                if (Context.PieceList.Contains(piece))
                {
                    Context.PieceList.Remove(piece);
                }
            }

            //If all pieces are gone
            if (Context.PieceList.Count == 0)
            {
                disposeMe = true;
                Context.BoundingBox = new Collision.HitBox(0, 0, 0, 0, Color.Blue);
                Context.BoundingBox.UpdateHitBox(Context.Position, Context.Sprite);
            }

            return disposeMe;
        }
    }
}
