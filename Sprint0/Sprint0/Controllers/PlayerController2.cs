using Cuphead.Player;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;

namespace Cuphead.Controllers
{
    internal class PlayerController2 :IComponent
    {
        public GameObject GameObject { get; set; }
        public GameObject playerObject;
        public bool enabled { get; set; } = true;

        private PlayerState player;
        private PlayerAnimation playerAnimation;
        private PlayerCollision playerCollision;
        private PlayerHealth playerHealth;
        private PlayerMovement playerMovement;
        private PlayerProjectile playerProjectile;

        private Texture2DStorage texture = new Texture2DStorage();
        private readonly KeyboardController keyboardController;
        private readonly MouseController mouseController;
        private ProjectileFactory projectileFactory;
        private BoxCollider Collider;
        float deltaTime;

        public PlayerController2(GameObject playerObject)
        {
            player = new PlayerState(playerObject);

            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            projectileFactory = new ProjectileFactory();
            Collider = player.GameObject.GetComponent<BoxCollider>();

            playerAnimation = new PlayerAnimation(player);
            playerCollision = new PlayerCollision(player, Collider, playerAnimation);
            playerHealth = new PlayerHealth(player, keyboardController);
            playerMovement = new PlayerMovement(player, keyboardController, Collider, playerAnimation);
            playerProjectile = new PlayerProjectile(player, keyboardController, projectileFactory, playerAnimation);
            this.playerObject = playerObject;
            
            if (player.IsSpawning)
            {
                playerAnimation.HandleSpawnAnimation();
                return;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (player.IsSpawning)
            {
                playerAnimation.HandleSpawnAnimation();
                return;
            }

            keyboardController.Update();
            mouseController.Update();

            var state = Keyboard.GetState();
            var animator = player.GameObject.GetComponent<SpriteRenderer>();
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player.IsDead)
            {
                if (animator.IsAnimationComplete())
                {
                    player.GameObject.destroyed = true;
                }
                return;
            }

            UpdateTimers(deltaTime);
            playerCollision.CollisionCheck();
            playerCollision.HandleGroundCheck(animator);
            playerCollision.CollisionCheck();
            playerMovement.HandleMovementAndActions(gameTime, deltaTime);
            playerProjectile.HandleShooting(state, animator);
            playerProjectile.HandleProjectileSwitching(state);
            playerHealth.HandleDamageDetection();
            playerMovement.UpdateGravity(deltaTime);
            playerAnimation.UpdateAnimationState(animator);
        }

        private void UpdateTimers(float deltaTime)
        {
            player.shootTime -= deltaTime;
            player.hitTime -= deltaTime;
            player.dashTime -= deltaTime;
            player.dashTime -= deltaTime;

            if (player.IsInvincible)
            {
                player.hitTime -= deltaTime;
                if (player.hitTime <= 0)
                {
                    player.IsInvincible = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) { }

    }
}
