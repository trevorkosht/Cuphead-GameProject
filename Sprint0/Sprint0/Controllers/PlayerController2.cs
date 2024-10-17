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

        //player contains all of the players variables
        private PlayerState player;

        //those 5 classes handle the 5 parts of player control
        private PlayerAnimation playerAnimation;
        private PlayerCollision playerCollision;
        private PlayerHealth playerHealth;
        private PlayerMovement playerMovement;
        private PlayerProjectile playerProjectile;

        private readonly KeyboardController keyboardController;
        private readonly MouseController mouseController;
        private ProjectileFactory projectileFactory;
        private BoxCollider Collider;
        float deltaTime;
        private int shotsFired;

        public PlayerController2(GameObject playerObject)
        {
            player = new PlayerState(playerObject);

            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            projectileFactory = new ProjectileFactory();
            Collider = player.GameObject.GetComponent<BoxCollider>();

            playerAnimation = new PlayerAnimation(player);
            playerCollision = new PlayerCollision(player, Collider, playerAnimation);
            playerHealth = new PlayerHealth(player, keyboardController, playerCollision);
            playerMovement = new PlayerMovement(player, keyboardController, Collider, playerAnimation);
            playerProjectile = new PlayerProjectile(player, keyboardController, projectileFactory, playerAnimation);
            this.playerObject = playerObject;
            this.shotsFired = 0;
            
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


            playerHealth.IsPleayerDead(animator);

            UpdateTimers(deltaTime);
            playerCollision.CollisionCheck();
            playerCollision.HandleGroundCheck(animator);
            playerCollision.CollisionCheck();
            playerMovement.HandleMovementAndActions(gameTime, deltaTime);
            shotsFired += playerProjectile.HandleShooting(state, animator, shotsFired);
            playerProjectile.HandleProjectileSwitching(state);
            playerHealth.HandleDamageDetection();
            playerMovement.UpdateGravity(deltaTime);
            playerAnimation.UpdateAnimationState(animator);
            playerHealth.UpdateInvincible(gameTime);

            if(shotsFired == 5)
            {
                shotsFired = 0;
            }
        }

        private void UpdateTimers(float deltaTime)
        {
            player.shootTime -= deltaTime;
            player.hitTime -= deltaTime;
            player.dashTime -= deltaTime;
            player.dashTime -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch) { }

    }
}
