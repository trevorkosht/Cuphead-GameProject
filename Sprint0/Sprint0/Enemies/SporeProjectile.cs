using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SporeProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 position;
    private Vector2 targetPosition;
    private float speed;
    private Texture2D sporeTexture;
    private bool isPink, firstFrame;
    private Vector2 direction;

    // New variable to track the lifetime
    private float lifetime;
    private float elapsedTime;

    public SporeProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture, bool isPink)
    {
        position = startPosition;
        this.targetPosition = targetPosition;
        sporeTexture = texture;
        speed = 150f;
        this.isPink = isPink;
        direction = targetPosition - position;

        // Initialize lifetime and elapsed time
        lifetime = 5f; // 5 seconds
        elapsedTime = 0f;
    }

    public void Update(GameTime gameTime)
    {
        direction.Normalize();

        // Update the projectile's position
        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.X = (int)position.X;
        GameObject.Y = (int)position.Y;

        // Check for collision with the player
        if (Vector2.Distance(position, GOManager.Instance.Player.position) < 10f)
            GameObject.Destroy();

        // Update elapsed time
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Destroy the projectile if its lifetime has been exceeded
        if (elapsedTime >= lifetime)
            GameObject.Destroy();

        firstFrame = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //if (firstFrame)
        //    spriteBatch.Draw(sporeTexture, GameObject.position, Color.White);
    }
}
