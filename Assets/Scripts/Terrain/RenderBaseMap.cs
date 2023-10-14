using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RenderBaseMap : MonoBehaviour
{
    public List<Tile> baseTiles = new List<Tile>();

    private int prevIdx;

    private Tilemap tilemap;
    private Vector3Int origin;
    public void Render() {
        tilemap = gameObject.GetComponent<Tilemap>();
        origin = tilemap.origin;

        prevIdx = 0;
        for (int i = 0; i<tilemap.size.x; i++) {
            for (int j = 0; j<tilemap.size.y; j++) {
                int newIdx = Random.Range(0, baseTiles.Count);
                if (newIdx == prevIdx) {
                    newIdx = (newIdx + 1) % baseTiles.Count;
                }
                tilemap.SetTile(new Vector3Int(origin.x + i, origin.y + j, 0), baseTiles[newIdx]);
                prevIdx = newIdx;
            }
        }
    }

}
