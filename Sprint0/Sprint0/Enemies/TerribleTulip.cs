using Microsoft.Xna.Framework;        
using Microsoft.Xna.Framework.Graphics; 
using System;
using System.Collections.Generic;   

public class TerribleTulip : BaseEnemy
{
    private double shootCooldown;
    private Texture2D projectileTexture;     
    private List<GameObject> projectiles = new List<GameObject>();

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("terribleTulipAnimation");
        shootCooldown = 1.0;
        projectileTexture = storage.GetTexture("Seed");
    }

    public override void Move(GameTime gameTime)
    {
    }

    public override void Shoot(GameTime gametime)
    {
        shootCooldown -= gametime.ElapsedGameTime.TotalSeconds;
        if (shootCooldown <= 0 && sRend.currentAnimation.Value.CurrentFrame == 7)
        {
            sRend.setAnimation("terribleTulipAnimation");
            if(sRend.currentAnimation.Value.CurrentFrame == 7)
            {
                Vector2 playerPosition = new Vector2(player.X, player.Y);
                GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new TullipProjectile(GameObject.position));
                projectiles.Add(projectile);
                SpriteRenderer projSrend = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), true);
                projectile.AddComponent(projSrend);
                projSrend.spriteScale = .5f;
                projSrend.addAnimation("spin", new Animation(projectileTexture, 3, 12, 144, 144));
                projSrend.setAnimation("spin");
                GOManager.Instance.allGOs.Add(projectile);
                shootCooldown = 3.0;
            }
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

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
        Shoot(gameTime);

        if (sRend.getAnimationName().Equals("terribleTulipAnimation") && sRend.currentAnimation.Value.CurrentFrame == 14) {
            sRend.setAnimation("Idle");
            sRend.currentAnimation.Value.CurrentFrame = 7;
        }
    }
}

