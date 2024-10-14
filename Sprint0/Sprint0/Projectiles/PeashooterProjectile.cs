using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PeashooterProjectile : Projectile
{
    private float speed = 5f;
    private bool isFacingRight;

    public PeashooterProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
           spriteRenderer.isFacingRight = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        
        if (isFacingRight)
        {
            GameObject.Move((int)(speed), 0); // Move right
            
        }
        else
        {
            GameObject.Move((int)(-speed), 0); // Move left
        }

        if (isFacingRight && GameObject.X > 1200 || !isFacingRight && GameObject.X < 0)
        {
            GameObject.Destroy();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
