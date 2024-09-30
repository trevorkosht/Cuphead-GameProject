using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ProjectileFactory
{
    public static GameObject CreateProjectile(ProjectileType type, float posX, float posY, bool isFacingRight)
    {
        GameObject projectile = new GameObject((int)posX, (int)posY);
        Projectile projectileLogic;
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        Texture2D projectileTexture = textureStorage.GetTexture("PurpleSpore"); // Default texture
        SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), false);

        switch (type)
        {
            case ProjectileType.Peashooter:
                projectileLogic = new PeashooterProjectile();
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
                projectileLogic.Initialize(textureStorage.GetTexture("PurpleSpore"), textureStorage);
                break;

            case ProjectileType.SpreadShot:
                projectileLogic = new SpreadShotProjectile();
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
                projectileLogic.Initialize(textureStorage.GetTexture("PurpleSpore"), textureStorage);
                break;

            case ProjectileType.Chaser:
                projectileLogic = new ChaserProjectile();
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
                projectileLogic.Initialize(textureStorage.GetTexture("PurpleSpore"), textureStorage);
                break;

            case ProjectileType.Lobber:
                projectileLogic = new LobberProjectile();
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
                projectileLogic.Initialize(textureStorage.GetTexture("PurpleSpore"), textureStorage);
                break;

            case ProjectileType.Roundabout:
                projectileLogic = new RoundaboutProjectile();
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("PurpleSporeAnimation", new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 1, 144, 144));
                projectileLogic.Initialize(textureStorage.GetTexture("PurpleSpore"), textureStorage);
                break;

            default:
                throw new ArgumentException("Invalid projectile type specified");
        }

        projectile.AddComponent(projectileLogic);
        spriteRenderer.loadAllAnimations();

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
