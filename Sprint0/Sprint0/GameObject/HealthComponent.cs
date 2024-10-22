using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthComponent : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public bool isDead { get; set; }
    public bool isPlayer { get; set; }
    public bool isDeadFull { get; set; }
    public float timeTillDeath = 1f;

    public HealthComponent(int maxHP, bool isDead = false, bool isPlayer = false)
    {
        maxHealth = maxHP;
        currentHealth = maxHealth;
        this.isDead = isDead;
        this.isPlayer = isPlayer;
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
        if (isDead && !isPlayer)
        {
            GameObject.Destroy();
        }
        else if (isDead && isPlayer)
        {
            timeTillDeath -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeTillDeath <= 0)
            {
                isDeadFull = true;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Non-visual component
    }
}
