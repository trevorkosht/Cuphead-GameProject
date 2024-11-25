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
            if (player.GameObject.Y > 700)
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

                VisualEffectFactory.createVisualEffect(new Rectangle(player.GameObject.X, player.GameObject.Y, 144, 144), new Rectangle(0,0,500, 500), GOManager.Instance.textureStorage.GetTexture("PlayerDamageVFX"), 2, 7, 1.0f, true);


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
                    GOManager.Instance.audioManager.getInstance("PlayerDeath").Play();
                    //player.GameObject.destroyed = true;
                }
                return;
            }
        }

    }
}
