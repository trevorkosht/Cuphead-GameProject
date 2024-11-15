using Cuphead.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
namespace Cuphead.Player
{
    public static class PlayerSetup
    {
        public static GameObject CreatePlayer(Texture2DStorage textureStorage, GraphicsDevice graphicsDevice)
        {
            GameObject player = new GameObject(50, 500);
            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(new Rectangle(50, 500, 144, 144), true);
            playerSpriteRenderer.orderInLayer = .3f;

            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
            player.AddComponent(new BoxCollider(new Vector2(90, 144), new Vector2(25, 0), graphicsDevice));
            player.type = "Player";
            player.AddComponent(new PlayerController2(new PlayerState(player)));
            player.AddComponent(new ScoreComponent());

            return player;
        }
    }
}
