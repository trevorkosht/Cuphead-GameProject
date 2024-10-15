using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class BlockFactory
{
    // Dictionary to store block sizes based on subtype
    private static Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)> blockSizes = new Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)>()
{
    { "LevelBackground", (1827, 546, new Vector2(1827, 546), Vector2.Zero, 0.95f) },
    { "Bush1", (1338, 625, new Vector2(1338, 625), Vector2.Zero, 0.9f) },
    { "Platform1", (427, 102, new Vector2(427, 25), new Vector2(0, 10), 0.5f) },
    { "Platform2", (208, 110, new Vector2(188, 25), new Vector2(10, 10), 0.501f) },
    { "BigHill1", (1042, 842, new Vector2(1042, 842), new Vector2(0, 35), 0.7f) },
    { "BigHill2", (998, 664, new Vector2(998, 664), Vector2.Zero, 0.701f) },
    { "BigHill3", (510, 668, new Vector2(410, 668), new Vector2(70, 35), 0.702f) },
    { "Hill1", (531, 180, new Vector2(431, 180), new Vector2(70, 35), 0.703f) },
    { "Hill2", (436, 176, new Vector2(336, 176), new Vector2(50, 35), 0.704f) },
    { "Hill3", (616, 288, new Vector2(616, 288), new Vector2(0, 35), 0.704f) },
    { "Log1", (832, 186, new Vector2(832, 186), new Vector2(0, 35), 0.502f) },
    { "Log2", (164, 996, new Vector2(84, 996), new Vector2(30, 35), 0.503f) },
    { "Stump1", (144, 144, new Vector2(124, 124), new Vector2(10, 10), 0.504f) },
    { "Stump2", (144, 144, new Vector2(124, 144), new Vector2(10, 0), 0.505f) },
    { "Stump3", (144, 144, new Vector2(124, 134), new Vector2(10, 5), 0.506f) },
    { "Rock1", (526, 306, new Vector2(526, 306), Vector2.Zero, 0.801f) },
    { "Rock2", (980, 760, new Vector2(980, 760), Vector2.Zero, 0.802f) },
    { "Rock3", (642, 270, new Vector2(642, 270), Vector2.Zero, 0.803f) },
    { "Rock4", (970, 682, new Vector2(970, 682), Vector2.Zero, 0.804f) },
    { "Rock5", (650, 472, new Vector2(650, 472), Vector2.Zero, 0.805f) },
    { "Rock6", (668, 522, new Vector2(668, 522), Vector2.Zero, 0.806f) },
    { "Leaves1", (902, 710, new Vector2(902, 710), Vector2.Zero, 0.851f) },
    { "Leaves2", (1564, 458, new Vector2(1564, 458), Vector2.Zero, 0.852f) },
    { "Leaves3", (1052, 350, new Vector2(1052, 350), Vector2.Zero, 0.853f) },
    { "Tree1", (273, 476, new Vector2(273, 476), Vector2.Zero, 0.807f) },
    { "Tree2", (476, 682, new Vector2(476, 682), Vector2.Zero, 0.808f) },
    { "Tree3", (259, 456, new Vector2(259, 456), Vector2.Zero, 0.809f) },
    { "Tree4", (500, 655, new Vector2(500, 655), Vector2.Zero, 0.81f) },
    { "Tree5", (922, 720, new Vector2(922, 720), Vector2.Zero, 0.811f) },
    { "Tree6", (852, 1064, new Vector2(852, 1064), Vector2.Zero, 0.812f) },
    { "Tree7", (304, 844, new Vector2(304, 844), Vector2.Zero, 0.813f) }
};

    public static GameObject CreateBlock(string subtype, Vector2 position)
    {
        // Get the correct texture from the texture storage
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        // Get the block's size, bounds, offset, and orderInLayer from the dictionary
        if (blockSizes.TryGetValue(subtype, out (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer) blockData))
        {
            // Apply the offset to the position
            Vector2 adjustedPosition = position;

            // Create a destination rectangle for the block
            Rectangle destRectangle = new Rectangle((int)adjustedPosition.X, (int)adjustedPosition.Y, blockData.width, blockData.height);

            // Determine if the block needs a collider
            bool collider = !subtype.Contains("Background") && !subtype.Contains("Tree") && !subtype.Contains("Leaves") &&
                            !subtype.Contains("Rock") && !subtype.Contains("Bush");

            // Create the block with the size, texture, and other properties
            GameObject block = CreateBlock(destRectangle, texture, blockData.width, blockData.height, collider, blockData.bounds, blockData.offset, blockData.orderInLayer);
            block.type = "Block"+subtype;
            return block;
        }
        else
        {
            Console.WriteLine($"Warning: Block subtype '{subtype}' not found. Using default size.");
            Rectangle defaultRect = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            bool collider = false;
            return CreateBlock(defaultRect, texture, 100, 100, collider, new Vector2(100, 100), Vector2.Zero, 0.75f); // Use default size and orderInLayer if not found
        }
    }

    // Consolidated method to handle block creation, frame size, and collider size
    public static GameObject CreateBlock(Rectangle destRectangle, Texture2D texture, int frameWidth, int frameHeight, bool collider, Vector2 bounds, Vector2 offset, float orderInLayer)
    {
        GameObject block = new GameObject(destRectangle.X, destRectangle.Y);

        // Create and add the sprite renderer
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        block.AddComponent(spriteRenderer);

        // Set the frame size for the animation
        Animation blockTexture = new Animation(texture, 1, 1, frameHeight, frameWidth);
        spriteRenderer.addAnimation("texture", blockTexture);
        spriteRenderer.setAnimation("texture");

        // Set the sprite's orderInLayer
        spriteRenderer.orderInLayer = orderInLayer;

        // Add a collider with the block's size if applicable
        if (collider)
        {
            BoxCollider boxCollider = new BoxCollider(bounds, offset, GOManager.Instance.GraphicsDevice);
            block.AddComponent(boxCollider);
        }

        // Add the block to the game objects list
        GOManager.Instance.allGOs.Add(block);

        return block;
    }
}