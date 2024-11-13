using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DaisyAnimator : IComponent {
    public bool enabled { get; set; }
    public GameObject GameObject { get; set; }

    public SpriteRenderer animator { get; set; }
    public DaisyState state { get; set; }

    public DaisyAnimator(SpriteRenderer animator, DaisyState daisyState) {
        this.animator = animator;
        state = daisyState;
        animator.setAnimation("Spawn");
    }

    public void Update(GameTime gameTime) {
        if (state.isSpawning) {
            animator.setAnimation("Spawn");
            if (state.isGrounded) {
                GOManager.Instance.audioManager.getInstance("DeadlyDaisyLanding").Play();
                state.isSpawning = false;
            }
            else if (GameObject.Y > 0 && GameObject.Y < 100) {
                GOManager.Instance.audioManager.getInstance("DeadlyDaisyFloat").Play();
            }
        }
        else if (state.isTurning && state.isGrounded) {
            animator.setAnimation("Turn");
        }
        else if (state.jumpRequested) {
            animator.setAnimation("Jump");
        }
        else if (state.isJumping) {
            animator.setAnimation("Jump");
            animator.currentAnimation.Value.CurrentFrame = 8;
        }
        else if (state.isWalking && state.isGrounded) {
            animator.setAnimation("deadlyDaisyAnimation");
        }
    }

    public void Draw(SpriteBatch spriteBatch) {

    }
}