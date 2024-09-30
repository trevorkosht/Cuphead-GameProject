using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SpreadShotProjectile : Projectile
{
    private float speed = 500f; // Adjust speed based on game requirements
    private float angleSpread = 60f; // Spread angle between projectiles (e.g., 180 degrees / 3)
    private Vector2[] directions; // Array to hold the spread directions

    public SpreadShotProjectile(bool isFacingRight)
    {

        // Create three directions for the spread shot (adjust based on isFacingRight)
        directions = new Vector2[]
        {
            new Vector2((float)Math.Cos(MathHelper.ToRadians(-angleSpread)), (float)Math.Sin(MathHelper.ToRadians(-angleSpread))), // Top
            new Vector2(1, 0), // Middle (straight)
            new Vector2((float)Math.Cos(MathHelper.ToRadians(angleSpread)), (float)Math.Sin(MathHelper.ToRadians(angleSpread))) // Bottom
        };

        // Flip the direction vectors horizontally if facing left
        if (!isFacingRight)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                directions[i].X = -directions[i].X;
            }
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        // Set other properties if needed (texture, origin, etc.)
    }

    public override void Update(GameTime gameTime)
    {
        // Create each spread shot instance with its respective direction
        for (int i = 0; i < directions.Length; i++)
        {
            // Create a copy of this projectile in a specific direction
            GameObject spreadShot = new GameObject(GameObject.X, GameObject.Y);
            var spreadLogic = new SpreadShotInstance(directions[i] * speed); // Create instance with specific direction
            SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(spreadShot.X, spreadShot.Y, 144, 144), false);

            // Set up the animation and texture
            spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
            spriteRenderer.setAnimation("PurpleSporeAnimation");

            // Attach components to the spread shot instance
            spreadShot.AddComponent(spreadLogic);
            spreadLogic.Initialize(spriteTexture, textureStorage);
            spreadShot.AddComponent(spriteRenderer);

            // Add the spread shot to the game object manager
            GOManager.Instance.allGOs.Add(spreadShot);
            spriteRenderer.loadAllAnimations();
        }

        // Deactivate the main projectile after shooting spread shots
        GameObject.Destroy();
    }
}

// This class handles the movement of each individual spread shot projectile
public class SpreadShotInstance : Projectile
{
    private Vector2 direction;
    private float lifetime = 2.0f; // Projectile lifetime in seconds (for demo purposes)

    public SpreadShotInstance(Vector2 direction)
    {
        this.direction = direction;
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        // Set other properties if needed (texture, origin, etc.)
    }

    public override void Update(GameTime gameTime)
    {
        // Move the projectile based on direction and speed
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.Move((int)(direction.X * deltaTime), (int)(direction.Y * deltaTime));

        // Check if the projectile goes off-screen or exceeds lifetime (example bounds)
        lifetime -= deltaTime;
        if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0 || lifetime <= 0)
        {
            GameObject.Destroy();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
