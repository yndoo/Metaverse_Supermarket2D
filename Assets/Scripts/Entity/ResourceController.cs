using System.Collections;
using System.Collections.Generic;
using System.Resources;
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

    private ResourceManager resourceManager;
    private WorkManager workManager;

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
        if(gameUI != null)
        {
            coinText = gameUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        resourceManager = FindObjectOfType<ResourceManager>();
        workManager = FindObjectOfType<WorkManager>();
        workManager.ResourceInit(this);
    }

    private void Start()
    {
        transform.position = GameManager.Instance.LastPosition;
        MoveSpeed = 5f;
        AddCoin(resourceManager.PlayerCoin);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        resourceManager.PlayerCoin = coin;
        gameUI.UpdateCoin(coin);
    }

    public void AddPopular(int amount)
    {
        popular += amount;
        resourceManager.PlayerPopular = popular;
        gameUI.UpdatePolular(popular);
    }
}
