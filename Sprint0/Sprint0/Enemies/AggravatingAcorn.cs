using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    GraphicsDevice _graphics;



    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(startPosition, hitPoints, texture, storage);
        base.setAnimation("aggravatingAcornAnimation");
        speed = 200f;  // Speed of horizontal movement
        isFalling = false;
        dropPosition = Vector2.Zero; // Will set when ready to fall
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {
            // Move horizontally across the screen at the top
            position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the player is underneath to trigger the fall
            if (PlayerIsUnderneath())
            {
                isFalling = true;
                dropPosition = new Vector2(position.X, position.Y + 500); // Drop to a target Y position
            }
        }
        else
        {
            // Fall straight down
            position.Y += speed * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Stop falling when reaching the ground (or a certain Y position)
            if (position.Y >= dropPosition.Y)
            {
                IsActive = false;  // Deactivate after the fall
            }
        }

    }

    public override void Shoot(GameTime gameTime)
    {
        // Aggravating Acorn doesn't shoot, so no implementation needed
    }

    private bool PlayerIsUnderneath()
    {
        // Logic to detect if the player is underneath the acorn
        return false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (HitPoints <= 0)
        {
            // Handle acorn being destroyed if it gets shot
            IsActive = false;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            base.Draw(spriteBatch);
            // Draw acorn sprite here
            //spriteBatch.Draw(spriteTexture, position, Color.White);
        }
    }
}
