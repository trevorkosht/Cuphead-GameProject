﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Projectiles
{    
    public class ProjectileCollision
    {
        private GameObject projectile;
        private Collider collider;
        private string collisionAnimationName;
        private bool collided = false;
        private string[] nonCollidables = { "Player", "Item", "PlayerProjectile", "NPCProjectile"};

        public ProjectileCollision(GameObject projectile, Collider collider, string collisionAnimationName)
        {
            this.projectile = projectile;
            this.collider = collider;
            this.collisionAnimationName = collisionAnimationName;
        }

        public bool CollisionCheck()
        {
            foreach(GameObject go in GOManager.Instance.allGOs)
            {
                if (go != null && collider.Intersects(go.GetComponent<Collider>()))
                {
                    if (!nonCollidables.Any(go.type.Contains))
                    {
                        HandleCollision(go);
                        return true;
                    }
                }
            }
            return false;
        }
        private void HandleCollision(GameObject go)
        {
            if (go.type.Contains("Enemy"))
            {
                HealthComponent enemyHealth = go.GetComponent<HealthComponent>();
                if (enemyHealth != null)
                {
                    enemyHealth.RemoveHealth(1);


                    GameObject player = GOManager.Instance.Player;
                    ScoreComponent scoreComponent = player.GetComponent<ScoreComponent>();
                    scoreComponent?.AddScore(2);
                }
            }

            SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
            spriteRenderer.setAnimation(collisionAnimationName);
        }
        public int LobberCollisionCheck()
        {
            foreach (GameObject go in GOManager.Instance.allGOs)
            {
                if (go != null && collider.Intersects(go.GetComponent<Collider>()))
                {
                    if (!nonCollidables.Any(go.type.Contains))
                    {
                        return LobberHandleCollision(go);
                    }
                }
            }
            return -1;
        }
        private int LobberHandleCollision(GameObject go)
        {
            if (go.type.Contains("Enemy"))
            {
                HealthComponent enemyHealth = go.GetComponent<HealthComponent>();
                if (enemyHealth != null)
                {
                    enemyHealth.RemoveHealth(1);


                    GameObject player = GOManager.Instance.Player;
                    ScoreComponent scoreComponent = player.GetComponent<ScoreComponent>();
                    scoreComponent?.AddScore(2);
                }
                SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
                spriteRenderer.setAnimation(collisionAnimationName);
                return 1;
            }
            else if (go.type.Contains("Block") || go.type.Contains("Platform"))
            {
                return 0;
            }
            else
                return -1;
        }
    }
}
