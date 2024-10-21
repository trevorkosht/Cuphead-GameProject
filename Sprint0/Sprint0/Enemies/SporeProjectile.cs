using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SporeProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 position;
    private Vector2 targetPosition;
    private float speed;
    private Texture2D sporeTexture;
    private Texture2D trailTexture = GOManager.Instance.textureStorage.GetTexture("SporeTrailVFX");
    private Texture2D explosionTexture = GOManager.Instance.textureStorage.GetTexture("SporeExplosionVFX");
    private bool isPink, firstFrame;
    private Vector2 direction;

    // New variable to track the lifetime
    private float lifetime;
    private float elapsedTime;

    public SporeProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture, bool isPink)
    {
        position = startPosition;
        this.targetPosition = targetPosition;
        sporeTexture = texture;
        speed = 150f;
        this.isPink = isPink;
        direction = targetPosition - position;

        // Initialize lifetime and elapsed time
        lifetime = 5f; // 5 seconds
        elapsedTime = 0f;
    }

    public void Update(GameTime gameTime)
    {
        direction.Normalize();
        GameObject.type = "NPCProjectile";

        // Update the projectile's position
        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.X = (int)position.X;
        GameObject.Y = (int)position.Y;
        

        Random rand = new Random();

        if(rand.Next(1, 40) == 1) {
            VisualEffectFactory.createVisualEffect(new Rectangle(GameObject.X, GameObject.Y - 36, 144, 144), trailTexture, 3, 8, 1.0f, true);
        }

        // Update elapsed time
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Check for collision with the player and end of projectile lifetime
        if ((Vector2.Distance(position, GOManager.Instance.Player.position) < 30f) || elapsedTime >= lifetime) {
            VisualEffectFactory.createVisualEffect(new Rectangle(GameObject.X, GameObject.Y - 36, 144, 144), explosionTexture, 2, 12, 1.0f, true);
            GameObject.Destroy();
        }

        firstFrame = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //if (firstFrame)
        //    spriteBatch.Draw(sporeTexture, GameObject.position, Color.White);
    }
}
