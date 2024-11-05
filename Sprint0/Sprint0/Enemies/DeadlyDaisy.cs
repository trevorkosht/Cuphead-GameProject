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
        if(GameObject.Y < 0 && GameObject.X - player.X > 750) {
            return;
        }

        sRend.isFacingRight = !movingRight;
        collisionManager = GameObject.GetComponent<DaisyCollisionManager>();

        if (sRend.getAnimationName() == "Spawn") 
        { 
            if(collisionManager.isGrounded)
            {
                GOManager.Instance.audioManager.getInstance("DeadlyDaisyLanding").Play();
            } else
            {
                if (GameObject.Y > 0 && GameObject.Y < 100)
                GOManager.Instance.audioManager.getInstance("DeadlyDaisyFloat").Play();
            }
        }

        if (collisionManager.isGrounded) {
            if(!collisionManager.isJumping || turnDelay < 1.5){
                collisionManager.isJumping = false;
                airVelocity = Vector2.Zero;
            }

            if (collisionManager.atPlatformEdge || GameObject.X < 1250){
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
                airVelocity.Y += gravity;
            }
            else {
                movingRight = player.X > GameObject.X;
                airVelocity.Y += gravity/15;
            }

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
        if(collisionManager.foundAdjacentPlatform == true) {
            return;
        }

        //check for jump
        if (!collisionManager.jumpRequested || (movingRight != player.X > GameObject.X && Math.Abs(player.X - GameObject.X) > 500) || (GameObject.X < 1250 && !movingRight)){
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
                turnDelay = 3.0f;
            }

        }
        else if(collisionManager.jumpRequested){
            HandleJumpRequested();
        }
        //check if turn needed





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


    public override void Shoot(GameTime gameTime)
    {
        // Implement shooting logic if needed
    }
}
