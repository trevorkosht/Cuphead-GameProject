﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Player
{
    internal class PlayerCollision
    {
        private PlayerState player;
        private BoxCollider collider;
        private PlayerAnimation playerAnimator;

        public PlayerCollision(PlayerState player, BoxCollider collider, PlayerAnimation playerAnimator)
        {
            this.player = player;
            this.collider = collider;
            this.playerAnimator = playerAnimator;
        }

        public void CollisionCheck()
        {
            bool collidedObstacle = false;
            foreach (GameObject go in GOManager.Instance.allGOs)
            {
                if (go.type != null)
                {
                    if (collider.Intersects(go.GetComponent<Collider>()))
                    {
                        if (go.type.Contains("Slope"))
                        {
                            HandleSlopeCollision(go);
                            collidedObstacle = true;
                        }
                        else if (go.type.Contains("Platform"))
                        {
                            HandlePlatformCollision(go);
                            collidedObstacle = true;
                        }
                        else if (go.type.Contains("Item"))
                        {
                            HandleItemCollision(go);
                        }
                        else if (go.type.Contains("Hill") || go.type.Contains("Log") || go.type.Contains("Stump"))
                        {
                            HandleObstacleCollision(go);
                            collidedObstacle = true;
                        }
                        else if (go.type.Contains("Enemy") || go.type.Contains("NPCProjectile"))
                        {
                            HandleEnemyCollision(go);
                        }
                    }
                }
            }
            if (!collidedObstacle)
            {
                player.GroundLevel = 9999;
                player.IsGrounded = false;
            }
        }

        public GameObject TypeCollide(String type)
        {
            foreach (GameObject go in GOManager.Instance.allGOs)
            {
                if (go != null && go.type != null)
                {
                    if (go.type.Contains(type))
                    {
                        if (collider.Intersects(go.GetComponent<Collider>()))
                        {
                            return go;
                        }
                    }
                    
                }
            }
            return null;
        }

        public void HandleGroundCheck(SpriteRenderer animator)
        {
            if (!player.isDuckingYAdjust)
            {
                if (player.GameObject.Y >= player.GroundLevel)
                {
                    if (!player.IsGrounded)
                    {
                        playerAnimator.CreateDustEffect();
                    }

                    player.IsGrounded = true;
                    player.HasDashed = false;
                    player.floorY = (int)player.GroundLevel;
                    player.airTime = 1;

                    if (player.velocity.Y > 0) player.velocity.Y = 0;
                    player.GameObject.Y = player.floorY;
                }
                else
                    player.IsGrounded = false;
            }
        }

        public void HandlePlatformCollision(GameObject platform)
        {
            Rectangle playerBounds = player.GameObject.GetComponent<BoxCollider>().BoundingBox;
            Rectangle colliderBounds = platform.GetComponent<BoxCollider>().BoundingBox;
            if (playerBounds.Bottom - 50 < colliderBounds.Top) 
            {
                player.velocity.Y = 0;
                player.GroundLevel = colliderBounds.Top;
                player.floorY = colliderBounds.Bottom + 100;
                player.IsGrounded = true;
                player.HasDashed = false;
            }

        }

        public void HandleObstacleCollision(GameObject obstacle)
        {
            Rectangle playerBounds = player.GameObject.GetComponent<BoxCollider>().BoundingBox;
            Rectangle colliderBounds = obstacle.GetComponent<BoxCollider>().BoundingBox;
            if (playerBounds.Bottom - 50 < colliderBounds.Top) 
            {
                int duckingOffset = 1;
                if (player.IsDucking) duckingOffset = -30;
                player.GameObject.Y = colliderBounds.Top - playerBounds.Height + duckingOffset;
                player.velocity.Y = 0;
                player.GroundLevel = colliderBounds.Top + 10;
                player.floorY = colliderBounds.Top + 10;
                player.IsGrounded = true;
                player.HasDashed = false;
            }
            else if (player.GameObject.X < obstacle.X)
            {
                player.GameObject.X = colliderBounds.Left - player.playerWidth - 5;
            }
            else if (player.GameObject.X > obstacle.X)
            {
                player.GameObject.X = colliderBounds.Right - 25;
            }
        }

        public void HandleSlopeCollision(GameObject obstacle)
        {
            var playerCollider = player.GameObject.GetComponent<BoxCollider>();
            var obstacleCollider = obstacle.GetComponent<BoxCollider>();

            Rectangle playerBounds = playerCollider.BoundingBox;

            Vector2[] obstacleCorners = obstacleCollider.GetRotatedCorners();

            Vector2 topLeft = obstacleCorners[0]; 
            Vector2 topRight = obstacleCorners[1]; 

            if (playerBounds.Bottom > Math.Min(topLeft.Y, topRight.Y) && playerBounds.Left >= topLeft.X && playerBounds.Right <= topRight.X)
            {
                float slopeHeightAtPlayerX = MathHelper.Lerp(topLeft.Y, topRight.Y, (playerBounds.Center.X - topLeft.X) / (topRight.X - topLeft.X));

                if (playerBounds.Bottom > slopeHeightAtPlayerX)
                {
                    int duckingOffset = 0;
                    if (player.IsDucking) duckingOffset = -30;
                    player.GameObject.Y = (int)slopeHeightAtPlayerX - playerBounds.Height + 10 + duckingOffset;
                    player.velocity.Y = 0;
                    player.IsGrounded = true;
                    player.HasDashed = false;
                }
            }
            else if (playerBounds.Right < topLeft.X) 
            {
                player.GameObject.X = (int)(topLeft.X - player.playerWidth - 5);
            }
            else if (playerBounds.Left > topRight.X) 
            {
                player.GameObject.X = (int)(topRight.X + 5);
            }
        }

        public void HandleItemCollision(GameObject item)
        {
            if (collider.Intersects(item.GetComponent<Collider>()))
            {
                String itemName = item.type.Remove(0, 10);
                item.Destroy();
                switch (itemName)
                {
                    case "Spreadshot":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Spreadshot] = true; break;
                    case "Chaser":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Chaser] = true; break;
                    case "Lobber":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Lobber] = true; break;
                    case "Roundabout":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Roundabout] = true; break;
                }
            }
        }
        public void HandleEnemyCollision(GameObject Enemy)
        {
            if (collider.Intersects(Enemy.GetComponent<Collider>()))
            {
                if (Enemy.type.Contains("AcornMaker"))
                {
                    player.GameObject.X = Enemy.X - player.playerWidth + 40;
                }
            }

        }
    }
}
