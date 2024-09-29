using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class SporeProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 position;
    private Vector2 targetPosition;
    private float speed;
    private Texture2D sporeTexture;
    private bool isPink, firstFrame; // Indicates if the spore is pink (parryable)
    Vector2 direction;

    public SporeProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture, bool isPink)
    {
        position = startPosition;
        this.targetPosition = targetPosition;
        sporeTexture = texture;
        speed = 150f; // Speed of the spore
        this.isPink = isPink; // Set whether it's a pink spore
        direction = targetPosition - position;
    }

    public void Update(GameTime gameTime)
    {
        // Move towards the player (homing-like behavior)
        direction.Normalize();

        // Move the spore
        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.X = (int)position.X;
        GameObject.Y = (int)position.Y;

        // Check if the spore hits the player (or if projectile is off screen)
        if (Vector2.Distance(position, GOManager.Instance.Player.position) < 10f) // Hit logic
            GameObject.Destroy();
        else if(GameObject.X < 0 || GameObject.X > 1200)
            GameObject.Destroy();
        else if(GameObject.Y < 0 || GameObject.Y > 700)
            GameObject.Destroy();
        firstFrame = true;
        // Additional logic for pink spores (parryable behavior could go here)
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw the spore
        if (firstFrame)
            spriteBatch.Draw(sporeTexture, GameObject.position, Color.White);
    }
}
