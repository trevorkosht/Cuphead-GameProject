using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D

public class ToothyTerror : BaseEnemy
{
    private float jumpHeight;
    private float gravity;
    private bool isJumping;
    private float jumpSpeed;
    private float startYPosition; // Starting Y position for jumping

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("toothyTerrorAnimation");
        jumpHeight = 150f; // The height it jumps up
        gravity = 300f;    // Gravity to pull it back down
        isJumping = true;  // Start with the ToothyTerror jumping out of the pit
        jumpSpeed = 250f;  // Initial jump speed
        startYPosition = GameObject.Y; // Save the starting Y position
    }

    public override void Move(GameTime gameTime)
    {
        if (isJumping)
        {
            // Handle upward jump
            GameObject.Y -= (int)(jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Slow down the speed as it ascends due to gravity
            jumpSpeed -= gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Once it reaches the peak of the jump, start falling down
            if (jumpSpeed <= 0)
            {
                isJumping = false;
            }
        }
        else
        {
            // Handle falling down back to the pit
            GameObject.Y += (int)(gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // If it reaches the starting Y position, reset to jump again
            if (GameObject.Y >= startYPosition)
            {
                GameObject.Y = (int)startYPosition; // Correct any overshooting
                jumpSpeed = 250f;           // Reset jump speed for the next jump
                isJumping = true;           // Start the next jump
            }
        }
    }

    public override void Shoot(GameTime gameTime)
    {
        // Toothy Terror doesn't shoot
    }
}
