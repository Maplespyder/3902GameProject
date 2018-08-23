using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.States;

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
			return new SingleLoopAnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Explode"),
						new Rectangle(0, 0, 90, 92), 1, 6, 0, 5, 8);
		}
    }
}
