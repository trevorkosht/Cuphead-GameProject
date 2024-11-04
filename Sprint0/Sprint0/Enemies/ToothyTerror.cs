using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class ToothyTerror : BaseEnemy
{
    private float jumpHeight;
    private float gravity;
    private bool isJumping;
    private float jumpSpeed;
    private float startYPosition;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("toothyTerrorAnimation");
        jumpHeight = 450f; // Can be modified to make jumps higher
        gravity = 400f;
        isJumping = true;
        startYPosition = GameObject.Y;

        // Set the initial jump speed based on jumpHeight and gravity
        jumpSpeed = (float)Math.Sqrt(2 * gravity * jumpHeight);
    }

    public override void Move(GameTime gameTime)
    {
        if (isJumping)
        {
            sRend.setAnimation("Attack");

            // Move up while jumping
            GameObject.Y -= (int)(jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Reduce the jump speed by gravity over time
            jumpSpeed -= gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If jumpSpeed reaches zero or below, the enemy starts falling
            if (jumpSpeed <= 0)
            {
                isJumping = false;
            }
        }
        else
        {
            sRend.setAnimation("toothyTerrorAnimation");
            if (Math.Abs(GameObject.X - player.X) <= 1000)
            {
                GOManager.Instance.audioManager.getInstance("ToothyTerrorBite").Play();
            }

            // Move down while falling
            GameObject.Y += (int)(gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // When back on the ground, reset the jump
            if (GameObject.Y >= startYPosition)
            {
                if (Math.Abs(GameObject.X - player.X) <= 1000)
                {
                    GOManager.Instance.audioManager.getInstance("ToothyTerrorUp").Play();
                }
                GameObject.Y = (int)startYPosition;

                // Reset jump speed based on jumpHeight
                jumpSpeed = (float)Math.Sqrt(2 * gravity * jumpHeight);
                isJumping = true;
            }
        }
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
