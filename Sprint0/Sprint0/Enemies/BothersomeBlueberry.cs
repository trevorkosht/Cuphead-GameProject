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
    private float turnDelay = -1.0f;


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
            if(turnDelay < 0) {
                movingRight = !movingRight;
                turnDelay = 0.5f;
            }
        }
        turnDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }


    private bool ReachedEdge()
    {
        CircleCollider blueberryCollider = GameObject.GetComponent<CircleCollider>();
        bool isOnPlatform = false;

        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.type != null /*&& (GO.type.Contains("Platform") || GO.type.Contains("Block"))*/) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                float leftEdge = blueberryCollider.Center.X - blueberryCollider.Radius;
                float rightEdge = blueberryCollider.Center.X + blueberryCollider.Radius;

                if (platformCollider != null && GO.Y > GameObject.Y + 50 && (platformCollider.BoundingBox.Left <= leftEdge - 50 && platformCollider.BoundingBox.Right >= rightEdge + 50)) {
                    isOnPlatform = true;
                }
            }
        }

        return !isOnPlatform;
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
