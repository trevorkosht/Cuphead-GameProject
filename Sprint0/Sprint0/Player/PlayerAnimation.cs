using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Cuphead.Player
{
    internal class PlayerAnimation
    {
        private readonly PlayerState player;
        private readonly Dictionary<string, IAnimationState> animationStates;

        public PlayerAnimation(PlayerState player)
        {
            this.player = player;
            animationStates = new Dictionary<string, IAnimationState>
            {
                { "Spawn", new SpawnAnimation(player) },
                { "Death", new DeathAnimation(player) },
                { "Hit", new HitAnimation(player) },
                { "Parry", new ParryAnimation(player) },
                { "Duck", new DuckAnimation(player) },
                { "Dash", new DashAnimation(player) },
                { "Jump", new JumpAnimation(player) },
                { "Run", new RunAnimation(player) },
                { "Shoot", new ShootAnimation(player) },
                { "Idle", new IdleAnimation(player) }
            };
        }

        public void HandleSpawnAnimation()
        {
            var animator = player.GameObject.GetComponent<SpriteRenderer>();
            animator.setAnimation("Spawn");

            if (animator.IsAnimationComplete())
            {
                player.IsSpawning = false;
                animator.setAnimation("Idle");
            }
        }

        public void UpdateAnimationState(SpriteRenderer animator)
        {
            string animationState = GetAnimationState();
            animationStates[animationState].Play(animator);
        }

        private string GetAnimationState()
        {
            if (player.IsDead) return "Death";
            if (player.hitTime > 0.6) return "Hit";
            if (player.IsParrying) return "Parry";
            if (player.IsDucking) return "Duck";
            if (player.IsDashing) return "Dash";
            if (!player.IsGrounded) return "Jump";
            if (player.IsRunning) return "Run";
            if (player.shootTime > 0) return "Shoot";
            return "Idle";
        }
        public void CreateShootingEffect(bool isFacingRight)
        {
            var textureStorage = GOManager.Instance.textureStorage;
            var effectPosition = CalculateEffectPosition(isFacingRight);
            var effectTexture = GetProjectileTexture(textureStorage);

            VisualEffectFactory.createVisualEffect(effectPosition, effectTexture, updatesPerFrame: 2, frameCount: 4, scale: 0.5f, isFacingRight);
        }

        private Rectangle CalculateEffectPosition(bool isFacingRight)
        {
            if (player.ShootUp)
            {
                return player.IsRunning
                    ? new Rectangle(player.GameObject.X + (isFacingRight ? 120 : -45), player.GameObject.Y - 15, 144, 144)
                    : new Rectangle(player.GameObject.X + (isFacingRight ? 70 : 5), player.GameObject.Y - 40, 144, 144);
            }
            else if (player.ShootDown)
            {
                return new Rectangle(player.GameObject.X + 30, player.GameObject.Y + 100, 144, 144);
            }
            else
            {
                return new Rectangle(player.GameObject.X + (isFacingRight ? 100 : -25), player.GameObject.Y + 25, 144, 144);
            }
        }

        private Texture2D GetProjectileTexture(Texture2DStorage textureStorage) =>
            player.currentProjectileType switch
            {
                ProjectileType.Peashooter => textureStorage.GetTexture("PeashooterSpawn"),
                ProjectileType.SpreadShot => textureStorage.GetTexture("SpreadSpawn"),
                ProjectileType.Chaser => textureStorage.GetTexture("ChaserSpawn"),
                ProjectileType.Lobber => textureStorage.GetTexture("LobberSpawn"),
                ProjectileType.Roundabout => textureStorage.GetTexture("RoundaboutSpawn"),
                _ => null
            };
        public void CreateDustEffect()
        {
            Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
            var dustPosition = new Rectangle(player.GameObject.X, player.GameObject.Y + 10, 144, 144);
            Texture2D dustTexture = textureStorage.GetTexture("Dust");
            VisualEffectFactory.createVisualEffect(dustPosition, dustTexture, updatesPerFrame: 1, frameCount: 14, scale: 1f, true);
        }
    }
}
