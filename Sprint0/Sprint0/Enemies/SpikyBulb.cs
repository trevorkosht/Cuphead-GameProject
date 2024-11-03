using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpikyBulb : BaseEnemy {
    private float springStrength = 0.005f;
    private int springOriginY;
    public float yVelocity { get; private set; } = 0f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage) {
        base.Initialize(texture, storage);
        sRend.setAnimation("SpikyBulb");
        springOriginY = GameObject.Y - 150;
    }

    public override void Shoot(GameTime gameTime) {

    }
    public override void Move(GameTime gameTime) {
        yVelocity += -springStrength * (GameObject.Y - springOriginY);
        GameObject.Y += (int)yVelocity;
    }

}