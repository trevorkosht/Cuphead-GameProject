using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        velocity = new Vector2(speed, -speed / 2);

        playerLaunchPosition = GOManager.Instance.Player.position;

        returning = false;
        elapsedTime = 0f;

        if (!isFacingRight)
        {
            velocity.X = -velocity.X;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!returning && elapsedTime >= launchDuration)
            {
                returning = true;
            }

            if (returning)
            {
                Vector2 directionBack = Vector2.Normalize(playerLaunchPosition - GameObject.position);
                velocity = directionBack * speed;
            }

            GameObject.Move((int)(velocity.X), (int)(velocity.Y));

            if (returning && Vector2.Distance(GameObject.position, playerLaunchPosition) < 5f)
            {
                GameObject.Destroy();
            }
            else if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0)
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
