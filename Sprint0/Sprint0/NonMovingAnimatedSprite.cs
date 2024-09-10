using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class NonMovingAnimatedSprite : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private Rectangle[] sourceRectangles;


    private float timer;
    private int timeInBetween = 250; 
    private int previousAnimationIndex;
    private int currentAnimationIndex;

    public NonMovingAnimatedSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;

        sourceRectangles = new Rectangle[3];
        sourceRectangles[0] = new Rectangle(10, 0, 18, 25);
        sourceRectangles[1] = new Rectangle(30, 0, 18, 25);
        sourceRectangles[2] = new Rectangle(50, 0, 18, 25);

        previousAnimationIndex = 2;
        currentAnimationIndex = 1;
    }

    public void Update(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (timer > timeInBetween)
        {
            if (currentAnimationIndex == 1)
            {
                currentAnimationIndex = previousAnimationIndex == 0 ? 2 : 0;
                previousAnimationIndex = currentAnimationIndex;
            }
            else
            {
                currentAnimationIndex = 1;
            }

            timer = 0f;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 scale = new Vector2(5f, 5f);
        spriteBatch.Draw(
            texture: texture,
            position: position,
            sourceRectangle: sourceRectangles[currentAnimationIndex],
            color: Color.White,
            rotation: 0f,
            origin: Vector2.Zero,
            scale: scale,
            effects: SpriteEffects.None,
            layerDepth: 0f
        );
    }
}
