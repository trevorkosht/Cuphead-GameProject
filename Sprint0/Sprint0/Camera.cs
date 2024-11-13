using Microsoft.Xna.Framework;

public class Camera
{
    public Vector2 Position { get; private set; }
    public Matrix Transform { get; private set; }

    public Camera()
    {
        Position = Vector2.Zero;
    }

    public void Update(Vector2 position)
    {
        Position = position;
        Transform = Matrix.CreateTranslation(new Vector3(-Position, 0));
    }
}
