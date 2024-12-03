using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using MonoGame.Extended;

public class FlyingFlower : BaseEnemy
{

    private const int FLIGHT_HEIGHT = 150;
    private const int FLIGHT_SPEED = 2;
    private const int LEFT_BOUNDS = -75;
    private const int RIGHT_BOUNDS = 525;
    private bool hasSpawned = false;

    public override void Move(GameTime gameTime)
    {
        if (hasSpawned && GameObject.Y > FLIGHT_HEIGHT)
        {
            GameObject.Y -= FLIGHT_SPEED;
        }
        else if (GameObject.Y <= FLIGHT_HEIGHT)
        {

            if(GameObject.X < LEFT_BOUNDS || GameObject.X >= RIGHT_BOUNDS)
            {
                GameObject.GetComponent<SpriteRenderer>().isFacingRight = !GameObject.GetComponent<SpriteRenderer>().isFacingRight;
            }

            if (GameObject.GetComponent<SpriteRenderer>().isFacingRight)
            {
                GameObject.X += FLIGHT_SPEED;
            }
            else
            {
                GameObject.X -= FLIGHT_SPEED;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        KeyValuePair<string, Animation> currentAnim = GameObject.GetComponent<SpriteRenderer>().currentAnimation;

        if(currentAnim.Key.Equals("Spawn") && GameObject.GetComponent<SpriteRenderer>().IsAnimationComplete())
        {
            GameObject.GetComponent<SpriteRenderer>().setAnimation("Fly");
            hasSpawned = true;
        }

        if (GameObject.Y <= FLIGHT_HEIGHT && GameObject.X != (LEFT_BOUNDS + RIGHT_BOUNDS) / 2)
        {
            double tan = (600 - GameObject.Y) / ((LEFT_BOUNDS + RIGHT_BOUNDS)/2 - GameObject.X);
            double rotationAngle = (Math.Atan(tan) - Math.PI/2);
            if((LEFT_BOUNDS + RIGHT_BOUNDS) / 2 < GameObject.X)
            {
                rotationAngle += Math.PI;
            }
            rotationAngle *= 0.25;

            GameObject.GetComponent<SpriteRenderer>().rotation = (float)rotationAngle;
        }
    }

    public override void Shoot(GameTime gameTime)
    {

    }
}