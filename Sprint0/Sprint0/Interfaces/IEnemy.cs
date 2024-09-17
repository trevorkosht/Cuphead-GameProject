using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D

public interface IEnemy
{
    void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture);
    void Move(GameTime gameTime);
    void Shoot();
    void TakeDamage(int damage);
}
