using Microsoft.Xna.Framework;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Player
{
    internal class PlayerHealth
    {
        private PlayerState player;
        private KeyboardController keyboardController;
        private PlayerCollision collision;
        private DelayGame delayGame = new DelayGame();

        private HealthComponent health = new HealthComponent(100);

        public PlayerHealth(PlayerState player, KeyboardController keyboardController, PlayerCollision collision)
        {
            this.player = player;
            this.keyboardController = keyboardController;
            this.collision = collision;
            player.GameObject.AddComponent(health);
        }

        public void HandleDamageDetection()
        {
            if (!player.IsInvincible && !player.IsDead)
            {
                if (collision.TypeCollide("Enemy"))
                {
                    TakeDamage(35);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            health.RemoveHealth(damage);
            player.hitTime = player.InvincibilityDuration;
            player.IsInvincible = true;

            if (health.isDead)
            {
                player.IsDead = true;
            }
            else
            {
                player.GameObject.GetComponent<SpriteRenderer>().setAnimation("HitGround");
            }
        }

        public void UpdateInvincible(GameTime gameTime)
        {
            if (player.IsInvincible)
            {
                if (delayGame.Cooldown(gameTime, player.InvincibilityDuration))
                {
                    player.IsInvincible = false;  // End invincibility after cooldown
                }
            }
        }
    }
}
