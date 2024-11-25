using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DaisyCollisionManager : IComponent {
    public GameObject GameObject{ get; set;}
    public bool enabled {  get; set;}

    public BoxCollider daisyCollider { get; set; }
    public DaisyState state;

    public DaisyCollisionManager(BoxCollider daisyCollider, DaisyState daisyState) {
        this.daisyCollider = daisyCollider;
        this.state = daisyState;
    }

    public void Update(GameTime gameTime) {
        state.isGrounded = false;
        state.atPlatformEdge = false;
        state.foundAdjacentPlatform = false;

        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.GetComponent<BoxCollider>() != null && GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Hill") || GO.type.Contains("Slope") || GO.type.Contains("Log")) ) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                if (CheckIfGrounded(GO) ) {
                    if(GO != state.currentPlatform) {
                        System.Diagnostics.Debug.WriteLine("Daisy grounded on new platform");
                    }

                    state.isGrounded = true;
                    state.currentPlatform = GO;
                    state.atPlatformEdge = CheckForPlatformEdge();
                    if (state.currentPlatform.type.Contains("Slope")) {
                        HandleSlopeCollision();
                    }
                }
                if (state.currentPlatform != null && !GO.Equals(state.currentPlatform)) {
                    state.foundAdjacentPlatform = state.foundAdjacentPlatform || CheckForAdjacentPlatforms(platformCollider);
                    state.jumpRequested = state.jumpRequested || (CheckForJump(platformCollider) && !state.isJumping);
                }
            }
        }
        state.atPlatformEdge = state.atPlatformEdge && !state.foundAdjacentPlatform;
    }
    public void Draw(SpriteBatch spriteBatch) {
        /* Non-visual component */
    }

    private bool CheckIfGrounded(GameObject platform) {
        BoxCollider platformCollider = platform.GetComponent<BoxCollider>();
        return (platform.type.Contains("Slope") || Math.Abs(platformCollider.BoundingBox.Top - daisyCollider.BoundingBox.Bottom) < 15) && platformCollider.Intersects(daisyCollider);
    }

    private bool CheckForJump(BoxCollider platformCollider) {
        bool requestJump = false;

        Rectangle box = daisyCollider.BoundingBox;
        Rectangle jumpCheckBounds = new Rectangle(box.Left, box.Top - 50, box.Width, box.Height + 400);

        int checkOffset = 0;
        if (GameObject.GetComponent<DeadlyDaisy>().movingRight) {
            checkOffset += 250;
        }
        else {
            checkOffset -= 250;
        }
        jumpCheckBounds.X += checkOffset;

        int jumpHeight = Math.Abs(platformCollider.BoundingBox.Top - state.currentPlatform.GetComponent<BoxCollider>().BoundingBox.Top);

        if ((jumpCheckBounds.Intersects(platformCollider.BoundingBox) && jumpCheckBounds.Left > platformCollider.BoundingBox.Left && jumpCheckBounds.Right < platformCollider.BoundingBox.Right && jumpHeight >= state.minJumpHeight)){
            state.landingSpot = new Rectangle(jumpCheckBounds.X + (checkOffset/5), platformCollider.BoundingBox.Top - box.Height, box.Width, box.Height);


            requestJump = true;

            System.Diagnostics.Debug.WriteLine("Properly registering jump request");
        }

        return requestJump;
    }

    private bool CheckForPlatformEdge() {
        float leftEdge = daisyCollider.BoundingBox.Left;
        float rightEdge = daisyCollider.BoundingBox.Right;
        float topEdge = daisyCollider.BoundingBox.Top;
        float bottomEdge = daisyCollider.BoundingBox.Bottom;

        Rectangle boundingBox = state.currentPlatform.GetComponent<BoxCollider>().BoundingBox;

        return (!GameObject.GetComponent<DeadlyDaisy>().movingRight && leftEdge - state.edgeCheckDistance < boundingBox.Left) || (GameObject.GetComponent<DeadlyDaisy>().movingRight && rightEdge + state.edgeCheckDistance > boundingBox.Right);
    }

    private bool CheckForAdjacentPlatforms(BoxCollider platformCollider) {
        Rectangle adjacentPlatformChecker = new Rectangle();
        adjacentPlatformChecker.X = daisyCollider.BoundingBox.X - 2 * state.edgeCheckDistance;
        adjacentPlatformChecker.Y = state.currentPlatform.GetComponent<BoxCollider>().BoundingBox.Y;
        adjacentPlatformChecker.Width = 4 * state.edgeCheckDistance + daisyCollider.BoundingBox.Width;
        adjacentPlatformChecker.Height = 5;

        return adjacentPlatformChecker.Intersects(platformCollider.BoundingBox) && Math.Abs(platformCollider.BoundingBox.Y - adjacentPlatformChecker.Y) < 10;
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
}

