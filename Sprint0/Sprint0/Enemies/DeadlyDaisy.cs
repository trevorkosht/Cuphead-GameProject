using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D


public class DeadlyDaisy : BaseEnemy
{
    public override void Move(GameTime gameTime)
    {
        // Chase the player logic
    }

    public override void Shoot()
    {
        // Deadly Daisy doesn't shoot, so this can be empty
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        base.Initialize(startPosition, hitPoints, texture);
    }
}
