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
        public enum Side
        {
            Left = 1,
            Right = 2,
            Top = 3,
            Bottom = 4,
            None = 0
        }

        private List<AbstractGameObject>[,] gameGrid;
		private Camera _camera;
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

        public GameGrid(int rows, int columns, int widthOfGame, Camera camera)
        {
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
            if (side == Side.Left)
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
                //throw new NotSupportedException();
            }
            return neighbours.ToList();
        }

        private List<AbstractGameObject> GetAllGameObjects()
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

        private List<AbstractGameObject> GetMovingGameObjects()
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

            Rectangle intersect = Rectangle.Intersect(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions);
            if(intersect.IsEmpty)
            {
                Side side = Side.None;
                if(RectangleSidesTouching(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions, out side))
                {
                    switch(side)
                    {
                        case Side.Left:
                            return new Vector2(1, 0);
                        case Side.Right:
                            return new Vector2(-1, 0);
                        case Side.Top:
                            return new Vector2(0, 1);
                        case Side.Bottom:
                            return new Vector2(0, -1);
                        default:
                            return new Vector2(0, 0);
                    }
                }
                else
                {
                    return new Vector2(0, 0);
                }

            }

            if (intersect.Height < intersect.Width)
            {
                if (intersect.Bottom == obj1.BoundingBox.Dimensions.Bottom)
                {
                    return new Vector2(0, -intersect.Height - 1);
                }
                else if (intersect.Top == obj1.BoundingBox.Dimensions.Top)
                {
                    return new Vector2(0, intersect.Height + 1);
                }
                else
                {
                    if (intersect.Left == obj1.BoundingBox.Dimensions.Left)
                    {
                        return new Vector2(intersect.Width + 1, 0);
                    }
                    else
                    {
                        return new Vector2(-intersect.Width - 1, 0);
                    }
                }
            }
            else
            {
                if (intersect.Left == obj1.BoundingBox.Dimensions.Left)
                {
                    return new Vector2(intersect.Width + 1, 0);
                }
                else
                {
                    return new Vector2(-intersect.Width - 1, 0);
                }
            }
        }

        public static Rectangle GetSweptBox(AbstractGameObject obj)
        {
            Rectangle sweptBox;
            sweptBox.X = obj.Velocity.X > 0 ? obj.BoundingBox.TopLeft.X : obj.BoundingBox.TopLeft.X + (int)obj.Velocity.X;
            sweptBox.Y = obj.Velocity.Y > 0 ? obj.BoundingBox.TopLeft.Y : obj.BoundingBox.TopLeft.Y + (int)obj.Velocity.Y;
            sweptBox.Width = obj.Velocity.X > 0 ? (int)obj.Velocity.X + obj.BoundingBox.Dimensions.Width : obj.BoundingBox.Dimensions.Width - (int)obj.Velocity.X;
            sweptBox.Height = obj.Velocity.Y > 0 ? (int)obj.Velocity.Y + obj.BoundingBox.Dimensions.Height : obj.BoundingBox.Dimensions.Height - (int)obj.Velocity.Y;
            return sweptBox;
        }

        private void UpdateObjects(GameTime gameTime, float percentToUpdate)
        {
            var removed = new List<AbstractGameObject>();
            var allObjects = GetAllGameObjects();
            foreach (AbstractGameObject obj in allObjects)
            {
                HitBox oldHitbox = (obj.BoundingBox != null) ? new HitBox(obj.BoundingBox) : null;
                if (obj.Update(gameTime, percentToUpdate))
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
        }

        public static bool CollisionCompare(Tuple<float, Side, AbstractGameObject, AbstractGameObject> col1, Tuple<float, Side, AbstractGameObject, AbstractGameObject> col2)
        {
            if (col1.Item1 != col2.Item1)
            {
                return false;
            }

            if (ReferenceEquals(col1.Item3, col2.Item3) && ReferenceEquals(col1.Item4, col2.Item4))
            {
                if (col1.Item2 == col2.Item2)
                {
                    return true;
                }
            }
            else if (ReferenceEquals(col1.Item3, col2.Item4) && ReferenceEquals(col1.Item4, col2.Item3))
            {
                if (col1.Item2 == GetOppositeSide(col2.Item2))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool RectangleSidesTouching(Rectangle rect1, Rectangle rect2, out Side side)
        {
            side = Side.None;

            if ((rect1.Left >= rect2.Left && rect1.Left < rect2.Right)
                    || (rect1.Right > rect2.Left && rect1.Right <= rect2.Right))
            {
                if (rect1.Bottom == rect2.Top)
                {
                    side = Side.Bottom;
                    return true;
                }
                else if (rect1.Top == rect2.Bottom)
                {
                    side = Side.Top;
                    return true;
                }
            }
            else if (rect1.Left <= rect2.Left && rect1.Right >= rect2.Right)
            {
                if (rect1.Bottom == rect2.Top)
                {
                    side = Side.Bottom;
                    return true;
                }
                else if (rect1.Top == rect2.Bottom)
                {
                    side = Side.Top;
                    return true;
                }
            }
            else if ((rect1.Top >= rect2.Top && rect1.Top < rect2.Bottom)
                    || (rect1.Bottom > rect2.Top && rect1.Bottom <= rect2.Bottom))
            {
                if (rect1.Left == rect2.Right)
                {
                    side = Side.Left;
                    return true;
                }
                else if (rect1.Right == rect2.Left)
                {
                    side = Side.Right;
                    return true;
                }
            }
            else if (rect1.Top <= rect2.Top && rect1.Bottom >= rect2.Bottom)
            {
                if (rect1.Left == rect2.Right)
                {
                    side = Side.Left;
                    return true;
                }
                else if (rect1.Right == rect2.Left)
                {
                    side = Side.Right;
                    return true;
                }
            }

            return false;
        }

        private static bool RectangleSidesTouching(Rectangle rect1, Rectangle rect2)
        {
            if ((rect1.Left >= rect2.Left && rect1.Left < rect2.Right)
                    || (rect1.Right > rect2.Left && rect1.Right <= rect2.Right))
            {
                if (rect1.Bottom == rect2.Top)
                {
                    return true;
                }
                else if (rect1.Top == rect2.Bottom)
                {
                    return true;
                }
            }
            else if(rect1.Left <= rect2.Left && rect1.Right >= rect2.Right)
            {
                if (rect1.Bottom == rect2.Top)
                {
                    return true;
                }
                else if (rect1.Top == rect2.Bottom)
                {
                    return true;
                }
            }
            else if ((rect1.Top >= rect2.Top && rect1.Top < rect2.Bottom)
                    || (rect1.Bottom > rect2.Top && rect1.Bottom <= rect2.Bottom))
            {
                if (rect1.Left == rect2.Right)
                {
                    return true;
                }
                else if (rect1.Right == rect2.Left)
                {
                    return true;
                }
            }
            else if(rect1.Top <= rect2.Top && rect1.Bottom >= rect2.Bottom)
            {
                if (rect1.Left == rect2.Right)
                {
                    return true;
                }
                else if (rect1.Right == rect2.Left)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool collided(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            if (obj1.BoundingBox.Dimensions.Intersects(obj2.BoundingBox.Dimensions)
                || RectangleSidesTouching(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions))
            {
                return true;
            }

            Rectangle sweptBox1 = GetSweptBox(obj1);
            Rectangle sweptBox2 = GetSweptBox(obj2);
            if (sweptBox1.Intersects(sweptBox2) || RectangleSidesTouching(sweptBox1, sweptBox2))
            {
                return true;
            }

            return false;
        }
        
        public void UpdateWorld(GameTime gameTime)
        {
            float percentCompleted = 0;
            List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> completedCollisions
                = new List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>();
            while (percentCompleted < 1)
            {
                List<AbstractGameObject> collidables = GetMovingGameObjects();
                List<AbstractGameObject> neighbours = new List<AbstractGameObject>();
                List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> collisions
                    = new List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>();

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

                        if (collided(obj, neighbour))
                        {
                            Side side = Side.None;
                            float percent = WhenCollisionCheck(obj, neighbour, percentCompleted, out side);

                            if (!(obj is AbstractBlock && neighbour is AbstractBlock))
                            {
                                if (!(obj is Mario && neighbour is HiddenBrickObject)
                                    || ((obj is Mario && neighbour is HiddenBrickObject) && !neighbour.Visible 
                                    && obj.BoundingBox.Dimensions.Top >= neighbour.BoundingBox.Dimensions.Bottom))
                                {
                                    collisions.Add(new Tuple<float, Side, AbstractGameObject, AbstractGameObject>(percent, side, obj, neighbour));
                                }
                                else if((obj is HiddenBrickObject && obj.Visible) || (neighbour is HiddenBrickObject && neighbour.Visible))
                                {
                                    collisions.Add(new Tuple<float, Side, AbstractGameObject, AbstractGameObject>(percent, side, obj, neighbour));
                                }
                            }
                        }
                    }
                }

                float earliestCollisionPercent;
                if (collisions.Count == 0)
                {
                    earliestCollisionPercent = 1;
                }
                else
                {
                    collisions.Sort((x, y) => (x.Item1.CompareTo(y.Item1)));
                    earliestCollisionPercent = collisions[0].Item1;
                }

                UpdateObjects(gameTime, earliestCollisionPercent - percentCompleted);

                List<Tuple<AbstractGameObject, AbstractGameObject>> alreadyProcessed = new List<Tuple<AbstractGameObject, AbstractGameObject>>();
                bool anySignificantCollision = false;
                for (int i = 0; i < collisions.Count && collisions[i].Item1 == earliestCollisionPercent; i++) //!anySignificantCollision; i++)
                {
                    Tuple<float, Side, AbstractGameObject, AbstractGameObject> collision = collisions[i];
                    Side side = collision.Item2;
                    AbstractGameObject obj1 = collision.Item3;
                    AbstractGameObject obj2 = collision.Item4;

                    bool alreadyDone = false;
                    foreach (Tuple<float, Side, AbstractGameObject, AbstractGameObject> done in completedCollisions)
                    {
                        //the collisions, if they reappear, will be in reverse order
                        if (CollisionCompare(collision, done))
                        {
                            alreadyDone = true;
                            break;
                        }
                    }

                    if (!alreadyDone)
                    {
                        anySignificantCollision = true;
                        HitBox oldHitbox1 = new HitBox(obj1.BoundingBox);
                        HitBox oldHitbox2 = new HitBox(obj2.BoundingBox);

                        obj1.CollisionResponse(obj2, side, gameTime);
                        obj2.CollisionResponse(obj1, GetOppositeSide(side), gameTime);

                        if(obj1.BoundingBox != null)
                        {
                            UpdateObjectGridPosition(obj1, oldHitbox1);
                        }
                        if (obj2.BoundingBox != null)
                        {
                            UpdateObjectGridPosition(obj2, oldHitbox2);
                        }

                        if (obj1.BoundingBox != null)
                        {
                            oldHitbox1 = obj1.BoundingBox;
                            if ((obj2.BoundingBox != null) 
                                && !(obj1 is AbstractBlock || obj1 is CoinObject || obj1 is FireFlowerObject)
                                && (obj2 is AbstractEnemy || (obj2 is AbstractBlock && obj2.Visible)))
                            {
                                if (collided(obj1, obj2))
                                {
                                    obj1.FixClipping(FindClippingCorrection(obj1, obj2));
                                    UpdateObjectGridPosition(obj1, oldHitbox1);
                                }
                            }
                        }

                        if (obj2.BoundingBox != null)
                        {
                            oldHitbox2 = obj2.BoundingBox;
                            if ((obj1.BoundingBox != null)
                                && !(obj2 is AbstractBlock || obj2 is CoinObject || obj2 is FireFlowerObject)
                                && (obj1 is AbstractEnemy || (obj1 is AbstractBlock && obj1.Visible)))
                            {
                                if (collided(obj2, obj1))
                                {
                                    obj2.FixClipping(FindClippingCorrection(obj2, obj1));
                                    UpdateObjectGridPosition(obj2, oldHitbox2);
                                }
                            }
                        }

                        completedCollisions.Add(collision);
                    }
                }

                if (!anySignificantCollision && !(earliestCollisionPercent == 1))
                {
                    earliestCollisionPercent = 1;
                    UpdateObjects(gameTime, earliestCollisionPercent - percentCompleted);
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
