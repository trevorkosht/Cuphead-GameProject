using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using MonoGame.Extended;

public class FollowingFlytrap : BaseEnemy
{
    private const float TURN_RATE =  (float)(Math.PI / 32);
    private const float SPEED = 3.0f;
    private bool hasSpawned = false;
    private Vector2 velocity = new Vector2(0, 0);
    private Vector2 position;
    private Vector2 targetDirection;
   
    public override void Move(GameTime gameTime)
    {
        if (hasSpawned)
        {
            Vector2 targetPosition;
            targetPosition.X = GOManager.Instance.Player.GetComponent<BoxCollider>().BoundingBox.Center.X;
            targetPosition.Y = GOManager.Instance.Player.GetComponent<BoxCollider>().BoundingBox.Center.Y;
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = velocity.X < 0;
            position = GameObject.GetComponent<CircleCollider>().Center;
            targetDirection = targetPosition - position;
            targetDirection.Normalize();

            float vMagnitude = velocity.Length();

            float dot = targetDirection.Dot(velocity);
            dot /= (vMagnitude * targetDirection.Length());
            float angle = (float)Math.Acos(dot);



            if (angle <= TURN_RATE)
            {
                velocity = targetDirection;
            }
            else if (targetDirection.Y > velocity.Y)
            {
                velocity = velocity.Rotate(-1 * TURN_RATE);
            }
            else
            {
                velocity = velocity.Rotate(TURN_RATE);
            }

            angle = (float)Math.Atan(velocity.Y / velocity.X);
            GameObject.GetComponent<SpriteRenderer>().rotation = angle;

            velocity.Normalize();
            GameObject.X += (int)(SPEED * velocity.X);
            GameObject.Y += (int)(SPEED * velocity.Y);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        KeyValuePair<string, Animation> currentAnim = spriteRenderer.currentAnimation;

        if (currentAnim.Key.Equals("Spawn") && spriteRenderer.IsAnimationComplete())
        {
            spriteRenderer.setAnimation("Attack");
            spriteRenderer.spriteScale = 0.75f;
            hasSpawned = true;

            if (!spriteRenderer.isFacingRight)
            {
                velocity.X = SPEED;
            }
            else
            {
                velocity.X = -1 * SPEED;
            }
        }

    }

    public override void Shoot(GameTime gameTime)
    {

    }
}