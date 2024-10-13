using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class BlockFactory
{
    // Dictionary to store block sizes based on subtype
    private static Dictionary<string, (int width, int height)> blockSizes = new Dictionary<string, (int width, int height)>()
    {
        { "LevelBackground", (1827, 546) },
        { "Bush1", (1338, 625) },
        { "Platform1", (427, 102) },
        { "Platform2", (208, 110) },
        { "BigHill1", (1042, 842) },
        { "BigHill2", (998, 664) },
        { "BigHill3", (510, 668) },
        { "Hill1", (531, 180) },
        { "Hill2", (436, 176) },
        { "Hill3", (616, 288) },
        { "Log1", (832, 186) },
        { "Log2", (164, 996) },
        { "Stump1", (144, 144) },
        { "Stump2", (144, 144) },
        { "Stump3", (144, 144) },
        { "Rock1", (526, 306) },
        { "Rock2", (980, 760) },
        { "Rock3", (642, 270) },
        { "Rock4", (970, 682) },
        { "Rock5", (650, 472) },
        { "Rock6", (668, 522) },
        { "Leaves1", (902, 710) },
        { "Leaves2", (1564, 458) },
        { "Leaves3", (1052, 350) },
        { "Tree1", (273, 476) },
        { "Tree2", (476, 682) },
        { "Tree3", (259, 456) },
        { "Tree4", (500, 655) },
        { "Tree5", (922, 720) },
        { "Tree6", (852, 1064) },
        { "Tree7", (304, 844) }
    };

    public static GameObject CreateBlock(string subtype, Vector2 position)
    {
        // Get the correct texture from the texture storage
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        // Get the block's size from the dictionary
        if (blockSizes.TryGetValue(subtype, out (int width, int height) size))
        {
            // Create a destination rectangle for the block
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, size.width, size.height);

            // Create the block with the size and texture
            return CreateBlock(destRectangle, texture, size.width, size.height);
        }
        else
        {
            Console.WriteLine($"Warning: Block subtype '{subtype}' not found. Using default size.");
            Rectangle defaultRect = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            return CreateBlock(defaultRect, texture, 100, 100); // Use default size if not found
        }
    }

    // Consolidated method to handle block creation, frame size, and collider size
    public static GameObject CreateBlock(Rectangle destRectangle, Texture2D texture, int frameWidth, int frameHeight)
    {
        GameObject block = new GameObject(destRectangle.X, destRectangle.Y);

        // Create and add the sprite renderer
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        block.AddComponent(spriteRenderer);

        // Set the frame size for the animation
        Animation blockTexture = new Animation(texture, 1, 1, frameWidth, frameHeight);
        spriteRenderer.addAnimation("texture", blockTexture);
        spriteRenderer.setAnimation("texture");

        // Add a collider with the block's size
        BoxCollider boxCollider = new BoxCollider(new Vector2(destRectangle.Width, destRectangle.Height), Vector2.Zero, GOManager.Instance.GraphicsDevice);
        block.AddComponent(boxCollider);

        // Add the block to the game objects list
        GOManager.Instance.allGOs.Add(block);

        return block;
    }
}