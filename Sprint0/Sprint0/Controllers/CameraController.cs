using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CameraController
{
    private Camera camera;
    private List<Vector2> railPoints; 
    private GameObject player;           
    private float t;      
    public CameraController(Camera camera, GameObject player, List<Vector2> railPoints)
    {
        this.camera = camera;
        this.player = player;
        this.railPoints = railPoints;
    }

    public void Update()
    {
        float cameraX = player.position.X-500;

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
}
