using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;
using System.Collections.Generic;       // For managing projectiles

public class TerribleTulip : BaseEnemy
{
    private double shootCooldown;
    private Texture2D projectileTexture;        // Store the projectile texture
    private List<GameObject> projectiles = new List<GameObject>();

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("terribleTulipAnimation");
        shootCooldown = 1.0;
        projectileTexture = storage.GetTexture("Seed"); // Get projectile texture from storage
    }

    public override void Move(GameTime gameTime)
    {
        // Terrible Tulip remains stationary, so no movement
    }

    public override void Shoot(GameTime gametime)
    {
        shootCooldown -= gametime.ElapsedGameTime.TotalSeconds;
        if (shootCooldown <= 0 && sRend.currentAnimation.Value.CurrentFrame == 7)
        {
            Vector2 playerPosition = new Vector2(player.X, player.Y);
            // Create and shoot a homing projectile towards the player
            GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new HomingProjectile(GameObject.position));
            projectiles.Add(projectile);
            SpriteRenderer projSrend = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), true);
            projectile.AddComponent(projSrend);
            projSrend.spriteScale = .5f;
            projSrend.addAnimation("spin", new Animation(projectileTexture, 3, 12, 144, 144));
            projSrend.setAnimation("spin");
            GOManager.Instance.allGOs.Add(projectile);
            shootCooldown = 3.0; // Reset the cooldown after shooting
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        //Logic to ensure the projectile is spinning in the proper direction relative to its motion
        int i = 0;
        while (i < projectiles.Count) {
            if (projectiles[i].GetComponent<SpriteRenderer>() == null) {
                projectiles.Remove(projectiles[i]);
            }
            else {
                if (projectiles[i].X > player.X) {
                    projectiles[i].GetComponent<SpriteRenderer>().isFacingRight = false;
                }
                else {
                    projectiles[i].GetComponent<SpriteRenderer>().isFacingRight = true;
                }
                i++;
            }
        }
        // Shoot logic handled in the Shoot method
        Shoot(gameTime);
    }
}

