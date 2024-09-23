using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class Projectile : GameObject {

    public int X { get; set; }
    public int Y { get; set; }
    public float rotation { get; set; }
    public Vector2 scale { get; set; }

    // List of components attached to this GameObjects
    private List<IComponent> components = new List<IComponent>();

    public Projectile(int xPosition, int yPosition, IComponent component) : base(xPosition, yPosition, component) {
        X = xPosition;
        Y = yPosition;
        rotation = 0;
    }

    public Rectangle Collider { get; set; }
    public Vector2 Velocity { get; set;}
    public int DamageAmount { get; set;}
    //public Projectile(int xPosition, int yPosition, IComponent component) {
    //    this.X = xPosition;
    //    this.Y = yPosition;
    //    AddComponent(component);
    //}

    public void Update(GameTime gameTime) {
        Collider = new Rectangle(Collider.X + (int)Velocity.X, Collider.Y + (int)Velocity.Y, Collider.Height, Collider.Width);
    }

}