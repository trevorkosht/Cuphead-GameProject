namespace Cuphead.Player
{
    internal class ParryAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public ParryAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            int currentFrame = animator.currentAnimation.Value.CurrentFrame;
            if (currentFrame == 7 && animator.animationName == "Parry")
            {
                player.IsParrying = false;
            }
            else if (currentFrame >= 1 && currentFrame < 4)
            {
                player.velocity.Y = -600;
            }
            animator.setAnimation("Parry");
        }
    }
}
