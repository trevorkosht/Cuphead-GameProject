using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MenuManager : IComponent
{
    
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }
    private Dictionary<string, Menu> menus;
    private Menu currentMenu;
    private string currentMenuName;

    public MenuManager()
    {
        menus = new Dictionary<string, Menu>();
    }

    public void AddMenu(string menuName, Menu menu)
    {
        menus[menuName] = menu;
    }

    public void SetMenu(string menuName)
    {
        currentMenu = menus[menuName];
        currentMenuName = menuName;
    }

    public Menu getCurrentMenu() {
        return currentMenu;
    }

    public string getCurrentMenuName() {
        return currentMenuName;
    }

    public void Update(GameTime gameTime)
    {
        currentMenu?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        currentMenu?.Draw(spriteBatch, font);
    }

    public void LoadContent(Texture2DStorage textures) {
        currentMenu.LoadContent(textures);
    }

    public void Draw(SpriteBatch spriteBatch) {

    }
}
