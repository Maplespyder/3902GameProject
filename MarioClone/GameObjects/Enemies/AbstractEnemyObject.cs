using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace MarioClone.GameObjects
{
    public abstract class AbstractEnemyClass : IGameObject, IDraw, IMoveable
    {
        public EnemyState State { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }

        public AbstractEnemyObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            State = new EnemyIdle(this);
            Velocity = velocity;
            Position = position;
        }


    }
}

