using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RoundaboutProjectile : Projectile
{
    private float speed = 3f;
    private Vector2 velocity;
    private Vector2 playerLaunchPosition;
    private bool returning;
    private bool isFacingRight;

    private float launchDuration = 1f;
    private float elapsedTime;

    public RoundaboutProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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

        // Set initial horizontal velocity
        velocity = new Vector2(isFacingRight ? speed : -speed, 0);

        playerLaunchPosition = GOManager.Instance.Player.position;

        returning = false;
        elapsedTime = 0f;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Switch to returning phase after the launch duration
            if (!returning && elapsedTime >= launchDuration)
            {
                returning = true;

                // In returning phase, move diagonally upwards while traveling back
                velocity = new Vector2(isFacingRight ? -speed : speed, -speed / 2);
            }

            // Update the projectile's position based on its velocity
            GameObject.Move((int)velocity.X, (int)velocity.Y);

            // Destroy when it goes off-screen in returning phase
            if (returning && (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y < 0 || GameObject.Y > 800))
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
