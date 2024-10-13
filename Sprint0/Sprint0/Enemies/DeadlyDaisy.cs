using Microsoft.Xna.Framework;          
using Microsoft.Xna.Framework.Graphics; 
using Sprint0;
using System;                       

public class DeadlyDaisy : BaseEnemy
{
    private float speed;
    private float jumpHeight;
    private bool isJumping;
    private Vector2 velocity;
    Vector2 pos;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("deadlyDaisyAnimation");
        speed = 300f; 
        jumpHeight = 200f; 
        isJumping = false;
        velocity = Vector2.Zero;  
    }

    public override void Move(GameTime gameTime)
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y);

        Vector2 direction = playerPosition - GameObject.position;
        float distance = direction.Length();

        float minDistance = 0.1f;

        if (distance > minDistance)
        {
            direction.Normalize(); 

            sRend.isFacingRight = playerPosition.X < GameObject.X;

            GameObject.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            GameObject.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
        }


        if (NeedsToJump(playerPosition))
        {
            Jump();
        }
        else 
        {
            sRend.setAnimation("deadlyDaisyAnimation");
        }

        if (isJumping)
        {
            velocity.Y += 400f * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            GameObject.Y += (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (GameObject.Y >= jumpHeight)
            {
                isJumping = false;
                velocity.Y = 0;
            }
        }
    }

    private bool NeedsToJump(Vector2 playerPosition)
    {
        return GameObject.Y > playerPosition.Y && !isJumping;
    }

    private void Jump()
    {
        isJumping = true;
        velocity.Y = -jumpHeight;
        sRend.setAnimation("Spawn");
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
