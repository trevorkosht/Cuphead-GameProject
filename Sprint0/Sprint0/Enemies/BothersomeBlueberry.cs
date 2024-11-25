using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class BothersomeBlueberry : BaseEnemy
{
    private bool isKnockedOut;
    private double respawnTimer;
    private float speed;
    private bool movingRight;
    private float respawnDelay = 5.0f;
    private float turnDelay = -1.0f;
    private bool atEdge = false;


    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("bothersomeBlueberryAnimation");

        speed = 300f;
        isKnockedOut = false;
        movingRight = true;
    }

    public override void Move(GameTime gameTime)
    {
        if (isKnockedOut)
            return;

        sRend.isFacingRight = !movingRight;

        if (Math.Abs(GameObject.X - player.X) <= 1000)
        {
            GOManager.Instance.audioManager.getInstance("BothersomeBlueberryIdle").Play();
        }
        
        if (atEdge)
        {
            sRend.setAnimation("Turn");
            if(sRend.currentAnimation.Value.CurrentFrame == 6) {
                movingRight = !movingRight;
                sRend.setAnimation("bothersomeBlueberryAnimation");
                atEdge = false;
                turnDelay = 0.5f;
            }
        }
        else {
            if (movingRight)
                GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            else
                GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (turnDelay <= 0) {
                atEdge = ReachedEdge();
            }
            turnDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }


    private bool ReachedEdge()
    {
        CircleCollider blueberryCollider = GameObject.GetComponent<CircleCollider>();
        bool isOnPlatform = false;

        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.type != null) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                float leftEdge = blueberryCollider.Center.X - blueberryCollider.Radius;
                float rightEdge = blueberryCollider.Center.X + blueberryCollider.Radius;

                if (platformCollider != null && GO.Y > GameObject.Y + 30 && (platformCollider.BoundingBox.Left <= leftEdge - 50 && platformCollider.BoundingBox.Right >= rightEdge + 50)) {
                    isOnPlatform = true;
                }
            }
        }

        return !isOnPlatform;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        HealthComponent healthComponent = GameObject.GetComponent<HealthComponent>();

        if (healthComponent.currentHealth <= 50)
        {
            if(!isKnockedOut)
            {
                GOManager.Instance.audioManager.getInstance("BothersomeBlueberryIdle").Stop();
                GOManager.Instance.audioManager.getInstance("BothersomeBlueberryDeath").Play();
            }
            isKnockedOut = true;
            respawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            healthComponent.currentHealth = 50;

            if (respawnTimer >= respawnDelay) {
                healthComponent.currentHealth = healthComponent.maxHealth;
                isKnockedOut = false;
                respawnTimer = 0;
                sRend.setAnimation("bothersomeBlueberryAnimation");
            }
            else if (respawnTimer <= 0.75) {
                sRend.setAnimation("Melt");
                GameObject.GetComponent<CircleCollider>().Radius = 0;
            }
            else if (respawnTimer >= respawnDelay - 1.25)
            {
                if (Math.Abs(GameObject.X - player.X) <= 1000)
                {
                    GOManager.Instance.audioManager.getInstance("BothersomeBlueberryRevive").Play();
                }
                sRend.setAnimation("Respawn");
            }
            else
            {
                sRend.setAnimation("WaitForRespawn");
            }
        }
        else {
            GameObject.GetComponent<CircleCollider>().Radius = 20;
        }
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
