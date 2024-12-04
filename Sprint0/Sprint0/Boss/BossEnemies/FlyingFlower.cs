using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using MonoGame.Extended;

public class FlyingFlower : BaseEnemy
{
    private const int FLIGHT_HEIGHT = 50;
    private const int FLIGHT_SPEED = 2;
    private const int MOVEMENT_RANGE = 150;
    private const float SPRING_CONST = 0.0004f;
    private bool hasSpawned = false;
    private bool isAttacking = false;
    private bool hasFired = false;
    private int spawnX = 0;
    private float xVelocity;
    private float prevVelocity = 1;
    private int turnsForAttack = -1;

    public override void Move(GameTime gameTime)
    {
        if (!isAttacking) {
            if (hasSpawned && GameObject.Y > FLIGHT_HEIGHT)
            {
                GameObject.Y -= FLIGHT_SPEED;
            }
            else if (GameObject.Y <= FLIGHT_HEIGHT)
            {
                xVelocity += SPRING_CONST * (spawnX - GameObject.X);
                if (prevVelocity * xVelocity < 0)
                {
                    turnsForAttack++;
                }
                prevVelocity = xVelocity;
                GameObject.X += (int)xVelocity;

                if (turnsForAttack == 3)
                {
                    isAttacking = true;
                }
            }
        }
        else
        {
            Attack(gameTime);
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        HealthComponent healthComponent = GameObject.GetComponent<HealthComponent>();
        SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>();

        KeyValuePair<string, Animation> currentAnim = spriteRenderer.currentAnimation;


        if (!hasSpawned || currentAnim.Key.Equals("Fly"))
        {
            healthComponent.currentHealth = healthComponent.maxHealth;
        }

        if(currentAnim.Key.Equals("Spawn") && spriteRenderer.IsAnimationComplete())
        {
            spriteRenderer.setAnimation("Fly");
            hasSpawned = true;
            spawnX = GameObject.X;

            xVelocity = (float)(0.5 * SPRING_CONST * -1 * (Math.Pow(MOVEMENT_RANGE, 2)));

        }
        else if (GameObject.Y <= FLIGHT_HEIGHT && GameObject.X != spawnX)
        {
            double tan = (600 - GameObject.Y) / (spawnX - GameObject.X);
            double rotationAngle = (Math.Atan(tan) - Math.PI/2);
            if(spawnX < GameObject.X)
            {
                rotationAngle += Math.PI;
            }
            rotationAngle *= 0.375;

            spriteRenderer.rotation = (float)rotationAngle;
        }
    }

    public void Attack(GameTime gameTime)
    {
        SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.setAnimation("Attack");
        spriteRenderer.spriteScale = 1f;
        GameObject.GetComponent<CircleCollider>().Radius = 50;

        if (spriteRenderer.IsAnimationComplete()) 
        {
            isAttacking = false;
            hasFired = false;
            turnsForAttack = 0;
            spriteRenderer.setAnimation("Fly");
            spriteRenderer.spriteScale = 0.75f;
            GameObject.GetComponent<CircleCollider>().Radius = 30;
        }
        else if (!hasFired && spriteRenderer.currentAnimation.Value.CurrentFrame == 13)
        {
            ShootProjectile(gameTime);
            hasFired = true;
        }


    }

    public void ShootProjectile(GameTime gameTime)
    {
        
        Texture2D projectileTexture = GOManager.Instance.textureStorage.GetTexture("MiniFlowerProjectile");
        Vector2 hitboxCenter = GameObject.GetComponent<CircleCollider>().Center;
        GameObject projectile = new GameObject((int)hitboxCenter.X - 22, (int)hitboxCenter.Y - 22);

        SpriteRenderer projectileRenderer = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y,44, 44), true);
        CircleCollider projectileCollider = new CircleCollider(15, new Vector2(-22, -22), GOManager.Instance.GraphicsDevice);
        FlowerProjectile projectileLogic = new FlowerProjectile(new Vector2(projectile.X, projectile.Y));
        projectile.AddComponent(projectileRenderer);
        projectile.AddComponent(projectileCollider);
        projectile.AddComponent(projectileLogic);

        Animation projectileAnim = new Animation(projectileTexture, 3, 4, 44, 44);
        projectileRenderer.addAnimation("Projectile", projectileAnim);
        projectileRenderer.setAnimation("Projectile");

        GOManager.Instance.allGOs.Add(projectile);
    }

    public override void Shoot(GameTime gameTime)
    {

    }


}