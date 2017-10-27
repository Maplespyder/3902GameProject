using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Cam
{
	public class Background
	{
		private SpriteBatch _spriteBatch;
		private Camera _camera;
		private Dictionary<Texture2D, Vector2> sprites = new Dictionary<Texture2D, Vector2>();
		private GraphicsDeviceManager _graphics;

		public Background(SpriteBatch spriteBatch, Camera camera, GraphicsDeviceManager graphics)
		{
			_spriteBatch = spriteBatch;
			_camera = camera;
			_graphics = graphics;
			initializeSprites();
			//add sprites here
		}
		public void Draw()
		{
			foreach (KeyValuePair<Texture2D, Vector2> item in sprites)
			{
				_spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.LinearWrap, null, null, null, _camera.GetViewMatrix(item.Value));
				_spriteBatch.Draw(item.Key, Vector2.Zero, new Rectangle((int)(_camera.Position.X * 0.5f), 
					(int)(_camera.Position.Y * 0.5f), _camera.Limits.Value.Width, item.Key.Height), Color.White);
				_spriteBatch.End();
			}
		}

		private void initializeSprites()
		{
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/BackMountains"), new Vector2(.30f));
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/ForeMountains"), new Vector2(.40f));
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/clouds"), new Vector2(.60f));
	
		}
	}
}
