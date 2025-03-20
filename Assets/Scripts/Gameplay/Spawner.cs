using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 孵化器的方向
    public int direction;

    public List<GameObject> spawnObjects;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(4f, 8f));
    }

    private void Spawn()
    {
        int index = Random.Range(0, spawnObjects.Count);
        GameObject car = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);
        car.GetComponent<MoveForward>().dir = direction;
    }
}
