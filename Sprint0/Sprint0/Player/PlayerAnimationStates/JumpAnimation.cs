namespace Cuphead.Player
{
    internal class JumpAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public JumpAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            animator.setAnimation("Jump");
        }
    }
}
