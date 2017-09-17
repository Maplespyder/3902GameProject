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
    class Mario : IGameObject, IDrawable, IMoveable
    { 
        public Sprite Sprite { get;  set; }

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get; set; }

        public Rectangle SheetSource { get; protected set; }

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();


         public MarioActionState State { get ; set; }

        
        

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public Mario(MarioActionState actionState, Sprite sprite, Vector2 velocity, Vector2 position)
        {
            State = actionState;
            Velocity = velocity;
        }

        public void Draw(GameTime gameTime)
        {
             if(Visible)
            {
                Sprite.Draw();
            }
            
        }

        public void Move()
        {
            if (Position.X < 100)
            {
                Velocity = new Vector2(10, 0);
            }
            else if (Position.X > 500)
            {
                Velocity = new Vector2(-10, 0);
            }
        }

        public void Update()
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }
    }
}
