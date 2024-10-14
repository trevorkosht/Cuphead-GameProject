using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public static class ItemFactory
{
    public static GameObject CreateItem(string type, Rectangle destRectangle)
    {
        Texture2D texture = null;

        switch (type) {
            case "Spreadshot":
                texture = GOManager.Instance.textureStorage.GetTexture("SpreadshotItem");
                break;
            case "Chaser":
                texture = GOManager.Instance.textureStorage.GetTexture("ChaserItem");
                break;
            case "'Lobber":
                texture = GOManager.Instance.textureStorage.GetTexture("LobberItem");
                break;
            case "Roundabout":
                texture = GOManager.Instance.textureStorage.GetTexture("RoundaboutItem");
                break;
            default:
                break;
        }

        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        GameObject item = new GameObject(destRectangle.X, destRectangle.Y);
        ItemManager itemManager = new ItemManager(type);
        item.AddComponent(itemManager);
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        Animation itemTexture = new Animation(texture, 1, 1, 144, 144);
        spriteRenderer.addAnimation("texture",itemTexture);
        spriteRenderer.setAnimation("texture");
        spriteRenderer.spriteScale = 1f;
        Collider boxCollider = new BoxCollider(new Vector2(destRectangle.Width, destRectangle.Height), new Vector2(0,0), GOManager.Instance.GraphicsDevice);

        GOManager.Instance.allGOs.Add(item);

        return item;
    }
}