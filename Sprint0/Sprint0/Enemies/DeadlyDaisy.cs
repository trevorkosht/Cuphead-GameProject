using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using Sprint0;
using System;                           // For Math

public class DeadlyDaisy : BaseEnemy
{
    private float speed;
    private float jumpHeight;
    private bool isJumping;
    private Vector2 velocity;

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(startPosition, hitPoints, texture, storage);
        base.setAnimation("deadlyDaisyAnimation");
        speed = 300f;  // Speed of movement towards the player
        jumpHeight = 200f;  // How high the daisy can jump
        isJumping = false;
        velocity = Vector2.Zero;  // Start with no velocity
    }

    public override void Move(GameTime gameTime)
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y); // Assuming this meant the character player and not game window

        // Calculate direction towards the player
        Vector2 direction = playerPosition - position;
        direction.Normalize();

        // Face right if the player is to the right, otherwise face left
        if (playerPosition.X > position.X)
        {
            base.isFacingRight = false; // Face right
        }
        else
        {
            base.isFacingRight = true; // Face left
        }

        // Move horizontally towards the player
        position.X += direction.X * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.Y += direction.Y * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // If the Daisy is near a ledge, simulate a jump
        if (NeedsToJump(playerPosition))
        {
            Jump();
        }

        // Apply jump movement if jumping
        if (isJumping)
        {
            velocity.Y += 400f * (float)gameTime.ElapsedGameTime.TotalSeconds; // Simulate gravity
            position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Stop jumping if the Daisy has reached the ground or max height
            if (position.Y >= jumpHeight)
            {
                isJumping = false;
                velocity.Y = 0;
            }
        }
    }

    private bool NeedsToJump(Vector2 playerPosition)
    {
        // Simple check if the player is above and the Daisy needs to jump to reach them
        return position.Y > playerPosition.Y && !isJumping;
    }

    private void Jump()
    {
        isJumping = true;
        velocity.Y = -jumpHeight;  // Set upward velocity
    }

    public override void Shoot(GameTime gameTime)
    {
        // Deadly Daisy doesn't shoot
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (HitPoints <= 0)
        {
            IsActive = false;  // Deactivate when hit
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            // Draw Deadly Daisy sprite here
            base.Draw(spriteBatch);
        }
    }
}
