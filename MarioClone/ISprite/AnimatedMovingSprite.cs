using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.ISprite
{
    class AnimatedMovingSprite : AbstractSprite
    {
        private Vector2 initialPosition;
        private Vector2 initialVelocity;

        private int frameCounter;
        private int framesUntilNextAnimationFrame;

        public AnimatedMovingSprite(Texture2D texture, Vector2 initialPosition, Vector2 initialVelocity, int spriteSheetRows, int spriteSheetColumns)
            : base(texture, spriteSheetRows, spriteSheetColumns, initialPosition, initialVelocity)
        {
            this.initialPosition = initialPosition;
            this.initialVelocity = initialVelocity;

            framesUntilNextAnimationFrame = (60 / (spriteSheetRows * spriteSheetColumns)); //60 is the fps i think
            frameCounter = 0;
        }

        public override void ToggleSpriteCommand()
        {
            if (!Visible)
            {
                ProtectedPosition = new Vector2(initialPosition.X, initialPosition.Y);
                CurrentAnimationFrame = 0;
                frameCounter = 0;
            }
            ToggleVisible();
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

                frameCounter++;
                if (frameCounter == framesUntilNextAnimationFrame)
                {
                    CurrentAnimationFrame++;
                    if (CurrentAnimationFrame == (SheetColumns * SheetRows))
                    {
                        CurrentAnimationFrame = 0;
                    }

                    frameCounter = 0;
                }
            }
        }
        #endregion
    }
}
