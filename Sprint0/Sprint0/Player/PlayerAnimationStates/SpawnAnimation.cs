namespace Cuphead.Player
{
    internal class SpawnAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public SpawnAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            animator.setAnimation("Spawn");
            if (animator.IsAnimationComplete())
            {
                player.IsSpawning = false;
                animator.setAnimation("Idle");
            }
        }
    }
}
