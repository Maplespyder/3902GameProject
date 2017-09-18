﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public interface IDraw : IGameObject
    {
        int DrawOrder { get; }
        bool Visible { get; }

        void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime);
    }
}
