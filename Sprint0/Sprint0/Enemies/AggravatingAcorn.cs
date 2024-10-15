using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    private bool movingRight;
    private float dropThreshold = 50f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("aggravatingAcornAnimation");
        speed = 200f;
        isFalling = false;
        dropPosition = Vector2.Zero;
        movingRight = false; // Initially move left
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {
            sRend.isFacingRight = !movingRight;

            // Move horizontally
            if (movingRight)
                GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            else
                GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Check if reached camera edge
            if (ReachedEdge())
                movingRight = !movingRight;

            // Check if player is underneath and start dropping
            if (PlayerIsUnderneath())
            {
                sRend.setAnimation("AcornDrop");
                sRend.spriteScale = 0.8f;
                isFalling = true;
                dropPosition = new Vector2(GameObject.X, GameObject.Y + 500); // Set the drop target position
            }
        }
        else
        {
            // Drop the acorn
            GameObject.Y += (int)(speed * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Destroy when reaching the drop position
            if (GameObject.Y >= dropPosition.Y)
            {
                GameObject.Destroy();
            }
        }
    }

    private bool PlayerIsUnderneath()
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y);
        return Math.Abs(GameObject.X - playerPosition.X) <= dropThreshold;
    }

    private bool ReachedEdge()
    {
        // Get camera bounds
        var cameraPosition = GOManager.Instance.Camera.Position;

        // Check if AggravatingAcorn has reached the edges of the camera
        if (GameObject.X < cameraPosition.X || GameObject.X > cameraPosition.X + 1000)
            return true;

        return false;
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
