using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Command;

namespace MarioClone.ISprite
{
    public interface ISprite
    {
        Texture2D Texture { get; set; }
        Rectangle GetCurrentFrame();
        bool Visible { get; }
        void ToggleVisible();
        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
        void Update();
        void ToggleSpriteCommand();
    }
}
