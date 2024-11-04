using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class AggravatingAcorn : BaseEnemy
{
    private Vector2 dropPosition;
    private bool isFalling;
    private float speed;
    private float gravity = 10f;
    private float yVelocity = 0;
    private float dropThreshold = 50f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("aggravatingAcornAnimation");
        speed = 550f;
        isFalling = false;
        dropPosition = Vector2.Zero;
    }

    public override void Move(GameTime gameTime)
    {
        if (!isFalling)
        {
            if (Math.Abs(GameObject.X - player.X) <= 1000)
            {
                GOManager.Instance.audioManager.getInstance("AggravatingAcornIdle").Play();
            }
            sRend.isFacingRight = true; // Always face left

            // Move left continuously
            GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Check if player is underneath and start dropping
            if (PlayerIsUnderneath())
            {
                GOManager.Instance.audioManager.getInstance("AggravatingAcornIdle").Stop();
                GOManager.Instance.audioManager.getInstance("AggravatingAcornDrop").Play();
                sRend.setAnimation("AcornDrop");
                sRend.spriteScale = 0.75f;
                isFalling = true;
                GameObject.GetComponent<CircleCollider>().offset = new Vector2(-50, -50);
                dropPosition = new Vector2(GameObject.X, GameObject.Y + 500); // Set the drop target position
                VisualEffectFactory.createVisualEffect(new Rectangle(GameObject.X - 10, GameObject.Y - 160, 144, 160), GOManager.Instance.textureStorage.GetTexture("PropVFX"), 3, 6, 0.9f, true);

                System.Diagnostics.Debug.WriteLine("propellor dropped, Xpos = " + GameObject.X + " at time t = " + gameTime.TotalGameTime.TotalMilliseconds + "ms");
            }
        }
        else
        {
            // Drop the acorn
            yVelocity += gravity;
            GameObject.Y += (int)(yVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Destroy when reaching the drop position
            if (GameObject.Y >= dropPosition.Y)
            {
                System.Diagnostics.Debug.WriteLine("game object destroyed Xpos = " + GameObject.X + " at time t = " + gameTime.TotalGameTime.TotalMilliseconds + "ms");
                GameObject.Destroy();
            }
        }
    }

    private bool PlayerIsUnderneath()
    {
        Vector2 playerPosition = new Vector2(player.X, player.Y);
        return Math.Abs(GameObject.X - playerPosition.X) <= dropThreshold;
    }

    public override void Shoot(GameTime gameTime)
    {
    }
}
