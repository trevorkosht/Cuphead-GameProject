using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System;                           // For Math

public class HomingProjectile
{
	private Vector2 position;
	private Vector2 targetPosition;
	private float speed;
	public bool IsActive { get; private set; }
	private Texture2D projectileTexture; // Store the texture for the projectile

	public HomingProjectile(Vector2 startPosition, Vector2 targetPosition, Texture2D texture)
	{
		position = startPosition;
		this.targetPosition = targetPosition;
		projectileTexture = texture;  // Inject the texture when creating the projectile
		speed = 150f; // Speed of the projectile
		IsActive = true;
	}

	public void Update(GameTime gameTime)
	{
		if (IsActive)
		{
			// Calculate direction to the target
			Vector2 direction = targetPosition - position;
			direction.Normalize();

			// Move towards the target
			position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Check if the projectile has hit the player or gone off-screen (simplified logic)
			if (Vector2.Distance(position, targetPosition) < 10f) // Consider a hit if close enough
			{
				IsActive = false; // Deactivate after hitting
			}
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		if (IsActive)
		{
			// Draw the projectile sprite
			spriteBatch.Draw(projectileTexture, position, Color.White);
		}
	}
}
