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

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(startPosition, hitPoints, texture, storage);
        respawnPosition = startPosition;
        speed = 150f; // Speed of horizontal movement
        isKnockedOut = false;
        movingRight = true; // Start by moving right
    }

    public override void Move(GameTime gameTime)
    {
        if (isKnockedOut)
            return; // No movement when knocked out

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Move left or right
        if (movingRight)
        {
            position.X += speed * deltaTime;
        }
        else
        {
            position.X -= speed * deltaTime;
        }

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
        return false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (HitPoints <= 0)
        {
            KnockOut();
        }
    }

    private void KnockOut()
    {
        isKnockedOut = true;
        respawnTimer = respawnDelay; // Start respawn timer
    }

    public override void Update(GameTime gameTime)
    {
        if (isKnockedOut)
        {
            // Handle respawn logic
            respawnTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (respawnTimer <= 0)
            {
                Respawn();
            }
        }
        else
        {
            // Update movement
            Move(gameTime);
        }
    }

    private void Respawn()
    {
        isKnockedOut = false;
        HitPoints = 2;  // Reset hit points (or whatever the initial HP is)
        position = respawnPosition;  // Respawn at the same position it died
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            // Draw the blueberry sprite if active (not knocked out)
            spriteBatch.Draw(spriteTexture, position, Color.White);
        }
    }
}
