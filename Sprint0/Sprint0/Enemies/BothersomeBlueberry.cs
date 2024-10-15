using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

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


    private bool ReachedEdge()
    {
        CircleCollider blueberryCollider = GameObject.GetComponent<CircleCollider>();

        foreach (GameObject GO in GOManager.Instance.allGOs)
        {
            // Ensure GO and its type are not null before checking its type
            if (GO != null && GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Block")))
            {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();

                if (blueberryCollider.Intersects(platformCollider))
                {
                    // Use the CircleCollider's Center and Radius to check the platform edges
                    float blueberryLeftEdge = blueberryCollider.Center.X - blueberryCollider.Radius;
                    float blueberryRightEdge = blueberryCollider.Center.X + blueberryCollider.Radius;

                    if (blueberryLeftEdge <= platformCollider.BoundingBox.Left ||
                        blueberryRightEdge >= platformCollider.BoundingBox.Right)
                    {
                        return true; // Reached the platform's edge
                    }
                }
            }
        }

        return false;
    }






    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (isKnockedOut)
        {
            respawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (respawnTimer >= respawnDelay / 2)
            {
                sRend.setAnimation("BlueberryRespawn");
            }
            else if (respawnTimer >= respawnDelay)
            {
                Respawn();
            }
            else
            {
                sRend.setAnimation("WaitForRespawn");
            }
        }
    }

    private void Respawn()
    {
        GameObject.X = (int)respawnPosition.X;
        GameObject.Y = (int)respawnPosition.Y;

        isKnockedOut = false;
        movingRight = true;
        sRend.setAnimation("bothersomeBlueberryAnimation");
    }

    public override void Shoot(GameTime gameTime)
    {
    }

    public void KnockOut()
    {
        isKnockedOut = true;
        respawnTimer = 0;
    }
}
