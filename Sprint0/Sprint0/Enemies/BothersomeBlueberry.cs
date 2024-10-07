using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BothersomeBlueberry : BaseEnemy
{
    private Vector2 respawnPosition; 
    private bool isKnockedOut; 
    private double respawnTimer;  
    private float speed;      
    private bool movingRight;
    private float respawnDelay = 3.0f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("bothersomeBlueberryAnimation");

        // Set the initial position for respawning
        respawnPosition = new Vector2(GameObject.X, GameObject.Y);

        speed = 300f;
        isKnockedOut = false;
        movingRight = true;
    }

    public override void Move(GameTime gameTime)
    {
        if (isKnockedOut)
            return;

        sRend.isFacingRight = !movingRight;

        if (movingRight)
            GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        else
            GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

        if (ReachedEdge())
        {
            movingRight = !movingRight;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (isKnockedOut)
        {
            respawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (respawnTimer >= respawnDelay)
            {
                Respawn();
            }
        }
    }

    public override void Shoot(GameTime gameTime)
    {
    }

    public void KnockOut()
    {
        isKnockedOut = true;
        respawnTimer = 0;
        //sRend.setAnimation("knockedOutAnimation");
    }

    private void Respawn()
    {
        GameObject.X = (int)respawnPosition.X;
        GameObject.Y = (int)respawnPosition.Y;

        isKnockedOut = false;
        movingRight = true;
        sRend.setAnimation("bothersomeBlueberryAnimation");
    }

    private bool ReachedEdge()
    {
        int screenWidth = 1280 - 144 / 2;

        if (GameObject.X <= 2 || GameObject.X >= screenWidth)
        {
            return true;
        }
        return false;
    }
}
