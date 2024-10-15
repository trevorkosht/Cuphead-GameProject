using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    private float dropThreshold = 50f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("aggravatingAcornAnimation");
        speed = 200f;
        isFalling = false;
        dropPosition = Vector2.Zero;
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {
            sRend.isFacingRight = true; // Always face left

            // Move left continuously
            GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

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

    public override void Shoot(GameTime gameTime)
    {
    }
}
