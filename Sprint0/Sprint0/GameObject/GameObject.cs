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
    public string type { get; set; }

    private List<IComponent> components = new List<IComponent>();

    public GameObject(int xPosition, int yPosition)
    {
        this.X = xPosition;
        this.Y = yPosition;
        this.type = "";
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

    public void AddComponent(IComponent component)
    {
        components.Add(component);
        component.GameObject = this;
        component.enabled = true;
    }

    public T GetComponent<T>() where T : IComponent
    {
        return components.OfType<T>().FirstOrDefault();
    }


    public void Update(GameTime gameTime)
    {
        position = new Vector2(X, Y);
        for (int i = 0; i < components.Count; i++)
        {
            IComponent component = components[i];
            component.Update(gameTime);
        }
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < components.Count; i++)
        {
            IComponent component = components[i];
            component.Draw(spriteBatch);
        }
    }


    public void Move(int deltaX, int deltaY) {
        X += deltaX;
        Y += deltaY;
    }


    public void MoveToPosition(int newX, int newY) {
        X = newX;
        Y = newY;
    }


    public void Destroy()
    {

        destroyed = true;
        foreach (var component in components)
        {
            component.enabled = false;
            component.GameObject = null;
        }

        components.Clear();
    }
}