using MarioClone.Cam;
using MarioClone.EventCenter;
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
        public int FullGameHeight { get; }
        public float CurrentLeftSideViewPort { get; set; }
        public float CurrentRightSideViewPort
        {
            get
            {
                return CurrentLeftSideViewPort + ScreenWidth;
            }
        }
        public float CurrentTopSideViewPort { get; set; }
        public float CurrentBottomSideViewPort
        {
            get
            {
                return CurrentTopSideViewPort + ScreenHeight;
            }
        }

        public static GameGrid Instance
        {
            get
            {
                return gridInstance;
            }
        }
        
        public GameGrid(int rows, Camera camera)
        {
            gridInstance = this;

            Rows = rows;
            _camera = camera;

            FullGameWidth = _camera.Limits.Value.Width;
            FullGameHeight = _camera.Limits.Value.Height * 2;
            ScreenWidth = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            ScreenHeight = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;

            GridSquareHeight = FullGameHeight / Rows;
			GridSquareWidth = GridSquareHeight;
			Columns = GridSquareWidth * (FullGameWidth / ScreenWidth) + 1;
			CurrentLeftSideViewPort = _camera.Position.X;
            CurrentTopSideViewPort = _camera.Position.Y;

            gameGrid = new List<AbstractGameObject>[Columns, Rows];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[j, i] = new List<AbstractGameObject>();
                }
            }

            EventManager.Instance.RaiseBadObjectRemovalEvent += FixBadObjectRemoval;
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
                if (!gameGrid[bucket.X, bucket.Y].Contains(obj))
                {
                    gameGrid[bucket.X, bucket.Y].Add(obj);
                }
            }
        }

        public void Remove(AbstractGameObject obj)
        {
            ISet<Point> squares = GetSquaresFromObject(obj.BoundingBox);
            foreach (Point bucket in squares)
            {
                gameGrid[bucket.X, bucket.Y].RemoveAll((x) => ReferenceEquals(x, obj));
            }
        }

        private List<Point> GetSquaresFromPoint(Point corner)
        {
            List<Point> squares = new List<Point>();
            int xBucket = corner.X / GridSquareWidth;
            int yBucket = corner.Y / GridSquareHeight;

            if (corner.Y >= FullGameHeight)
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

            if ((corner.Y > 0) && (corner.Y < FullGameHeight))
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
                if((pt.X >= Columns || pt.X < 0) || (pt.Y >= Rows || pt.Y < 0))
                {
                    continue;
                }
                
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

        private void FixBadObjectRemoval(object sender, BadObjectRemovalEventArgs e)
        {
            ISet<Point> potentialSquares = new HashSet<Point>();
            ISet<Point> additionalSquares = new HashSet<Point>();

            if (e.LastHitBox != null)
            {
                potentialSquares.UnionWith(GetSquaresFromObject(e.LastHitBox));                
            }
            else if(e.Position != null)
            {
                potentialSquares.UnionWith(GetSquaresFromPoint(new Point((int)e.Position.X, (int)e.Position.Y)));
            }

            foreach (Point p in potentialSquares)
            {
                for (int x = p.X - 5; x < p.X + 6; x++)
                {
                    for (int y = p.Y - 5; y < p.Y + 6; y++)
                    {
                        additionalSquares.Add(new Point(x, y));
                    }
                }
            }

            potentialSquares.UnionWith(additionalSquares);

            int numFound = 0;
            foreach(Point p in potentialSquares)
            {
                if(p.X >=0 && p.X < Columns && p.Y >= 0 && p.Y < Rows)
                {
                    numFound += gameGrid[p.X, p.Y].RemoveAll((obj) => ReferenceEquals(obj, e.Sender));
                }
            }

            if (numFound == 0)
            {
                int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
                if (leftHandColumn > 0) { leftHandColumn--; }

                int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
                if (rightHandColumn < Columns - 1) { rightHandColumn++; }

                int topRow = (int)(CurrentTopSideViewPort / GridSquareHeight);
                if (topRow > 0) { topRow--; }

                int bottomRow = (int)(CurrentBottomSideViewPort / GridSquareHeight);
                if (bottomRow < bottomRow - 1) { bottomRow++; }

                for (int i = leftHandColumn; i < rightHandColumn; i++)
                {
                    for (int j = topRow; j < bottomRow; j++)
                    {
                        numFound += gameGrid[i, j].RemoveAll((obj) => ReferenceEquals(obj, e.Sender));
                    }
                }
            }

            if(numFound == 0)
            {
                for (int i = 0; i < Columns; i++)
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        numFound += gameGrid[i, j].RemoveAll((obj) => ReferenceEquals(obj, e.Sender));
                    }
                }
            }
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

        public List<AbstractGameObject> GetAllCurrentGameObjects
        {
            get
            {
                int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
                if (leftHandColumn > 0) { leftHandColumn--; }

                int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
                if (rightHandColumn < Columns - 1) { rightHandColumn++; }

                int topRow = (int)(CurrentTopSideViewPort / GridSquareHeight);
                if (topRow > 0) { topRow--; }

                int bottomRow = (int)(CurrentBottomSideViewPort / GridSquareHeight);
                if (bottomRow < bottomRow - 1) { bottomRow++; }

                List<AbstractGameObject> objectList = new List<AbstractGameObject>();

                for (int rows = topRow; rows < bottomRow; rows++)
                {
                    for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                    {
                        objectList = objectList.Union(gameGrid[columns, rows]).ToList();
                    }
                }

                return objectList;
            }
        }

        public List<AbstractGameObject> GetCurrentMovingAndPlayerGameObjects
        {
            get 
            {
                int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
                if (leftHandColumn > 0) { leftHandColumn--; }

                int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
                if (rightHandColumn < Columns - 1) { rightHandColumn++; }

                int topRow = (int)(CurrentTopSideViewPort / GridSquareHeight);
                if (topRow > 0) { topRow--; }

                int bottomRow = (int)(CurrentBottomSideViewPort / GridSquareHeight);
                if (bottomRow < bottomRow - 1) { bottomRow++; }

                List<AbstractGameObject> objectList = new List<AbstractGameObject>();

                for (int rows = topRow; rows < bottomRow; rows++)
                {
                    for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                    {
                        objectList = objectList.Union(gameGrid[columns, rows])
                            .Where((x) => (x.Velocity.X != 0) || (x.Velocity.Y != 0) || (x is Mario)).ToList();
                    }
                }

                return objectList;
            }
        }

        public List<AbstractGameObject> GetAllCurrentStaticGameObjects
        {
            get
            {
                int leftHandColumn = (int)(CurrentLeftSideViewPort / GridSquareWidth);
                if (leftHandColumn > 0) { leftHandColumn--; }

                int rightHandColumn = (int)(CurrentRightSideViewPort / GridSquareWidth);
                if (rightHandColumn < Columns - 1) { rightHandColumn++; }

                int topRow = (int)(CurrentTopSideViewPort / GridSquareHeight);
                if (topRow > 0) { topRow--; }

                int bottomRow = (int)(CurrentBottomSideViewPort / GridSquareHeight);
                if (bottomRow < bottomRow - 1) { bottomRow++; }

                List<AbstractGameObject> objectList = new List<AbstractGameObject>();

                for (int rows = topRow; rows < bottomRow; rows++)
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
}
