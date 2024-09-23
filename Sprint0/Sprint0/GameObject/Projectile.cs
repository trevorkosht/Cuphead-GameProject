using Microsoft.Xna.Framework;

public class Projectile {
    public Rectangle Collider {get;, set;}
    public Vector2 Velocity { get; set;}
    public int DamageAmount { get; set;}
    public Projectile(Rectangle collider, Vector2 velocity, int damageAmount) {
        Collider = collider;
        Velocity = velocity;
        DamageAmount = damageAmount;
    }

    public void Update(GameTime gameTime) {
        Collider = new Rectangle(Collider.X + (int)Velocity.X, Collider.Y + (int)Velocity.Y, Collider.Height, Collider.Width);
    }

}