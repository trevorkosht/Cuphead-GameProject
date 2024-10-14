using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthComponent : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public bool isDead { get; set; }

    public HealthComponent(int maxHP, bool isDead = false)
    {
        maxHealth = maxHP;
        currentHealth = maxHealth;
        this.isDead = isDead;
    }

    public void AddHealth(int healthAmount)
    {
        if (currentHealth + healthAmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void RemoveHealth(int healthAmount)
    {
        if (currentHealth > healthAmount)
        {
            currentHealth -= healthAmount;
        }
        else
        {
            currentHealth = 0;
            isDead = true; // Mark as dead if health reaches zero
        }
    }

    public void Update(GameTime gameTime)
    {
        // Destroy the GameObject if it is dead
        if (isDead)
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Non-visual component
    }
}
