using Cuphead;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class GOManager
{
    private static GOManager instance;
    public GameObject Player { get; set; }
    public Texture2DStorage textureStorage { get; set; }
    public MenuManager menuManager { get; set; } 
    public AudioManager audioManager { get; set; }
    public List<GameObject> allGOs { get; set; }

    public GameObject currentEnemy { get; set; }
    public bool IsDebugging = false;
    public GraphicsDevice GraphicsDevice { get; set; }

    public Camera Camera { get; set; }

    private GOManager() { }

    public static GOManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GOManager();
            }
            return instance;
        }
    }
}