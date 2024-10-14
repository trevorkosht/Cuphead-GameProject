using Microsoft.Xna.Framework;
using System.Collections.Generic;

public static class ProjectileCollisionHandler
{
    public static void HandleCollision(Projectile projectile)
    {
        // Loop through all enemies and blocks to check for collisions
        List<GameObject> allGameObjects = GOManager.Instance.allGOs;

        foreach (var gameObject in allGameObjects)
        {
            Collider enemyCollider = gameObject.GetComponent<Collider>();
            HealthComponent enemyHealth = gameObject.GetComponent<HealthComponent>();

            if (enemyCollider != null && projectile.GameObject.GetComponent<Collider>().Intersects(enemyCollider))
            {
                if (enemyHealth != null)
                {
                    enemyHealth.RemoveHealth(10);

                    var spriteRenderer = projectile.GameObject.GetComponent<SpriteRenderer>();
                    //spriteRenderer.setAnimation("ExplosionAnimation");
                    projectile.IsActive = false;
                    projectile.GameObject.Destroy();
                }
            }


        }
    }
}
