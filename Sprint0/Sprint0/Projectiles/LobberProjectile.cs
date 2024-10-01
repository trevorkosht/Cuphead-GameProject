using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LobberProjectile : Projectile
{
    private float speed = 5f;
    private float gravity = 0.2f;
    private Vector2 velocity;
    private bool isFacingRight;
    private int bounceCount = 0;
    private int maxBounces = 3;

    public LobberProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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
        velocity = new Vector2(speed, -speed);
    }

    public override void Update(GameTime gameTime)
    {
        velocity.Y += gravity;

        if (isFacingRight)
        {
            GameObject.Move((int)velocity.X, (int)velocity.Y);
        }
        else
        {
            GameObject.Move((int)(-velocity.X), (int)velocity.Y);
        }

        if (GameObject.Y > 600)
        {
            velocity.Y = -velocity.Y * 0.7f;
            bounceCount++;
            GameObject.MoveToPosition(GameObject.X, 600);

            if (bounceCount >= maxBounces)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
