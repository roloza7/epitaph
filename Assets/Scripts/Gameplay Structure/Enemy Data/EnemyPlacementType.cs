using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPlacementType
{
    public  GameObject enemy;
    [Range(0f, 100f)] [SerializeField] public float spawnRate;

    public int width;
    public int height;

    private List<Vector3> spawnLocations = new List<Vector3>();

    public int SpawnLocations {
        get
        {
            return SpawnLocations;
        }
    }
}