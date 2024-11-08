namespace Cuphead.Player
{
    internal class DeathAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public DeathAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            animator.setAnimation("Death");
        }
    }
}
