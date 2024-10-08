﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;

public class BoxCollider : Collider
{
    public Rectangle BoundingBox { get; private set; }

    private Texture2D _debugTexture;
    public Vector2 offset { get; set; }
    public Vector2 bounds { get; set; } // Width = X and Height = Y


    public BoxCollider(Vector2 bounds, Vector2 offset, GraphicsDevice graphicsDevice)
    {
        this.offset = offset;
        this.bounds = bounds;
        BoundingBox = new Rectangle(0, 0, (int)bounds.X, (int)bounds.Y);
        _debugTexture = new Texture2D(graphicsDevice, 1, 1);
        _debugTexture.SetData(new[] { Color.White }); // For drawing the debug rectangle
    }

    public override void Update(GameTime gameTime)
    {
        if (GameObject != null)
        {
            
            BoundingBox = new Rectangle(GameObject.X + (int)offset.X, GameObject.Y + (int)offset.Y, (int)bounds.X, (int)bounds.Y);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw a translucent red rectangle to visualize the bounds of the collider
        if (GOManager.Instance.IsDebugging)
        {
            spriteBatch.Draw(_debugTexture, BoundingBox, Color.Red * 0.5f);
        }
    }

    public override bool Intersects(Collider other)
    {
        if (other is BoxCollider box)
        {
            return BoundingBox.Intersects(box.BoundingBox);
        }
        else if (other is CircleCollider circle)
        {
            return CheckCircleCollision(circle);
        }
        return false;
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