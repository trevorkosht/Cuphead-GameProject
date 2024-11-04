using Cuphead.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class PeashooterProjectile : Projectile
{
    private float speed = 9f;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private ProjectileCollision projectileCollision;
    private bool collided;
    private float explosionTimer;
    private const float explosionDuration = 1.0f;
    private const string collisionAnimationName = "PeashooterExplosionAnimation";
    private Vector2 direction;

    public PeashooterProjectile(bool isFacingRight, SpriteRenderer spriteRenderer, float angleInDegrees)
    {
        collided = false;
        explosionTimer = 0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        //if (!isFacingRight)
        //{
        //   spriteRenderer.isFacingRight = false;
        //}
        float angleInRadians = MathHelper.ToRadians(angleInDegrees);
        direction = new Vector2((float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians));
    }
    public override void Update(GameTime gameTime)
    {
        
        if(collided)
        {
            explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (explosionTimer >= explosionDuration)
            {
                GameObject.Destroy();
                return;
            }
        } else { 
            if(GameObject != null)
            {
                if (isFacingRight)
                {
                    GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed)); // Move right

                }
                else
                {
                    GameObject.Move((int)(direction.X * -speed), (int)(direction.Y * speed)); // Move left
                }
            }

            collider = GameObject.GetComponent<Collider>();
            projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
            collided = projectileCollision.CollisionCheck();

            Camera camera = GOManager.Instance.Camera;
            if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X || GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
