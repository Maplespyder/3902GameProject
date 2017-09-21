using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public class Mario : IGameObject, IDraw, IMoveable
    {
        public MarioActionState ActionState { get; protected set; }

        public MarioPowerupState PowerupState { get; protected set; }

        public ISprite Sprite
        {
            get
            {
                switch(PowerupState.GetType().Name)
                {
                    case "MarioNormal":
                        return NormalMarioSpriteFactory.Instance.Create(ActionState);
                    case "MarioSuper":
                        return SuperMarioSpriteFactory.Instance.Create(ActionState);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }

        public Mario(Vector2 velocity, Vector2 position)
        {
            ActionState = new MarioIdle(this);
            PowerupState = new MarioNormal(this);
            Velocity = velocity;
            Position = position;
        }

        // action state methods, will likely be linked to commands
        public void BecomeDead()
        {
            ActionState.BecomeDead();
        }

        // powerup state methods, will likely be linked to commands
        public void BecomeNormal()
        {
            PowerupState.BecomeNormal();
        }

        public void BecomeSuper()
        {
            PowerupState.BecomeSuper();
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }           
        }
    }
}