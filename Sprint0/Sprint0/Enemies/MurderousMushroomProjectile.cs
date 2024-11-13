using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class MurderousMushroomProjectile
{
    private Texture2D purpleSporeTexture;
    private Texture2D pinkSporeTexture;
    private Texture2D attackVFX;
    private float projectileScale;

    public MurderousMushroomProjectile(Texture2D purpleSpore, Texture2D pinkSpore, Texture2D attackEffect, float scale)
    {
        purpleSporeTexture = purpleSpore;
        pinkSporeTexture = pinkSpore;
        attackVFX = attackEffect;
        projectileScale = scale;
    }

    public void SpawnProjectile(Vector2 mushroomPosition, Vector2 playerPosition, SpriteRenderer sRend)
    {
        bool shootPinkSpore = (new Random().Next(0, 2) == 0);
        sRend.isFacingRight = playerPosition.X < mushroomPosition.X;

        Texture2D sporeTexture = shootPinkSpore ? pinkSporeTexture : purpleSporeTexture;

        GameObject projectile = new GameObject((int)mushroomPosition.X, (int)mushroomPosition.Y, new SporeProjectile(mushroomPosition, playerPosition, sporeTexture, shootPinkSpore));
        SpriteRenderer projectileRenderer = new SpriteRenderer(new Rectangle((int)mushroomPosition.X, (int)mushroomPosition.Y, (int)(144 * projectileScale), (int)(144 * projectileScale)), false);
        projectile.AddComponent(projectileRenderer);

        if (shootPinkSpore)
        {
            projectileRenderer.addAnimation("pinkSpore", new Animation(sporeTexture, 3, 12, 144, 144));
            projectileRenderer.setAnimation("pinkSpore");
        }
        else
        {
            projectileRenderer.addAnimation("purpleSpore", new Animation(sporeTexture, 3, 12, 144, 144));
            projectileRenderer.setAnimation("purpleSpore");
        }

        CircleCollider collider = new CircleCollider(30, new Vector2(-30, -35), GOManager.Instance.GraphicsDevice);
        projectile.AddComponent(collider);

        Rectangle effectPosition = new Rectangle();
        if (sRend.isFacingRight)
        {
            effectPosition = new Rectangle((int)mushroomPosition.X - 7, (int)mushroomPosition.Y, 144, 144);
        }
        else
        {
            effectPosition = new Rectangle((int)mushroomPosition.X + 45, (int)mushroomPosition.Y, 144, 144);
        }

        VisualEffectFactory.createVisualEffect(effectPosition, attackVFX, 3, 5, 0.5f, sRend.isFacingRight);

        GOManager.Instance.allGOs.Add(projectile);
    }
}
