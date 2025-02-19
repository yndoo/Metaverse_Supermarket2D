using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float offset = 8f;
    public Vector3 SetRandomPosition(Vector3 lastPosition)
    {
        Vector3 randomPos = lastPosition + new Vector3(offset, 0);
        randomPos.y = Random.Range(-2.5f, 2.5f);

        transform.position = randomPos;
        return randomPos;
    }
}
