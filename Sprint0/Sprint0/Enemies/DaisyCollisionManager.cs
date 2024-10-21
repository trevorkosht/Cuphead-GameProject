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
        bool onPlatform = false; ;


        foreach(GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.GetComponent<BoxCollider>() != null && GO.type != null && (GO.type.Contains("Platform") || GO.type.Contains("Hill") || GO.type.Contains("Slope")) ) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                if (CheckIfGrounded(platformCollider) ) {
                    isGrounded = true;
                    currentPlatform = GO;
                }
                if (!isJumping && CheckForJump(platformCollider) && !GO.Equals(currentPlatform)) {
                    jumpRequested = true;
                }
                if (isGrounded && CheckForPlatformEdge(platformCollider)) {
                    onPlatform = true;
                }
            }
        }

        if (!onPlatform) {
            atPlatformEdge = true;
        }

    }
    public void Draw(SpriteBatch spriteBatch) {
        /* Non-visual component */
    }

    private bool CheckIfGrounded(BoxCollider platformCollider) {
        if (platformCollider.Intersects(daisyCollider)) {
            if((Math.Abs(platformCollider.BoundingBox.Top - daisyCollider.BoundingBox.Bottom) < 30)) {
                return true;
            }
        }
        return false;
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

        if (currentPlatform != null && !currentPlatform.GetComponent<BoxCollider>().Equals(platformCollider)) {
            if (jumpCheckBounds.Left > platformCollider.BoundingBox.Left && jumpCheckBounds.Right < platformCollider.BoundingBox.Right) {
                if (Math.Abs(platformCollider.BoundingBox.Top - currentPlatform.GetComponent<BoxCollider>().BoundingBox.Top) >= minJumpHeight) {
                    landingSpot = new Rectangle(jumpCheckBounds.X, platformCollider.BoundingBox.Top - box.Height, box.Width, box.Height);
                    requestJump = true;
                }
            }
        }
        //return requestJump;
        return false;
    }

    private bool CheckForPlatformEdge() {
        float leftEdge = daisyCollider.BoundingBox.Left;
        float rightEdge = daisyCollider.BoundingBox.Right;
        float topEdge = daisyCollider.BoundingBox.Top;
        float bottomEdge = daisyCollider.BoundingBox.Bottom;

        Rectangle boundingBox = currentPlatform.GetComponent<BoxCollider>().BoundingBox;

        if(leftEdge - edgeCheckDistance < boundingBox.Left || rightEdge + edgeCheckDistance > boundingBox.Right) {
            return true;
        }
        return false;
    }
}

