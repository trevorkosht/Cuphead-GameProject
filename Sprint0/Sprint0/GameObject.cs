using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

public class GameObject
{
    // List of components attached to this GameObjects
    private List<IComponent> components = new List<IComponent>();

    public GameObject(IComponent component)
    {
        AddComponent(component);
    }
    public GameObject(List<IComponent> components)
    {
        foreach(IComponent component in components)
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
        foreach (var component in components)
        {
            component.Update(gameTime);
        }
    }

    // Draw each component (for visual components)
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}