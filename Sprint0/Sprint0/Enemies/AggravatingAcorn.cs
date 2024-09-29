using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    private bool movingRight;            // Controls direction of movement
    private float dropThreshold = 50f;   // Threshold to detect player proximity for drop

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("aggravatingAcornAnimation");
        speed = 200f;  // Speed of horizontal movement
        isFalling = false;
        dropPosition = Vector2.Zero;     // Will set when ready to fall
        movingRight = true;              // Start by moving right
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {


            // Move left or right
            sRend.isFacingRight = !movingRight;
            if (movingRight)
                GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            else
                GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);


            // Check for screen edges and reverse direction
            if (ReachedEdge())
                movingRight = !movingRight;

            // Check if the player is underneath to trigger the fall
            if (PlayerIsUnderneath())
            {
                isFalling = true;
                dropPosition = new Vector2(GameObject.X, GameObject.Y + 500); // Drop to a target Y position
            }
        }
        else
        {
            // Fall straight down
            GameObject.Y += (int)(speed * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Stop falling when reaching the ground (or a certain Y position)
            if (GameObject.Y >= dropPosition.Y)
            {
                GameObject.Destroy();
            }
        }
    }

    private bool PlayerIsUnderneath()
    {
        // Assuming there's a global player object with a position (player.X, player.Y)
        Vector2 playerPosition = new Vector2(player.X, player.Y);

        // Check if the acorn's X is within the drop threshold of the player's X
        return Math.Abs(GameObject.X - playerPosition.X) <= dropThreshold;
    }

    private bool ReachedEdge()
    {
        // Get the screen width from the graphics device viewport
        int screenWidth = 1280 - 144/2; //144 is sprite width /2 is origin

        // Check if the blueberry has reached the left or right edge of the screen
        if (GameObject.X < 0 || GameObject.X > screenWidth)
            return true;
        return false;
    }

    public override void Shoot(GameTime gameTime)
    {
        // Aggravating Acorn doesn't shoot
    }
}
