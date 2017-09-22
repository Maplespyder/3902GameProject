using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MarioClone.GameObjects.Bricks;
using MarioClone.Factories;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class BreakableBrickObject : IGameObject, IMoveable, IDraw
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

		private List<IGameObject> PieceList = new List<IGameObject>();

		public BreakableBrickObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
			Visible = true;
        }

        public void HitByMario()
        {
			//if small
			Bump();
			//else big
			Break();
        }

        public void Break()
        {
			//TODO: Have the Brick cease drawing & create 4 nuggets
			//Create nuggets 
			for(int i = 0; i < 4; i++)
			{
				var piece = MarioFactory.Create(BlockType BrokenBlock, Position); 
				PieceList.Add(piece);
			}

        }
		public void Bump()
		{
			//TODO: Move it up and down
		}

		public void Update(GameTime gameTime)
        {
           
        }

		public void Move()
		{
			throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
		{
			if (Visible)
			{
				Sprite.Draw(spriteBatch, Position, layer, gameTime);
			}
		}
	}
}
