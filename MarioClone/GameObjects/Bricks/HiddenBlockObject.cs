using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;
using MarioClone.Factories;

namespace MarioClone.GameObjects
{
    public class HiddenBrickObject : AbstractBlock
    {
        private AbstractBlock revealedBrick = null;

        #region Property Overrides
        public override HitBox BoundingBox
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.BoundingBox;
                }
                else
                {
                    return revealedBrick.BoundingBox;
                }
            }
            set
            {
                if(revealedBrick == null)
                {
                    base.BoundingBox = value;
                }
                else
                {
                    revealedBrick.BoundingBox = value;
                }
            }
        }

        public override ISprite Sprite
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.Sprite;
                }
                else
                {
                    return revealedBrick.Sprite;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.Sprite = value;
                }
                else
                {
                    revealedBrick.Sprite = value;
                }
            }
        }

        public override Vector2 Position
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.Position;
                }
                else
                {
                    return revealedBrick.Position;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.Position = value;
                }
                else
                {
                    revealedBrick.Position = value;
                }
            }
        }

        public override float DrawOrder
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.DrawOrder;
                }
                else
                {
                    return revealedBrick.DrawOrder;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.DrawOrder = value;
                }
                else
                {
                    revealedBrick.DrawOrder = value;
                }
            }
        }

        public override bool Visible
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.Visible;
                }
                else
                {
                    return revealedBrick.Visible;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.Visible = value;
                }
                else
                {
                    revealedBrick.Visible = value;
                }
            }
        }
        
        public override Vector2 Velocity
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.Velocity;
                }
                else
                {
                    return revealedBrick.Velocity;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.Velocity = value;
                }
                else
                {
                    revealedBrick.Velocity = value;
                }
            }
        }

        public new int CoinCount
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.CoinCount;
                }
                else
                {
                    return revealedBrick.CoinCount;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.CoinCount = value;
                }
                else
                {
                    revealedBrick.CoinCount = value;
                }
            }
        }

        public new PowerUpType ContainedPowerup
        {
            get
            {
                if (revealedBrick == null)
                {
                    return base.ContainedPowerup;
                }
                else
                {
                    return revealedBrick.ContainedPowerup;
                }
            }
            set
            {
                if (revealedBrick == null)
                {
                    base.ContainedPowerup = value;
                }
                else
                {
                    revealedBrick.ContainedPowerup = value;
                }
            }
        }
        #endregion


        public HiddenBrickObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Visible = false;
        }
        
        public override void Bump()
        {
            Visible = true;
            revealedBrick = BlockFactory.Instance.Create(BlockType.BreakableBrick, Position);
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if(revealedBrick != null)
            {
                return revealedBrick.CollisionResponse(gameObject, side, gameTime);
            }

            if (gameObject is Mario && side == Side.Bottom)
            {
                Bump();
                return true;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (revealedBrick == null)
            {
                base.Draw(spriteBatch, gameTime);
            }
            else
            {
                revealedBrick.Draw(spriteBatch, gameTime);
            }
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (revealedBrick == null)
            {
                return base.Update(gameTime, percent);
            }

            if (revealedBrick.Update(gameTime, percent))
            {
                revealedBrick = null;
                return true;
            }
            return false;
        }
    }
}
