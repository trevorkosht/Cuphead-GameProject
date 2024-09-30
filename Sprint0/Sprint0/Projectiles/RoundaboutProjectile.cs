using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class RoundaboutProjectile : Projectile
{
    private float speed = 3f;
    private float direction = 1f; // 1 for right, -1 for left
    private float maxDistance = 300f;
    private Vector2 startPosition;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        startPosition = GameObject.position; // Store the starting position
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            GameObject.Move((int)(speed * direction), 0); // Move horizontally

            // Change direction if it reaches max distance
            if (Math.Abs(GameObject.position.X - startPosition.X) >= maxDistance)
            {
                direction *= -1; // Reverse direction
            }

            // Deactivate if it goes off-screen
            if (GameObject.X > 800 || GameObject.X < 0)
            {
                IsActive = false;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // DO Nothing, Handled by Sprite Renderer
    }
}
