using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D


public class AggravatingAcorn : BaseEnemy


{
	private Vector2 dropPosition;
	private bool isFalling;
	private float speed;

	public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
	{
		base.Initialize(startPosition, hitPoints, texture);
		speed = 200f;  // Speed of horizontal movement
		isFalling = false;
		dropPosition = Vector2.Zero; // Will set when ready to fall
	}

	public override void Move(GameTime gameTime)
	{
		if (!isFalling)
		{
			// Move horizontally across the screen at the top
			position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Check if the player is underneath to trigger the fall
			if (PlayerIsUnderneath())
			{
				isFalling = true;
				dropPosition = new Vector2(position.X, position.Y + 500); // Drop to a target Y position
			}
		}
		else
		{
			// Fall straight down
			position.Y += speed * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Stop falling when reaching the ground (or a certain Y position)
			if (position.Y >= dropPosition.Y)
			{
				IsActive = false;  // Deactivate after the fall
			}
		}

		// Handle screen wrapping when moving horizontally
		if (position.X > ScreenWidth)
		{
			position.X = -Width; // Re-enter from the left side
		}
	}

	public override void Shoot()
	{
		// Aggravating Acorn doesn't shoot, so no implementation needed
	}

	private bool PlayerIsUnderneath()
	{
		// Logic to detect if the player is underneath the acorn
		// This could use player bounding box or proximity checks
		var playerPosition = Player.Instance.Position; // Assuming Player.Instance gives player's position
		return Math.Abs(playerPosition.X - position.X) < 50;  // If player is within 50 pixels of acorn's X
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		if (HitPoints <= 0)
		{
			// Handle acorn being destroyed if it gets shot
			IsActive = false;
		}
	}

	public override void Draw(SpriteBatch spriteBatch)
	{
		if (IsActive)
		{
			// Draw acorn sprite here
		}
	}
}
