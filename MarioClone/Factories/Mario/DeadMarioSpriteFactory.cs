using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MarioClone.States.MarioActionState;

namespace MarioClone.Factories
{
    public class DeadMarioSpriteFactory : MarioSpriteFactory
    {
        static DeadMarioSpriteFactory _instance;

        public static DeadMarioSpriteFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DeadMarioSpriteFactory();
                }
                return _instance;
            }
        }

        public override ISprite Create(MarioAction action)
        {
            return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SmallMario"), new Rectangle(192, 0, 32, 32));
        }
    }
}
