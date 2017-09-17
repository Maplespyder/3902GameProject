using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public interface IGameObject
    { 
       State<IGameObject> State { get; }
       ISprite Sprite { get; }
       Vector2 Position { get; }
       void Update();
    }
}
