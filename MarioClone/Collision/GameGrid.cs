using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarioClone.Collision
{
    public class GameGrid
    {
        private List<ICollidable>[,] gameGrid;
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

            gameGrid = new List<ICollidable>[Rows, Columns];
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[i, j] = new List<ICollidable>();
                }
            }
        }

        public void ClearGrid()
        {
            foreach(List<ICollidable> li in gameGrid)
            {
                li.Clear();
            }
        }

        private List<Point> GetSquaresFromPoint(Point corner)
        {
            List<Point> squares = new List<Point>();
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
            squares.Add(new Point(yBucket, xBucket));

            if((corner.X > 0) && (corner.X < ScreenWidth))
            {
                if((corner.X % GridSquareWidth) == 0)
                {
                    squares.Add(new Point(xBucket - 1, yBucket));
                    if((corner.Y > 0) && (corner.Y < ScreenHeight))
                    {
                        squares.Add(new Point(xBucket - 1, yBucket - Columns));
                    }
                }
            }

            if((corner.Y > 0) && (corner.Y < ScreenHeight))
            {
                if((corner.Y % GridSquareHeight) == 0)
                {
                    squares.Add(new Point(xBucket, yBucket - Columns));
                }
            }
            return squares;
        }

        public void AddToGrid(ICollidable obj)
        {
            ISet<Point> squares = new HashSet<Point>();
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Left, obj.BoundingBox.Dimensions.Top)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Right, obj.BoundingBox.Dimensions.Top)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Left, obj.BoundingBox.Dimensions.Bottom)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Right, obj.BoundingBox.Dimensions.Bottom)));

            //should have a rectangle to easily access all four points
            foreach(Point bucket in squares)
            {
                gameGrid[bucket.X, bucket.Y].Add(obj);
            }
        }

        private ISet<ICollidable> GetNeighbours(Point square)
        {
            ISet<ICollidable> neighbours = new HashSet<ICollidable>();
            neighbours.UnionWith(gameGrid[square.X, square.Y]);
            
            if(square.X > 0)
            {
                neighbours.UnionWith(gameGrid[square.X - 1, square.Y]);
                if(square.Y > 0)
                {
                    neighbours.UnionWith(gameGrid[square.X - 1, square.Y - 1]);
                }
                if(square.Y < (Rows - 1))
                {
                    neighbours.UnionWith(gameGrid[square.X - 1, square.Y + 1]);
                }
            }
            if(square.X < (Columns - 1))
            {
                neighbours.UnionWith(gameGrid[square.X + 1, square.Y]);
                if (square.Y > 0)
                {
                    neighbours.UnionWith(gameGrid[square.X + 1, square.Y - 1]);
                }
                if (square.Y < (Rows - 1))
                {
                    neighbours.UnionWith(gameGrid[square.X + 1, square.Y + 1]);
                }
            }
            if(square.Y > 0)
            {
                neighbours.UnionWith(gameGrid[square.X, square.Y - 1]);
            }
            if(square.Y < (Rows - 1))
            {
                neighbours.UnionWith(gameGrid[square.X, square.Y + 1]);
            }

            return neighbours;
        }

        public List<ICollidable> FindNeighbours(ICollidable obj)
        {
            ISet<ICollidable> neighbours = new HashSet<ICollidable>();

            ISet<Point> squares = new HashSet<Point>();
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Left, obj.BoundingBox.Dimensions.Top)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Right, obj.BoundingBox.Dimensions.Top)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Left, obj.BoundingBox.Dimensions.Bottom)));
            squares.UnionWith(GetSquaresFromPoint(new Point(obj.BoundingBox.Dimensions.Right, obj.BoundingBox.Dimensions.Bottom)));

            foreach(Point pt in squares)
            {
                neighbours.UnionWith(GetNeighbours(pt));
            }

            if(!neighbours.Remove(obj))
            {
                //the given object was never in the grid in the first place, because neighbors includes stuff in its own bucket, including itself
                throw new NotSupportedException();
            }
            return neighbours.ToList();
        }
    }
}
