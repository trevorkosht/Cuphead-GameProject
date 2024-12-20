﻿using Cuphead;
using Cuphead.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ChaserProjectile : Projectile
{
    private float speed = 9f;
    private GameObject targetEnemy;
    private Vector2 lastDirection;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionTimer;
    private ProjectileCollision projectileCollision;
    private const float explosionDuration = 0.5f;
    private const string collisionAnimationName = "ChaserExplosionAnimation";
    private SoundEffectInstance impactSoundInstance;

    public ChaserProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        collided = false;
        explosionTimer = 0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
            spriteRenderer.isFacingRight = false;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        lastDirection = isFacingRight ? Vector2.UnitX : -Vector2.UnitX; // Start based on facing direction
        impactSoundInstance = GOManager.Instance.audioManager.getNewInstance("ChaserShotImpact");
    }

    public override void Update(GameTime gameTime)
    {
        if (GameObject == null)
            return;
        if (collided)
        {
            impactSoundInstance.Play();
            explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (explosionTimer >= explosionDuration)
            {
                GameObject.Destroy();
                return;
            }
        }
        else
        {
            impactSoundInstance.Stop();
            if (IsActive)
            {
                // Find the closest enemy in the direction the player is facing
                targetEnemy = FindClosestEnemy();

                Vector2 direction;

                if (targetEnemy != null)
                {
                    direction = Vector2.Normalize(targetEnemy.position - GameObject.position);
                    lastDirection = direction;

                    // Destroy the projectile if it's very close to the enemy
                    if (Vector2.Distance(GameObject.position, targetEnemy.position) < 20f)
                    {
                        GameObject.Destroy();
                        return;
                    }
                }
                else
                {
                    // No enemy detected, continue in the last known direction
                    direction = lastDirection;
                }

                // Move in the direction of the target or last direction
                GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

                // Check for collisions with other game objects
                collider = GameObject.GetComponent<Collider>();
                projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
                collided = projectileCollision.CollisionCheck();

                // Check if projectile is out of bounds
                Camera camera = GOManager.Instance.Camera;
                if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X || GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720)
                {
                    GameObject.Destroy();
                }
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var gameObject in GOManager.Instance.allGOs)
        {
            // Check if the game object is an enemy
            if(gameObject.type == null) continue;
            if (gameObject.type.Contains("Enemy"))
            {
                Vector2 directionToEnemy = gameObject.position - GameObject.position;
                float distance = directionToEnemy.Length();

                // Filter enemies based on the direction the player is facing
                //if ((isFacingRight && directionToEnemy.X > 0) || (!isFacingRight && directionToEnemy.X < 0))
                //{
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = gameObject;
                    }
                //}
            }
        }

        return closestEnemy;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
