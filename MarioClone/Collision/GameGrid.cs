using MarioClone.Cam;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarioClone.Collision
{
    public class GameGrid
    {
        private List<AbstractGameObject>[,] gameGrid;
		private Camera _camera;
        private static GameGrid gridInstance;

        public int Rows { get; }
        public int Columns { get; }
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }
        public int GridSquareWidth { get; }
        public int GridSquareHeight { get; }
        public int FullGameWidth { get; }
        public float CurrentLeftSideViewPort { get; set; }
        public float CurrentRightSideViewPort
        {
            get
            {
                return CurrentLeftSideViewPort + ScreenWidth;
            }
        }

        public static GameGrid Instance
        {
            get
            {
                return gridInstance;
            }
        }

        public GameGrid(int rows, int widthOfGame, Camera camera)
        {
            gridInstance = this;

            Rows = rows;
			_camera = camera;
            ScreenWidth = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            ScreenHeight = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;
            GridSquareHeight = ScreenHeight / Rows;
			FullGameWidth = widthOfGame;
			GridSquareWidth = GridSquareHeight;
			Columns = GridSquareWidth * (FullGameWidth / ScreenWidth) + 1;
			CurrentLeftSideViewPort = _camera.Position.X;

            gameGrid = new List<AbstractGameObject>[Columns, Rows];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[j, i] = new List<AbstractGameObject>();
                }
            }
        }
        
        public void ClearGrid()
        {
            foreach (List<AbstractGameObject> li in gameGrid)
            {
                li.Clear();
            }
        }

        public void Add(AbstractGameObject obj)
        {
            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach (Point bucket in squares)
            {
                gameGrid[bucket.X, bucket.Y].Add(obj);
            }
        }

        public void Remove(AbstractGameObject obj)
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
            int yBucket = corner.Y / GridSquareHeight;

            if (corner.Y >= ScreenHeight)
            {
                yBucket -= 1;
            }

            if (corner.X >= FullGameWidth)
            {
                xBucket -= 1;
            }
            squares.Add(new Point(xBucket, yBucket));

            if ((corner.X > 0) && (corner.X < FullGameWidth))
            {
                if ((corner.X % GridSquareWidth) == 0)
                {
                    squares.Add(new Point(xBucket - 1, yBucket));
                    if ((corner.Y > 0) && (corner.Y < ScreenHeight))
                    {
                        if ((corner.Y % GridSquareHeight) == 0)
                        {
                            squares.Add(new Point(xBucket - 1, yBucket - 1));
                        }
                    }
                }
            }

            if ((corner.Y > 0) && (corner.Y < ScreenHeight))
            {
                if ((corner.Y % GridSquareHeight) == 0)
                {
                    squares.Add(new Point(xBucket, yBucket - 1));
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

        public void UpdateObjectGridPosition(AbstractGameObject obj, HitBox oldHitbox)
        {
            //should end up in a future Update() method that handles all updates

            if (obj.BoundingBox.Equals(oldHitbox))
            {
                return;
            }

            ISet<Point> newSquares = GetSquaresFromObject(obj.BoundingBox);
            ISet<Point> oldSquares = GetSquaresFromObject(oldHitbox);

            foreach (Point pt in oldSquares)
            {
                gameGrid[pt.X, pt.Y].RemoveAll((x) => ReferenceEquals(x, obj));
            }

            foreach (Point pt in newSquares)
            {
                gameGrid[pt.X, pt.Y].Add(obj);
            }

        }

        private ISet<AbstractGameObject> GetNeighbours(Point square)
        {
            ISet<AbstractGameObject> neighbours = new HashSet<AbstractGameObject>();
            neighbours.UnionWith(gameGrid[square.X, square.Y]);

            if (square.X > 0)
            {
                neighbours.UnionWith(gameGrid[square.X - 1, square.Y]);
                if (square.Y > 0)
                {
                    neighbours.UnionWith(gameGrid[square.X - 1, square.Y - 1]);
                }
                if (square.Y < (Rows - 1))
                {
                    neighbours.UnionWith(gameGrid[square.X - 1, square.Y + 1]);
                }
            }
            if (square.X < (Columns - 1))
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
            if (square.Y > 0)
            {
                neighbours.UnionWith(gameGrid[square.X, square.Y - 1]);
            }
            if (square.Y < (Rows - 1))
            {
                neighbours.UnionWith(gameGrid[square.X, square.Y + 1]);
            }

            return neighbours;
        }

        public List<AbstractGameObject> FindNeighbours(AbstractGameObject obj)
        {
            ISet<AbstractGameObject> neighbours = new HashSet<AbstractGameObject>();

            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach (Point pt in squares)
            {
                neighbours.UnionWith(GetNeighbours(pt));
            }

            if (!neighbours.Remove(obj))
            {
                //the given object was never in the grid in the first place, because neighbors
                //includes stuff in its own bucket, including itself
                //throw new NotSupportedException();
            }
            return neighbours.ToList();
        }

        public List<AbstractGameObject> GetAllCurrentGameObjects()
        {
			int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
            int rightHandColumn =(int)(CurrentRightSideViewPort / GridSquareWidth);
            List<AbstractGameObject> objectList = new List<AbstractGameObject>();

            for (int rows = 0; rows < Rows; rows++)
            {
                for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                {
                    objectList = objectList.Union(gameGrid[columns, rows]).ToList();
                }
            }

            return objectList;
        }

        public List<AbstractGameObject> GetCurrentMovingAndPlayerGameObjects()
        {
            int leftHandColumn =(int)(CurrentLeftSideViewPort / GridSquareWidth);
            int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
            List<AbstractGameObject> objectList = new List<AbstractGameObject>();

            for (int rows = 0; rows < Rows; rows++)
            {
                for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                {
                    objectList = objectList.Union(gameGrid[columns, rows])
                        .Where((x) => (x.Velocity.X != 0) || (x.Velocity.Y != 0) || (x is Mario)).ToList();
                }
            }

            return objectList;
        }

        public List<AbstractGameObject> GetAllCurrenStaticGameObjects()
        {
            int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
            int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
            List<AbstractGameObject> objectList = new List<AbstractGameObject>();

            for (int rows = 0; rows < Rows; rows++)
            {
                for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                {
                    objectList = objectList.Union(gameGrid[columns, rows])
                        .Where((x) => ((x.Velocity.X == 0) && (x.Velocity.Y == 0)) && !(x is Mario)).ToList();
                }
            }

            return objectList;
        }
    }
}
