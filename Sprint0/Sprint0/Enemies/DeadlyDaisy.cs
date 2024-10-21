using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class DeadlyDaisy : BaseEnemy
{
    private float speed;
    private Vector2 airVelocity;
    private int airTime = 50;
    private float gravity = 0.5f;
    public bool movingRight { get; private set; }
    public float turnDelay { get; private set; } = -1.0f;
    private DaisyCollisionManager collisionManager;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("Spawn");
        speed = 300f;
        airVelocity = Vector2.Zero;
    }

    public override void Move(GameTime gameTime)
    {
        if(GameObject.Y < 0 && GameObject.X - player.X > 1500) {
            return;
        }

        sRend.isFacingRight = !movingRight;
        collisionManager = GameObject.GetComponent<DaisyCollisionManager>();

        if (collisionManager.isGrounded) {
            if(!collisionManager.isJumping || turnDelay < 1.5){
                collisionManager.isJumping = false;
                airVelocity = Vector2.Zero;
            }

            if (collisionManager.jumpRequested) {
                HandleJumpRequested();
            }
            else if (collisionManager.atPlatformEdge && turnDelay < 0){
                HandlePlatformEdge();
            }
            else {
                sRend.setAnimation("deadlyDaisyAnimation");

                if(collisionManager.currentPlatform.type != null && collisionManager.currentPlatform.type.Contains("Slope")) {
                    HandleSlopeCollision();
                }


                if (movingRight)
                    GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                else
                    GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
        else {
            if (collisionManager.isJumping) {
                sRend.setAnimation("Jump");
                sRend.currentAnimation.Value.CurrentFrame = 8;
            }
            else {
                movingRight = player.X > GameObject.X;
            }

            // Apply gravity if in the air
            airVelocity.Y += gravity;
        }
        turnDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        GameObject.Move((int)airVelocity.X, (int)airVelocity.Y);
    }

    private void HandleJumpRequested() {
        sRend.setAnimation("Jump");

        if (sRend.currentAnimation.Value.CurrentFrame == 7) {
            Rectangle landingSpot = collisionManager.landingSpot;
            float verticalSpeed = Math.Abs(((landingSpot.Y - GameObject.Y) - gravity * airTime * airTime / 2) / (airTime));
            float horizontalSpeed = (landingSpot.X - GameObject.X) / (airTime);

            airVelocity = new Vector2(horizontalSpeed, -verticalSpeed);

            collisionManager.jumpRequested = false;
            collisionManager.isJumping = true;
            turnDelay = 1.75f;
        }

    }

    private void HandlePlatformEdge() {
        sRend.setAnimation("Turn");

        //Makes the sprite look a little less offset during the animation.
        if (movingRight) {
            GameObject.X += 1;
        }
        else {
            GameObject.X -= 1;
        }

        if (sRend.currentAnimation.Value.CurrentFrame == 17) {
            movingRight = !movingRight;
            sRend.setAnimation("deadlyDaisyAnimation");
            turnDelay = 0.5f;
        }
    }

    private void HandleSlopeCollision() {
        BoxCollider daisyCollider = GameObject.GetComponent<BoxCollider>();
        BoxCollider slopeCollider = collisionManager.currentPlatform.GetComponent<BoxCollider>();
        Rectangle daisyBounds = daisyCollider.BoundingBox;

        // Get the rotated corners of the obstacle's bounding box (assuming the obstacle is sloped)
        Vector2[] slopeCorners = slopeCollider.GetRotatedCorners();

        // Get the top edge of the slope (assuming it's a left-to-right slope)
        Vector2 topLeft = slopeCorners[0]; // Top-left corner of the slope
        Vector2 topRight = slopeCorners[1]; // Top-right corner of the slope

        // Check if the player is within the horizontal bounds of the slope's top edge
        if (daisyBounds.Bottom > Math.Min(topLeft.Y, topRight.Y) && daisyBounds.Left >= topLeft.X && daisyBounds.Right <= topRight.X) {
            // Calculate the player's Y position relative to the slope
            float slopeHeightAtPlayerX = MathHelper.Lerp(topLeft.Y, topRight.Y, (daisyBounds.Center.X - topLeft.X) / (topRight.X - topLeft.X));

            if (daisyBounds.Bottom > slopeHeightAtPlayerX) {
                // Place the player on top of the slope
                GameObject.Y = (int)slopeHeightAtPlayerX - daisyBounds.Height + 10;
                collisionManager.isGrounded = true;
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


    public override void Shoot(GameTime gameTime)
    {
        // Implement shooting logic if needed
    }
}
