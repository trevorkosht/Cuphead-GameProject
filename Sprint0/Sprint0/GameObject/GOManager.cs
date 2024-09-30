using System.Collections.Generic;

public class GOManager
{
    private static GOManager instance;
    public GameObject Player { get; set; }
    public Texture2DStorage textureStorage { get; set; }
    public List<GameObject> allGOs { get; set; }

    public GameObject currentEnemy { get; set; }

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