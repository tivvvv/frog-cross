using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public float offsetY;

    public List<GameObject> terrainObjects;

    private GameObject spawnObject;

    private int lastIndex;

    private void OnEnable()
    {
        EventHandler.GetPointEvent += OnGetPointEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
    }

    private void OnGetPointEvent(int point)
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        if (transform.position.y - Camera.main.transform.position.y < offsetY / 2)
        {
            transform.position = new Vector3(0, Camera.main.transform.position.y + offsetY, 0);
            SpawnTerrain();
        }
    }

    private void SpawnTerrain()
    {
        var randomIndex = Random.Range(0, terrainObjects.Count);

        while (randomIndex == lastIndex)
        {
            randomIndex = Random.Range(0, terrainObjects.Count);
        }
        lastIndex = randomIndex;
        spawnObject = terrainObjects[randomIndex];

        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
