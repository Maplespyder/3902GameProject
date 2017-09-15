using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Sprite
{
    class MotionlessSprite : AbstractSprite
    {
        /// <summary>
        /// Texture is the sprite sheet that the sprite will pull it's animation
        /// images from, and rows and columns are the number of rows and columns
        /// of sprites on the sprite sheet
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="initialPosition"></param>
        /// <param name="sheetRows"></param>
        /// <param name="sheetColumns"></param>
        public MotionlessSprite(Texture2D texture, Vector2 initialPosition) : base(texture, 1, 1, initialPosition, new Vector2(0, 0)) { }

        #region AbstractSprite

        public override void ToggleVisible()
        {
            Visible = !Visible;
        }

        public override void Update()
        {
            //do nothing
        }

        public override void ToggleSpriteCommand()
        {
            ToggleVisible();
        }
        #endregion
    }
}