using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class ProjectileFactory
{
    public static GameObject CreateProjectile(ProjectileType type, float posX, float posY, bool isFacingRight, float angle)
    {
        GameObject projectile = new GameObject((int)posX+70, (int)posY+80);
        Projectile projectileLogic;
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        float tempAngle = angle;
        Vector2 offset = new Vector2();
        if (type == ProjectileType.Chaser || type == ProjectileType.Lobber)
        {
            tempAngle = 0;
            projectile.Y += -80;

            if (angle < -89f)
                projectile.Y += -40;
            else if (angle < 0)
                projectile.Y += -20;
            else if (angle > 89)
                projectile.Y += 60;
        }
        else
        {
            if (!isFacingRight && angle > -89f)
            {
                tempAngle = -180 - angle;
                projectile.Y += 70;
                projectile.X += 70;
                offset = new Vector2(-70, -75);
            }

            if (angle < 0 && angle > -89f)
            {
                projectile.Y += 60;
                offset.Y += isFacingRight ? -40 : -10;
                offset.X += 25;
            }
            else if (angle < -89f)
            {
                offset.Y += -60;
            }

            if (angle > -89f)
            {
                float radians = MathHelper.ToRadians(angle);
                Vector2 offset2 = new Vector2(
                    (float)(Math.Cos(radians) * 72 - 72),
                    (float)(Math.Sin(radians) * 72 - 72)
                );
                projectile.X += (int)offset2.X;
                projectile.Y += (int)offset2.Y;
            }
            else
            {
                projectile.Y += -70;
                if (!isFacingRight)
                    projectile.X += 20;
            }

            if (angle > 89)
            {
                projectile.X += 110;
                offset += isFacingRight ? new Vector2(-74, 0) : new Vector2(0, 74);
            }
        }


        SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), true, tempAngle);

        switch (type)
        {
            case ProjectileType.Peashooter:
                projectileLogic = new PeashooterProjectile(isFacingRight, spriteRenderer, angle);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new BoxCollider(new Vector2(60, 20), new Vector2(10 + offset.X, 28 + offset.Y), GOManager.Instance.GraphicsDevice, MathHelper.ToRadians(tempAngle)));
                spriteRenderer.addAnimation("PeashooterAnimation", new Animation(textureStorage.GetTexture("Peashooter"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("PeashooterExplosionAnimation", new Animation(textureStorage.GetTexture("PeashooterExplosion"), 5, 6, 144, 144));
                spriteRenderer.setAnimation("PeashooterAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Peashooter"), textureStorage);
                break;

            case ProjectileType.SpreadShot:
                projectileLogic = new SpreadShotProjectile(isFacingRight, angle, offset);
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("SpreadAnimation", new Animation(textureStorage.GetTexture("Spread"), 5, 4, 144, 144));
                spriteRenderer.setAnimation("SpreadAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Spread"), textureStorage);
                break;

            case ProjectileType.Chaser:
                projectileLogic = new ChaserProjectile(isFacingRight, spriteRenderer);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new CircleCollider(30f, new Vector2(-40 - offset.X, -35 - offset.Y), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("ChaserAnimation", new Animation(textureStorage.GetTexture("Chaser"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("ChaserExplosionAnimation", new Animation(textureStorage.GetTexture("ChaserExplosion"), 5, 3, 144, 144));
                spriteRenderer.setAnimation("ChaserAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Chaser"), textureStorage);
                break;

            case ProjectileType.Lobber:
                projectileLogic = new LobberProjectile(isFacingRight, spriteRenderer, angle);
                projectile.AddComponent(projectileLogic); 
                projectile.AddComponent(new CircleCollider(35f, new Vector2(-40 - offset.X, -40 - offset.Y), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("LobberAnimation", new Animation(textureStorage.GetTexture("Lobber"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("LobberExplosionAnimation", new Animation(textureStorage.GetTexture("LobberExplosion"), 5, 6, 144, 144));
                spriteRenderer.setAnimation("LobberAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Lobber"), textureStorage);
                break;

            case ProjectileType.Roundabout:
                projectileLogic = new RoundaboutProjectile(isFacingRight, spriteRenderer, angle);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new BoxCollider(new Vector2(55, 27), new Vector2(8 + offset.X, 24 + offset.Y), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("RoundaboutAnimation", new Animation(textureStorage.GetTexture("Roundabout"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("RoundaboutExplosionAnimation", new Animation(textureStorage.GetTexture("RoundaboutExplosion"), 5, 6, 144, 144));
                spriteRenderer.setAnimation("RoundaboutAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Roundabout"), textureStorage);
                break;

            default:
                throw new ArgumentException("Invalid projectile type specified");
        }

        spriteRenderer.spriteScale = 0.5f;
        projectile.AddComponent(spriteRenderer);
        projectile.AddComponent(projectileLogic);
        projectile.type = "PlayerProjectile";

        return projectile;
    }


    
}

// Enumeration of different projectile types
public enum ProjectileType
{
    Peashooter,
    SpreadShot,
    Chaser,
    Lobber,
    Roundabout
}
