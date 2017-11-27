using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Cam
{
	public enum BackgroundType{
		Underworld, 
		Overworld
	}
	public class Background
	{
		private SpriteBatch _spriteBatch;
		private Camera _camera;
		private Dictionary<Texture2D, Vector2> sprites = new Dictionary<Texture2D, Vector2>();

		public Background(SpriteBatch spriteBatch, Camera camera, BackgroundType type)
		{
			_spriteBatch = spriteBatch;
			_camera = camera;
			if(type == BackgroundType.Underworld)
			{
				initializeUnderWorldSprites();
			}
			else
			{
				initializeOverWorldSprites();
			}
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

		private void initializeOverWorldSprites()
		{
			//sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Sky"), new Vector2(.20f));
			sprites.Clear();
            sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Sky"), new Vector2(0f));
            sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BackMountains"), new Vector2(.10f));
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/ForeMountains"), new Vector2(.20f));
            sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Cloud1"), new Vector2(.2f));
            sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Cloud2"), new Vector2(.35f));
            sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BackTrees"), new Vector2(.30f));
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BackBrick"), new Vector2(.5f));
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Mist"), new Vector2(.60f));
		}
		private void initializeUnderWorldSprites()
		{
			sprites.Clear();
			sprites.Add(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/UnderworldBackground"), new Vector2(0f));
		}
	}
}
