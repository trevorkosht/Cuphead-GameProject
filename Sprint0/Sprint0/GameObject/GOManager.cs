public class GOManager
{
    private static GOManager instance;
    public GameObject Player { get; set; }
    public Texture2DStorage textureStorage { get; set; }

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