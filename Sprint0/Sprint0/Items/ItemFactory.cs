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
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
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