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
    public interface IGameObject : IDraw
    { 
       ISprite Sprite { get; }

       Vector2 Position { get; }

       bool Update(GameTime gameTime);
    }
}
