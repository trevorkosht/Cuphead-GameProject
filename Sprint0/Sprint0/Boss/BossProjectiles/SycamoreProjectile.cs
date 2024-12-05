using Cuphead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SycamoreProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private bool returning;
    private float speed = 500f;

    public SycamoreProjectile(Vector2 startPosition)
    {
        velocity = new Vector2(-speed, 0);
        returning = false;
    }

    public void Update(GameTime gameTime)
    {
        GOManager.Instance.audioManager.getInstance("BoomerangLoop").Play();
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!returning)
        {
            GameObject.Move((int)(velocity.X * delta), 0);

            if (GameObject.X <= -400)
            {
                returning = true;

                velocity = new Vector2(speed, 0);
                GameObject.Y += 200;
            }
        }
        else
        {
            GameObject.Move((int)(velocity.X * delta), 0);

            if (GameObject.X > 800)
            {
                GameObject.Destroy();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
