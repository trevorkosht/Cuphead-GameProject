namespace Cuphead.Player
{
    internal class DuckAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public DuckAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            string animation = player.shootTime > 0 ? "DuckShoot" : "Duck";
            animator.setAnimation(animation);
        }
    }
}
