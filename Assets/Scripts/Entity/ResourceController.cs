using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    #region ¸®¼Ò½º
    public float MoveSpeed { get; set; }
    private int coin = 0;
    private int popular = 0;
    #endregion


    private GameUI gameUI;
    private TextMeshProUGUI coinText;

    private GameManager gameManager;
    private WorkManager workManager;

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
        if(gameUI != null)
        {
            coinText = gameUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        gameManager = FindObjectOfType<GameManager>();
        workManager = FindObjectOfType<WorkManager>();
        workManager.ResourceInit(this);
    }

    private void Start()
    {
        transform.position = GameManager.Instance.LastPosition;
        MoveSpeed = 5f;
        AddCoin(gameManager.PlayerCoin);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        gameManager.PlayerCoin = coin;
        gameUI.UpdateCoin(coin);
    }

    public void AddPopular(int amount)
    {
        popular += amount;
        gameUI.UpdatePolular(popular);
    }
}
