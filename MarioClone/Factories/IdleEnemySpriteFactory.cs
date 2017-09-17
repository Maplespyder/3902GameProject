using MarioClone.ISprite;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
    public class IdleEnemySpriteFactory : EnemySpriteFactory
    {
        static IdleEnemySpriteFactory _factory;
        public static IdleEnemySpriteFactory Instance
        {
            get
            {
                if(_factory == null)
                {
                    _factory = new IdleEnemySpriteFactory();
                }
                return _factory;
            }
        }

        public override Sprite Create(EnemyType type, Vector2 position)
        {
            List<Rectangle> bounds = new List<Rectangle>();
            bounds.Add(new Rectangle(0, 0, 800, 600));
            switch (type)
            {
                case EnemyType.Goomba:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(0, 0, 32, 32));
                case EnemyType.GreenKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/GreenKoopa"),
                        new Rectangle(64, 0, 32, 57));
                case EnemyType.RedKoopa:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/RedKoopa"),
                        new Rectangle(0, 0, 32, 57));
                default:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Goomba"),
                        new Rectangle(0, 0, 32, 32));
            }
        }
    }
}
