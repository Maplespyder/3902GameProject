using MarioClone.Sprites;
using MarioClone.States.EnemyStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.States.EnemyStates.Powerup;

namespace MarioClone.GameObjects.Enemies
{
    public abstract class AbstractEnemy : AbstractGameObject
    {
        public AbstractEnemy(ISprite sprite, Vector2 position) : base(sprite, position, Color.Red)
        {}
        public object EnemyState { get; internal set; }
        public object SpriteFactory { get; internal set; }
        public EnemyPowerupState PowerupState { get; internal set; }
    }
}
