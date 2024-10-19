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
            foreach (GameObject go in GOManager.Instance.allGOs)
            {
                if (go.type != null)
                {
                    if (collider.Intersects(go.GetComponent<Collider>()))
                    {
                        Rectangle playerBox = go.GetComponent<SpriteRenderer>().destRectangle;
                        Rectangle box2 = go.GetComponent<SpriteRenderer>().destRectangle;
                        String location = CollisionSide(playerBox, box2);
                        int distance = CollisionDistance(playerBox, box2);

                        if (go.type.Contains("Platform"))
                        {
                            HandleHillCollision(go);
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
                            HandleHillCollision(go);
                        }
                        if (go.type.Contains("Stump"))
                        {
                            HandleHillCollision(go);
                        }
                        if (go.type.Contains("Enemy"))
                        {
                            HandleEnemyCollision(go);
                        }
                    }
                    

                }
            }
        }

        //maybe move this into collider.cs
        private string CollisionSide(Rectangle playerBounds, Rectangle itemBounds)
        {
            // Determine the side of the player collider where the collision happens
            int leftDiff = Math.Abs(playerBounds.Right - itemBounds.Left);
            int rightDiff = Math.Abs(playerBounds.Left - itemBounds.Right);
            int topDiff = Math.Abs(playerBounds.Bottom - itemBounds.Top);
            int bottomDiff = Math.Abs(playerBounds.Top - itemBounds.Bottom);

            // Find the smallest difference (closest side)
            int minDiff = Math.Min(Math.Min(leftDiff, rightDiff), Math.Min(topDiff, bottomDiff));

            if (minDiff == leftDiff) return "Left";
            if (minDiff == rightDiff) return "Right";
            if (minDiff == topDiff) return "Top";
            return "Bottom";
        }

        //maybe move this into collider.cs
        public int CollisionDistance(Rectangle playerBounds, Rectangle itemBounds)
        {
            // Determine the side of the player collider where the collision happens
            int leftDiff = Math.Abs(playerBounds.Right - itemBounds.Left);
            int rightDiff = Math.Abs(playerBounds.Left - itemBounds.Right);
            int topDiff = Math.Abs(playerBounds.Bottom - itemBounds.Top);
            int bottomDiff = Math.Abs(playerBounds.Top - itemBounds.Bottom);

            // Find the smallest difference (closest side)
            int minDiff = Math.Min(Math.Min(leftDiff, rightDiff), Math.Min(topDiff, bottomDiff));

            return minDiff;
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

        public void HandlePlatformCollision(String location, int distance)
        {
            if (location != null)
            {
                if(location == "Bottom")
                {
                    player.GroundLevel = player.GameObject.Y + player.height;
                    player.floorY = player.GameObject.Y + player.height;
                    player.IsGrounded = true;
                }
                else if (location == "Right")
                {
                    player.GameObject.X = player.GameObject.X + distance + 1;
                }
                else if (location == "Left")
                {
                    player.GameObject.X = player.GameObject.X - distance - 1;
                }
                else
                {

                }
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
