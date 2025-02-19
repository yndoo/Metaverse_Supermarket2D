using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    private int coin = 0;
    private GameUI gameUI;
    private TextMeshProUGUI coinText;

    private GameManager gameManager;
    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
        if(gameUI != null)
        {
            coinText = gameUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        transform.position = GameManager.Instance.LastPosition;
        ChangeCoin(gameManager.PlayerCoin);
    }

    public void ChangeCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }
}
