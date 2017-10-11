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
        public enum Side
        {
            Left = 1,
            Right = 2,
            Top = 3,
            Bottom = 4,
            None = 0
        }

        private List<AbstractGameObject>[,] gameGrid;

        public int Rows { get; }
        public int Columns { get; }
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }
        public int GridSquareWidth { get; }
        public int GridSquareHeight { get; }
        public int FullGameWidth { get; }
        public int CurrentLeftSideViewPort { get; set; }
        public int CurrentRightSideViewPort
        {
            get
            {
                return CurrentLeftSideViewPort + ScreenWidth;
            }
        }
        public GameGrid(int rows, int columns, int widthOfGame)
        {
            Rows = rows;
            Columns = columns;
            ScreenWidth = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferWidth;
            ScreenHeight = MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight;
            GridSquareWidth = ScreenWidth / Columns;
            GridSquareHeight = ScreenHeight / Rows;
            FullGameWidth = widthOfGame;
            CurrentLeftSideViewPort = 0;

            gameGrid = new List<AbstractGameObject>[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[i, j] = new List<AbstractGameObject>();
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

        private void UpdateObjectGridPosition(AbstractGameObject obj, HitBox oldHitbox)
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
                gameGrid[pt.X, pt.Y].Remove(obj);
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

        private List<AbstractGameObject> FindNeighbours(AbstractGameObject obj)
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
                throw new NotSupportedException();
            }
            return neighbours.ToList();
        }

        private List<AbstractGameObject> GetCollidableGameObjects()
        {
            int leftHandColumn = CurrentLeftSideViewPort / GridSquareWidth;
            int rightHandColumn = CurrentRightSideViewPort / GridSquareWidth;
            List<AbstractGameObject> objectList = new List<AbstractGameObject>();

            for (int rows = 0; rows < Rows - 1; rows++)
            {
                for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                {
                    objectList = objectList.Union(gameGrid[rows, columns]).ToList();
                }
            }

            return objectList;
        }

        public bool MightCollide(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            Vector2 relativeVelocity = obj2.Velocity - obj1.Velocity;
            if (obj2.Position.X < obj1.Position.X)
            {
                relativeVelocity = new Vector2(-relativeVelocity.X, relativeVelocity.Y);
            }
            if (obj2.Position.Y < obj1.Position.Y)
            {
                relativeVelocity = new Vector2(relativeVelocity.X, -relativeVelocity.Y);
            }

            return (relativeVelocity.X < 0 || relativeVelocity.Y < 0);
        }

        public float WhenCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2, float percentCompleted, out Side side)
        {

            float xTestEntry, yTestEntry, xEntry, yEntry;
            Point o1 = obj1.BoundingBox.BottomLeft;
            Point o2 = obj2.BoundingBox.BottomLeft;

            /*determined, relative on OBJ1, that:
            * -X relativeVelocity -> Obj1 is hit on the left 
            * +X relVel -> Obj1 hit on right
            * -Y relVel -> Obj1 hit on top
            * +Y relVel -> Obj1 hit on bottom
            * 
            * I didn't look at it for too long, so it might not be totally correct, but it worked on all paper tests I made up. 
            */
            Vector2 relativeVelocity = (obj1.Velocity - obj2.Velocity) * (1 - percentCompleted);
            side = Side.None;
            //distances of X and Y axis of both objects
            if (relativeVelocity.X > 0f)
            {
                xTestEntry = obj2.BoundingBox.BottomLeft.X - (obj1.BoundingBox.BottomLeft.X + obj1.BoundingBox.Dimensions.Width);
            }
            else
            {
                xTestEntry = (obj2.BoundingBox.BottomLeft.X + obj2.BoundingBox.Dimensions.Width) - obj1.BoundingBox.BottomLeft.X;
            }

            if (relativeVelocity.Y > 0f)
            {
                yTestEntry = (obj2.BoundingBox.BottomLeft.Y - obj2.BoundingBox.Dimensions.Height) - obj1.BoundingBox.BottomLeft.Y;
            }
            else
            {
                yTestEntry = obj2.BoundingBox.BottomLeft.Y - (obj1.BoundingBox.BottomLeft.Y - obj1.BoundingBox.Dimensions.Height);
            }

            //determine times when X and Y axis hit
            if (relativeVelocity.X == 0f)
            {
                xEntry = float.PositiveInfinity;
            }
            else
            {
                xEntry = xTestEntry / relativeVelocity.X;
            }

            if (relativeVelocity.Y == 0f)
            {
                yEntry = float.PositiveInfinity;
            }
            else
            {
                yEntry = yTestEntry / relativeVelocity.Y;
            }


            if (xEntry > yEntry)
            {
                if (xTestEntry < 0)
                {
                    side = Side.Top;
                }
                else if (xTestEntry > 0)
                {
                    side = Side.Bottom;
                }
            }
            else
            {
                if (yTestEntry < 0)
                {
                    side = Side.Left;
                }
                else if (yTestEntry > 0)
                {
                    side = Side.Right;
                }
            }

            if(yEntry > 0f && yEntry < 1f)
            {
                return 1.0f;
            }

            if (xEntry < 0f && yEntry < 0f || xEntry > 1.0f || yEntry > 1.0f) //no collision
            {
                return 1.0f; //no collision
            }
            else
            {
                return Math.Max(xTestEntry, yTestEntry);
            }
        }

        public float IfCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2, float percentCompleted, out Side side)
        {
            Rectangle obj1Sweep = GetSweptBox(obj1);
            Rectangle obj2Sweep = GetSweptBox(obj2);
            side = Side.None;
            float collisionTime = 1;
            if (CollisionCheck(obj1Sweep, obj2Sweep))
            {
                collisionTime = WhenCollisionCheck(obj1, obj2, percentCompleted, out side);
                int p = 50;
            }
            return collisionTime;
        }

        public Rectangle GetSweptBox(AbstractGameObject obj)
        {
            Rectangle sweptBox;
            sweptBox.X = obj.Velocity.X > 0 ? obj.BoundingBox.TopLeft.X : obj.BoundingBox.TopLeft.X + (int)obj.Velocity.X;
            sweptBox.Y = obj.Velocity.Y > 0 ? obj.BoundingBox.TopLeft.Y: obj.BoundingBox.TopLeft.Y + (int)obj.Velocity.Y;
            sweptBox.Width = obj.Velocity.X > 0 ? (int)obj.Velocity.X + obj.BoundingBox.Dimensions.Width : obj.BoundingBox.Dimensions.Width - (int)obj.Velocity.X;
            sweptBox.Height = obj.Velocity.Y > 0 ? (int)obj.Velocity.Y + obj.BoundingBox.Dimensions.Height : obj.BoundingBox.Dimensions.Height - (int)obj.Velocity.Y;
            return sweptBox;
        }

        public bool CollisionCheck(Rectangle obj1, Rectangle obj2)
        {
            return obj1.Intersects(obj2);
        }

        public void UpdateWorld(GameTime gameTime)
        {
            float earliestCollisionPercent = 1;
            float percentCompleted = 0;

            while (percentCompleted < 1)
            {

                List<AbstractGameObject> collidables = GetCollidableGameObjects();
                List<AbstractGameObject> neighbours = new List<AbstractGameObject>();
                Tuple<Side, AbstractGameObject, AbstractGameObject> firstCollision = null;

                foreach (AbstractGameObject obj in collidables)
                {
                    if (obj.BoundingBox == null)
                    {
                        continue;
                    }
                    neighbours = FindNeighbours(obj);
                    foreach (AbstractGameObject neighbour in neighbours)
                    {
                        if (neighbour.BoundingBox == null)
                        {
                            continue;
                        }

                        if (MightCollide(obj, neighbour))
                        {
                            Side side = Side.None;
                            float percent = IfCollisionCheck(obj, neighbour, percentCompleted, out side);
                            if ((side != Side.None) && percent < earliestCollisionPercent)
                            {
                                firstCollision = new Tuple<Side, AbstractGameObject, AbstractGameObject>(side, obj, neighbour);
                                earliestCollisionPercent = percentCompleted;
                            }
                        }
                    }
                }

                if (firstCollision == null)
                {
                    earliestCollisionPercent = 1;
                }

                var removed = new List<AbstractGameObject>();
                foreach (AbstractGameObject obj in collidables)
                {
                    HitBox oldHitbox = new HitBox(obj.BoundingBox);
                    if (obj.Update(gameTime, earliestCollisionPercent - percentCompleted))
                    {
                        removed.Add(obj);
                    }
                    else
                    {
                        UpdateObjectGridPosition(obj, oldHitbox);
                    }
                }

                foreach (var obj in removed)
                {
                    Remove(obj);
                }

                if (firstCollision != null)
                {
                    if(firstCollision.Item2 is Mario)
                    ((Mario)firstCollision.Item2).Process(firstCollision.Item3, firstCollision.Item1); //if side is left, do side.right for object2
                    //firstCollision.Item3.Process(firstCollision.Item2, firstCollision.Item1); //and vice versa
                }
                percentCompleted += earliestCollisionPercent;
            }
        }

        public void DrawWorld(SpriteBatch spriteBatch, GameTime gameTime)
        {
            List<AbstractGameObject> collidables = GetCollidableGameObjects();
            foreach (var obj in collidables)
            {
                obj.Draw(spriteBatch, gameTime);
            }

            int leftHandColumn = CurrentLeftSideViewPort / GridSquareWidth;
            int rightHandColumn = CurrentRightSideViewPort / GridSquareWidth;

            Texture2D pixel = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.Orange });
            for (int i = 0; i < Rows; i++)
            {
                for (int j = leftHandColumn; j < rightHandColumn; j++)
                {
                    Rectangle line = new Rectangle(j * GridSquareWidth, 0, 1, ScreenHeight);
                    spriteBatch.Draw(pixel, line, Color.White);
                }
                Rectangle rowline = new Rectangle(0, i * GridSquareHeight, ScreenWidth, 1);
                spriteBatch.Draw(pixel, rowline, Color.White);
            }
        }
    }
}
