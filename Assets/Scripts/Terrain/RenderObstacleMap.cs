using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RenderObstacleMap : MonoBehaviour
{
    public Tile obstacleTile;
    public Tile obstacleTopTile;
    private Tilemap tilemap;
    private Vector3Int origin;
    private int[,] terrain;
    public void Render(int[,] map, Vector3Int o) {
        terrain = map;

        tilemap = gameObject.GetComponent<Tilemap>();
        origin = o;
        
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                // 1 = tile, 0 = no tile
                if (terrain[i, j] == 1)
                {
                    if (j < tilemap.size.y - 1 && terrain[i, j + 1] == 0) {
                        tilemap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), obstacleTopTile);

                    } else {
                        tilemap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), obstacleTile);
                    }
                }
            }
        }
    }
}
