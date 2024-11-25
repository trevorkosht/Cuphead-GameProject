using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Timers;

public class DeadlyDaisy : BaseEnemy {
    private float speed;
    private Vector2 Velocity;
    private int airTime = 50;
    private float gravity = 0.5f;
    public bool movingRight { get; private set; }
    public float turnDelay { get; private set; } = -1.0f;
    private DaisyCollisionManager collisionManager;
    private DaisyState state;

    public override void Initialize(Texture2D texture, Texture2DStorage storage) {
        base.Initialize(texture, storage);
        speed = 4f;
        Velocity = Vector2.Zero;
    }

    public override void Move(GameTime gameTime) {
        sRend.isFacingRight = !movingRight;
        collisionManager = GameObject.GetComponent<DaisyCollisionManager>();
        state = GameObject.GetComponent<DaisyState>();

        if (state.Spawned) {
            if (state.isGrounded) {
                HandleGroundMovement(gameTime);
            }
            else {
                HandleAirMovement(gameTime);
            }
            turnDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            GameObject.Move((int)Velocity.X, (int)Velocity.Y);
        }
        else if (GameObject.Y <= 0 && GameObject.X - player.X < 750) {
            state.Spawned = true;
        }
    }

    private void HandleAirMovement(GameTime gameTime) {
        if (state.isJumping && !state.isSpawning) {
            Velocity.Y += gravity;
        }
        else {
            movingRight = player.X > GameObject.X;
            Velocity.Y += gravity / 15;
        }
    }

    private void HandleGroundMovement(GameTime gameTime) {
        if (!state.isJumping || turnDelay < 1.5) {
            state.isJumping = false;
            Velocity = Vector2.Zero;
        }

        if (state.atPlatformEdge || GameObject.X < 1250) {
            HandlePlatformEdge();
        }
        else if (state.jumpRequested) {
            HandleJumpRequested();
        }
        else {
            state.isWalking = true;
            state.isTurning = false;

            if (movingRight)
                Velocity.X = speed;
            else
                Velocity.X = -1 * speed;
        }
    }

    private void HandlePlatformEdge() {
        if (!state.foundAdjacentPlatform || GameObject.X < 1250) {
            if ((!state.jumpRequested || ((movingRight != player.X > GameObject.X && Math.Abs(player.X - GameObject.X) > 500) || (GameObject.X < 1250 && !movingRight)))) {
                HandleTurnRequested();
            }
            else if (state.jumpRequested) {
                HandleJumpRequested();
            }
        }

    }

    private void HandleTurnRequested() {
        state.isTurning = true;
        state.jumpRequested = false;
        state.isWalking = false;

        //Makes the sprite look a little less offset during the animation.
        if (movingRight) {
            GameObject.X += 1;
        }
        else {
            GameObject.X -= 1;
        }

        if (sRend.currentAnimation.Value.CurrentFrame == 17) {
            movingRight = !movingRight;
            state.isTurning = false;
            turnDelay = 3.0f;
        }
    }


    private void HandleJumpRequested() {
        state.isTurning = false;
        state.isWalking = false;
        Velocity.X = 0;

        if (sRend.currentAnimation.Value.CurrentFrame == 7) {
            Rectangle landingSpot = state.landingSpot;
            float verticalSpeed = Math.Abs(((landingSpot.Y - GameObject.Y) - gravity * airTime * airTime / 2) / (airTime));
            float horizontalSpeed = (landingSpot.X - GameObject.X) / (airTime);

            Velocity = new Vector2(horizontalSpeed, -verticalSpeed);

            state.jumpRequested = false;
            state.isJumping = true;
            turnDelay = 1.75f;
        }
    }
    public override void Shoot(GameTime gameTime) {
        // Implement shooting logic if needed
    }
}
