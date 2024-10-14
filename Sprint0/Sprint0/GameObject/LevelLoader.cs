using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LevelLoader
{
    public static void LoadLevel(string filePath)
    {
        //Format for line:
        //Enemy,DeadlyDaisy,300,400
        string[] lines = File.ReadAllLines(filePath);
        
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Contains("//")) continue;

            // Split the line by comma
            string[] parts = line.Split(',');

            if (parts.Length < 4)
            {
                Console.WriteLine($"Invalid line format: {line}");
                continue;
            }

            // Parse entity type, subtype, and position
            string entityType = parts[0];
            string subtype = parts[1];
            int posX = int.Parse(parts[2]);
            int posY = int.Parse(parts[3]);

            // Instantiate based on the type
            switch (entityType)
            {
                case "Enemy":
                    SpawnEnemy(subtype, posX, posY);
                    break;

                case "Block":
                    SpawnBlock(subtype, new Vector2(posX, posY));
                    break;

                case "Item":
                    SpawnItem(subtype, posX, posY);
                    break;

                default:
                    Console.WriteLine($"Unknown entity type: {entityType}");
                    break;
            }
        }
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

    private static void SpawnItem(string subtype, int x, int y)
    {
        Rectangle itemPosition = new Rectangle(x, y, 144, 144);

        // Use the ItemFactory to create the item
        GameObject item = ItemFactory.CreateItem(subtype, itemPosition);

        if (item != null)
        {
            GOManager.Instance.allGOs.Add(item);
        }
    }
}