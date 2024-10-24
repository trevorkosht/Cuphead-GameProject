﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Player
{
    internal class PlayerAnimation
    {
        private PlayerState player;
        public PlayerAnimation(PlayerState player) 
        {
            this.player = player;
        }

        public void HandleSpawnAnimation()
        {
            SpriteRenderer animator = player.GameObject.GetComponent<SpriteRenderer>();
            animator.setAnimation("Spawn");

            if (animator.IsAnimationComplete())
            {
                player.IsSpawning = false;
                animator.setAnimation("Idle");
            }
        }

        public void UpdateAnimationState(SpriteRenderer animator)
        {
            if (player.IsDead)
            {
                animator.setAnimation("Death");
                return;
            }

            if (player.hitTime > 0)
            {
                animator.setAnimation(player.IsGrounded ? "HitGround" : "HitAir");
            }
            else if (player.IsDucking)
            {
                animator.setAnimation(player.shootTime > 0 ? "DuckShoot" : "Duck");
            }
            else if (player.IsDashing)
            {
                animator.setAnimation(player.IsGrounded ? "DashGround" : "DashAir");

            }
            else if (!player.IsGrounded)
            {
                animator.setAnimation("Jump");
            }
            else if (player.IsRunning)
            {
                animator.setAnimation(player.shootTime > 0 ? "RunShootingStraight" : "Run");
                if (animator.currentAnimation.Value.CurrentFrame == 5 || animator.currentAnimation.Value.CurrentFrame == 12)
                {
                    CreateDustEffect();
                }
            }
            else if (player.shootTime > 0)
            {
                animator.setAnimation("ShootStraight");
            }
            else
            {
                animator.setAnimation("Idle");
            }
        }

        public void CreateShootingEffect(bool isFacingRight)
        {
            Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
            Rectangle effectPosition;
            if (isFacingRight)
            {
                effectPosition = new Rectangle(player.GameObject.X + 100, player.GameObject.Y + 25, 144, 144);
            }
            else
            {
                effectPosition = new Rectangle(player.GameObject.X - 25, player.GameObject.Y + 25, 144, 144);
            }

            Texture2D effectTexture;

            switch ((int)player.currentProjectileType)
            {
                case 0:
                    effectTexture = textureStorage.GetTexture("PeashooterSpawn");
                    break;
                case 1:
                    effectTexture = textureStorage.GetTexture("SpreadSpawn");
                    break;
                case 2:
                    effectTexture = textureStorage.GetTexture("ChaserSpawn");
                    break;
                case 3:
                    effectTexture = textureStorage.GetTexture("LobberSpawn");
                    break;
                case 4:
                    effectTexture = textureStorage.GetTexture("RoundaboutSpawn");
                    break;
                default:
                    effectTexture = null;
                    break;
            }
            VisualEffectFactory.createVisualEffect(effectPosition, effectTexture, updatesPerFrame: 2, frameCount: 4, scale: 0.5f, true);
        }

        public void CreateDustEffect()
        {
            Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
            Rectangle dustPosition = new Rectangle(player.GameObject.X, player.GameObject.Y + 10, 144, 144); // Adjust Y position as needed
            Texture2D dustTexture = textureStorage.GetTexture("Dust");
            GameObject dustEffect = VisualEffectFactory.createVisualEffect(dustPosition, dustTexture, updatesPerFrame: 1, frameCount: 14, scale: 1f, true);
        }


    }
}
