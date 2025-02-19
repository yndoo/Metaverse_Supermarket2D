using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    private int numBgCount = 4;
    private const float widthOfBgObject = 18f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��� looper
        if(collision.CompareTag("background"))
        {
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        // ��ֹ� looper
        if (collision.CompareTag("obstacle"))
        {

        }
    }
}
