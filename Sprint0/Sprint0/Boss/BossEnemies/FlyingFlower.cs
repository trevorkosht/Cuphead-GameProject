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
    private int spawnX = 0;
    private float xVelocity;

    public override void Move(GameTime gameTime)
    {
        if (hasSpawned && GameObject.Y > FLIGHT_HEIGHT)
        {
            GameObject.Y -= FLIGHT_SPEED;
        }
        else if (GameObject.Y <= FLIGHT_HEIGHT)
        {
            xVelocity += SPRING_CONST * (spawnX - GameObject.X);
            GameObject.X += (int)xVelocity;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        KeyValuePair<string, Animation> currentAnim = GameObject.GetComponent<SpriteRenderer>().currentAnimation;

        HealthComponent healthComponent = GameObject.GetComponent<HealthComponent>();
        SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>();

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
            rotationAngle *= 0.25;

            spriteRenderer.rotation = (float)rotationAngle;
        }
    }

    public override void Shoot(GameTime gameTime)
    {

    }
}