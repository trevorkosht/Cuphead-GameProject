using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public static class ItemFactory
{
    public static GameObject CreateItem(string itemName, Rectangle destRectangle)
    {
        Texture2D texture = null;

        switch (itemName) {
            case "Spreadshot":
                texture = GOManager.Instance.textureStorage.GetTexture("SpreadshotItem");
                break;
            case "Chaser":
                texture = GOManager.Instance.textureStorage.GetTexture("ChaserItem");
                break;
            case "Lobber":
                texture = GOManager.Instance.textureStorage.GetTexture("LobberItem");
                break;
            case "Roundabout":
                texture = GOManager.Instance.textureStorage.GetTexture("RoundaboutItem");
                break;
            default:
                break;
        }

        GameObject item = new GameObject(destRectangle.X, destRectangle.Y);
        item.type = "ItemPickup";
        item.type += itemName;
        ItemManager itemManager = new ItemManager(itemName);
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        spriteRenderer.orderInLayer = .15f;
        Animation itemTexture = new Animation(texture, 1, 1, 144, 144);
        spriteRenderer.addAnimation("texture",itemTexture);
        spriteRenderer.setAnimation("texture");
        spriteRenderer.spriteScale = 0.5f;
        Collider boxCollider = new BoxCollider(new Vector2(spriteRenderer.spriteScale* destRectangle.Width, spriteRenderer.spriteScale*destRectangle.Height), new Vector2(0,0), GOManager.Instance.GraphicsDevice);
        item.AddComponent(itemManager);
        item.AddComponent(spriteRenderer);
        item.AddComponent(boxCollider);

        GOManager.Instance.allGOs.Add(item);

        return item;
    }
}