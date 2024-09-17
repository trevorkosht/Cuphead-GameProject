using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D


public class ToothyTerror : BaseEnemy
{
    public override void Move(GameTime gameTime)
    {
        // Jumping and chomping behavior
    }

    public override void Shoot()
    {
        // No shooting for Toothy Terror
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        base.Initialize(startPosition, hitPoints, texture);
    }

    public override void TakeDamage(int damage)
    {
        // Do nothing since it can't be defeated
    }
}
