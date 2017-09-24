using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public static class MarioFactory
    {
        public static Mario Create(Vector2 position)
        {
            if (Mario.Instance == null)
            {
                return new Mario(new Vector2(0, 0), position);
            }
            return Mario.Instance;
        }
    }
}
