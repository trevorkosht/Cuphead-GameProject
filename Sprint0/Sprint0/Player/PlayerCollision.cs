using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<string, SoundEffectInstance> SFXList = new Dictionary<string, SoundEffectInstance>();

        public PlayerCollision(PlayerState player, BoxCollider collider, PlayerAnimation playerAnimator)
        {
            this.player = player;
            this.collider = collider;
            this.playerAnimator = playerAnimator;
        }

        public void CollisionCheck()
        {
            bool collidedObstacle = false;
            player.CanParry = false;
            List<GameObject>go = GOManager.Instance.allGOs;
            for(int i = 0; i < go.Count; i++)
            {
                if (go[i].type != null)
                {
                    if (collider.Intersects(go[i].GetComponent<Collider>()))
                    {
                        if (go[i].type.Contains("Slope"))
                        {
                            HandleSlopeCollision(go[i]);
                            collidedObstacle = true;
                        }
                        else if (go[i].type.Contains("Platform"))
                        {
                            HandlePlatformCollision(go[i]);
                            collidedObstacle = true;
                        }
                        else if (go[i].type.Contains("Item"))
                        {
                            HandleItemCollision(go[i]);
                        }
                        else if (go[i].type.Contains("Hill") || go[i].type.Contains("Log") || go[i].type.Contains("Stump"))
                        {
                            HandleObstacleCollision(go[i]);
                            collidedObstacle = true;
                        }
                        else if (go[i].type.Contains("Enemy") || go[i].type.Contains("NPCProjectile"))
                        {
                            HandleEnemyCollision(go[i]);
                        }
                    }
                    else 
                    {
                        if (go[i].type.Contains("SpikyBulb") && !player.CanParry) {
                            int xDist = Math.Abs(go[i].X - player.GameObject.X - 16);
                            int yDist = Math.Abs(go[i].Y - player.GameObject.Y - 52);
                            player.CanParry = (xDist <= 95) && (yDist <= 135);
                            player.parryableObject = go[i];
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

        public void HandleItemCollision(GameObject item) {
            if (collider.Intersects(item.GetComponent<Collider>()))
            {
                item.type = item.type.Remove(0, 10);
                string itemName = item.type;
                switch (itemName) {
                    case "Spreadshot":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Spreadshot] = true;  break;
                    case "Lobber":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Lobber] = true; break;
                    case "Roundabout":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Roundabout] = true; break;
                    case "Chaser":
                        player.projectileUnlock[(int)PlayerState.projectiletype.Chaser] = true; break;
                    case "Coin":
                        GOManager.Instance.audioManager.getInstance("CoinPickup").Play();
                        player.coinCount++;
                        Rectangle destRectangle = item.GetComponent<SpriteRenderer>().destRectangle;
                        destRectangle.X *= 1;
                        destRectangle.Y *= 1;

                        GameObject effect = VisualEffectFactory.createVisualEffect(destRectangle, GOManager.Instance.textureStorage.GetTexture("CoinVFX"), 2, 17, 0.5f, true);
                        effect.GetComponent<VisualEffectRenderer>().animation = new Animation(GOManager.Instance.textureStorage.GetTexture("CoinVFX"), 2, 17, 288, 288);
                        break;
                    default:
                        break;
                }
                if(itemName != "Coin")
                {
                    GOManager.Instance.audioManager.getInstance("ProjectileItemPickup").Play();
                }

                item.Destroy();
            }
        }
        
        public void HandleEnemyCollision(GameObject Enemy)
        {
            if (collider.Intersects(Enemy.GetComponent<Collider>()))
            {
                if(!player.IsInvincible && !player.IsDead)
                {
                    GOManager.Instance.audioManager.getInstance("PlayerDamaged").Play();
                }
                if (Enemy.type.Contains("AcornMaker"))
                {
                    player.GameObject.X = Enemy.X - player.playerWidth + 40;
                }
            }

        }
    }
}
