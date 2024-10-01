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
    private bool isPink, firstFrame;
    Vector2 direction;

    public SporeProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture, bool isPink)
    {
        position = startPosition;
        this.targetPosition = targetPosition;
        sporeTexture = texture;
        speed = 150f;
        this.isPink = isPink;
        direction = targetPosition - position;
    }

    public void Update(GameTime gameTime)
    {
        direction.Normalize();

        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.X = (int)position.X;
        GameObject.Y = (int)position.Y;

        if (Vector2.Distance(position, GOManager.Instance.Player.position) < 10f)
            GameObject.Destroy();
        else if(GameObject.X < 0 || GameObject.X > 1200)
            GameObject.Destroy();
        else if(GameObject.Y < 0 || GameObject.Y > 700)
            GameObject.Destroy();
        firstFrame = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (firstFrame)
            spriteBatch.Draw(sporeTexture, GameObject.position, Color.White);
    }
}
