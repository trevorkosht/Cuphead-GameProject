using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LobberProjectile : Projectile
{
    private float speed = 5f;
    private float gravity = 0.2f;
    private Vector2 velocity;
    private bool isFacingRight;
    private int bounceCount = 0; // Track the number of bounces
    private int maxBounces = 3;  // Maximum number of bounces before destroying

    public LobberProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
            spriteRenderer.isFacingRight = false;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        velocity = new Vector2(speed, -speed); // Initial upward velocity
    }

    public override void Update(GameTime gameTime)
    {
        velocity.Y += gravity; // Apply gravity to the Y velocity for a falling effect

        // Move the projectile horizontally based on the facing direction and update position with velocity
        if (isFacingRight)
        {
            GameObject.Move((int)velocity.X, (int)velocity.Y);
        }
        else
        {
            GameObject.Move((int)(-velocity.X), (int)velocity.Y);
        }

        // Check if the projectile hits the bottom of the screen (ground level)
        if (GameObject.Y > 600) // Assuming 600 is the ground level or screen bottom
        {
            // Reverse the Y velocity to simulate a bounce
            velocity.Y = -velocity.Y * 0.7f; // Reduce velocity to simulate energy loss after bounce
            bounceCount++; // Increment the bounce counter
            GameObject.MoveToPosition(GameObject.X, 600); // Set Y position to ground level to avoid sticking below

            // Destroy the projectile after it reaches the maximum number of bounces
            if (bounceCount >= maxBounces)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
