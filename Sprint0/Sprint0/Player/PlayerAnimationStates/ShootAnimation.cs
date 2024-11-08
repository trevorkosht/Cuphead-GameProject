namespace Cuphead.Player
{
    internal class ShootAnimation : IAnimationState
    {
        private readonly PlayerState player;

        public ShootAnimation(PlayerState player)
        {
            this.player = player;
        }

        public void Play(SpriteRenderer animator)
        {
            string animation = player.ShootUp ? "ShootUp" : "ShootStraight";
            animator.setAnimation(animation);
        }
    }
}
