using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class FlowerProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 position;
    private Vector2 targetPosition;
    private float speed;
    private Texture2D hitVFXTexture = GOManager.Instance.textureStorage.GetTexture("MiniFlowerHitVFX");
    private Vector2 direction;

    public FlowerProjectile(Vector2 startPosition)
    {
        position = startPosition;
        this.targetPosition.X = GOManager.Instance.Player.GetComponent<BoxCollider>().BoundingBox.Center.X;
        this.targetPosition.Y = GOManager.Instance.Player.GetComponent<BoxCollider>().BoundingBox.Center.Y;
        speed = 200f;
        direction = targetPosition - position;

    }

    public void Update(GameTime gameTime)
    {
        direction.Normalize();
        GameObject.type = "NPCProjectile";

        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.X = (int)position.X;
        GameObject.Y = (int)position.Y;


        Random rand = new Random();

        if (GameObject.GetComponent<CircleCollider>().Intersects(GOManager.Instance.Player.GetComponent<BoxCollider>()) || GameObject.Y >= 600)
        {
            VisualEffectFactory.createVisualEffect(new Rectangle(GameObject.X - 44, GameObject.Y - 44, 132, 132), hitVFXTexture, 2, 4, 1.0f, true);

            GameObject.Destroy();

        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {

    }
}
