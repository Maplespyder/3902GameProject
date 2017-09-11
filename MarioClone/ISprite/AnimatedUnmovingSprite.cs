using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.ISprite
{
    class AnimatedUnmovingSprite : AbstractSprite
    {
        private int frameCounter;
        private int framesUntilNextAnimationFrame;

        public AnimatedUnmovingSprite(Texture2D texture, Vector2 initialPosition, int spriteSheetRows, int spriteSheetColumns)
            : base(texture, spriteSheetRows, spriteSheetColumns, initialPosition, new Vector2(0, 0))
        {
            frameCounter = 0;
            framesUntilNextAnimationFrame = (60 / (spriteSheetRows * spriteSheetColumns)); //60 is the fps i think
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

        public override void ToggleSpriteCommand()
        {
            if (Visible)
            {
                CurrentAnimationFrame = 0;
                frameCounter = 0;
            }
            ToggleVisible();
        }
        #endregion
    }
}
