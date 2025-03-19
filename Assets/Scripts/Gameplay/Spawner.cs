using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 孵化器的方向
    public int direction;

    public List<GameObject> spawnObject;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(3f, 7f));
    }

    private void Spawn()
    {
        int index = Random.Range(0, spawnObject.Count);
        GameObject car = Instantiate(spawnObject[index], transform.position, Quaternion.identity, transform);
        car.GetComponent<MoveForward>().dir = direction;
    }
}
