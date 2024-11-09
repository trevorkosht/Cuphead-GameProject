namespace Cuphead.Player
{
    internal class DashAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public DashAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            string animation = player.IsGrounded ? "DashGround" : "DashAir";
            animator.setAnimation(animation);
            animator.spriteScale = 1.6f;
        }
    }
}
