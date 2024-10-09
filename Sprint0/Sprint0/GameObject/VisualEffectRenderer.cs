using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class VisualEffectRenderer : IComponent {
    public GameObject GameObject {  get; set; }
    public Animation animation { get; set; }
    public Rectangle destRectangle { get; set; }
    public float effectScale { get; set; } = 1f;
    public bool enabled { get; set; } = true;

    public VisualEffectRenderer(Rectangle destRectangle, Animation animation) { 
        this.destRectangle = destRectangle;
        this.animation = animation;
    }

    public void Update(GameTime gameTime) {
        if (!enabled) return;

        destRectangle = new Rectangle(GameObject.X, GameObject.Y, destRectangle.Width, destRectangle.Height);
        animation.updateAnimation();

        if(animation.CurrentFrame >= animation.FrameCount - 1) {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        if (!enabled) return;

        Rectangle scaledDestRectangle = new Rectangle(
            destRectangle.X,
            destRectangle.Y,
            (int)(destRectangle.Width * effectScale),
            (int)(destRectangle.Height * effectScale));

        animation.draw(spriteBatch, scaledDestRectangle, true);
    }

}