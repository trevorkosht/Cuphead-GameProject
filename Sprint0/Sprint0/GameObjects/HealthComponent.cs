using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

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
    bool delayDeath;

    public HealthComponent(int maxHP, bool isDead = false, bool isPlayer = false, bool delayDeath = false)
    {
        maxHealth = maxHP;
        currentHealth = maxHealth;
        this.isDead = isDead;
        this.isPlayer = isPlayer;
        this.delayDeath = delayDeath;
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
            isDead = true;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (isDead && !(isPlayer || delayDeath))
        {
            GameObject.Destroy();
        }
        else if (isDead && (isPlayer || delayDeath))
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
    }
}
