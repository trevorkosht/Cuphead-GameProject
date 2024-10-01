using Microsoft.Xna.Framework;        
using Microsoft.Xna.Framework.Graphics;
using System;

public class MurderousMushroom : BaseEnemy
{
    private bool isHidden;
    private double shootCooldown;
    private Texture2D purpleSporeTexture;
    private Texture2D pinkSporeTexture;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("murderousMushroomAnimation");
        isHidden = false;
        shootCooldown = 2.0;

        purpleSporeTexture = storage.GetTexture("PurpleSpore");
        pinkSporeTexture = storage.GetTexture("PinkSpore");
    }

    public override void Move(GameTime gameTime)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!isHidden)
        {
            shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (shootCooldown <= 0)
            {
                Vector2 playerPosition = new Vector2(player.X, player.Y);
                bool shootPinkSpore = (new Random().Next(0, 2) == 0);

                sRend.isFacingRight = player.X < GameObject.X;

                Texture2D sporeTexture = shootPinkSpore ? pinkSporeTexture : purpleSporeTexture;
                GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new SporeProjectile(GameObject.position, playerPosition, sporeTexture, shootPinkSpore));


                GOManager.Instance.allGOs.Add(projectile);

                shootCooldown = 2.0; 
            }
        }
    }

    public void HideUnderCap()
    {
        isHidden = true;
    }

    public void EmergeFromCap()
    {
        isHidden = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Shoot(gameTime);
    }
}
