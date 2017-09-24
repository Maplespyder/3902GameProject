﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    class CoinObject : IGameObject, IMoveable
    {
        public ISprite Sprite { get; set; }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime);
            }
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}