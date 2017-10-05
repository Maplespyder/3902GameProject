using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarioClone.Collision
{
    public class GameGrid
    {
        private List<AbstractGameObject>[,] gameGrid;
        private List<AbstractGameObject> currentObjects;

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

            gameGrid = new List<AbstractGameObject>[Rows, Columns];
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[i, j] = new List<AbstractGameObject>();
                }
            }
            currentObjects = new List<AbstractGameObject>();
        }

        public void ClearGrid()
        {
            foreach(List<AbstractGameObject> li in gameGrid)
            {
                li.Clear();
            }
        }

        public void Add(AbstractGameObject obj)
        {
            currentObjects.Add(obj);
            if (obj.BoundingBox != null)
            {
                AddToGrid(obj);
            }
        }

        public void Remove(AbstractGameObject obj)
        {
            currentObjects.Remove(obj);
            if (obj.BoundingBox != null)
            {
                RemoveFromGrid(obj);
            }
        }

        private void AddToGrid(AbstractGameObject obj)
        {
            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach (Point bucket in squares)
            {
                gameGrid[bucket.X, bucket.Y].Add(obj);
            }
        }

        private void RemoveFromGrid(AbstractGameObject obj)
        {
            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach (Point bucket in squares)
            {
                gameGrid[bucket.X, bucket.Y].Remove(obj);
            }
        }

        private List<Point> GetSquaresFromPoint(Point corner)
        {
            List<Point> squares = new List<Point>();
            int xBucket = corner.X / GridSquareWidth;
            int yBucket = (corner.Y / GridSquareHeight) * Columns;

            if(corner.Y >= ScreenHeight)
            {
                yBucket -= Columns;
            }
            
            if (corner.X >= ScreenWidth)
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

        private ISet<Point> GetSquaresFromObject(HitBox hitBox)
        {
            ISet<Point> squares = new HashSet<Point>();
            squares.UnionWith(GetSquaresFromPoint(hitBox.TopLeft));
            squares.UnionWith(GetSquaresFromPoint(hitBox.TopRight));
            squares.UnionWith(GetSquaresFromPoint(hitBox.BottomLeft));
            squares.UnionWith(GetSquaresFromPoint(hitBox.BottomRight));
            return squares;
        }

        private void UpdateObjectGridPosition(AbstractGameObject obj, HitBox oldHitbox)
        {
            //should end up in a future Update() method that handles all updates
            if(obj.BoundingBox == null)
            {
                RemoveFromGrid(obj);
                return;
            }

            if (obj.BoundingBox.Equals(oldHitbox))
            {
                return;
            }

            if(!obj.BoundingBox.Dimensions.Location.Equals(oldHitbox.Dimensions.Location) 
                || obj.BoundingBox.Dimensions.Width != oldHitbox.Dimensions.Width 
                || obj.BoundingBox.Dimensions.Height != oldHitbox.Dimensions.Height)
            {
                ISet<Point> newSquares = GetSquaresFromObject(obj.BoundingBox);
                ISet<Point> oldSquares = GetSquaresFromObject(oldHitbox);
                ISet<Point> difference = (ISet<Point>)newSquares.Intersect(oldSquares);
                if(difference.Count > 0)
                {
                    foreach(Point pt in difference)
                    {
                        //either old squares or new squares must contain
                        //the point, if it's in old that means it's not in new,
                        //so it should be removed, and vice versa
                        if(newSquares.Contains(pt))
                        {
                            gameGrid[pt.X, pt.Y].Add(obj);
                        }
                        else
                        {
                            gameGrid[pt.X, pt.Y].Remove(obj);
                        }
                    }
                }
            }
        }

        private ISet<AbstractGameObject> GetNeighbours(Point square)
        {
            ISet<AbstractGameObject> neighbours = new HashSet<AbstractGameObject>();
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

        private List<AbstractGameObject> FindNeighbours(AbstractGameObject obj)
        {
            ISet<AbstractGameObject> neighbours = new HashSet<AbstractGameObject>();

            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach(Point pt in squares)
            {
                neighbours.UnionWith(GetNeighbours(pt));
            }

            if(!neighbours.Remove(obj))
            {
                //the given object was never in the grid in the first place, because neighbors
                //includes stuff in its own bucket, including itself
                throw new NotSupportedException();
            }
            return neighbours.ToList();
        }

        public bool MightCollide(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            Vector2 relativeVelocity = obj2.Velocity - obj1.Velocity;
            if(obj1.Position.X < obj2.Position.X)
            {
                relativeVelocity = new Vector2(-relativeVelocity.X, relativeVelocity.Y);
            }
            if(obj1.Position.Y < obj2.Position.Y)
            {
                relativeVelocity = new Vector2(relativeVelocity.X, -relativeVelocity.Y);
            }

            return (relativeVelocity.X <= 0 && relativeVelocity.Y < 0) || (relativeVelocity.X < 0 && relativeVelocity.Y <= 0);
        }
    }
}
