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

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        // Adding the BoxCollider component once the projectile is initialized
        GameObject.AddComponent(new BoxCollider(new Vector2(80, 80), new Vector2(-72, -60), GOManager.Instance.GraphicsDevice));
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Move the projectile based on its facing direction
            if (isFacingRight)
            {
                GameObject.Move((int)(speed), 0); // Move right
            }
            else
            {
                GameObject.Move((int)(-speed), 0); // Move left
            }

            // Destroy the projectile if it goes outside camera bounds
            Camera camera = GOManager.Instance.Camera;
            if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X)
            {
                GameObject.Destroy();
            }

            // Check for collisions with enemies and other objects
            ProjectileCollisionHandler.HandleCollision(this);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by SpriteRenderer
    }
}
