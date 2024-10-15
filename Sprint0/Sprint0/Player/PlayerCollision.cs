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
            foreach (GameObject go in GOManager.Instance.allGOs)
            {
                if (go.type != null)
                {
                    if (go.type.Contains("Platform"))
                    {
                        HandlePlatformCollision(go);
                    }
                    if (go.type.Contains("Item"))
                    {
                        HandleItemCollision(go);
                    }
                    if (go.type.Contains("Hill"))
                    {
                        HandleHillCollision(go);
                    }
                    if (go.type.Contains("Log"))
                    {
                        HandleLogCollision(go);
                    }
                    if (go.type.Contains("Stump"))
                    {
                        HandleStumpCollision(go);
                    }
                    if (go.type.Contains("Enemy"))
                    {
                        HandleEnemyCollision(go);
                    }

                }
            }
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

        public void HandleHillCollision(GameObject platform)
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

        public void HandleLogCollision(GameObject platform)
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

        public void HandleStumpCollision(GameObject platform)
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
                item.Destroy();
                String itemName = item.type.Remove(0, 10);
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
