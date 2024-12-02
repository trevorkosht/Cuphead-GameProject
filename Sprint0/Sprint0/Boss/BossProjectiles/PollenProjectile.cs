using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class PollenProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private float amplitude; // Height of the sine wave
    private float frequency; // Frequency of the sine wave
    private float phaseOffset; // Keeps track of the wave offset for smooth animation
    private Texture2D texture;
    private bool isPink; // Indicates if the projectile is pink or not
    private int damage = 20;

    public PollenProjectile(Vector2 startPosition, Texture2D texture, bool isPink, float speed)
    {
        this.texture = texture;
        this.isPink = isPink;
        amplitude = 50f; // Adjust for a higher/lower wave
        frequency = 5f; // Adjust for tighter/wider waves
        velocity = new Vector2(-speed, 0);
        phaseOffset = startPosition.Y;
    }

    public void Update(GameTime gameTime)
    {
        float time = (float)gameTime.TotalGameTime.TotalSeconds;
        GameObject.Move((int)velocity.X, (int)(amplitude * MathF.Sin(frequency * time) + phaseOffset - GameObject.Y));


    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the SpriteRenderer
    }
}