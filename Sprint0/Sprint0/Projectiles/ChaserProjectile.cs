using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;

public class ChaserProjectile : Projectile
{
    private float speed = 5f;
    private GameObject targetEnemy;
    private Vector2 lastDirection;
    private bool isFacingRight;

    public ChaserProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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
        lastDirection = Vector2.UnitX;
        GameObject.AddComponent(new BoxCollider(new Vector2(80, 80), new Vector2(-72, -60), GOManager.Instance.GraphicsDevice));
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            if (targetEnemy == null || targetEnemy.destroyed)
            {
                targetEnemy = GOManager.Instance.currentEnemy;
            }

            Vector2 direction;

            if (targetEnemy != null)
            {
                direction = Vector2.Normalize(targetEnemy.position - GameObject.position);

                lastDirection = direction;

                if (Vector2.Distance(GameObject.position, targetEnemy.position) < 20f)
                {
                    GameObject.Destroy();
                    return;
                }
            }
            else
            {
                // No enemy detected, continue in the last known direction
                direction = lastDirection;
            }

            GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

            if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0)
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
