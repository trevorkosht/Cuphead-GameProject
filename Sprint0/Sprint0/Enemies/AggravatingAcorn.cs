using Microsoft.Xna.Framework;          
using Microsoft.Xna.Framework.Graphics; 
using System;                           

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    private bool movingRight;          
    private float dropThreshold = 50f;   

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("aggravatingAcornAnimation");
        speed = 200f; 
        isFalling = false;
        dropPosition = Vector2.Zero;    
        movingRight = true;             
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {
            sRend.isFacingRight = !movingRight;
            if (movingRight)
                GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            else
                GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);


            if (ReachedEdge())
                movingRight = !movingRight;

            if (PlayerIsUnderneath())
            {
                sRend.setAnimation("AcornDrop");
                sRend.spriteScale = 0.8f;
                isFalling = true;
                dropPosition = new Vector2(GameObject.X, GameObject.Y + 500); 
            }
        }
        else
        {
            GameObject.Y += (int)(speed * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (GameObject.Y >= dropPosition.Y)
            {
                GameObject.Destroy();
            }
        }
    }

    private bool PlayerIsUnderneath()
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y);

        return Math.Abs(GameObject.X - playerPosition.X) <= dropThreshold;
    }

    private bool ReachedEdge()
    {
        int screenWidth = 1280 - 144/2; 

        if (GameObject.X < 0 || GameObject.X > screenWidth)
            return true;
        return false;
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
