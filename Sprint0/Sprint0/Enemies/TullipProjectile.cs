using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TullipProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private int airTime = 75;
    private float gravity = 0.5f;
    private float speed;
    private Point targetPosition;
    private Texture2D hitVFX;
    private int damage = 50;

    public TullipProjectile(Vector2 startPosition)
    {
        hitVFX = GOManager.Instance.textureStorage.GetTexture("TulipHitVFX");

        targetPosition = GOManager.Instance.Player.GetComponent<BoxCollider>().BoundingBox.Center;

        float verticalSpeed = Math.Abs(((targetPosition.Y - startPosition.Y) - gravity * airTime * airTime / 2) / (airTime));
        float horizontalSpeed = (startPosition.X - targetPosition.X) / airTime;

        velocity = new Vector2(-horizontalSpeed, -verticalSpeed);
    }

    public void Update(GameTime gameTime)
    {
        velocity.Y += gravity;
        BoxCollider playerCollider = GOManager.Instance.Player.GetComponent<BoxCollider>();
        Rectangle playerBounds = playerCollider.BoundingBox;
        GameObject.type = "NPCProjectile";

        GameObject.Move((int)velocity.X, (int)velocity.Y);

        bool hitPlatform = false;
        Rectangle hitPlatformBounds = new Rectangle();
        foreach (GameObject GO in GOManager.Instance.allGOs) {
            if (GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Hill") || GO.type.Contains("Slope") || GO.type.Contains("Log"))) {
                if (GO.GetComponent<BoxCollider>().Intersects(GameObject.GetComponent<CircleCollider>())) {
                    hitPlatformBounds = GO.GetComponent<BoxCollider>().BoundingBox;
                    hitPlatform = true;
                }
            }

        }
        if (hitPlatform) {
            Rectangle vfxDestRectangle = new Rectangle((int)GameObject.GetComponent<CircleCollider>().Center.X - 144,  hitPlatformBounds.Top - 288,144, 144);
            VisualEffectFactory.createVisualEffect(vfxDestRectangle, hitVFX, 2, 23, 2.0f, true);
            GameObject.Destroy(); 
        }
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
