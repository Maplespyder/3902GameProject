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
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[i, j] = new List<AbstractGameObject>();
                }
            }
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
            int yBucket = (corner.Y / GridSquareHeight) * Columns;

            if(corner.Y >= ScreenHeight)
            {
                yBucket -= Columns;
            }
            
            if (corner.X >= FullGameWidth)
            {
                xBucket -= 1;
            }
            squares.Add(new Point(xBucket, yBucket));

            if((corner.X > 0) && (corner.X < FullGameWidth))
            {
                if((corner.X % GridSquareWidth) == 0)
                {
                    squares.Add(new Point(xBucket - 1, yBucket));
                    if((corner.Y > 0) && (corner.Y < ScreenHeight))
                    {
                        if ((corner.Y % GridSquareHeight) == 0)
                        {
                            squares.Add(new Point(xBucket - 1, yBucket - Columns));
                        }
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

        private List<AbstractGameObject> GetCollidableGameObjects()
        {
            int leftHandColumn = CurrentLeftSideViewPort / GridSquareWidth;
            int rightHandColumn = CurrentRightSideViewPort / GridSquareWidth;
            if (rightHandColumn == Columns)
            {
                rightHandColumn -= 1;
            }
            List<AbstractGameObject> objectList = new List<AbstractGameObject>();

            for (int rows = 0; rows < Rows - 1; rows++)
            {
                for (int columns = leftHandColumn; columns < rightHandColumn; columns++)
                {
                    objectList.Union(gameGrid[rows, columns].Where((obj) => obj.BoundingBox != null));
                }
            }

            return objectList;
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

        public void IfCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            Vector2 futureObj1Position = new Vector2(obj1.BoundingBox.BottomLeft.X + obj1.Velocity.X, obj1.BoundingBox.BottomLeft.Y + obj1.Velocity.Y);
            Vector2 futureObj2Position = new Vector2(obj2.BoundingBox.BottomLeft.X + obj2.Velocity.X, obj2.BoundingBox.BottomLeft.Y + obj2.Velocity.Y);

            Rectangle obj1Sweep = new Rectangle(obj1.BoundingBox.BottomLeft.X, obj1.BoundingBox.BottomLeft.Y, (int)futureObj1Position.X-(int)obj1.BoundingBox.BottomLeft.X,
                 (int)futureObj1Position.Y - (int)obj2.BoundingBox.BottomLeft.Y);
        }

        public float WhenCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2)
        {

            float xTestEntry, yTestEntry, xEntry, yEntry, entryPercentage;
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
            Vector2 relativeVelocity = obj1.Velocity - obj2.Velocity;
            
            //distances of X and Y axis of both objects
            if(relativeVelocity.X > 0f)
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
            if(relativeVelocity.X == 0f) 
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

            

            if(xTestEntry < 0f && yTestEntry < 0f || xTestEntry > 1.0f || yTestEntry > 1.0f) //no collision
            {
                return 1.0f; //no collision; not sure what to return
            }
            else
            {
                return Math.Max(xTestEntry, yTestEntry);
            }
        }

        public float IfCollisionWithSweep(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            Rectangle obj1Sweep = GetSweptBox(obj1);
            Rectangle obj2Sweep = GetSweptBox(obj2);
            float collisionTime = -1;
            if(CollisionCheck(obj1Sweep, obj2Sweep))
            {
                collisionTime = WhenCollisionCheck(obj1, obj2);
            }
            return collisionTime;
        }

        public Rectangle GetSweptBox(AbstractGameObject obj)
        {
            Rectangle sweptBox;
            sweptBox.X = obj.Velocity.X > 0 ? obj.BoundingBox.BottomLeft.X : obj.BoundingBox.BottomLeft.X + obj.BoundingBox.Dimensions.Width;
            sweptBox.Y = obj.Velocity.Y > 0 ? obj.BoundingBox.BottomLeft.Y : obj.BoundingBox.BottomLeft.Y + obj.BoundingBox.Dimensions.Height;
            sweptBox.Width = obj.Velocity.X > 0 ? (int)obj.Velocity.X + obj.BoundingBox.Dimensions.Width : obj.BoundingBox.Dimensions.Width - (int)obj.Velocity.X;
            sweptBox.Height = obj.Velocity.Y > 0 ? (int)obj.Velocity.Y + obj.BoundingBox.Dimensions.Height : obj.BoundingBox.Dimensions.Height - (int)obj.Velocity.Y;
            return sweptBox;
        }

        public bool CollisionCheck(Rectangle obj1, Rectangle obj2)
        {
            if (!(obj1.X + obj1.Width < obj2.X || obj1.X > obj2.X + obj2.Width || obj1.Y + obj1.Height < obj2.Y || obj1.Y > obj2.Y + obj2.Height))
            {
                return false;
            } 
            else
            {
                return true;
            }
        }

        public void UpdateWorld()
        {
            List<AbstractGameObject> collidables = GetCollidableGameObjects();
            foreach(AbstractGameObject obj in collidables)
            {

            }
        }
    }
}
