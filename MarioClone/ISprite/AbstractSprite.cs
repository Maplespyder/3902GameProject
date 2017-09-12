using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.ISprite
{
    public abstract class AbstractSprite : ISprite
    {
        //ertetr
        private Vector2 position;
        private Vector2 velocity;
        private bool visible;
        private Texture2D spriteSheet;

        private int sheetRows;
        private int sheetColumns;
        private int currentAnimationFrame;

        protected AbstractSprite(Texture2D spriteSheet, int sheetRows, int sheetColumns,  Vector2 initialPosition, 
            Vector2 initialVelocity, bool isVisible = false)
        {
            this.spriteSheet = spriteSheet;
            this.sheetRows = sheetRows;
            this.sheetColumns = sheetColumns;
            this.currentAnimationFrame = 0;
            this.position = initialPosition;
            this.velocity = initialVelocity;
            this.visible = isVisible;
        }

        #region Properties
        protected int SheetRows
        {
            get { return sheetRows; }
            set { sheetRows = value; }
        }

        protected int SheetColumns
        {
            get { return sheetColumns; }
            set { sheetColumns = value; }
        }

        protected int CurrentAnimationFrame
        {
            get { return currentAnimationFrame; }
            set { currentAnimationFrame = value; }
        }

        public Texture2D Texture
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        protected Vector2 ProtectedPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(position.X, position.Y); }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool Visible
        {
            get { return visible; }
            protected set { visible = value; }
        }
        #endregion

        #region ISprite
        public Rectangle GetCurrentFrame()
        {
            int width = spriteSheet.Width / sheetColumns;
            int height = spriteSheet.Height / sheetRows;
            int row = currentAnimationFrame / sheetColumns;
            int column = currentAnimationFrame % sheetColumns;

            return new Rectangle(width * column, height * row, width, height);
        }

        public abstract void ToggleVisible();

        public abstract void Update();

        public abstract void ToggleSpriteCommand();

        #endregion
    }
}
