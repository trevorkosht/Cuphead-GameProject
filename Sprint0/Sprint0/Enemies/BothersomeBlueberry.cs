using Microsoft.Xna.Framework;    
using Microsoft.Xna.Framework.Graphics;
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

    public override void Shoot(GameTime gameTime)
    {
    }

    private bool ReachedEdge()
    {
        int screenWidth = 1280-144/2;

        if (GameObject.X <= 2 || GameObject.X >= screenWidth)
        {
            return true;
        }
        return false;
    }
}
