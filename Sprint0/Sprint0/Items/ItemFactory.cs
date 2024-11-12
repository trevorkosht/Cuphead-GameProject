using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public static class ItemFactory
{
    public static GameObject CreateItem(string itemName, Rectangle destRectangle)
    {
        Texture2D texture = null;
        Texture2D VFXTexture = null;

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
            case "Coin":
                texture = GOManager.Instance.textureStorage.GetTexture("Coin");
                VFXTexture = GOManager.Instance.textureStorage.GetTexture("CoinVFX");
                break;
            default:
                break;
        }

        GameObject item = new GameObject(destRectangle.X, destRectangle.Y);
        item.type = "ItemPickup";
        item.type += itemName;
        ItemManager itemManager = new ItemManager(itemName);
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        spriteRenderer.orderInLayer = .4f;
        Animation itemTexture = new Animation(texture, 1, 1, 144, 144);
        Animation coinTexture = new Animation(texture, 5, 17, 144, 144);
        Animation vfxTextureAnimation = new Animation(VFXTexture, 5, 17, 288, 288);
        spriteRenderer.addAnimation("texture",itemTexture);
        spriteRenderer.addAnimation("coinTexture", coinTexture);
        spriteRenderer.setAnimation("texture");
        spriteRenderer.spriteScale = 0.5f;
        if (itemName == "Coin") {
            spriteRenderer.setAnimation("coinTexture");
            
        }
 
        Collider boxCollider = new BoxCollider(new Vector2(spriteRenderer.spriteScale* destRectangle.Width, spriteRenderer.spriteScale*destRectangle.Height), new Vector2(0,0), GOManager.Instance.GraphicsDevice);
        item.AddComponent(itemManager);
        item.AddComponent(spriteRenderer);
        item.AddComponent(boxCollider);

        GOManager.Instance.allGOs.Add(item);

        return item;
    }
}