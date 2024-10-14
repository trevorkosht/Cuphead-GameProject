using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class VisualEffectRenderer : IComponent {
    public GameObject GameObject {  get; set; }
    public Animation animation { get; set; }
    public Rectangle destRectangle { get; set; }
    public float effectScale { get; set; } = 1f;
    public bool enabled { get; set; } = true;

    private bool hasPlayed = false;
    public float orderInLayer { get; set; } = .5f;

    public VisualEffectRenderer(Rectangle destRectangle, Animation animation) { 
        this.destRectangle = destRectangle;
        this.animation = animation;
    }

    public void Update(GameTime gameTime) {
        if (!enabled) return;

        //destRectangle = new Rectangle(GameObject.X, GameObject.Y, destRectangle.Width, destRectangle.Height);
        animation.updateAnimation();

    }

    public void Draw(SpriteBatch spriteBatch) {
        if (!enabled) return;

        Rectangle scaledDestRectangle = new Rectangle(
            destRectangle.X,
            destRectangle.Y,
            (int)(destRectangle.Width * effectScale),
            (int)(destRectangle.Height * effectScale));

        animation.draw(spriteBatch, scaledDestRectangle, true, orderInLayer);

        if (animation.CurrentFrame == animation.FrameCount - 1) {
            hasPlayed = true;
        }
        if(hasPlayed && animation.CurrentFrame == 0){
            GameObject.Destroy();
        }
    }

}