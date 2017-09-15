using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Sprite
{
    class UnanimatedMovingSprite : AbstractSprite
    {
        private Vector2 initialPosition;
        private Vector2 initialVelocity;

        public UnanimatedMovingSprite(Texture2D texture, Vector2 initialPosition, Vector2 initialVelocity)
            : base(texture, 1, 1, initialPosition, initialVelocity)
        {
            this.initialPosition = initialPosition;
            this.initialVelocity = initialVelocity;
        }

        #region ISprite
        public override void ToggleVisible()
        {
            Visible = !Visible;
        }

        public override void Update()
        {
            if (Visible)
            {
                ProtectedPosition = new Vector2(ProtectedPosition.X + Velocity.X, ProtectedPosition.Y + Velocity.Y);
            }
        }

        public override void ToggleSpriteCommand()
        {
            if (!Visible)
            {
                ProtectedPosition = initialPosition;
                Velocity = initialVelocity;
            }
            ToggleVisible();
        }
        #endregion
    }
}
