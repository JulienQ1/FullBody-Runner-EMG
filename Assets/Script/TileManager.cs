using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    public Vector3 currentTilesLocation = Vector3.zero;
    public GameObject previousTile;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        SpawnTiles(3);
    }

    private void SpawnTiles(int number) 
    {
        for (int i = 0; i < number; i++)
        {
            previousTile = Instantiate(tiles[Random.Range(0, tiles.Count)], currentTilesLocation, Quaternion.identity);
            currentTilesLocation += Vector3.Scale(previousTile.GetComponent<Renderer>().bounds.size, Vector3.forward);
        }
    
    
    }
}
