using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone
{
    public interface IGameObject : IDrawable
    { 
       State<IGameObject> State { get; set; }
       Sprite Sprite { get;  set; }
       Vector2 Position { get; set; }
       void Update();
       


    


    }
}
