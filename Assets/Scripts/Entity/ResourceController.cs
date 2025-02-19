using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    #region ¼±¾ð
    public float MoveSpeed { get; set; }

    private int coin = 0;
    private GameUI gameUI;
    private TextMeshProUGUI coinText;

    private GameManager gameManager;
    private MissionManager missionManager;
    #endregion

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
        if(gameUI != null)
        {
            coinText = gameUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        gameManager = FindObjectOfType<GameManager>();
        missionManager = FindObjectOfType<MissionManager>();
        missionManager.ResourceInit(this);
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
        coinText.text = coin.ToString();
    }
}
