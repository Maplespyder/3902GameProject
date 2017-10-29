using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects
{
    public abstract class AbstractEnemy : AbstractGameObject
    {
        public static int MaxTimeDead { get { return 250; } }
        public static int MaxTimeShell {  get { return 1000;  } }
        public int TimeDead { get; set; }
        protected AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red) { }
        public EnemyPowerupState PowerupState { get; internal set; }
    }
}
