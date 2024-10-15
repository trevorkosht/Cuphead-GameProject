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
        public PlayerHealth(PlayerState player, KeyboardController keyboardController)
        {
            this.player = player;
            this.keyboardController = keyboardController;
        }

        public void HandleDamageDetection()
        {
            if (keyboardController.IsDamageRequested() && !player.IsInvincible && !player.IsDead)
            {
                TakeDamage(20); // Example damage value
            }
        }

        public void TakeDamage(int damage)
        {
            player.Health -= damage;
            player.hitTime = player.InvincibilityDuration;
            player.IsInvincible = true;

            if (player.Health <= 0)
            {
                player.IsDead = true;
            }
            else
            {
                player.GameObject.GetComponent<SpriteRenderer>().setAnimation("HitGround");
            }
        }
    }
}
