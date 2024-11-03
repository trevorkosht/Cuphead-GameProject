using Microsoft.Xna.Framework.Audio;
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
        private ProjectileFactory projectileFactory = new ProjectileFactory();
        private PlayerAnimation playerAnimator;
        private int additionalShotHeight, shotsFired;
        public PlayerProjectile(PlayerState player, KeyboardController keyboardController, PlayerAnimation animator)
        {
            this.player = player;
            this.keyboardController = keyboardController;
            this.playerAnimator = animator;
            this.additionalShotHeight = 0;
            this.shotsFired = 0;
        }

        public void HandleShooting(KeyboardState state, SpriteRenderer animator)
        {
            if (!keyboardController.IsShootRequested())
            {
                player.isShooting = false;
                getCurrentProjectileSFX(player.currentProjectileType).Stop();
            }
            else
            {
                getCurrentProjectileSFX(player.currentProjectileType).Play();
                if (player.shootTime <= 0 && player.hitTime <= 0)
                {
                    player.isShooting = true;
                    player.shootTime = player.timeTillNextBullet;
                    GameObject newProjectile;

                    additionalShotHeight = AddProjectileHeight(shotsFired, player.currentProjectileType);

                    if (player.GameObject.GetComponent<SpriteRenderer>().isFacingRight)
                    {
                        newProjectile = ProjectileFactory.CreateProjectile(player.currentProjectileType, player.GameObject.X, player.GameObject.Y + additionalShotHeight, player.GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                        playerAnimator.CreateShootingEffect(true);
                    }
                    else
                    {
                        newProjectile = ProjectileFactory.CreateProjectile(player.currentProjectileType, player.GameObject.X - 90, player.GameObject.Y + additionalShotHeight, player.GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                        playerAnimator.CreateShootingEffect(false);
                    }
                    GOManager.Instance.allGOs.Add(newProjectile);

                    shotsFired++;
                    if (shotsFired == 5)
                    {
                        shotsFired = 0;
                    }
                }
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
                1 => 1 / (41.33f / 10.2f), // Spread
                2 => 1 / (25f / 8.3f), // Chaser
                3 => 1 / (33.14f / 11.6f), // Lobber
                4 => 1 / (25f / 8.3f), // Roundabout
                _ => player.timeTillNextBullet
            };
        }

        private int AddProjectileHeight(int shotsFired, ProjectileType type)
        {
            if ((type != ProjectileType.Peashooter && type != ProjectileType.Chaser) || shotsFired == 0 || shotsFired == 4)
            {
                return 0;
            }
            else if (shotsFired == 1 || shotsFired == 3)
            {
                return 10;
            }
            else
            {
                return 20;
            }
        }

        private SoundEffectInstance getCurrentProjectileSFX(ProjectileType type)
        {
            string currentProjectileSFXName = "PeashooterShotLoop";
            switch (type)
            {
                case ProjectileType.Peashooter:
                    currentProjectileSFXName = "PeashooterShotLoop";
                    break;
                case ProjectileType.SpreadShot:
                    currentProjectileSFXName = "SpreadshotShotLoop";
                    break;
                case ProjectileType.Chaser:
                    currentProjectileSFXName = "ChaserShotLoop";
                    break;
                case ProjectileType.Lobber:
                    currentProjectileSFXName = "LobberShot";
                    break;
                case ProjectileType.Roundabout:
                    currentProjectileSFXName = "RoundaboutShot";
                    break;
            }
            
            return GOManager.Instance.audioManager.getInstance(currentProjectileSFXName);
        }
    }
}
