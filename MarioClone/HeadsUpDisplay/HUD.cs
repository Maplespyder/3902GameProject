using MarioClone.Cam;
using MarioClone.EventCenter;
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

        public bool Underground { get; set; }
        public float DrawOrder { get; set; }
        public bool Visible { get; set; }
        public float ScreenLeft
        {
            get
            {
                return MarioCloneGame.GetCamera.Position.X;
            }
        }
        public float ScreenRight
        { 
            get
            {
                return ScreenLeft + MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            }
        }
        public float ScreenTop
        {
            get
            {
                return MarioCloneGame.GetCamera.Position.Y;
            }
        }
        public float ScreenBottom
        {
            get
            {
                return ScreenTop + MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;
            }
        }

        public HUD(Mario player)
        {
            Player = player;
            Visible = true;
            Underground = false;
            DrawOrder = 0;
            
            Modules = new List<HUDModule>();
            Modules.Add(new PlayerNameModule(this));
            Modules.Add(new PlayerScoreModule(this));
            Modules.Add(new CoinCollectionModule(this));
            Modules.Add(new PlayerLivesModule(this));
            Modules.Add(new TimeModule(this));
            EventManager.Instance.RaisePlayerWarpingEvent += PlayerWarped;
        }

        private void PlayerWarped(object sender, PlayerWarpingEventArgs e)
        {
            if(ReferenceEquals(e.Warper, Player))
            {
                if(e.WarpExit.LevelArea != 0)
                {
                    Underground = true;
                }
                else
                {
                    Underground = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
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
