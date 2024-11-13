using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DaisyCollisionManager : IComponent {
    public GameObject GameObject{ get; set;}
    public bool enabled {  get; set;}

    public BoxCollider daisyCollider { get; private set; }
    public DaisyState state;

    public DaisyCollisionManager(BoxCollider daisyCollider, DaisyState daisyState) {
        this.daisyCollider = daisyCollider;
        this.state = daisyState;
    }

    public void Update(GameTime gameTime) {
        state.isGrounded = false;
        state.atPlatformEdge = false;
        state.foundAdjacentPlatform = false;
        //state.isTurning = false;
        //state.jumpRequested = false;

        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.GetComponent<BoxCollider>() != null && GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Hill") || GO.type.Contains("Slope") || GO.type.Contains("Log")) ) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                if (CheckIfGrounded(GO) ) {
                    state.isGrounded = true;
                    state.currentPlatform = GO;
                    state.atPlatformEdge = CheckForPlatformEdge();
                }
                if (state.currentPlatform != null && !GO.Equals(state.currentPlatform)) {
                    state.jumpRequested = state.jumpRequested || (!state.isJumping && CheckForJump(platformCollider));
                    state.foundAdjacentPlatform = state.foundAdjacentPlatform || CheckForAdjacentPlatforms(platformCollider);
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
        return (platform.type.Contains("Slope") || Math.Abs(platformCollider.BoundingBox.Top - daisyCollider.BoundingBox.Bottom) < 10) && platformCollider.Intersects(daisyCollider);
    }

    private bool CheckForJump(BoxCollider platformCollider) {
        bool requestJump = false;

        Rectangle box = daisyCollider.BoundingBox;
        Rectangle jumpCheckBounds = new Rectangle(box.X, box.Y - 50, box.Width, box.Height + 400);

        if (GameObject.GetComponent<DeadlyDaisy>().movingRight) {
            jumpCheckBounds.X += 200;
        }
        else {
            jumpCheckBounds.X -= 200;
        }

        if (jumpCheckBounds.Left > platformCollider.BoundingBox.Left && jumpCheckBounds.Right < platformCollider.BoundingBox.Right && (Math.Abs(platformCollider.BoundingBox.Top - state.currentPlatform.GetComponent<BoxCollider>().BoundingBox.Top) >= state.minJumpHeight)) {
            state.landingSpot = new Rectangle(jumpCheckBounds.X, platformCollider.BoundingBox.Top - box.Height, box.Width, box.Height);
            requestJump = true;
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
}

