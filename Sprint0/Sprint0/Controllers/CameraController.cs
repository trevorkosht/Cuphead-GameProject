using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CameraController
{
    private Camera camera;
    private List<Vector2> railPoints; // Define points for the rail (Y values)
    private GameObject player;            // Reference to the player
    private float t;                  // Parameter for interpolation between rail points

    public CameraController(Camera camera, GameObject player, List<Vector2> railPoints)
    {
        this.camera = camera;
        this.player = player;
        this.railPoints = railPoints;
    }

    public void Update()
    {
        // Follow the player's X position
        float cameraX = player.position.X-500;

        // Find Y position on the rail by interpolating between points
        float cameraY = GetRailYPosition(cameraX);

        // Update the camera's position
        camera.Update(new Vector2(cameraX, cameraY));
    }

    private float GetRailYPosition(float playerX)
    {
        // Find which points on the rail the player is between
        for (int i = 0; i < railPoints.Count - 1; i++)
        {
            if (playerX >= railPoints[i].X && playerX <= railPoints[i + 1].X)
            {
                // Linearly interpolate (or use smoother interpolation) between these two points
                float t = (playerX - railPoints[i].X) / (railPoints[i + 1].X - railPoints[i].X);
                return MathHelper.Lerp(railPoints[i].Y, railPoints[i + 1].Y, t);
            }
        }

        // Default to the last point's Y if the player is past the rail's end
        return railPoints[railPoints.Count - 1].Y;
    }
}
