using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Player
{
    internal class PlayerMovement
    {
        private PlayerState player;
        private KeyboardController keyboardController;
        private BoxCollider collider;
        private DelayGame gameDelay = new DelayGame();
        private PlayerAnimation playerAnimator;
        public PlayerMovement(PlayerState player, KeyboardController keyboard, BoxCollider collider, PlayerAnimation playerAnimator)
        {
            this.player = player;
            this.keyboardController = keyboard;
            this.collider = collider;
            this.playerAnimator = playerAnimator;
        }


        public void HandleMovementAndActions(GameTime gameTime, float deltaTime)
        {
            Vector2 input = keyboardController.GetMovementInput();
            bool jumpRequested = keyboardController.IsJumpRequested();
            bool duckRequested = keyboardController.IsDuckRequested();
            bool dashRequested = keyboardController.IsDashRequested();

            UpdateFacingDirection(input);

            HandleDucking(duckRequested);

            if (!player.IsDucking)
            {
                if (input.X != 0 && player.IsGrounded)
                {
                    player.IsRunning = true;
                }
                else
                {
                    player.IsRunning = false;
                }
                player.GameObject.X += (int)(input.X * player.Speed * deltaTime);

                HandleDash(dashRequested, gameTime, deltaTime);
            }

            if (jumpRequested && player.IsGrounded && !player.IsDucking)
            {
                player.velocity.Y = player.JumpForce;
                player.IsGrounded = false;
            }

        }

        private void UpdateFacingDirection(Vector2 input)
        {
            if (input.X < 0 && !player.IsDashing)
            {
                player.GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
            }
            else if (input.X > 0 && !player.IsDashing)
            {
                player.GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
            }
        }

        public void UpdateGravity(float deltaTime)
        {
            if (!player.IsGrounded)
            {
                player.airTime += deltaTime;
                //velocity.Y += Gravity * deltaTime * airTime * 2;
                player.velocity.Y += player.Gravity * deltaTime;

                player.GameObject.Y += (int)(player.velocity.Y * deltaTime);
            }
        }

        private void HandleDucking(bool duckRequested)
        {
            var animator = player.GameObject.GetComponent<SpriteRenderer>();
            if (player.hitTime > 0) return;

            if (duckRequested && player.IsGrounded)
            {
                if (!player.IsDucking)
                {
                    player.GameObject.Y = player.floorY + player.DuckingYOffset;
                    player.IsDucking = true;
                    player.isDuckingYAdjust = true;

                    collider.bounds = new Vector2(144, 70);
                    collider.offset = new Vector2(0, 30);
                }
            }
            else
            {
                if (player.IsDucking)
                {
                    player.GameObject.Y = player.floorY;
                    player.IsDucking = false;
                    player.isDuckingYAdjust = false;
                    collider.bounds = new Vector2(90, 144);
                    collider.offset = new Vector2(25, 0);
                }
            }
        }

        private void HandleDash(bool dashRequested, GameTime gameTime, float deltaTime)
        {
            if (player.IsDashing)
            {
                // Continue dashing
                PerformDash(gameTime, player.height, deltaTime);
            }
            else if (dashRequested && gameDelay.Cooldown(gameTime, player.TimeTillNextDash))
            {
                // Start dash
                player.IsDashing = true;
                player.dashTime = player.dashDuration;
                player.height = player.GameObject.Y;
                player.Gravity = 0;
                player.Speed = 0;
                playerAnimator.CreateDustEffect();
                PerformDash(gameTime, player.height, deltaTime);
            }
        }

        private void PerformDash(GameTime gameTime, int height, float deltaTime)
        {
            if (player.dashTime > 0)
            {
                // Continue dashing within the duration
                float dashDistance = player.dashSpeed * deltaTime;


                player.GameObject.GetComponent<SpriteRenderer>().spriteScale = 1 + 7 * (player.dashDuration - player.dashTime) / (12 * player.dashDuration);

                if (player.GameObject.GetComponent<SpriteRenderer>().isFacingRight)
                {
                    player.GameObject.X += (int)dashDistance;
                }
                else
                {
                    player.GameObject.X -= (int)dashDistance;
                }
                player.GameObject.Y = height;
            }
            else
            {
                // Dash duration is over, stop dashing
                player.GameObject.GetComponent<SpriteRenderer>().spriteScale = 1f;
                player.IsDashing = false;
                player.Gravity = 1200f;
                player.Speed = 700f;
            }
        }
    }
}
