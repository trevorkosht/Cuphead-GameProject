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
    Vector2 pos;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("deadlyDaisyAnimation");
        speed = 300f;  // Speed of movement towards the player
        jumpHeight = 200f;  // How high the daisy can jump
        isJumping = false;
        velocity = Vector2.Zero;  // Start with no velocity
    }

    public override void Move(GameTime gameTime)
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y); // Assuming this meant the character player and not game window

        Vector2 direction = playerPosition - GameObject.position;
        float distance = direction.Length();

        // Minimum distance to prevent "teleportation" when very close to the player
        float minDistance = 0.1f;

        if (distance > minDistance)
        {
            direction.Normalize(); // Only normalize when the distance is large enough to avoid errors

            // Face right if the player is to the right, otherwise face left
            sRend.isFacingRight = playerPosition.X < GameObject.X;

            // Move towards the player
            GameObject.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            GameObject.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
        }


        // If the Daisy is near a ledge, simulate a jump
        if (NeedsToJump(playerPosition))
        {
            Jump();
        }

        // Apply jump movement if jumping
        if (isJumping)
        {
            velocity.Y += 400f * (float)gameTime.ElapsedGameTime.TotalSeconds; // Simulate gravity
            GameObject.Y += (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Stop jumping if the Daisy has reached the ground or max height
            if (GameObject.Y >= jumpHeight)
            {
                isJumping = false;
                velocity.Y = 0;
            }
        }
    }

    private bool NeedsToJump(Vector2 playerPosition)
    {
        // Simple check if the player is above and the Daisy needs to jump to reach them
        return GameObject.Y > playerPosition.Y && !isJumping;
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
}
