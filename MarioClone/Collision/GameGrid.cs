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

            gameGrid = new List<AbstractGameObject>[Columns, Rows];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameGrid[j, i] = new List<AbstractGameObject>();
                }
            }
        }

        public static Side GetOppositeSide(Side side)
        {
            if (side == Side.Top)
            {
                return Side.Bottom;
            }
            if (side == Side.Bottom)
            {
                return Side.Top;
            } 
            if(side == Side.Left)
            {
                return Side.Right;
            }
            if (side == Side.Right)
            {
                return Side.Left;
            }
            return Side.None;
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

        private List<AbstractGameObject> GetAllGameObjects()
        {
            int leftHandColumn = CurrentLeftSideViewPort / GridSquareWidth;
            int rightHandColumn = CurrentRightSideViewPort / GridSquareWidth;
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

        private List<AbstractGameObject> GetMovingGameObjects()
        {
            int leftHandColumn = CurrentLeftSideViewPort / GridSquareWidth;
            int rightHandColumn = CurrentRightSideViewPort / GridSquareWidth;
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

        public static bool MightCollide(AbstractGameObject obj1, AbstractGameObject obj2)
        {

            Vector2 relativeVelocity = obj2.Velocity - obj1.Velocity;
            if (obj2.BoundingBox.TopLeft.X <= obj1.BoundingBox.TopLeft.X)
            {
                relativeVelocity = new Vector2(-relativeVelocity.X, relativeVelocity.Y);
            }
            if (obj2.BoundingBox.TopLeft.Y <= obj1.BoundingBox.TopLeft.Y)
            {
                relativeVelocity = new Vector2(relativeVelocity.X, -relativeVelocity.Y);
            }

            return (relativeVelocity.X < 0 || relativeVelocity.Y < 0)
                || obj1.BoundingBox.Dimensions.Intersects(obj2.BoundingBox.Dimensions);
        }

        public static float WhenCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2, float percentCompleted, out Side side)
        {

            float xDirectionDistance, yDirectionDistance, xEntryPercent, yEntryPercent;
            
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
                xDirectionDistance = obj2.BoundingBox.TopLeft.X - (obj1.BoundingBox.TopLeft.X + obj1.BoundingBox.Dimensions.Width);
            }
            else
            {
                xDirectionDistance = (obj2.BoundingBox.TopLeft.X + obj2.BoundingBox.Dimensions.Width) - obj1.BoundingBox.TopLeft.X;
            }

            if (relativeVelocity.Y > 0f)
            {
                yDirectionDistance = (obj2.BoundingBox.TopLeft.Y - (obj1.BoundingBox.Dimensions.Height + obj1.BoundingBox.TopLeft.Y));
            }
            else
            {
                yDirectionDistance = (obj2.BoundingBox.TopLeft.Y + obj2.BoundingBox.Dimensions.Height) - obj1.BoundingBox.TopLeft.Y;
            }

            //determine times when X and Y axis hit
            if (relativeVelocity.X == 0f)
            {
                xEntryPercent = -(float.PositiveInfinity);
            }
            else
            {
                xEntryPercent = xDirectionDistance / relativeVelocity.X;
            }

            if (relativeVelocity.Y == 0f)
            {
                yEntryPercent = -(float.PositiveInfinity);
            }
            else
            {
                yEntryPercent = yDirectionDistance / relativeVelocity.Y;
            }


            if (xEntryPercent > yEntryPercent)
            {
                if (relativeVelocity.X < 0)
                {
                    side = Side.Left;
                }
                else if (relativeVelocity.X > 0)
                {
                    side = Side.Right;
                }
            }
            else
            {
                if (relativeVelocity.Y < 0)
                {
                    side = Side.Top;
                }
                else if (relativeVelocity.Y > 0)
                {
                    side = Side.Bottom;
                }
            }


            if (xEntryPercent < 0f && yEntryPercent < 0f || (xEntryPercent > 1.0f || yEntryPercent > 1.0f)) //no collision
            {
                return 1.0f; //no collision
            }
            else
            {
                return Math.Max(xEntryPercent, yEntryPercent);
            }
        }

        public static Vector2 FindClippingCorrection(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            if (CollisionCheck(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions))
            {
                Rectangle intersect = Rectangle.Intersect(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions);
                if (intersect.Height < intersect.Width)
                {
                    if (intersect.Bottom == obj1.BoundingBox.Dimensions.Bottom)
                    {
                        return new Vector2(0, -intersect.Height);
                    }
                    else if (intersect.Top == obj1.BoundingBox.Dimensions.Top)
                    {
                        return new Vector2(0, intersect.Height);
                    }
                    else
                    {
                        if (intersect.Left == obj1.BoundingBox.Dimensions.Left)
                        {
                            return new Vector2(intersect.Width, 0);
                        }
                        else
                        {
                            return new Vector2(-intersect.Width, 0);
                        }
                    }
                }
                else
                {
                    if (intersect.Left == obj1.BoundingBox.Dimensions.Left)
                    {
                        return new Vector2(intersect.Width, 0);
                    }
                    else
                    {
                        return new Vector2(-intersect.Width, 0);
                    }
                }
            }

            return new Vector2(0, 0);
        }

        public static float IfCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2, float percentCompleted, out Side side)
        {
            Rectangle obj1Sweep = GetSweptBox(obj1);
            Rectangle obj2Sweep = GetSweptBox(obj2);
            side = Side.None;
            float collisionTime = 1;

            if (CollisionCheck(obj1Sweep, obj2Sweep))
            {
				collisionTime = WhenCollisionCheck(obj1, obj2, percentCompleted, out side);
            }
            return collisionTime;
        }

        public static Rectangle GetSweptBox(AbstractGameObject obj)
        {
            Rectangle sweptBox;
            sweptBox.X = obj.Velocity.X > 0 ? obj.BoundingBox.TopLeft.X : obj.BoundingBox.TopLeft.X + (int)obj.Velocity.X;
            sweptBox.Y = obj.Velocity.Y > 0 ? obj.BoundingBox.TopLeft.Y: obj.BoundingBox.TopLeft.Y + (int)obj.Velocity.Y;
            sweptBox.Width = obj.Velocity.X > 0 ? (int)obj.Velocity.X + obj.BoundingBox.Dimensions.Width : obj.BoundingBox.Dimensions.Width - (int)obj.Velocity.X;
            sweptBox.Height = obj.Velocity.Y > 0 ? (int)obj.Velocity.Y + obj.BoundingBox.Dimensions.Height : obj.BoundingBox.Dimensions.Height - (int)obj.Velocity.Y;
            return sweptBox;
        }

        public static bool CollisionCheck(Rectangle obj1, Rectangle obj2)
        {
            return obj1.Intersects(obj2);
        }

        public void UpdateWorld(GameTime gameTime)
        {
            float earliestCollisionPercent = 1;
            float percentCompleted = 0;

            while (percentCompleted < 1)
            {
                earliestCollisionPercent = 1;
                List<AbstractGameObject> collidables = GetMovingGameObjects();
                List<AbstractGameObject> neighbours = new List<AbstractGameObject>();
                List<Tuple<Side, AbstractGameObject, AbstractGameObject>> firstCollisions
                    = new List<Tuple<Side, AbstractGameObject, AbstractGameObject>>();

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

                            if ((side != Side.None))
                            {
                                if (percent == earliestCollisionPercent)
                                {
                                    firstCollisions.Add(new Tuple<Side, AbstractGameObject, AbstractGameObject>(side, obj, neighbour));
                                }
                                else if (percent < earliestCollisionPercent)
                                {
                                    firstCollisions.Clear();
                                    firstCollisions.Add(new Tuple<Side, AbstractGameObject, AbstractGameObject>(side, obj, neighbour));
                                    earliestCollisionPercent = percent;
                                }
                            }
                        }
                    }
                }

                if (firstCollisions.Count == 0)
                {
                    earliestCollisionPercent = 1;
                }

                var removed = new List<AbstractGameObject>();
                var allObjects = GetAllGameObjects();
                foreach (AbstractGameObject obj in allObjects)
                {
                    HitBox oldHitbox = (obj.BoundingBox != null) ? new HitBox(obj.BoundingBox) : null;
                    if (obj.Update(gameTime, earliestCollisionPercent - percentCompleted))
                    {
                        removed.Add(obj);
                    }
                    else
                    {
                        if (oldHitbox != null)
                        {
                            UpdateObjectGridPosition(obj, oldHitbox);
                        }
                    }
                }

                foreach (var obj in removed)
                {
                    Remove(obj);
                }

                List<Tuple<AbstractGameObject, AbstractGameObject>> alreadyProcessed = new List<Tuple<AbstractGameObject, AbstractGameObject>>();
                foreach (Tuple<Side, AbstractGameObject, AbstractGameObject> collision in firstCollisions)
                {
                    bool alreadyDone = false;
                    foreach (Tuple<AbstractGameObject, AbstractGameObject> done in alreadyProcessed)
                    {
                        if (ReferenceEquals(done.Item1, collision.Item3) && ReferenceEquals(done.Item2, collision.Item2))
                        {
                            alreadyDone = true;
                            break;
                        }
                    }


                    if (!alreadyDone)
                    {
                        HitBox oldHitbox2 = new HitBox(collision.Item2.BoundingBox);
                        HitBox oldHitbox3 = new HitBox(collision.Item3.BoundingBox);
                        if (earliestCollisionPercent == percentCompleted)
                        {
                            if (collision.Item2 is AbstractBlock)
                            {
                                collision.Item3.FixClipping(FindClippingCorrection(collision.Item3, collision.Item2));
                            }
                            else if (collision.Item3 is AbstractBlock)
                            {
                                collision.Item2.FixClipping(FindClippingCorrection(collision.Item2, collision.Item3));
                            }
                            /*if (collision.Item2 is Mario)
                            {
                                collision.Item2.FixClipping(FindClippingCorrection(collision.Item2, collision.Item3));
                            }
                            else if(collision.Item3 is Mario)
                            {
                                collision.Item3.FixClipping(FindClippingCorrection(collision.Item2, collision.Item3));
                            }
                            else if(!(collision.Item2 is AbstractBlock || collision.Item2 is CoinObject))
                            {
                                collision.Item2.FixClipping(FindClippingCorrection(collision.Item2, collision.Item3));
                            }
                            else if(!(collision.Item3 is AbstractBlock || collision.Item2 is CoinObject))
                            {
                                collision.Item3.FixClipping(FindClippingCorrection(collision.Item2, collision.Item3));
                            }*/
                        }

                        collision.Item2.CollisionResponse(collision.Item3, collision.Item1, gameTime);
                        collision.Item3.CollisionResponse(collision.Item2, GetOppositeSide(collision.Item1), gameTime);

                        if (collision.Item2.BoundingBox != null)
                        {
                            UpdateObjectGridPosition(collision.Item2, oldHitbox2);
                        }
                        if (collision.Item3.BoundingBox != null)
                        {
                            UpdateObjectGridPosition(collision.Item3, oldHitbox3);
                        }
                    }
                }

                percentCompleted += (earliestCollisionPercent - percentCompleted);
            }
        }


        public void DrawWorld(SpriteBatch spriteBatch, GameTime gameTime)
        {
            List<AbstractGameObject> allObjects = GetAllGameObjects();
            foreach (var obj in allObjects)
            {
                obj.Draw(spriteBatch, gameTime);
            }
        }
    }
}
