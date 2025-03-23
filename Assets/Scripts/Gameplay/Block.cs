using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Update()
    {
        checkPosition();
    }

    private void checkPosition()
    {
        if (Camera.main.transform.position.y - transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
