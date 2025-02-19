using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    private int numBgCount = 3;
    private const float widthOfBgObject = 18f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("background"))
        {
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
    }
}
