using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthController : IComponent {
    public GameObject GameObject {  get; set; }
    public bool enabled { get; set; }
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public bool isDead { get; set; }

    public HealthController(GameObject gameObject, int maxHealth, int currentHealth, bool isDead) {
        GameObject = gameObject;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.isDead = isDead;
    }

    public void AddHealth(int healthAmount) {
        if (currentHealth + healthAmount > maxHealth) {
            currentHealth = maxHealth;
        }
        else {
            currentHealth += healthAmount;
        }
    }

    public void RemoveHealth(int healthAmount) {
        if (currentHealth > healthAmount) {
            currentHealth -= healthAmount;
        }
        else {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void Update(GameTime gameTime) { /* No logic here*/ }
    public void Draw(SpriteBatch spriteBatch) { /* Non-visual component */ }
}

