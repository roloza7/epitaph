using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PerlinGenerator
{
    Tilemap tilemap;
    Tilemap tilebase;
    float threshold;
    Vector2Int size;
    float magnification;


    public PerlinGenerator(Tilemap tilemap, Tilemap tilebase, float threshold, float mag) {
        this.tilemap = tilemap;
        this.tilebase = tilebase;
        this.size = new Vector2Int(tilebase.size.x, tilebase.size.y);
        this.threshold = threshold;
        this.magnification = mag;

    }
    public int[,] InitPerlin(Vector2 seed, TileBase obsTile) {
        int[,] map = new int[(int)size.x, (int)size.y];
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                float sample = Mathf.PerlinNoise((float)((seed.x+i)/magnification), (float)((seed.y+j)/magnification));
                if (sample < threshold) {
                    map[i, j] = 1;
                } else {
                    map[i, j] = 0;
                }
            }
        }
        return map;
    }


}
