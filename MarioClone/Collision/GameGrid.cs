using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioClone.Collision
{
    public class GameGrid
    {
        private List<IGameObject>[] gameGrid;
        public int Rows { get; }
        public int Columns { get; }
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }
        public int GridSquareWidth { get; }
        public int GridSquareHeight { get; }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            ScreenWidth = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            ScreenHeight = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;
            GridSquareWidth = ScreenWidth / Columns;
            GridSquareHeight = ScreenHeight / Rows;

            gameGrid = new List<IGameObject>[rows*columns];
            for(int i = 0; i < rows*columns; i++)
            {
                gameGrid[i] = new List<IGameObject>();
            }
        }

        public void ClearGrid()
        {
            foreach(List<IGameObject> li in gameGrid)
            {
                li.Clear();
            }
        }

        private List<int> GetSquaresFromPoint(Point corner, IGameObject obj)
        {
            List<int> squares = new List<int>();
            int xBucket = corner.X / GridSquareWidth;
            int yBucket = (corner.Y / GridSquareHeight) * Columns;

            if(corner.Y == ScreenHeight)
            {
                yBucket -= Columns;
            }
            
            if (corner.X == ScreenWidth)
            {
                xBucket -= 1;
            }
            squares.Add(yBucket + xBucket);

            if((corner.X > 0) && (corner.X < ScreenWidth))
            {
                if((corner.X % GridSquareWidth) == 0)
                {
                    squares.Add((xBucket - 1) + yBucket);
                    if((corner.Y > 0) && (corner.Y < ScreenHeight))
                    {
                        squares.Add((xBucket - 1) + (yBucket - Columns));
                    }
                }
            }

            if((corner.Y > 0) && (corner.Y < ScreenHeight))
            {
                if((corner.Y % GridSquareHeight) == 0)
                {
                    squares.Add(xBucket + (yBucket - Columns));
                }
            }
            return squares;
        }

        public void AddToGrid(IGameObject obj)
        {
            ISet<int> squares = new HashSet<int>();
            squares.UnionWith(GetSquaresFromPoint(obj.Position.ToPoint(), obj));
            //should have a rectangle to easily access all four points
        }
    }
}
