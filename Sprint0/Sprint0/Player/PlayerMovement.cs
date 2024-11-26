using Microsoft.Xna.Framework;

namespace Cuphead.Player {
    internal class PlayerMovement {
        private PlayerState player;
        private KeyboardController keyboardController;
        private BoxCollider collider;
        private DelayGame DashCooldown = new DelayGame();
        private DelayGame parryDelay = new DelayGame(); // For parry timing window
        private DelayGame parryCooldown = new DelayGame(); // For parry cooldown
        private PlayerAnimation playerAnimator;
        private PlayerCollision PlayerCollision;
        public PlayerMovement(PlayerState player, KeyboardController keyboard, PlayerAnimation playerAnimator, PlayerCollision playerCollision) {
            this.player = player;
            this.keyboardController = keyboard;
            this.playerAnimator = playerAnimator;
            this.collider = this.player.GameObject.GetComponent<BoxCollider>();
            this.PlayerCollision = playerCollision;
        }


        public void HandleMovementAndActions(GameTime gameTime, float deltaTime) {
            if (player.IsDead) {
                return;
            }
            Vector2 input = keyboardController.GetMovementInput();
            bool jumpRequested = keyboardController.IsJumpRequested();
            bool duckRequested = keyboardController.IsDuckRequested();
            bool dashRequested = keyboardController.IsDashRequested();

            UpdateFacingDirection(input);

            HandleDucking(duckRequested);

            //HandleKnockBack(player.IsTakingDamage, gameTime, deltaTime);

            if (!player.IsDucking && !player.IsKnockBacked) {
                if (input.X != 0 && player.IsGrounded) {
                    player.IsRunning = true;
                    GOManager.Instance.audioManager.getInstance("PlayerWalk").Play();
                }
                else {
                    player.IsRunning = false;
                    GOManager.Instance.audioManager.getInstance("PlayerWalk").Stop();
                }
                player.GameObject.X += (int)(input.X * player.Speed * deltaTime);

                HandleDash(dashRequested, gameTime, deltaTime);

            }

            if (!player.isFalling && jumpRequested && player.IsGrounded && !player.IsDucking) {
                if (!player.IsDead) {
                    GOManager.Instance.audioManager.getInstance("PlayerJump").Play();
                }
                player.velocity.Y = player.JumpForce;
                player.IsGrounded = false;

                player.GameObject.GetComponent<BoxCollider>().ChangeHeight(-20);
                player.changeColliderBound = true;
            }

            if (!player.IsGrounded)
            {
                if (jumpRequested && player.CanParry && parryDelay.Delay(gameTime, 300)) 
                {
                    GOManager.Instance.audioManager.getInstance("PlayerParry").Play();
                    GOManager.Instance.audioManager.getInstance("SpikyBulbDeath").Play();

                    Rectangle vfxDestRectangle = new Rectangle(player.parryableObject.X - 30, player.parryableObject.Y - 30, 288, 288);
                    VisualEffectFactory.createVisualEffect(vfxDestRectangle, GOManager.Instance.textureStorage.GetTexture("ParryVFX"), 3, 9, 0.625f, true);

                    GOManager.Instance.Player.GetComponent<ScoreComponent>().AddScore(10);
                    player.IsParrying = true;
                    player.parryCount++;
                    player.parryableObject.Destroy();

                    parryCooldown.Cooldown(gameTime, 1000);
                    player.CanParry = false; 
                }
                else if (parryCooldown.Cooldown(gameTime, 1000))
                {
                    player.CanParry = true;
                }
            }

            if (!player.IsGrounded && player.velocity.Y < 0) {
                player.isFalling = true;
            }

            if (player.isFalling && player.IsGrounded) {
                player.isFalling = false;
                GOManager.Instance.audioManager.getInstance("PlayerLanding").Play();
            }

        }

        private void UpdateFacingDirection(Vector2 input) {
            if (input.X < 0 && !player.IsDashing && !player.IsKnockBacked) {
                player.GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
            }
            else if (input.X > 0 && !player.IsDashing && !player.IsKnockBacked) {
                player.GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
            }
        }

        public void UpdateGravity(float deltaTime) {
            if (!player.IsGrounded) {
                player.airTime += deltaTime;
                //velocity.Y += Gravity * deltaTime * airTime * 2;
                player.velocity.Y += player.Gravity * deltaTime;

                player.GameObject.Y += (int)(player.velocity.Y * deltaTime);
            }
        }

        private void HandleDucking(bool duckRequested) {
            var animator = player.GameObject.GetComponent<SpriteRenderer>();
            if (player.hitTime > 0) {
                if (player.IsDucking) {
                    player.GameObject.Y = player.floorY + player.DuckingYOffset - 50;
                    player.IsDucking = false;
                    player.isDuckingYAdjust = false;
                    collider.bounds = new Vector2(90, 144);
                    collider.offset = new Vector2(25, 0);
                }
                return;
            }

            if (duckRequested && player.IsGrounded) {
                if (!player.IsDucking) {
                    player.GameObject.Y = player.floorY + player.DuckingYOffset;
                    player.IsDucking = true;
                    player.isDuckingYAdjust = true;

                    collider.bounds = new Vector2(144, 80);
                    collider.offset = new Vector2(0, 30);
                }
            }
            else {
                if (player.IsDucking) {
                    player.GameObject.Y = player.floorY + player.DuckingYOffset - 50;
                    player.IsDucking = false;
                    player.isDuckingYAdjust = false;
                    collider.bounds = new Vector2(90, 144);
                    collider.offset = new Vector2(25, 0);
                }
            }
        }

        private void HandleDash(bool dashRequested, GameTime gameTime, float deltaTime) {
            if (player.IsDashing) {
                if (!player.IsDead) {
                    GOManager.Instance.audioManager.getInstance("PlayerDash").Play();
                }
                // Continue dashing
                PerformDash(gameTime, player.height, deltaTime);
            }
            else if (!player.HasDashed && dashRequested && DashCooldown.Cooldown(gameTime, player.TimeTillNextDash) && !player.IsInvincible) {
                // Start dash
                player.IsDashing = true;
                player.HasDashed = true;
                player.dashTime = player.dashDuration;
                player.height = player.GameObject.Y;
                player.Gravity = 0;
                player.Speed = 0;
                playerAnimator.CreateDustEffect();
                PerformDash(gameTime, player.height, deltaTime);
            }
        }

        private void PerformDash(GameTime gameTime, int height, float deltaTime) {
            if (player.dashTime > 0) {
                // Continue dashing within the duration
                float dashDistance = player.dashSpeed * deltaTime;


                player.GameObject.GetComponent<SpriteRenderer>().spriteScale = 1 + 7 * (player.dashDuration - player.dashTime) / (12 * player.dashDuration);

                if (player.GameObject.GetComponent<SpriteRenderer>().isFacingRight) {
                    player.GameObject.X += (int)dashDistance;
                }
                else {
                    player.GameObject.X -= (int)dashDistance;
                }
                player.GameObject.Y = height;

                if (PlayerCollision.TypeCollide("Enemy") != null) {
                    player.GameObject.GetComponent<SpriteRenderer>().spriteScale = 1f;
                    player.IsDashing = false;
                    player.Gravity = 3000f;
                    player.Speed = 700f;
                }
            }
            else {
                // Dash duration is over, stop dashing
                player.GameObject.GetComponent<SpriteRenderer>().spriteScale = 1f;
                player.IsDashing = false;
                player.Gravity = 3000f;
                player.Speed = 700f;
            }
        }

        //public void HandleKnockBack(bool knockBackRequested, GameTime gameTime, float deltaTime) {
        //    if (player.IsKnockBacked) {
        //        // Continue knockback
        //        PerformKnockBack(gameTime, deltaTime);
        //    }
        //    else if (knockBackRequested && gameDelay.Cooldown(gameTime, player.InvincibilityDuration)) {
        //        // Start knockback
        //        player.IsKnockBacked = true;
        //        player.knockBackTime = player.InvincibilityDuration;
        //        //player.Gravity = 0;
        //        player.Speed = 0;
        //        playerAnimator.CreateDustEffect();
        //        PerformDash(gameTime, player.height, deltaTime);
        //    }
        //}

        public void PerformKnockBack(GameTime gameTime, float deltaTime) {
            if (player.knockBackTime > 0) {
                float knockbackspeed = (500f * deltaTime);

                int didtwice = 0;
                if (player.knockBackTime * 5 > player.knockBackDuration) {
                    if (player.GameObject.GetComponent<SpriteRenderer>().isFacingRight) {
                        player.GameObject.X -= (int)knockbackspeed / 5;
                    }
                    else {
                        player.GameObject.X += (int)knockbackspeed / 5;
                    }

                    player.GameObject.Y -= (int)knockbackspeed;
                    didtwice++;
                }
                else {
                    player.GameObject.GetComponent<SpriteRenderer>().setAnimation("Idle");
                }

                if (didtwice > 1 && PlayerCollision.TypeCollide("Block") != null) {
                    player.knockBackTime = 0;
                    PerformKnockBack(gameTime, deltaTime);
                }

            }
            else {
                // Dash duration is over, stop knockback
                player.IsKnockBacked = false;
                player.Gravity = 1200f;
                player.Speed = 700f;
                player.IsTakingDamage = false;
            }
        }
    }
}
