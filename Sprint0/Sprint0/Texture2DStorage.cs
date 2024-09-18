using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Texture2DStorage
{
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

    // Initialize the texture storage with the ContentManager
    public void LoadContent(ContentManager content)
    {
        // Example: Load your enemy textures
        _textures["DeadlyDaisy"] = content.Load<Texture2D>("DeadlyDaisySprite");
        _textures["MurderousMushroom"] = content.Load<Texture2D>("MurderousMushroomSprite");
        _textures["TerribleTulip"] = content.Load<Texture2D>("TerribleTulipSprite");
		_textures["ToothyTerror"] = content.Load<Texture2D>("ToothyTerrorSprite");
		_textures["BothersomeBlueberry"] = content.Load<Texture2D>("BothersomeBlueberrySprite");
        _textures["AggravatingAcorn"] = content.Load<Texture2D>("AggravatingAcornSprite");
        _textures["AcornMaker"] = content.Load<Texture2D>("AcornMakerSprite");
        _textures["Seed"] = content.Load<Texture2D>("lobber_seed_0001");
        _textures["PurpleSpore"] = content.Load<Texture2D>("mushroom_poison_cloud_0001");
        _textures["PinkSpore"] = content.Load<Texture2D>("mushroom_poison_cloud_pink_0003");
        // Add more textures as needed
    }

    // Method to retrieve a texture
    public Texture2D GetTexture(string textureName)
    {
        if (_textures.ContainsKey(textureName))
            return _textures[textureName];

        return null; // Handle missing textures if necessary
    }
}
