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
    private Vector2 targetPosition;

    public TullipProjectile(Vector2 startPosition)
    {
        targetPosition = GOManager.Instance.Player.position;

        float verticalSpeed = Math.Abs(((targetPosition.Y - startPosition.Y) - gravity * airTime * airTime / 2) / (airTime));
        float horizontalSpeed = (startPosition.X - targetPosition.X) / airTime;

        velocity = new Vector2(-horizontalSpeed, -verticalSpeed);
    }

    public void Update(GameTime gameTime)
    {
        velocity.Y += gravity;

        GameObject.Move((int)velocity.X, (int)velocity.Y);

        if (GameObject.GetComponent<CircleCollider>().Intersects(GOManager.Instance.Player.GetComponent<BoxCollider>()))
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
