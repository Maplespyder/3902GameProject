using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.HeadsUpDisplay
{
    public class PlayerNameModule : HUDModule
    {
        SpriteFont nameFont;
        private String playerName;

        public Vector2 RelativePosition { get; set; }
        public Vector2 AbsolutePosition { get; set; }

        public HUD ParentHUD { get; private set; }
        public float DrawOrder { get { return ParentHUD.DrawOrder; } }
        public bool Visible { get; set; }

        public PlayerNameModule(HUD parent)
        {
            ParentHUD = parent;
            Visible = true;
            nameFont = MarioCloneGame.GameContent.Load<SpriteFont>("Fonts/Name");
            playerName = ParentHUD.Player.GetType().Name;

            RelativePosition = new Vector2(10, 50);
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(Visible)
            {
                spriteBatch.DrawString(nameFont, "Player: " + playerName, AbsolutePosition, Color.Red);
            }
        }

        public void Update(GameTime gameTime)
        {
            AbsolutePosition = new Vector2(RelativePosition.X + ParentHUD.ScreenLeft, RelativePosition.Y + ParentHUD.ScreenTop);
        }

        public void Dispose()
        {
            nameFont = null;
            ParentHUD = null;
        }
    }
}
