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
