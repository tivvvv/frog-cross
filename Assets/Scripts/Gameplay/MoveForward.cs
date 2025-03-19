using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    // 汽车的方向
    public int dir;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        transform.localScale = new Vector3(dir, 1, 1);
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
        transform.position += transform.right * dir * speed * Time.deltaTime;
    }
}
