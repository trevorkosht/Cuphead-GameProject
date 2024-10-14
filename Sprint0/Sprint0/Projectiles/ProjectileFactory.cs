using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ProjectileFactory
{
    public static GameObject CreateProjectile(ProjectileType type, float posX, float posY, bool isFacingRight)
    {
        GameObject projectile = new GameObject((int)posX+70, (int)posY+20);
        Projectile projectileLogic;
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(projectile.X, projectile.Y, 144, 144), true);

        switch (type)
        {
            case ProjectileType.Peashooter:
                projectileLogic = new PeashooterProjectile(isFacingRight, spriteRenderer);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new BoxCollider(new Vector2(60, 20), new Vector2(10, 28), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("PeashooterAnimation", new Animation(textureStorage.GetTexture("Peashooter"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("PeashooterExplosionAnimation", new Animation(textureStorage.GetTexture("PeashooterExplosion"), 5, 6, 144, 144));
                spriteRenderer.setAnimation("PeashooterAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Peashooter"), textureStorage);
                break;

            case ProjectileType.SpreadShot:
                projectileLogic = new SpreadShotProjectile(isFacingRight);
                projectile.AddComponent(projectileLogic);
                spriteRenderer.addAnimation("SpreadAnimation", new Animation(textureStorage.GetTexture("Spread"), 5, 4, 144, 144));
                spriteRenderer.setAnimation("SpreadAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Spread"), textureStorage);
                break;

            case ProjectileType.Chaser:
                projectileLogic = new ChaserProjectile(isFacingRight, spriteRenderer);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new CircleCollider(30f, new Vector2(-40, -35), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("ChaserAnimation", new Animation(textureStorage.GetTexture("Chaser"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("ChaserExplosionAnimation", new Animation(textureStorage.GetTexture("ChaserExplosion"), 5, 3, 144, 144));
                spriteRenderer.setAnimation("ChaserAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Chaser"), textureStorage);
                break;

            case ProjectileType.Lobber:
                projectileLogic = new LobberProjectile(isFacingRight, spriteRenderer);
                projectile.AddComponent(projectileLogic); 
                projectile.AddComponent(new CircleCollider(35f, new Vector2(-40, -40), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("LobberAnimation", new Animation(textureStorage.GetTexture("Lobber"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("LobberExplosionAnimation", new Animation(textureStorage.GetTexture("LobberExplosion"), 5, 6, 144, 144));
                spriteRenderer.setAnimation("LobberAnimation");
                projectileLogic.Initialize(textureStorage.GetTexture("Lobber"), textureStorage);
                break;


            case ProjectileType.Roundabout:
                projectileLogic = new RoundaboutProjectile(isFacingRight, spriteRenderer);
                projectile.AddComponent(projectileLogic);
                projectile.AddComponent(new BoxCollider(new Vector2(55, 27), new Vector2(8, 24), GOManager.Instance.GraphicsDevice));
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
