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

        public PlayerState player { get; set; }

        private PlayerAnimation playerAnimation;
        private PlayerCollision playerCollision;
        private PlayerHealth playerHealth;
        private PlayerMovement playerMovement;
        private PlayerProjectile playerProjectile;

        private readonly KeyboardController keyboardController;
        private readonly MouseController mouseController;
        
        private BoxCollider Collider;
        float deltaTime;

        public PlayerController2(GameObject playerObject)
        {
            player = new PlayerState(playerObject);

            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            Collider = player.GameObject.GetComponent<BoxCollider>();

            playerAnimation = new PlayerAnimation(player);
            playerCollision = new PlayerCollision(player, Collider, playerAnimation);
            playerMovement = new PlayerMovement(player, keyboardController, playerAnimation, playerCollision);
            playerHealth = new PlayerHealth(player, keyboardController, playerCollision, playerMovement);
            playerProjectile = new PlayerProjectile(player, keyboardController, playerAnimation);
            this.playerObject = playerObject;

            playerAnimation.HandleSpawnAnimation();
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

            KeyboardState state = Keyboard.GetState();
            SpriteRenderer animator = player.GameObject.GetComponent<SpriteRenderer>();
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            //playerHealth.IsPleayerDead(animator);

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
            playerHealth.UpdateInvincible(gameTime);
        }

        private void UpdateTimers(float deltaTime)
        {
            player.shootTime -= deltaTime;
            player.hitTime -= deltaTime;
            player.dashTime -= deltaTime;
            player.dashTime -= deltaTime;
            player.knockBackTime -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch) { }

    }
}
