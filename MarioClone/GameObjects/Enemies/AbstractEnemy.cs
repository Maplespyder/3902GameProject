using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects.Enemies
{
    public abstract class AbstractEnemy : AbstractGameObject
    {
        public int MaxTimeDead { get { return 250; } }
        public int TimeDead { get; set; }
        public AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red) { }
        public EnemyPowerupState PowerupState { get; internal set; }
    }
}
