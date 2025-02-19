using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    private int numBgCount = 4;
    private const float widthOfBgObject = 18f;

    private int obstacleCount = 0;
    private Vector3 lastObstaclePos;

    private void Awake()
    {
        lastObstaclePos = new Vector3(15, 0, 0);
    }
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleCount = obstacles.Length;

        for(int i = 0; i < obstacleCount; i++)
        {
            lastObstaclePos = obstacles[i].SetRandomPosition(lastObstaclePos);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 배경 looper
        if(collision.CompareTag("background"))
        {
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        // 장애물 looper
        if (collision.CompareTag("obstacle"))
        {
            Obstacle obs = collision.GetComponent<Obstacle>();
            if (obs != null)
            {
                lastObstaclePos =  obs.SetRandomPosition(lastObstaclePos);
            }
        }
    }
}
