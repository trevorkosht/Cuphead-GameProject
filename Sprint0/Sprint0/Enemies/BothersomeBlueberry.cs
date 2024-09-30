using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class BothersomeBlueberry : BaseEnemy
{
    private Vector2 respawnPosition;   // Position where the blueberry will respawn after being knocked out
    private bool isKnockedOut;         // Whether the blueberry is knocked out
    private double respawnTimer;       // Timer for respawning
    private float speed;               // Speed of movement
    private bool movingRight;          // Whether the blueberry is moving to the right
    private float respawnDelay = 3.0f; // Delay in seconds before respawning

    

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("bothersomeBlueberryAnimation");
        respawnPosition = new Vector2(GameObject.X, GameObject.Y);
        speed = 300f; // Speed of horizontal movement
        isKnockedOut = false;
        movingRight = true; // Start by moving right
    }

    public override void Move(GameTime gameTime)
    {
        // Move left or right

        sRend.isFacingRight = !movingRight;
        if (movingRight)
            GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        else
            GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

        // Check for edges and reverse direction if necessary
        if (ReachedEdge())
        {
            movingRight = !movingRight; // Reverse direction
        }
    }

    public override void Shoot(GameTime gameTime)
    {
        // BothersomeBlueberry doesn't shoot, so no implementation needed
    }

    private bool ReachedEdge()
    {
        // Get the screen width from the graphics device viewport
        int screenWidth = 1280-144/2;

        // Check if the blueberry has reached the left or right edge of the screen
        if (GameObject.X <= 2 || GameObject.X >= screenWidth)
        {
            return true;
        }
        return false;
    }
}
