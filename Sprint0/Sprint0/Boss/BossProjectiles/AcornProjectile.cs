using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AcornProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private const float Speed = 600f; // Adjust the speed as necessary
    private GameObject player; // Reference to the player

    public AcornProjectile(Vector2 startPosition, GameObject player)
    {
        this.player = player;

        // Calculate the center of the player by adding an offset to their position
        Vector2 playerCenter = new Vector2(player.position.X, player.position.Y + 20);

        // Calculate the direction to the player's center
        Vector2 direction = playerCenter - startPosition;
        direction.Normalize();

        // Set velocity based on the calculated direction and speed
        velocity = direction * Speed;
    }


    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Update position based on velocity
        GameObject.Move((int)(velocity.X * delta), (int)(velocity.Y * delta));

        // Destroy the acorn if it goes off-screen
        if (GameObject.X < -200 || GameObject.Y > 600) // Replace 600 with dynamic screen height if needed
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
