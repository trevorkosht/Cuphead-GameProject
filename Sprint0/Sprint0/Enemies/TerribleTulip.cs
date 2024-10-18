using Microsoft.Xna.Framework;        
using Microsoft.Xna.Framework.Graphics; 
using System;
using System.Collections.Generic;   

public class TerribleTulip : BaseEnemy
{
    private double shootCooldown;
    private Texture2D projectileTexture;     

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
        if(GameObject.X > GOManager.Instance.Camera.Position.X + 1200)
        {
            return;
        }
        shootCooldown -= gametime.ElapsedGameTime.TotalSeconds;
        if (shootCooldown <= 0 && sRend.currentAnimation.Value.CurrentFrame == 7)
        {
            sRend.setAnimation("terribleTulipAnimation");
            if(sRend.currentAnimation.Value.CurrentFrame == 7)
            {
                Vector2 playerPosition = new Vector2(player.X, player.Y);
                GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new TullipProjectile(GameObject.position));
                SpriteRenderer projSrend = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), (player.X > GameObject.X));
                CircleCollider collider = new CircleCollider(40, new Vector2(-28, -32), GOManager.Instance.GraphicsDevice);
                projectile.AddComponent(collider);
                projectile.AddComponent(projSrend);
                projSrend.spriteScale = .5f;
                projSrend.addAnimation("spin", new Animation(projectileTexture, 2, 12, 144, 144));
                projSrend.setAnimation("spin");
                GOManager.Instance.allGOs.Add(projectile);
                shootCooldown = 3.0;
            }
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        Shoot(gameTime);

        if (sRend.getAnimationName().Equals("terribleTulipAnimation") && sRend.currentAnimation.Value.CurrentFrame == 14) {
            sRend.setAnimation("Idle");
            sRend.currentAnimation.Value.CurrentFrame = 7;
        }
    }
}

