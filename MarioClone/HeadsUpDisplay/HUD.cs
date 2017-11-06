using MarioClone.Cam;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.HeadsUpDisplay
{
    public class HUD : IDraw
    {
        public Mario Player { get; private set; }
        public ICollection<HUDModule> Modules { get; private set; }

        public float DrawOrder { get; set; }
        public bool Visible { get; set; }
        public float ScreenLeft { get; set; }
        public float ScreenRight { get; set; }
        public float ScreenTop { get; set; }
        public float ScreenBottom { get; set; }

        public HUD(Mario player)
        {
            Player = player;
            Visible = true;
            DrawOrder = 0;

            ScreenLeft = 0;
            ScreenRight = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            ScreenTop = 0;
            ScreenBottom = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;

            Modules = new List<HUDModule>();
            Modules.Add(new PlayerNameModule(this));
            Modules.Add(new PlayerScoreModule(this));
            Modules.Add(new CoinCollectionModule(this));
        }

        public void Update(Camera camera, GameTime gameTime)
        {
            ScreenLeft = camera.Position.X;
            ScreenRight = camera.Position.X + camera.Limits.GetValueOrDefault().Width;
            ScreenTop = camera.Position.Y;
            ScreenBottom = camera.Position.Y + camera.Limits.GetValueOrDefault().Height;

            foreach(HUDModule module in Modules)
            {
                module.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                foreach (HUDModule module in Modules)
                {
                    module.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Dispose()
        {
            foreach (HUDModule module in Modules)
            {
                module.Dispose();
            }
            Modules.Clear();
            Modules = null;
            Player = null;
        }
    }
}
