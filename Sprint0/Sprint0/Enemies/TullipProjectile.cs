using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TullipProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private float gravity = 0.3f;
    private float speed;
    private Vector2 targetPosition;

    public TullipProjectile(Vector2 startPosition)
    {
        speed = 5f;
        targetPosition = GOManager.Instance.Player.position;

        Vector2 direction = targetPosition - startPosition;
        direction.Normalize();

        // Adjust vertical component to ensure a consistent jump
        float verticalSpeed = -Math.Max(Math.Abs(direction.Y * speed), 2f); // Ensuring a minimum upward speed

        velocity = new Vector2(direction.X * speed, verticalSpeed);
    }

    public void Update(GameTime gameTime)
    {
        velocity.Y += gravity;

        GameObject.Move((int)velocity.X, (int)velocity.Y);

        if (GameObject.Y > 600)
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
