using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class DeadlyDaisy : BaseEnemy
{
    private float speed;
    private float jumpHeight;
    private bool isJumping;
    private Vector2 velocity;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("deadlyDaisyAnimation");
        speed = 300f;
        jumpHeight = 200f;
        isJumping = false;
        velocity = Vector2.Zero;
    }

    public override void Move(GameTime gameTime)
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y);

        // Move towards the player horizontally only
        Vector2 direction = new Vector2(playerPosition.X - GameObject.X, 0);
        float distance = Math.Abs(playerPosition.X - GameObject.X);
        float minDistance = 0.1f;

        if (distance > minDistance)
        {
            direction.Normalize();
            sRend.isFacingRight = playerPosition.X < GameObject.X;

            GameObject.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
        }

        // Check if the DeadlyDaisy needs to jump to reach the player
        if (NeedsToJump(playerPosition))
        {
            Jump();
        }

        // Apply gravity if jumping
        if (isJumping)
        {
            velocity.Y += 400f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameObject.Y += (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Check if we hit the ground
            if (IsGrounded())
            {
                isJumping = false;
                velocity.Y = 0;
            }
        }

    }

    private bool NeedsToJump(Vector2 playerPosition)
    {
        // The DeadlyDaisy jumps if the player is on a higher platform
        return GameObject.Y > playerPosition.Y && !isJumping && IsGrounded();
    }

    private void Jump()
    {
        isJumping = true;
        velocity.Y = -jumpHeight; // Apply upward force
        sRend.setAnimation("Spawn"); // Jumping animation
    }

    private bool IsGrounded()
    {
        // Check collision with blocks from BlockFactory to see if standing on the ground
        BoxCollider collider = GameObject.GetComponent<BoxCollider>();

        foreach (GameObject block in GOManager.Instance.allGOs)
        {
            if(block.type is null) continue;
            if (block.type.Contains("Platform") || block.type.Contains("Block"))
            {
                if (collider.Intersects(block.GetComponent<Collider>()))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public override void Shoot(GameTime gameTime)
    {
        // Implement shooting logic if needed
    }
}
