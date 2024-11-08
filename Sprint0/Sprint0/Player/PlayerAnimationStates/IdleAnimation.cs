namespace Cuphead.Player
{
    internal class IdleAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public IdleAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            animator.setAnimation("Idle");
        }
    }
}
