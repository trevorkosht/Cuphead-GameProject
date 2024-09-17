using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteRenderer : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    public Texture2D texture { get; set; }
    private Transform transform;

    public SpriteRenderer(Texture2D texture)
    {
        this.texture = texture;
    }

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;
        if(transform == null)
            transform = GameObject.GetComponent<Transform>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!enabled) return;

        if (transform != null)
        {
            spriteBatch.Draw(texture, transform.Position, null, Color.White, transform.Rotation,
                             new Vector2(texture.Width / 2, texture.Height / 2),
                             transform.Scale, SpriteEffects.None, 0f);
        }
    }
}