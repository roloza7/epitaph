using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;
using UnityEngine.AI;
public class LevelGeneration : MonoBehaviour
{
    private GoLAutomaton automaton;
    private EnemyPlacement enemyPlacement;
    private int[,] terrain;
    [SerializeField] private Tilemap topMap;
    [SerializeField] private Tilemap baseMap;
    [SerializeField] private Tile obstacleTile;
    [SerializeField] private NavMeshSurface surface2D;
    [SerializeField] private int bufferSize;
    private int width;
    private int height;
    private Vector3Int origin;

    // Start is called before the first frame update
    public void Start()
    {
        automaton = gameObject.GetComponent<GoLAutomaton>();
        enemyPlacement = gameObject.GetComponent<EnemyPlacement>();
        origin = baseMap.origin;
        width = baseMap.size.x;
        height = baseMap.size.y - bufferSize;
        terrain = automaton.Simulate(width, height);
        RenderMap();
        surface2D.BuildNavMeshAsync();
    }

    public void PlaceEnemies() {
        enemyPlacement.PlaceEnemies(terrain, bufferSize);
    }

    void RenderMap() {
        topMap.ClearAllTiles();
        for (int i = 0; i < width ; i++) {
            for (int j = bufferSize; j < height; j++) {
                // 1 = tile, 0 = no tile
                if (terrain[i, j] == 1)
                {
                    topMap.SetTile(new Vector3Int(i + origin.x, j + origin.y, 0), obstacleTile);
                }
            }
        }
        baseMap.gameObject.GetComponent<RenderBaseMap>().Render();
        topMap.GetComponent<Collider2D>().enabled = false;
        topMap.GetComponent<Collider2D>().enabled = true;

    }
}
