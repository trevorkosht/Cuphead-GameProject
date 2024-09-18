using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class SporeProjectile
{
    private Vector2 position;
    private Vector2 targetPosition;
    private float speed;
    public bool IsActive { get; private set; }
    private Texture2D sporeTexture;
    private bool isPink; // Indicates if the spore is pink (parryable)

    public SporeProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture, bool isPink)
    {
        position = startPosition;
        this.targetPosition = targetPosition;
        sporeTexture = texture;
        speed = 150f; // Speed of the spore
        IsActive = true;
        this.isPink = isPink; // Set whether it's a pink spore
    }

    public void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Move towards the player (homing-like behavior)
            Vector2 direction = targetPosition - position;
            direction.Normalize();

            // Move the spore
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the spore hits the player or goes off-screen
            if (Vector2.Distance(position, targetPosition) < 10f) // Hit logic
            {
                IsActive = false; // Deactivate after hitting
            }

            // Additional logic for pink spores (parryable behavior could go here)
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            // Draw the spore
            spriteBatch.Draw(sporeTexture, position, Color.White);
        }
    }
}
