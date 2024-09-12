using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Object {벽 = 1, 물 = 2, 코인 = 3, 플레이어 = 4 };
public class CreateObstacle : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject water;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject player;
    [SerializeField] float wallY;
    [SerializeField] float waterY;
    [SerializeField] float coinY;
    [SerializeField] float playerY;

    string[] lines;
    int[,] map;

    void Start()
    {
        GetMapData();
        for (int z = 0; z < lines.Length; z++)
        {
            for (int x = 0; x < lines.Length; x++)
            {
                Debug.Log($"{-z}, {x}, {map[z, x]}");
                if (map[z, x] == (int)Object.벽)
                {
                    GameObject obstacle = Instantiate(wall, transform);
                    obstacle.transform.position = new Vector3(x, wallY, -z);
                }
                else if (map[z, x] == (int)Object.물)
                {
                    GameObject obstacle = Instantiate(water, transform);
                    obstacle.transform.position = new Vector3(x, waterY, -z);
                }
                else if (map[z, x] == (int)Object.코인)
                {
                    GameObject obstacle = Instantiate(coin, transform);
                    obstacle.transform.position = new Vector3(x, coinY, -z);
                }
                else if (map[z, x] == (int)Object.플레이어)
                {
                    player.transform.position = new Vector3(x, playerY, -z);
                }
            }
        }

    }

    void GetMapData()
    {

        string path = Application.dataPath + "/Data";
        string file = File.ReadAllText(path + "/Map.csv");
        lines = file.Split("\n");
        map = new int[lines.Length, lines.Length];
        Debug.Log(lines.Length);

        int z = 0;
        int x = 0;
        foreach(string line in lines)
        {
            string[] obstacles = line.Split(",");
            foreach(string obstacle in obstacles)
            {
                if (obstacle == "wall")
                {
                    map[z, x] = (int)Object.벽;
                }
                else if(obstacle == "water")
                {
                    map[z, x] = (int)Object.물;
                }
                else if(obstacle == "coin")
                {
                    map[z, x] = (int)Object.코인;
                }
                else if(obstacle == "player")
                {
                    map[z, x] = (int)Object.플레이어;
                }
                Debug.Log($"{z}, {x}, {map[z, x]}");
                x++;
            }
            z++;
            x = 0;
        }

    }


}
