using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 25)
        {
            Destroy(this.gameObject);
        }
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
