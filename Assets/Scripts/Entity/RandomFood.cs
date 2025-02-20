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
        int idx = Random.Range(0, maxIndex);
        foodRenderer.sprite = sprites[idx];
        return idx;
    }

    public void SetSpriteNum(int num)
    {
        foodRenderer.sprite = sprites[num];
    }

    public void SpriteColorOn()
    {
        foodRenderer.color = Color.white;
    }
}
