using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CameraController
{
    private Camera camera;
    private List<Vector2> railPoints;
    private GameObject player;
    private float t;
    public CameraController(Camera camera, GameObject player)
    {
        this.camera = camera;
        this.player = player;
        this.railPoints = GetRailPoints();
    }

    public void Update()
    {
        float cameraX = player.position.X - 500;

        float cameraY = GetRailYPosition(cameraX);

        camera.Update(new Vector2(cameraX, cameraY));
    }

    private float GetRailYPosition(float playerX)
    {
        for (int i = 0; i < railPoints.Count - 1; i++)
        {
            if (playerX >= railPoints[i].X && playerX <= railPoints[i + 1].X)
            {
                float t = (playerX - railPoints[i].X) / (railPoints[i + 1].X - railPoints[i].X);
                return MathHelper.Lerp(railPoints[i].Y, railPoints[i + 1].Y, t);
            }
        }

        return railPoints[railPoints.Count - 1].Y;
    }

    public  List<Vector2> GetRailPoints()
    {
        List<Vector2> railPoints = new List<Vector2>()
            {
                new Vector2(0, 0),
                new Vector2(500, 0),
                new Vector2(1000, 0),
                new Vector2(1200, -50),
                new Vector2(1500, -50),
                new Vector2(1700, -50),
                new Vector2(2000, 0),
                new Vector2(2200, 0),
                new Vector2(2500, 0),
                new Vector2(2700, 0),
                new Vector2(3000, -25),
                new Vector2(3200, -25),
                new Vector2(3500, -25),
                new Vector2(3700, 0),
                new Vector2(4000, 0),
                new Vector2(4200, 0),
                new Vector2(4500, 0),
                new Vector2(4700, -50),
                new Vector2(5000, -100),
                new Vector2(5200, -100),
                new Vector2(5500, -100),
                new Vector2(5700, -100),
                new Vector2(6000, -100),
                new Vector2(6200, -100),
                new Vector2(6500, 0),
                new Vector2(6700, 0),
                new Vector2(7000, 0),
                new Vector2(7200, 0),
                new Vector2(7500, -50),
                new Vector2(7700, -50),
                new Vector2(8000, 0),
                new Vector2(8200, 0),
                new Vector2(8500, 0),
                new Vector2(8700, 0)
            };
        return railPoints;
    }
}
