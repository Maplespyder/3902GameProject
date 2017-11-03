using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public interface IDraw 
    {
        float DrawOrder { get; }
        bool Visible { get; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
