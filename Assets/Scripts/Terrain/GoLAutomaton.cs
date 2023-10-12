using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoLAutomaton : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private int initChance;
    [Range(0, 8)] [SerializeField] private int birthLimit;
    [Range(1, 8)] [SerializeField] private int deathLimit;
    [Range(1, 10)] [SerializeField] private int numRuns;
    [SerializeField] private Tilemap topMap;
    [SerializeField] private Tilemap baseMap;
    [SerializeField] private Tile obstacleTile;
    private int[,] terrain;
    private Vector3Int origin;

    private int width;
    private int height;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = baseMap.origin;
        Simulate(numRuns);
    }

    public void Simulate(int runs) {
        width = baseMap.size.x;
        height = baseMap.size.y;
        ClearMap(false);
        if (terrain == null) {
            terrain = new int[width, height];
            InitPos();
        }

        for (int i = 0; i < runs; i++) {
            terrain = GenTilePos(terrain);
        }

        for (int i = 0; i < width ; i++) {
            for (int j = 0; j < height; j++) {
                // 1 = tile, 0 = no tile
                if (terrain[i, j] == 1)
                {
                    topMap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), obstacleTile);
                }
            }
        }
    }

    private int[,] GenTilePos(int[,] prevMap) {
        int[,] currMap = new int[width, height];
        int neighbors;
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                neighbors = 0;
                foreach (var b in bounds.allPositionsWithin) {
                    if (b.x == 0 && b.y == 0) {
                        continue;
                    }
                    if (i+b.x >= 0 && i+b.x < width && j+b.y >= 0 & j+b.y < height) {
                        neighbors += prevMap[i + b.x, j + b.y];
                    }
                }

                if (prevMap[i,j] == 1) {
                    if (neighbors < deathLimit) {
                        currMap[i, j] = 0;
                    } else {
                        currMap[i, j] = 1;
                    }
                }

                if (prevMap[i, j] == 0) {
                    if (neighbors > birthLimit) {
                        currMap[i, j] = 1;
                    } else {
                        currMap[i, j] = 0;
                    }

                }
            }
        }
        return currMap;
    }

    private void InitPos() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                terrain[i, j] = Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }

    private void ClearMap(bool complete) {
        topMap.ClearAllTiles();

        if (complete) {
            terrain = null;
        }
    }

}
