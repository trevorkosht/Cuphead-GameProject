using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class Projectile : GameObject {
    public Rectangle Collider { get; set; }
    public Vector2 Velocity { get; set; }
    public int DamageAmount { get; set; }

    public Projectile(int xPosition, int yPosition, IComponent component, int colliderHeight, int colliderWidth, int damageAmount) : base(xPosition, yPosition, component) {
        X = xPosition;
        Y = yPosition;
        rotation = 0;
        Collider = new Rectangle(X, Y, colliderHeight, colliderWidth);
        DamageAmount = damageAmount;
    }

    new public void Update(GameTime gameTime) {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        X += (int)(Velocity.X * deltaTime);
        Y += (int)(Velocity.Y * deltaTime);

        Collider = new Rectangle(X, Y, Collider.Height, Collider.Width);
    }

}