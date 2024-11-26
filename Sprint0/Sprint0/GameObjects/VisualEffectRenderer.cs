using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

public class VisualEffectRenderer : IComponent {
    public GameObject GameObject {  get; set; }
    public Animation animation { get; set; }
    public Rectangle destRectangle { get; set; }
    public float effectScale { get; set; } = 1f;
    public bool enabled { get; set; } = true;

    private bool hasPlayed = false;
    public float orderInLayer { get; set; } = 0f;
    public bool isFacingRight {  get; set; }

    public VisualEffectRenderer(Rectangle destRectangle, Animation animation, bool isFacingRight) { 
        this.destRectangle = destRectangle;
        this.animation = animation;
        this.isFacingRight = isFacingRight;
    }

    public void Update(GameTime gameTime) {
        if (!enabled) return;

        animation.updateAnimation();

    }

    //same thing but without game time
    public void Update()
    {
        if (!enabled) return;

        animation.updateAnimation();

    }

    public void Draw(SpriteBatch spriteBatch) {
        if (!enabled) return;

        Rectangle scaledDestRectangle = new Rectangle(
            destRectangle.X,
            destRectangle.Y,
            (int)(destRectangle.Width * effectScale),
            (int)(destRectangle.Height * effectScale));


        if (animation.CurrentFrame == animation.FrameCount - 1) {
            hasPlayed = true;
        }
        if (hasPlayed && animation.CurrentFrame == 0) {
            GameObject.Destroy();
        }
        else {
            animation.draw(spriteBatch, scaledDestRectangle, isFacingRight, orderInLayer, 0, 1f);
        }

    }

}