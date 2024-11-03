using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LevelLoader
{
    public static void LoadLevel(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Contains("//")) continue;

            string[] parts = line.Split(',');

            if (parts.Length < 4)
            {
                Console.WriteLine($"Invalid line format: {line}");
                continue;
            }

            string entityType = parts[0];
            string subtype = parts[1];
            int posX = int.Parse(parts[2]);
            int posY = int.Parse(parts[3]);

            switch (entityType)
            {
                case "Enemy":
                    SpawnEnemy(subtype, posX, posY);
                    break;

                case "Block":
                    SpawnBlock(subtype, new Vector2(posX, posY));
                    break;

                case "Background":
                    SpawnBackground(subtype, new Vector2(posX, posY));
                    break;

                case "Platform":
                    SpawnPlatform(subtype, new Vector2(posX, posY));
                    break;

                case "Item":
                    SpawnItem(subtype, posX, posY);
                    break;

                default:
                    Console.WriteLine($"Unknown entity type: {entityType}");
                    break;
            }
        }
        BackgroundFactory.layerBoost = 0f;
        BlockFactory.layerBoost = 0f;
    }

    private static void SpawnEnemy(string subtype, int x, int y)
    {
        if (Enum.TryParse(subtype, out EnemyType enemyType))
        {
            GameObject enemy = EnemyFactory.CreateEnemy(enemyType, x, y);
            GOManager.Instance.allGOs.Add(enemy);
        }
        else
        {
            Console.WriteLine($"Unknown enemy type: {subtype}");
        }
    }

    private static void SpawnBlock(string subtype, Vector2 position)
    {
        GameObject block = BlockFactory.CreateBlock(subtype, position);
        GOManager.Instance.allGOs.Add(block);

    }

    private static void SpawnBackground(string subtype, Vector2 position)
    {
        GameObject background = BackgroundFactory.CreateBackground(subtype, position);
        GOManager.Instance.allGOs.Add(background);
    }

    private static void SpawnPlatform(string subtype, Vector2 position)
    {
        GameObject platform = PlatformFactory.CreatePlatform(subtype, position);
        GOManager.Instance.allGOs.Add(platform);
    }

    private static void SpawnItem(string subtype, int x, int y)
    {
        Rectangle itemPosition = new Rectangle(x, y, 144, 144);

        GameObject item = ItemFactory.CreateItem(subtype, itemPosition);

        if (item != null)
        {
            GOManager.Instance.allGOs.Add(item);
        }
    }
    private static void SpawnEnd(string subtype, Vector2 position)
    {
        GameObject endElement = EndingFactory.CreateElement(subtype, position);
        GOManager.Instance.allGOs.Add(endElement);
    }
}