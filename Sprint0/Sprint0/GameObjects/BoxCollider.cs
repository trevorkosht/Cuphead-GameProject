using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class BoxCollider : Collider
{
    public Rectangle BoundingBox { get; private set; }

    private Texture2D _debugTexture;

    private int changeSize { get; set; }
    public Vector2 offset { get; set; }
    public Vector2 bounds { get; set; } // Width = X and Height = Y
    public float Rotation { get; set; } // Rotation in radians

    public BoxCollider(Vector2 bounds, Vector2 offset, GraphicsDevice graphicsDevice, float rotation = 0f)
    {
        this.offset = offset;
        this.bounds = bounds;
        this.Rotation = rotation;
        BoundingBox = new Rectangle(0, 0, (int)bounds.X, (int)bounds.Y);
        _debugTexture = new Texture2D(graphicsDevice, 1, 1);
        _debugTexture.SetData(new[] { Color.White }); // For drawing the debug rectangle
    }

    public override void Update(GameTime gameTime)
    {
        if (GameObject != null)
        {
            // Set the non-rotated bounding box
            int x = GameObject.X + (int)offset.X - changeSize;
            int y = GameObject.Y + (int)offset.Y - changeSize;
            int width = (int)bounds.X + (2 * changeSize);
            int height = (int)bounds.Y + (2 * changeSize);
            BoundingBox = new Rectangle(x, y, width, height);
        }
    }

    public override void ChangeSize(int change)
    {
        this.changeSize = change;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (GOManager.Instance.IsDebugging)
        {
            // Get the rotated corners of the rectangle
            Vector2[] corners = GetRotatedCorners();

            // Draw lines between each corner to represent the edges of the rectangle
            DrawLine(spriteBatch, corners[0], corners[1], Color.Red); // Top edge
            DrawLine(spriteBatch, corners[1], corners[3], Color.Red); // Right edge
            DrawLine(spriteBatch, corners[3], corners[2], Color.Red); // Bottom edge
            DrawLine(spriteBatch, corners[2], corners[0], Color.Red); // Left edge
        }
    }

    private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 3f)
    {
        // Calculate the distance and angle between the two points
        Vector2 edge = end - start;
        float angle = (float)Math.Atan2(edge.Y, edge.X);

        // Draw a thicker line by adjusting the thickness parameter
        spriteBatch.Draw(_debugTexture, start, null, color * 0.5f, angle,
            Vector2.Zero, new Vector2(edge.Length(), thickness), SpriteEffects.None, 0.0f);
    }

    public override bool Intersects(Collider other)
    {
        if (other is BoxCollider box)
        {
            return CheckRotatedCollision(box);
        }
        else if (other is CircleCollider circle)
        {
            return CheckCircleCollision(circle);
        }
        return false;
    }

    private bool CheckRotatedCollision(BoxCollider other)
    {
        Vector2[] thisCorners = GetRotatedCorners();
        Vector2[] otherCorners = other.GetRotatedCorners();

        // Implement the SAT check
        return SATCollision(thisCorners, otherCorners);
    }

    public Vector2[] GetRotatedCorners()
    {
        // Calculate the four corners of the rotated bounding box
        Vector2 center = new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y);
        Vector2 halfExtents = new Vector2(BoundingBox.Width / 2, BoundingBox.Height / 2);

        Vector2 topLeft = new Vector2(-halfExtents.X, -halfExtents.Y);
        Vector2 topRight = new Vector2(halfExtents.X, -halfExtents.Y);
        Vector2 bottomLeft = new Vector2(-halfExtents.X, halfExtents.Y);
        Vector2 bottomRight = new Vector2(halfExtents.X, halfExtents.Y);

        topLeft = RotatePoint(topLeft, Rotation) + center;
        topRight = RotatePoint(topRight, Rotation) + center;
        bottomLeft = RotatePoint(bottomLeft, Rotation) + center;
        bottomRight = RotatePoint(bottomRight, Rotation) + center;

        return new[] { topLeft, topRight, bottomLeft, bottomRight };
    }

    private Vector2 RotatePoint(Vector2 point, float angle)
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2(point.X * cos - point.Y * sin, point.X * sin + point.Y * cos);
    }

    private bool SATCollision(Vector2[] thisCorners, Vector2[] otherCorners)
    {
        Vector2[] axesToTest = GetAxesToTest(thisCorners, otherCorners);

        foreach (Vector2 axis in axesToTest)
        {
            (float minA, float maxA) = ProjectPolygon(thisCorners, axis);
            (float minB, float maxB) = ProjectPolygon(otherCorners, axis);

            if (maxA < minB || maxB < minA)
            {
                return false; 
            }
        }

        return true;
    }

    private Vector2[] GetAxesToTest(Vector2[] thisCorners, Vector2[] otherCorners)
    {
        Vector2[] axes = new Vector2[4];

        axes[0] = GetEdgeNormal(thisCorners[0], thisCorners[1]);
        axes[1] = GetEdgeNormal(thisCorners[1], thisCorners[3]);

        axes[2] = GetEdgeNormal(otherCorners[0], otherCorners[1]);
        axes[3] = GetEdgeNormal(otherCorners[1], otherCorners[3]);

        return axes;
    }

    private Vector2 GetEdgeNormal(Vector2 point1, Vector2 point2)
    {
        Vector2 edge = point2 - point1;
        return new Vector2(-edge.Y, edge.X);
    }

    private (float, float) ProjectPolygon(Vector2[] corners, Vector2 axis)
    {
        float min = Vector2.Dot(corners[0], axis);
        float max = min;

        for (int i = 1; i < corners.Length; i++)
        {
            float projection = Vector2.Dot(corners[i], axis);
            if (projection < min)
            {
                min = projection;
            }
            else if (projection > max)
            {
                max = projection;
            }
        }

        return (min, max);
    }

    private bool CheckCircleCollision(CircleCollider circle)
    {
        Vector2 boxCenter = new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y);
        Vector2 circleCenter = circle.Center;
        Vector2 closestPoint = new Vector2(
            MathHelper.Clamp(circleCenter.X, BoundingBox.Left, BoundingBox.Right),
            MathHelper.Clamp(circleCenter.Y, BoundingBox.Top, BoundingBox.Bottom)
        );

        float distanceSquared = Vector2.DistanceSquared(circleCenter, closestPoint);
        return distanceSquared < circle.Radius * circle.Radius;
    }
}