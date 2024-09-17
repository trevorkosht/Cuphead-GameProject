using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D


public class MurderousMushroom : BaseEnemy
{
    private bool isHidden;
    private double shootCooldown;

    public override void Move(GameTime gameTime)
    {
        // Stationary, so no movement needed
    }

    public override void Shoot()
    {
        if (!isHidden)
        {
            shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (shootCooldown <= 0)
            {
                // Fire spores
                shootCooldown = 2.0; // Reset cooldown
            }
        }
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        base.Initialize(startPosition, hitPoints, texture);
        isHidden = false;
        shootCooldown = 2.0;
    }

    public void HideUnderCap()
    {
        isHidden = true;
    }

    public void EmergeFromCap()
    {
        isHidden = false;
    }

    public override void TakeDamage(int damage)
    {
        if (!isHidden)
        {
            base.TakeDamage(damage);
        }
    }
}

