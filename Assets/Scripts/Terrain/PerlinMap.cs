using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinMap : MonoBehaviour
{
    // change this later if more obstacles are needed (e.g obstacle types)
    [SerializeField] private float threshold;
    [SerializeField] private float magnification;
    [SerializeField] private Tilemap tilebase;
    [SerializeField] private TileBase tile;

    [SerializeField] private bool randomSeed;
    [SerializeField] private Vector2Int seed;


    private Vector3Int origin;
    private int[,] map;
    private Tilemap tilemap;
    private Collider2D collider;

    private PerlinGenerator perlin;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        if (randomSeed) {
            seed = new Vector2Int((int)Random.Range(0, 1000), (int)Random.Range(0, 1000));
        }
        origin = tilebase.origin;
        perlin = new PerlinGenerator(tilemap, tilebase, threshold, magnification);

        map = perlin.InitPerlin(seed, tile);

        collider = gameObject.GetComponent<Collider2D>();
        
        collider.enabled = false;
        RenderMap();
        collider.enabled = true;
    }
    public void RenderMap() {
        tilemap.ClearAllTiles();
        for (int i = 0; i < map.GetUpperBound(0) ; i++) {
            for (int j = 0; j < map.GetUpperBound(1); j++) {
                // 1 = tile, 0 = no tile
                if (map[i, j] == 1 && CheckSurroundings(i, j))
                {
                    tilemap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), tile);
                }
            }
        }
    }

    private bool CheckSurroundings(int x, int y) {
        int surrounding = 0;
        if (x != 0) {
            if (map[x-1, y] == 1) {
                surrounding += 1;
            }
        }
        if (x != map.GetUpperBound(0) - 1) {
            if (map[x+1, y] == 1) {
                surrounding += 1;
            }
        }
        if (y != 0) {
            if (map[x, y-1] == 1) {
                surrounding += 1;
            }
        }
        if (y != map.GetUpperBound(1) - 1) {
            if (map[x, y+1] == 1) {
                surrounding += 1;
            }
        }
        return surrounding > 0;
    }

}
