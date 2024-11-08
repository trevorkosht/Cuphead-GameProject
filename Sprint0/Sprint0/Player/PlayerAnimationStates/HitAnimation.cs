namespace Cuphead.Player
{
    internal class HitAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public HitAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            string animation = player.IsGrounded ? "HitGround" : "HitAir";
            animator.setAnimation(animation);
            player.IsParrying = false;
        }
    }
}
