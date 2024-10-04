﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CircleCollider : Collider
{
    public Vector2 Center { get; private set; }
    public float Radius { get; private set; }

    private Texture2D _debugTexture;
    public Vector2 offset { get; set; }

    public CircleCollider(float radius, Vector2 offset, GraphicsDevice graphicsDevice)
    {
        this.offset = offset;
        Radius = radius;
        _debugTexture = new Texture2D(graphicsDevice, 1, 1);
        _debugTexture = CreateCircleTexture((int)Radius * 2, graphicsDevice);
    }

    public override void Update(GameTime gameTime)
    {
        if (GameObject != null)
        {
            // Center the circle collider around the GameObject's position
            Center = GameObject.position - offset;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw a translucent blue circle to visualize the collider
        if (GOManager.Instance.IsDebugging)
        {
            Rectangle circleBounds = new Rectangle(
            (int)(Center.X - Radius),
            (int)(Center.Y - Radius),
            (int)(Radius * 2),
            (int)(Radius * 2)
        );
            spriteBatch.Draw(_debugTexture, circleBounds, Color.Blue * 0.5f);
        }
    }

    public override bool Intersects(Collider other)
    {
        if (other is CircleCollider circle)
        {
            return CheckCircleCollision(circle);
        }
        else if (other is BoxCollider box)
        {
            return box.Intersects(this); // Use BoxCollider's method to check collision
        }
        return false;
    }

    private bool CheckCircleCollision(CircleCollider other)
    {
        float distanceSquared = Vector2.DistanceSquared(this.Center, other.Center);
        float combinedRadii = this.Radius + other.Radius;
        return distanceSquared < combinedRadii * combinedRadii;
    }

    private Texture2D CreateCircleTexture(int diameter, GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

        float radius = diameter / 2f;
        float radiusSquared = radius * radius;

        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                int index = y * diameter + x;
                // Calculate the distance from the center of the texture
                float distanceSquared = (x - radius) * (x - radius) + (y - radius) * (y - radius);

                // If the pixel is within the circle, color it white, otherwise make it transparent
                if (distanceSquared <= radiusSquared)
                {
                    colorData[index] = Color.White;
                }
                else
                {
                    colorData[index] = Color.Transparent;
                }
            }
        }

        texture.SetData(colorData);
        return texture;
    }
}