using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Cuphead.Player
{
    internal class RunAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public RunAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            if (player.shootTime > 0)
            {
                string animation = player.ShootUp ? "RunShootingDiagonalUp" : "RunShootingStraight";
                animator.setAnimation(animation);
            }
            else
            {
                animator.setAnimation("Run");
            }

            if (animator.currentAnimation.Value.CurrentFrame == 5 || animator.currentAnimation.Value.CurrentFrame == 12)
            {
                CreateDustEffect();
            }
        }

        private void CreateDustEffect()
        {
            Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
            var dustPosition = new Rectangle(player.GameObject.X, player.GameObject.Y + 10, 144, 144);
            Texture2D dustTexture = textureStorage.GetTexture("Dust");
            VisualEffectFactory.createVisualEffect(dustPosition, dustTexture, updatesPerFrame: 1, frameCount: 14, scale: 1f, true);
        }
    }
}
