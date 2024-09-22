using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;
using System.Collections.Generic;       // For managing spores

public class MurderousMushroom : BaseEnemy
{
    private bool isHidden;
    private double shootCooldown;
    private List<SporeProjectile> spores; // List of spores fired by the mushroom
    private Texture2D purpleSporeTexture;
    private Texture2D pinkSporeTexture;

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(startPosition, hitPoints, texture, storage);
        isHidden = false;
        shootCooldown = 2.0;
        spores = new List<SporeProjectile>();

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

                // Shoot a spore in the direction of the player
                Texture2D sporeTexture = shootPinkSpore ? pinkSporeTexture : purpleSporeTexture;
                spores.Add(new SporeProjectile(position, playerPosition, sporeTexture, shootPinkSpore));

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

        // Update each spore's position and behavior
        for (int i = 0; i < spores.Count; i++)
        {
            spores[i].Update(gameTime);

            // Remove spores that are no longer active
            if (!spores[i].IsActive)
            {
                spores.RemoveAt(i);
                i--; // Adjust the index after removal
            }
        }

        // Handle shooting logic
        Shoot(gameTime);
    }

    public override void TakeDamage(int damage)
    {
        if (!isHidden)
        {
            base.TakeDamage(damage);
            if (HitPoints <= 0)
            {
                IsActive = false; // Deactivate if HP reaches 0
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            // Draw the mushroom sprite
            spriteBatch.Draw(spriteTexture, position, Color.White);

            // Draw each active spore
            foreach (var spore in spores)
            {
                spore.Draw(spriteBatch);
            }
        }
    }
}
