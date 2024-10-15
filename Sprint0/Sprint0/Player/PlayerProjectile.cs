using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Player
{
    internal class PlayerProjectile
    {
        private PlayerState player;
        private KeyboardController keyboardController;
        private ProjectileFactory projectileFactory;
        private PlayerAnimation playerAnimator;
        public PlayerProjectile(PlayerState player, KeyboardController keyboardController, ProjectileFactory projectileFactory, PlayerAnimation animator)
        {
            this.player = player;
            this.keyboardController = keyboardController;
            this.projectileFactory = projectileFactory;
            this.playerAnimator = animator;
        }

        public void HandleShooting(KeyboardState state, SpriteRenderer animator)
        {
            if (keyboardController.IsShootRequested() && player.shootTime <= 0 && player.hitTime <= 0)
            {
                player.isShooting = true;
                player.shootTime = player.timeTillNextBullet;
                GameObject newProjectile;
                if (player.GameObject.GetComponent<SpriteRenderer>().isFacingRight)
                {
                    newProjectile = ProjectileFactory.CreateProjectile(player.currentProjectileType, player.GameObject.X, player.GameObject.Y, player.GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                    playerAnimator.CreateShootingEffect(true);
                }
                else
                {
                    newProjectile = ProjectileFactory.CreateProjectile(player.currentProjectileType, player.GameObject.X - 90, player.GameObject.Y, player.GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                    playerAnimator.CreateShootingEffect(false);
                }
                GOManager.Instance.allGOs.Add(newProjectile);

            }
        }

        public void HandleProjectileSwitching(KeyboardState state)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (keyboardController.IsProjectileSwitchRequested(i) && player.projectileUnlock[i])
                {
                    player.currentProjectileType = (ProjectileType)(i - 1);
                    player.timeTillNextBullet = GetBulletCooldown(i - 1);
                    break;
                }
            }
        }

        private float GetBulletCooldown(int projectileType)
        {
            return projectileType switch
            {
                0 => 1 / (25f / 8.3f), // Default
                1 => 1 / (41.33f / 6.2f), // Spread
                2 => 1 / (25.1f / 13.85f), // Chaser
                3 => 1 / (33.14f / 11.6f), // Lobber
                4 => 1 / (20.38f / 8f), // Roundabout
                _ => player.timeTillNextBullet
            };
        }
    }
}
