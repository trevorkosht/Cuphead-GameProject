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
        private PlayerMovement move;
        private DelayGame delayGame = new DelayGame();

        public HealthComponent health = new HealthComponent(300, false, true); //300

        public PlayerHealth(PlayerState player, KeyboardController keyboardController, PlayerCollision collision, PlayerMovement move)
        {
            this.player = player;
            this.keyboardController = keyboardController;
            this.collision = collision;
            this.move = move;
            player.GameObject.AddComponent(health);
        }

        public void HandleDamageDetection()
        {
            if (player.GameObject.Y > 600)
            {
                TakeDamage(300);
            }
            if (!player.IsInvincible && !player.IsDead)
            {
                GameObject enemy = collision.TypeCollide("Enemy");
                if(enemy == null){
                    enemy = collision.TypeCollide("NPCProjectile");
                }
                if (enemy != null)
                {
                    TakeDamage(50);
                    GOManager.Instance.audioManager.getInstance("PlayerDamaged").Play();
                } else
                {
                    GOManager.Instance.audioManager.getInstance("PlayerDamaged").Stop();
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
                player.IsTakingDamage = true;
                SpriteRenderer sRend = player.GameObject.GetComponent<SpriteRenderer>();


                if (player.IsGrounded) {
                    sRend.setAnimation("HitGround");
                }
                else {
                    sRend.setAnimation("HitAir");
                }
                

            }
        }

        public void UpdateInvincible(GameTime gameTime)
        {
            if (player.IsInvincible)
            {
                if (delayGame.Cooldown(gameTime, player.InvincibilityDuration))
                {
                    player.IsInvincible = false; 
                }
            }
        }

        public void IsPleayerDead(SpriteRenderer animator)
        {
            if (player.IsDead)
            {
                if (animator.IsAnimationComplete())
                {
                    //player.GameObject.destroyed = true;
                }
                return;
            }
        }

    }
}
