using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AcornProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private const float Speed = 600f;
    private GameObject player;

    public AcornProjectile(Vector2 startPosition, GameObject player)
    {
        this.player = player;

        Vector2 playerCenter = new Vector2(player.position.X, player.position.Y + 20);

        Vector2 direction = playerCenter - startPosition;
        direction.Normalize();

        velocity = direction * Speed;
    }


    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        GameObject.Move((int)(velocity.X * delta), (int)(velocity.Y * delta));

        if (GameObject.X < -200 || GameObject.Y > 600)
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
