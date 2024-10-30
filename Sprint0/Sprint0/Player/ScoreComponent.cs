using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ScoreComponent : IComponent
{
    public int Score { get; private set; }
    public int CardFlips { get; private set; }
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public void AddScore(int points)
    {
        Score += points;
        if (Score >= (CardFlips + 1) * 100)
        {
            CardFlips++;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
       // Handled Else Where
    }

    public float GetCardFillPercent()
    {
        return (Score % 100) / 100f;
    }

    public void Update(GameTime gameTime)
    {
        // Handled Else Where
    }
}
