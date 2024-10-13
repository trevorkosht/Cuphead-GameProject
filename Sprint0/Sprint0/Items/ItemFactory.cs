using Microsoft.Xna.Framework;
using System;

public static class ItemFactory
{
    public static GameObject CreateItem(string type, Vector2 itemPosition)
    {
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        switch (type)
        {
            case "YellowPotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.YellowPotion(itemPosition, textureStorage.GetTexture("Item1_3")));

            case "SkybluePotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.SkybluePotion(itemPosition, textureStorage.GetTexture("Item1_3")));

            case "RedPotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.RedPotion(itemPosition, textureStorage.GetTexture("Item1_3")));

            case "BluePotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.BluePotion(itemPosition, textureStorage.GetTexture("Item4_6")));

            case "OceanbluePotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.OceanbluePotion(itemPosition, textureStorage.GetTexture("Item4_6")));

            case "RedKetchupPotion":
                return new GameObject((int)itemPosition.X, (int)itemPosition.Y,
                    new Cuphead.Items.RedKetchupPotion(itemPosition, textureStorage.GetTexture("Item4_6")));

            default:
                Console.WriteLine($"Unknown item type: {type}");
                return null;
        }
    }
}