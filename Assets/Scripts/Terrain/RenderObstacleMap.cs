using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RenderObstacleMap : MonoBehaviour
{
    public List<Tile> obstacleTiles = new List<Tile>();
    private Tilemap tilemap;
    private Vector3Int origin;
    public void Render(int[,] map) {
        tilemap = gameObject.GetComponent<Tilemap>();
        origin = tilemap.origin;
        for (int i = 0; i < map.GetLength(0) ; i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                // 1 = tile, 0 = no tile
                // if (terrain[i, j] == 1)
                // {
                //     if (terrain[i-1, j] == 0) {

                //     }
                //     // tilemap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), obstacleTile);
                // }
            }
        }
    }
}
