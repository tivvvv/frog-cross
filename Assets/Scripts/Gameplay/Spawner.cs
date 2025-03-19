using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnObject;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(3f, 7f));
    }

    private void Spawn()
    {
        var index = Random.Range(0, spawnObject.Count);
        Instantiate(spawnObject[index], transform.position, Quaternion.identity, transform);
    }
}
