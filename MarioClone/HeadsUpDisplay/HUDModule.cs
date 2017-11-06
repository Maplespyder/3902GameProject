using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.HeadsUpDisplay
{
    public interface HUDModule : IDraw
    {
        Vector2 RelativePosition { get; set; }
        Vector2 AbsolutePosition { get; set; }

        HUD ParentHUD { get; }

        void Update(GameTime gameTime);
        void Dispose();
    }
}
