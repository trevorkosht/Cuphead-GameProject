using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;
using System.Collections.Generic;       // For managing spores

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

        // Fetch the spore textures from the texture storage
        purpleSporeTexture = storage.GetTexture("PurpleSpore");
        pinkSporeTexture = storage.GetTexture("PinkSpore");
    }

    public override void Move(GameTime gameTime)
    {
        // Stationary, no movement needed for the mushroom
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!isHidden)
        {
            shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (shootCooldown <= 0)
            {
                Vector2 playerPosition = new Vector2(player.X, player.Y);
                // Randomly decide between shooting a purple or pink spore
                bool shootPinkSpore = (new Random().Next(0, 2) == 0); // 50% chance

                sRend.isFacingRight = player.X < GameObject.X;

                // Shoot a spore in the direction of the player
                Texture2D sporeTexture = shootPinkSpore ? pinkSporeTexture : purpleSporeTexture;
                GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new SporeProjectile(GameObject.position, playerPosition, sporeTexture, shootPinkSpore));


                GOManager.Instance.allGOs.Add(projectile);

                shootCooldown = 2.0; // Reset the cooldown for the next spore
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

        // Handle shooting logic
        Shoot(gameTime);
    }
}
