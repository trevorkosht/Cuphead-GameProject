using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class HomingProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 position;
	private float speed;

	public HomingProjectile(Vector2 pos)
	{
		speed = 300f;
		position = pos;
	}

	public void Update(GameTime gameTime)
	{

		Vector2 direction = GOManager.Instance.Player.position - position;
		direction.Normalize();

		position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
		GameObject.X = (int)position.X;
		GameObject.Y = (int)position.Y;

		if (Vector2.Distance(position, GOManager.Instance.Player.position) < 10f)
		{
			GameObject.Destroy();
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
	}
}
