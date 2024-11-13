using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Timers;

public class DeadlyDaisy : BaseEnemy {
    private float speed;
    private Vector2 airVelocity;
    private int airTime = 50;
    private float gravity = 0.5f;
    public bool movingRight { get; private set; }
    public float turnDelay { get; private set; } = -1.0f;
    private DaisyCollisionManager collisionManager;
    private DaisyState state;

    public override void Initialize(Texture2D texture, Texture2DStorage storage) {
        base.Initialize(texture, storage);
        speed = 300f;
        airVelocity = Vector2.Zero;
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

            GameObject.Move((int)airVelocity.X, (int)airVelocity.Y);
        }
        else if (GameObject.Y <= 0 && GameObject.X - player.X < 750) {
            state.Spawned = true;
        }
    }

    private void HandleAirMovement(GameTime gameTime) {
        if (state.isJumping && !state.isSpawning) {
            airVelocity.Y += gravity;
        }
        else {
            movingRight = player.X > GameObject.X;
            airVelocity.Y += gravity / 15;
        }
    }

    private void HandleGroundMovement(GameTime gameTime) {
        if (!state.isJumping || turnDelay < 1.5) {
            state.isJumping = false;
            airVelocity = Vector2.Zero;
        }

        if (state.atPlatformEdge || GameObject.X < 1250) {
            HandlePlatformEdge();
        }
        else {
            if (state.currentPlatform.type != null && state.currentPlatform.type.Contains("Slope")) {
                HandleSlopeCollision();
            }

            state.isWalking = true;

            if (movingRight)
                GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            else
                GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    private void HandlePlatformEdge() {
        if (!state.foundAdjacentPlatform || GameObject.X < 1250) {
            if ((!state.jumpRequested || ((movingRight != player.X > GameObject.X && Math.Abs(player.X - GameObject.X) > 500) || (GameObject.X < 1250 && !movingRight)))) {
                state.isTurning = true;
                state.jumpRequested = false;
                HandleTurnRequested();
            }
            else if (state.jumpRequested) {
                HandleJumpRequested();
            }
        }

    }

    private void HandleTurnRequested() {
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
        if (sRend.currentAnimation.Value.CurrentFrame == 7) {
            Rectangle landingSpot = state.landingSpot;
            float verticalSpeed = Math.Abs(((landingSpot.Y - GameObject.Y) - gravity * airTime * airTime / 2) / (airTime));
            float horizontalSpeed = (landingSpot.X - GameObject.X) / (airTime);

            airVelocity = new Vector2(horizontalSpeed, -verticalSpeed);

            state.isTurning = false;
            state.jumpRequested = false;
            state.isJumping = true;
            turnDelay = 1.75f;
        }
    }


    private void HandleSlopeCollision() {
        BoxCollider daisyCollider = GameObject.GetComponent<BoxCollider>();
        BoxCollider slopeCollider = state.currentPlatform.GetComponent<BoxCollider>();
        Rectangle daisyBounds = daisyCollider.BoundingBox;

        // Get the rotated corners of the obstacle's bounding box (assuming the obstacle is sloped)
        Vector2[] slopeCorners = slopeCollider.GetRotatedCorners();

        // Get the top edge of the slope (assuming it's a left-to-right slope)
        Vector2 topLeft = slopeCorners[0]; // Top-left corner of the slope
        Vector2 topRight = slopeCorners[1]; // Top-right corner of the slope

        // Check if the player is within the horizontal bounds of the slope's top edge
        if (daisyBounds.Bottom > Math.Min(topLeft.Y, topRight.Y) && daisyBounds.Left >= topLeft.X && daisyBounds.Right <= topRight.X) {
            float slopeHeightAtDaisyX = MathHelper.Lerp(topLeft.Y, topRight.Y, (daisyBounds.Center.X - topLeft.X) / (topRight.X - topLeft.X));

            if (daisyBounds.Bottom + 20 > slopeHeightAtDaisyX) {
                GameObject.Y = (int)slopeHeightAtDaisyX - daisyBounds.Height + 10;
            }
        }
        else if (daisyBounds.Right < topLeft.X) // Left of the slope
        {
            GameObject.X = (int)(topLeft.X - daisyBounds.Width - 5);
        }
        else if (daisyBounds.Left > topRight.X) // Right of the slope
        {
            GameObject.X = (int)(topRight.X + 5);
        }
    }

    public override void Shoot(GameTime gameTime) {
        // Implement shooting logic if needed
    }
}
