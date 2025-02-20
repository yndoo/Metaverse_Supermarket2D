using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFood : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer foodRenderer;
    private int maxIndex;
    private void Start()
    {
        maxIndex = sprites.Length;
        foodRenderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }
    public int RandomOn()
    {
        gameObject.SetActive(true); 
        int idx = Random.Range(0, maxIndex + 1);
        if(idx < 0 || idx > maxIndex) idx = 0;
        foodRenderer.sprite = sprites[idx];
        return idx;
    }

    public void SetSpriteNum(int num)
    {
        if (num < 0 || num > maxIndex) num = 0;

        foodRenderer.sprite = sprites[num];
    }

    public void SpriteColorOn()
    {
        foodRenderer.color = Color.white;
    }
}
