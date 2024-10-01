using Microsoft.Xna.Framework;         
using Microsoft.Xna.Framework.Graphics;

public class ToothyTerror : BaseEnemy
{
    private float jumpHeight;
    private float gravity;
    private bool isJumping;
    private float jumpSpeed;
    private float startYPosition;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("toothyTerrorAnimation");
        jumpHeight = 150f;
        gravity = 300f;   
        isJumping = true;  
        jumpSpeed = 250f; 
        startYPosition = GameObject.Y;
    }

    public override void Move(GameTime gameTime)
    {
        if (isJumping)
        {
            GameObject.Y -= (int)(jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            jumpSpeed -= gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (jumpSpeed <= 0)
            {
                isJumping = false;
            }
        }
        else
        {
            GameObject.Y += (int)(gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (GameObject.Y >= startYPosition)
            {
                GameObject.Y = (int)startYPosition;
                jumpSpeed = 250f;       
                isJumping = true;          
            }
        }
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
