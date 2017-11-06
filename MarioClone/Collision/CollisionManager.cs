using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MarioClone.Collision
{
    public enum Side
    {
        Left = 1,
        Right = 2,
        Top = 3,
        Bottom = 4,
        None = 0
    }

    public static class CollisionManager
    {
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

        private static float WhenCollisionCheck(AbstractGameObject obj1, AbstractGameObject obj2, float percentCompleted, out Side side)
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

            if (RectangleSidesTouching(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions, out side) 
                || obj1.BoundingBox.Dimensions.Intersects(obj2.BoundingBox.Dimensions))
            {
                return percentCompleted;
            }

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
            if (intersect.IsEmpty)
            {
                Side side = Side.None;
                if (RectangleSidesTouching(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions, out side))
                {
                    switch (side)
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
                else if (intersect.Right == obj1.BoundingBox.Dimensions.Right)
                {
                    return new Vector2(-intersect.Width - 1, 0);
                }
                else
                {
                    if (intersect.Top == obj1.BoundingBox.Dimensions.Top)
                    {
                        return new Vector2(intersect.Width + 1, 0);
                    }
                    else
                    {
                        return new Vector2(-intersect.Width - 1, 0);
                    }
                }
            }
        }

        public static Rectangle GetSweptBox(AbstractGameObject obj)
        {

            Rectangle sweptBox;
            sweptBox.X = obj.Velocity.X > 0 ? obj.BoundingBox.TopLeft.X : (int)(obj.BoundingBox.TopLeft.X + obj.Velocity.X);
            sweptBox.Y = obj.Velocity.Y > 0 ? obj.BoundingBox.TopLeft.Y : (int)(obj.BoundingBox.TopLeft.Y + obj.Velocity.Y);

            sweptBox.Width = obj.Velocity.X > 0 ? (int)obj.Velocity.X + obj.BoundingBox.Dimensions.Width : obj.BoundingBox.Dimensions.Width - (int)obj.Velocity.X;
            if ((int)(obj.Position.X + (obj.Velocity.X - (int)obj.Velocity.X)) > (int)(obj.Position.X))
            {
                sweptBox.Width += 1;
            }

            sweptBox.Height = obj.Velocity.Y > 0 ? (int)obj.Velocity.Y + obj.BoundingBox.Dimensions.Height : obj.BoundingBox.Dimensions.Height - (int)obj.Velocity.Y;
            if ((int)(obj.Position.Y + (obj.Velocity.Y - (int)obj.Velocity.Y)) > (int)(obj.Position.Y))
            {
                sweptBox.Height = sweptBox.Height + 1;
            }
            return sweptBox;
        }

        public static bool CollisionCompareOpposites(Tuple<float, Side, AbstractGameObject, AbstractGameObject> col1, Tuple<float, Side, AbstractGameObject, AbstractGameObject> col2)
        {
            if (col1.Item1 != col2.Item1)
            {
                return false;
            }

            if (ReferenceEquals(col1.Item3, col2.Item4) && ReferenceEquals(col1.Item4, col2.Item3))
            {
                if (col1.Item2 == GetOppositeSide(col2.Item2))
                {
                    return true;
                }
            }

            return false;
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

            if ((rect1.Left >= rect2.Left && rect1.Left <= rect2.Right)
                    || (rect1.Right >= rect2.Left && rect1.Right <= rect2.Right))
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

            if ((rect1.Top >= rect2.Top && rect1.Top <= rect2.Bottom)
                    || (rect1.Bottom >= rect2.Top && rect1.Bottom <= rect2.Bottom))
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
            if ((rect1.Left >= rect2.Left && rect1.Left <= rect2.Right)
                    || (rect1.Right >= rect2.Left && rect1.Right <= rect2.Right))
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
            else if (rect1.Left <= rect2.Left && rect1.Right >= rect2.Right)
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
            else if ((rect1.Top >= rect2.Top && rect1.Top <= rect2.Bottom)
                    || (rect1.Bottom >= rect2.Top && rect1.Bottom <= rect2.Bottom))
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
            else if (rect1.Top <= rect2.Top && rect1.Bottom >= rect2.Bottom)
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

        private static List<AbstractGameObject> UpdateObjects(List<AbstractGameObject> objects, GameGrid grid, 
            GameTime gameTime, float percentToUpdate)
        {
            var removed = new List<AbstractGameObject>();
            foreach (AbstractGameObject obj in objects)
            {
                HitBox oldHitbox = (obj.BoundingBox != null) ? new HitBox(obj.BoundingBox) : null;
                if (obj.Update(gameTime, percentToUpdate))
                { 
                    removed.Add(obj);
                }

                if (oldHitbox != null && obj.BoundingBox != null)
                {
                    grid.UpdateObjectGridPosition(obj, oldHitbox);
                }
            }

            return removed;
        }

        private static List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> filterCollisions
            (List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> completed,
            List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> collisions,
            out float earliestCollisionPercent)
        {
            if (collisions.Count > 0)
            {
                earliestCollisionPercent = collisions[0].Item1;
            }
            else
            {
                earliestCollisionPercent = 1;
                return collisions;
            }

            List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> removed =
                new List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>();

            bool repeatCollision = true;

            for (int i = 0; i < collisions.Count 
                && (repeatCollision || ((i + 1 < collisions.Count) && (collisions[i+1].Item1 == earliestCollisionPercent))); i++)
            {
                Tuple<float, Side, AbstractGameObject, AbstractGameObject> collision = collisions[i];

                repeatCollision = false;
                foreach (Tuple<float, Side, AbstractGameObject, AbstractGameObject> done in completed)
                {
                    if (CollisionCompare(collision, done))
                    {
                        repeatCollision = true;
                        removed.Add(collision);
                        if ((i + 1) < collisions.Count)
                        {
                            earliestCollisionPercent = collisions[i + 1].Item1;
                        }
                        break;
                    }
                }

                for (int j = i; j < collisions.Count; j++)
                {
                    Tuple<float, Side, AbstractGameObject, AbstractGameObject> coll = collisions[j];
                    if (CollisionCompareOpposites(collision, coll))
                    {
                        repeatCollision = true;
                        removed.Add(collision);
                        if ((i + 1) < collisions.Count)
                        {
                            earliestCollisionPercent = collisions[i + 1].Item1;
                        }
                        break;
                    }
                }
            }

            collisions.RemoveAll((x) => removed.Contains(x));

            if (collisions.Count == 0)
            {
                earliestCollisionPercent = 1;
            }

            return collisions;
        }

        public static List<AbstractGameObject> ProcessFrame(GameTime gameTime, List<AbstractGameObject> collidables, GameGrid grid)
        {
            float percentCompleted = 0;
            List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> completedCollisions
                = new List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>();
            List<AbstractGameObject> removedGameObjects = new List<AbstractGameObject>();

            while (percentCompleted < 1)
            {
                List<AbstractGameObject> neighbours = new List<AbstractGameObject>();
                List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>> collisions
                    = new List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>();

                foreach (AbstractGameObject obj in collidables)
                {
                    if (obj.BoundingBox == null)
                    {
                        continue;
                    }
                    neighbours = grid.FindNeighbours(obj);
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
                                else if ((obj is HiddenBrickObject && obj.Visible) || (neighbour is HiddenBrickObject && neighbour.Visible))
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
                    filterCollisions(completedCollisions, collisions, out earliestCollisionPercent);
                }

                if ((earliestCollisionPercent - percentCompleted ) != 0)
                {
                    int numRemoved = 1;
                    while (numRemoved != 0)
                    {
                        removedGameObjects.AddRange(UpdateObjects(collidables, grid, gameTime, earliestCollisionPercent - percentCompleted));
                        percentCompleted += (earliestCollisionPercent - percentCompleted);
                        collidables.RemoveAll((x) => removedGameObjects.Contains(x));

                        numRemoved = collisions.RemoveAll((x) => (removedGameObjects.Contains(x.Item3) || removedGameObjects.Contains(x.Item4)));
                        filterCollisions(completedCollisions, collisions, out earliestCollisionPercent);
                        collisions.Sort((x, y) => (x.Item1.CompareTo(y.Item1)));
                    }
                }
                
                bool anySignificantCollision = false;
                for (int i = 0; i < collisions.Count && collisions[i].Item1 == earliestCollisionPercent; i++)
                {
                    Tuple<float, Side, AbstractGameObject, AbstractGameObject> collision = collisions[i];
                    Side side = collision.Item2;
                    AbstractGameObject obj1 = collision.Item3;
                    AbstractGameObject obj2 = collision.Item4;


                    anySignificantCollision = true;

                    if(obj2 is Mario)
                    {
                        obj2.CollisionResponse(obj1, GetOppositeSide(side), gameTime);
                        obj1.CollisionResponse(obj2, side, gameTime);
                    }
                    else
                    {
                        obj1.CollisionResponse(obj2, side, gameTime);
                        obj2.CollisionResponse(obj1, GetOppositeSide(side), gameTime);
                    }

                    if (obj1.BoundingBox != null)
                    {
                        if ((obj2.BoundingBox != null)
                            && !(obj1 is AbstractBlock || obj1 is CoinObject || obj1 is FireFlowerObject)
                            && (obj2 is AbstractEnemy || (obj2 is AbstractBlock && obj2.Visible)))
                        {
                            if (RectangleSidesTouching(obj1.BoundingBox.Dimensions, obj2.BoundingBox.Dimensions)
                                || obj1.BoundingBox.Dimensions.Intersects(obj2.BoundingBox.Dimensions))
                            {
                                obj1.FixClipping(FindClippingCorrection(obj1, obj2), obj1, obj2);
                            }
                        }
                    }

                    if (obj2.BoundingBox != null)
                    {
                        if ((obj1.BoundingBox != null)
                            && !(obj2 is AbstractBlock || obj2 is CoinObject || obj2 is FireFlowerObject)
                            && (obj1 is AbstractEnemy || (obj1 is AbstractBlock && obj1.Visible)))
                        {
                            if (RectangleSidesTouching(obj2.BoundingBox.Dimensions, obj1.BoundingBox.Dimensions)
                                || (obj2.BoundingBox.Dimensions.Intersects(obj1.BoundingBox.Dimensions)))
                            {
                                obj2.FixClipping(FindClippingCorrection(obj2, obj1), obj1, obj2);
                            }
                        }
                    }

                    completedCollisions.Add(collision);
                }

                if (!anySignificantCollision && !(earliestCollisionPercent == 1))
                {
                    earliestCollisionPercent = 1;
                    removedGameObjects.AddRange(UpdateObjects(collidables, grid, gameTime, earliestCollisionPercent - percentCompleted));
                    collidables.RemoveAll((x) => removedGameObjects.Contains(x));
                }
                percentCompleted += (earliestCollisionPercent - percentCompleted);
            }

            return removedGameObjects;
        }

        public static Vector2 ScreenClamp(Vector2 newPosition, Rectangle objectDimensions)
        {
            if ((newPosition.X + objectDimensions.Width) > GameGrid.Instance.FullGameWidth)
            {
                newPosition.X = GameGrid.Instance.CurrentRightSideViewPort - objectDimensions.Width;
            }
            else if (newPosition.X < 0) 
            {
                newPosition.X = 0;
            }

            if (newPosition.Y - objectDimensions.Height < 0)
            {
                newPosition.Y = 0 + objectDimensions.Height;
            }

            return newPosition;
        }
    }
}
