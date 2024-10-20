using Microsoft.Xna.Framework;
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
                        if (go.type.Contains("Platform"))
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
                        else if (go.type.Contains("Enemy"))
                        {
                            HandleEnemyCollision(go);
                        }
                    }
                }
            }
            if (!collidedObstacle)
            {
                player.GroundLevel = 9999; //Fall
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
            if (playerBounds.Bottom - 50 < colliderBounds.Top) //On top (50 for extra space in case not checked collision immeditately)
            {
                player.velocity.Y = 0;
                player.GroundLevel = colliderBounds.Top;
                player.floorY = colliderBounds.Bottom + 100;
                player.IsGrounded = true;
            }

        }

        public void HandleObstacleCollision(GameObject obstacle)
        {
            Rectangle playerBounds = player.GameObject.GetComponent<BoxCollider>().BoundingBox;
            Rectangle colliderBounds = obstacle.GetComponent<BoxCollider>().BoundingBox;
            if (playerBounds.Bottom - 50 < colliderBounds.Top) //On top (50 for extra space in case not checked collision immeditately)
            {
                player.velocity.Y = 0;
                player.GroundLevel = colliderBounds.Top + 10;
                player.floorY = colliderBounds.Top + 10;
                player.IsGrounded = true;
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

        public void HandleDeprecatedCollision(GameObject platform)
        {
            if (collider.Intersects(platform.GetComponent<Collider>()))
            {
                player.GroundLevel = (float)platform.Y;
                player.floorY = platform.Y;
                player.IsGrounded = true;
            }
            else
            {
                player.GroundLevel = 99999;
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
