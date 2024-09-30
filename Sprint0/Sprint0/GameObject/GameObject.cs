using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

public class GameObject
{
    public int X;
    public int Y;
    public float rotation {  get; set; }
    public Vector2 scale { get; set; }
    public Vector2 position { get; private set; }
    public bool destroyed { get; set; } = false;

    // List of components attached to this GameObjects
    private List<IComponent> components = new List<IComponent>();

    public GameObject(int xPosition, int yPosition)
    {
        this.X = xPosition;
        this.Y = yPosition;
    }

    public GameObject(int xPosition, int yPosition, IComponent component)
    {
        X = xPosition;
        Y = yPosition;
        AddComponent(component);
    }
    public GameObject(int xPosition, int yPosition, List<IComponent> components)
    {
        X = xPosition;
        Y = yPosition;

        foreach (IComponent component in components)
        {
            AddComponent(component);
        }
    }

    // Add a component to this GameObject
    public void AddComponent(IComponent component)
    {
        components.Add(component);
        component.GameObject = this;
        component.enabled = true;
    }

    // Get a component of a specific type
    public T GetComponent<T>() where T : IComponent
    {
        return components.OfType<T>().FirstOrDefault();
    }

    // Update each component
    public void Update(GameTime gameTime)
    {
        position = new Vector2(X, Y);
        for (int i = 0; i < components.Count; i++)
        {
            IComponent component = components[i];
            component.Update(gameTime);
        }
    }

    // Draw each component (for visual components)
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < components.Count; i++)
        {
            IComponent component = components[i];
            component.Draw(spriteBatch);
        }
    }

    //Move an amount relative to current position
    public void Move(int deltaX, int deltaY) {
        X += deltaX;
        Y += deltaY;
    }

    //Moves the GameObject to the position specified by the vector
    public void MoveToPosition(int newX, int newY) {
        X = newX;
        Y = newY;
    }

    // Destroy the GameObject and remove all its components
    public void Destroy()
    {
        // Remove all components
        destroyed = true;
        foreach (var component in components)
        {
            component.enabled = false;
            component.GameObject = null;
        }

        components.Clear(); // Clear the list of components to remove references
    }
}