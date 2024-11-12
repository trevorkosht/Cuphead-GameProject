using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DaisyCollisionManager : IComponent {
    public GameObject GameObject{ get; set;}
    public bool enabled {  get; set;}

    public bool isGrounded { get; set; } = false;
    public bool jumpRequested { get; set; } = false;
    public bool atPlatformEdge { get; private set; } = false;
    public bool isJumping { get; set; } = false;
    public bool foundAdjacentPlatform { get; private set; } = false;
    public GameObject currentPlatform { get; private set; }
    public BoxCollider daisyCollider { get; private set; }
    public Rectangle landingSpot { get; private set; }
    private const int edgeCheckDistance = 50;
    private const int minJumpHeight = 25;

    public DaisyCollisionManager(BoxCollider daisyCollider) {
        this.daisyCollider = daisyCollider;
    }

    public void Update(GameTime gameTime) {
        isGrounded = false;
        atPlatformEdge = false;
        foundAdjacentPlatform = false;

        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.GetComponent<BoxCollider>() != null && GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Hill") || GO.type.Contains("Slope") || GO.type.Contains("Log")) ) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                if (CheckIfGrounded(GO) ) {
                    isGrounded = true;
                    currentPlatform = GO;
                    atPlatformEdge = CheckForPlatformEdge();
                }
                if (currentPlatform != null && !GO.Equals(currentPlatform)) {
                    jumpRequested = jumpRequested || (!isJumping && CheckForJump(platformCollider));
                    foundAdjacentPlatform = foundAdjacentPlatform || CheckForAdjacentPlatforms(platformCollider);
                }
            }
        }
        atPlatformEdge = atPlatformEdge && !foundAdjacentPlatform;
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
            jumpCheckBounds.X += 300;
        }
        else {
            jumpCheckBounds.X -= 300;
        }

        if (jumpCheckBounds.Left > platformCollider.BoundingBox.Left && jumpCheckBounds.Right < platformCollider.BoundingBox.Right && (Math.Abs(platformCollider.BoundingBox.Top - currentPlatform.GetComponent<BoxCollider>().BoundingBox.Top) >= minJumpHeight)) {
            landingSpot = new Rectangle(jumpCheckBounds.X, platformCollider.BoundingBox.Top - box.Height, box.Width, box.Height);
            requestJump = true;
        }
        
        return requestJump;
    }

    private bool CheckForPlatformEdge() {
        float leftEdge = daisyCollider.BoundingBox.Left;
        float rightEdge = daisyCollider.BoundingBox.Right;
        float topEdge = daisyCollider.BoundingBox.Top;
        float bottomEdge = daisyCollider.BoundingBox.Bottom;

        Rectangle boundingBox = currentPlatform.GetComponent<BoxCollider>().BoundingBox;

        return (!GameObject.GetComponent<DeadlyDaisy>().movingRight && leftEdge - edgeCheckDistance < boundingBox.Left) || (GameObject.GetComponent<DeadlyDaisy>().movingRight && rightEdge + edgeCheckDistance > boundingBox.Right);
    }

    private bool CheckForAdjacentPlatforms(BoxCollider platformCollider) {
        Rectangle adjacentPlatformChecker = new Rectangle();
        adjacentPlatformChecker.X = daisyCollider.BoundingBox.X - 2 * edgeCheckDistance;
        adjacentPlatformChecker.Y = currentPlatform.GetComponent<BoxCollider>().BoundingBox.Y;
        adjacentPlatformChecker.Width = 4 * edgeCheckDistance + daisyCollider.BoundingBox.Width;
        adjacentPlatformChecker.Height = 5;

        return adjacentPlatformChecker.Intersects(platformCollider.BoundingBox) && Math.Abs(platformCollider.BoundingBox.Y - adjacentPlatformChecker.Y) < 10;
    }
}

