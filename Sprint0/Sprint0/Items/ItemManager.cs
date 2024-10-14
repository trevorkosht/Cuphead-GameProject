using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ItemManager : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;
    public string itemType { get; set; }

    public ItemManager(string itemType) {
        this.itemType = itemType;
    }

    public void Update(GameTime gameTime) {
        /* Collision logic handled by player */
    }

    public void Draw(SpriteBatch spriteBatch) {
        /* Non-visual component */
    }
}