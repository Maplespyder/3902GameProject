using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MarioClone.GameObjects
{
    public class QuestionBrickObject : BrickObject, IMoveable, IGameObject
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        int DrawOrder { get; }

        bool Visible { get; }


        void Draw(bool visible, SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (visible)
            {
                sprite.Draw(spritebatch, position, layer, gametime);
            }
        }

        public void move()
        {
            throw new NotImplementedException();
        }

        public void Update(Gametime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
