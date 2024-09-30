using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class RoundaboutProjectile : Projectile
{
    private float speed = 3f;
    private float gravity = 0.1f;
    private Vector2 velocity;
    private Vector2 playerLaunchPosition;
    private bool returning;
    private bool isFacingRight; // Determines the launch direction

    private float launchDuration = 1f; // Duration in seconds to fly outward before returning
    private float elapsedTime; // Tracks time since launch

    public RoundaboutProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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

        // Set initial velocity with an upward angle
        velocity = new Vector2(speed, -speed / 2);

        // Capture the player's position when the projectile is launched
        playerLaunchPosition = GOManager.Instance.Player.position;

        returning = false; // Initially, not returning
        elapsedTime = 0f; // Reset elapsed time

        if (!isFacingRight)
        {
            velocity.X = -velocity.X;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Update elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the projectile should start returning after the launch duration
            if (!returning && elapsedTime >= launchDuration)
            {
                returning = true; // Begin returning to the launch position
            }

            if (returning)
            {
                // Calculate direction back to the launch position
                Vector2 directionBack = Vector2.Normalize(playerLaunchPosition - GameObject.position);
                velocity = directionBack * speed; // Adjust velocity towards launch position
            }

            // Move the projectile using the calculated velocity
            GameObject.Move((int)(velocity.X), (int)(velocity.Y));

            // Deactivate the projectile if it returns to the launch position or goes off-screen
            if (returning && Vector2.Distance(GameObject.position, playerLaunchPosition) < 5f)
            {
                GameObject.Destroy();
            }
            else if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
